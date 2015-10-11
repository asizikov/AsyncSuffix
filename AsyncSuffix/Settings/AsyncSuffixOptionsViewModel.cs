using JetBrains.Annotations;
using JetBrains.Application.Settings;
using JetBrains.DataFlow;
using JetBrains.UI.Options;

namespace Sizikov.AsyncSuffix.Settings
{
    public sealed class AsyncSuffixOptionsViewModel
    {
        [NotNull]
        private OptionsSettingsSmartContext SettingsStore { [UsedImplicitly] get; set; }

        public AsyncSuffixOptionsViewModel([NotNull] Lifetime lifetime,
                                           [NotNull] OptionsSettingsSmartContext settings)
        {
            SettingsStore = settings;
            ExcludeTestMethodsFromAnalysis = new Property<bool>(lifetime, "ExcludeTestMethodsFromAnalysis");
            CustomAsyncTypesEnabled = new Property<bool>(lifetime, "CustomAsyncTypesEnabled");
            settings.SetBinding(lifetime, AsyncSuffixSettingsAccessor.ExcludeTestMethodsFromAnalysis,
                ExcludeTestMethodsFromAnalysis);
            settings.SetBinding(lifetime, AsyncSuffixSettingsAccessor.CustomAsyncTypesEnabled, CustomAsyncTypesEnabled);
        }


        [NotNull]
        public IProperty<bool> ExcludeTestMethodsFromAnalysis { get; private set; }

        [NotNull]
        public IProperty<bool> CustomAsyncTypesEnabled { get; private set; }
    }
}