using System.Linq;
using JetBrains.Annotations;
using JetBrains.Application.Interop.NativeHook;
using JetBrains.Application.Settings;
using JetBrains.Application.UI.Components;
using JetBrains.Application.UI.Controls.StringCollectionEdit.Impl;
using JetBrains.Application.UI.Controls.StringCollectionEdit.Impl.Items;
using JetBrains.Application.UI.Options;
using JetBrains.Application.UI.Options.OptionPages;
using JetBrains.Application.UI.Options.OptionsDialog;
using JetBrains.Application.UI.Validation;
using JetBrains.DataFlow;
using JetBrains.ReSharper.Feature.Services.Resources;
using JetBrains.UI.Wpf.Controls.StringCollectionEdit.Impl.Buttons;
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
            store.EnumEntryIndices(AsyncSuffixSettingsAccessor.CustomAsyncTypes)
                .ToArray()
                .ForEach(x => customAsyncTypes.AddItem(x));
            customAsyncTypes.Items.CollectionChanged += (o, e) =>
            {
                foreach (
                    var entryIndex in
                        OptionsSettingsSmartContext.EnumEntryIndices(AsyncSuffixSettingsAccessor.CustomAsyncTypes)
                            .ToArray())
                {
                    OptionsSettingsSmartContext.RemoveIndexedValue(AsyncSuffixSettingsAccessor.CustomAsyncTypes,
                        entryIndex);
                }
                foreach (
                    var editItemViewModel in
                        customAsyncTypes.Items)
                {
                    OptionsSettingsSmartContext.SetIndexedValue(AsyncSuffixSettingsAccessor.CustomAsyncTypes,
                        editItemViewModel.PresentableName, editItemViewModel.PresentableName);
                }
            };
            AddHeader("Custom types");
            AddCustomOption(customAsyncTypes);
        }
    }
}