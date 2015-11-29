using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using JetBrains.Application.Interop.NativeHook;
using JetBrains.Application.Settings;
using JetBrains.Application.Settings.Store;
using JetBrains.DataFlow;
using JetBrains.Metadata.Reader.API;
using JetBrains.Metadata.Reader.Impl;
using JetBrains.ReSharper.Feature.Services.Resources;
using JetBrains.UI.Application;
using JetBrains.UI.Options;
using JetBrains.UI.Options.OptionPages;
using JetBrains.UI.Options.OptionsDialog2.SimpleOptions;
using JetBrains.UI.Validation;
using JetBrains.UI.Wpf.Controls.StringCollectionEdit.Impl;
using JetBrains.UI.Wpf.Controls.StringCollectionEdit.Impl.Buttons;
using JetBrains.UI.Wpf.Controls.StringCollectionEdit.Impl.Items;
using JetBrains.Util;

namespace Sizikov.AsyncSuffix.Settings
{
    [OptionsPage(PageId, "AsyncSuffix", typeof (ServicesThemedIcons.AnalyzeThis),
        ParentId = EnvironmentPage.Pid)]
    public sealed class AsyncSuffixOptionsPage : CustomSimpleOptionsPage
    {
        private const string PageId = "AsyncSuffix";

        public AsyncSuffixOptionsPage([NotNull] Lifetime lifetime, [NotNull] OptionsSettingsSmartContext store,
            IWindowsHookManager windowsHookManager, FormValidators formValidators, IUIApplication iuiApplication)
            : base(lifetime, store)
        {
            AddHeader("Tests");
            AddBoolOption(
                (AsyncSuffixSettings options) => options.ExcludeTestMethodsFromAnalysis,
                "Exclude test methods from analysis");
            var editItemViewModelFactory = new DefaultCollectionEditItemViewModelFactory(null);
            var buttonProviderFactory = new DefaultButtonProviderFactory(lifetime, windowsHookManager, formValidators,
                iuiApplication, editItemViewModelFactory);
            var customAsyncTypes = new StringCollectionEditViewModel(lifetime, "Treat these types as async:",
                buttonProviderFactory, editItemViewModelFactory);
            ContextBoundSettingStoreEx.EnumerateIndexedEntry(store, AsyncSuffixSettingsAccessor.CustomAsyncTypes)
                .Where(pair => pair.First == pair.Second.FullName)
                .Select(pair => pair.First)
                .ToArray()
                .ForEach(x => customAsyncTypes.AddItem(x));
            customAsyncTypes.Items.CollectionChanged += (o, e) => ApplyDiff(AsyncSuffixSettingsAccessor.CustomAsyncTypes, customAsyncTypes.Items.Select(x => x.PresentableName.ToString(false)));
            AddHeader("Custom types");
            AddCustomOption(customAsyncTypes);
        }

        private void ApplyDiff(Expression<Func<AsyncSuffixSettings, IIndexedEntry<string, IClrTypeName>>> keyExpression, IEnumerable<string> newValues)
        {
            var addedAlreadyGeneratedFileMasks = new HashSet<string>();
            foreach (var pair in ContextBoundSettingStoreEx.EnumerateIndexedEntry(OptionsSettingsSmartContext, keyExpression))
            {
                var entryIndex = pair.First;
                if (!newValues.Contains(entryIndex))
                    OptionsSettingsSmartContext.RemoveIndexedValue(keyExpression, entryIndex);
                else
                    addedAlreadyGeneratedFileMasks.Add(entryIndex);
            }
            foreach (var entryIndex in newValues.Where(x => !addedAlreadyGeneratedFileMasks.Contains(x)))
                OptionsSettingsSmartContext.SetIndexedValue(keyExpression, entryIndex, new ClrTypeName(entryIndex));
        }
    }
}