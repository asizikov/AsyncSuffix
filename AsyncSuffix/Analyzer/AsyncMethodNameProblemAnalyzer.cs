using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Daemon.Stages.Dispatcher;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace Sizikov.AsyncSuffix.Analyzer
{
    [ElementProblemAnalyzer(typeof (IMethodDeclaration),
        HighlightingTypes = new[] {typeof (ConsiderUsingAsyncSuffixHighlighting)})]
    public sealed class AsyncMethodNameProblemAnalyzer : ElementProblemAnalyzer<IMethodDeclaration>
    {
        protected override void Run(IMethodDeclaration methodDeclaration, ElementProblemAnalyzerData data,
                                    IHighlightingConsumer consumer)
        {
            var declaredElement = methodDeclaration.DeclaredElement;
            if (declaredElement == null)
            {
                return;
            }

            if (methodDeclaration.IsAsyncSuffixMissing())
            {
                consumer.AddHighlighting(new ConsiderUsingAsyncSuffixHighlighting(methodDeclaration));
            }
        }
    }
}