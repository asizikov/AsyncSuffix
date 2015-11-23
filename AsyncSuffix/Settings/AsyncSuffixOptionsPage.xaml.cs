using JetBrains.Annotations;
using JetBrains.DataFlow;
using JetBrains.ReSharper.Feature.Services.Resources;
using JetBrains.UI.CrossFramework;
using JetBrains.UI.Options;
using JetBrains.UI.Options.OptionPages;
using JetBrains.UI.Options.OptionsDialog2.SimpleOptions;

namespace Sizikov.AsyncSuffix.Settings
{
    [OptionsPage(PageId, "AsyncSuffix", typeof(ServicesThemedIcons.AnalyzeThis),
     ParentId = EnvironmentPage.Pid)]
    public sealed partial class AsyncSuffixOptionsPage : CustomSimpleOptionsPage
    {
        private const string PageId = "AsyncSuffix";

        public AsyncSuffixOptionsPage([NotNull] Lifetime lifetime, [NotNull] OptionsSettingsSmartContext store
                                  ) : base(lifetime, store)
        {
            AddHeader("Options");
            AddBoolOption(
                (AsyncSuffixSettings options) => options.ExcludeTestMethodsFromAnalysis,
                "Exclude test methods from analysis");
        }
    }
}
