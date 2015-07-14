using JetBrains.DocumentModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using Sizikov.AsyncSuffix.Analyzer;


[assembly: RegisterConfigurableSeverity(ConsiderUsingAsyncSuffixHighlighting.SeverityId,
  null,
  HighlightingGroupIds.BestPractice,
  "Consider adding Async suffix",
  "Library code should use ConfigureAwait with every await. Always specifying ConfigureAwait makes it clearer how the continuation is invoked and avoids synchonization bugs.",
  Severity.SUGGESTION,
  false)]
namespace Sizikov.AsyncSuffix.Analyzer
{
    [ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name, OverlapResolve = OverlapResolveKind.WARNING)]
    public sealed class ConsiderUsingAsyncSuffixHighlighting : IHighlighting
    {
        public IMethodDeclaration MethodDeclaration { get; set; }
        public const string SeverityId = "ConsiderUsingAsyncSuffix";

        public ConsiderUsingAsyncSuffixHighlighting(IMethodDeclaration methodDeclaration)
        {
            MethodDeclaration = methodDeclaration;
        }

        public DocumentRange CalculateRange()
        {
            return MethodDeclaration.NameIdentifier.GetDocumentRange();
        }

        public string ToolTip
        {
            get { return "Async method name does not have 'Async' suffix"; }
        }

        public string ErrorStripeToolTip
        {
            get { return ToolTip; }
        }

        public int NavigationOffsetPatch
        {
            get { return 0; }
        }

        public bool IsValid()
        {
            return MethodDeclaration == null || MethodDeclaration.IsValid();
        }
    }
}