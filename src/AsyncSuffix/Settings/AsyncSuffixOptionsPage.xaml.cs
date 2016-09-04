using System.Linq;
using JetBrains.Annotations;
using JetBrains.Application.Interop.NativeHook;
using JetBrains.Application.Settings;
using JetBrains.DataFlow;
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