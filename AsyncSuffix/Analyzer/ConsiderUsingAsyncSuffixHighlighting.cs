using JetBrains.DocumentModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace Sizikov.AsyncSuffix.Analyzer
{
    [ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name, OverlapResolve = OverlapResolveKind.WARNING)]
    public sealed class ConsiderUsingAsyncSuffixHighlighting : IHighlighting
    {
        public IMethodDeclaration MethodDeclaration { get; set; }
        public const string SeverityId = "ConsiderUsingAssyncSuffix";

        public ConsiderUsingAsyncSuffixHighlighting(IMethodDeclaration methodDeclaration)
        {
            MethodDeclaration = methodDeclaration;
        }

        public DocumentRange CalculateRange()
        {
            return MethodDeclaration.GetHighlightingRange();
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
            return true;
            //            return MethodDeclaration == null || MethodDeclaration.IsValid();
        }
    }
}