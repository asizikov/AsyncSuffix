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
            settings.SetBinding(lifetime, AsyncSuffixSettingsAccessor.ExcludeTestMethodsFromAnalysis,
                ExcludeTestMethodsFromAnalysis);
        }


        [NotNull]
        public IProperty<bool> ExcludeTestMethodsFromAnalysis { get; private set; }
    }
}