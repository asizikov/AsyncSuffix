using JetBrains.Annotations;
using JetBrains.DataFlow;
using JetBrains.ReSharper.Feature.Services.Resources;
using JetBrains.UI.CrossFramework;
using JetBrains.UI.Options;
using JetBrains.UI.Options.OptionPages;

namespace Sizikov.AsyncSuffix.Settings
{
    [OptionsPage(
     id: PID, name: "AsyncSuffix",
     typeofIcon: typeof(ServicesThemedIcons.AnalyzeThis),
     ParentId = EnvironmentPage.Pid)]
    public sealed partial class AsyncSuffixOptionsPage : IOptionsPage
    {
        // ReSharper disable once InconsistentNaming
        private const string PID = "AsyncSuffix";

        public AsyncSuffixOptionsPage([NotNull] Lifetime lifetime, [NotNull] OptionsSettingsSmartContext store
                                  )
        {
            InitializeComponent();
            DataContext = new AsyncSuffixOptionsViewModel(lifetime, store);
            Control = this;
        }

        public EitherControl Control { get; private set; }
        public string Id { get { return PID; } }
        public bool OnOk() { return true; }
        public bool ValidatePage() { return true; }
    }
}
