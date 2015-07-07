using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Daemon.Stages.Dispatcher;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncSuffix.Daemon
{
    [ElementProblemAnalyzer(typeof (IMethodDeclaration), HighlightingTypes = new[] {typeof (ConsiderUsingAsyncSuffixHighlighting)})]
    public sealed class ConsiderUsingAsyncSuffixAnalyzer : ElementProblemAnalyzer<IMethodDeclaration>
    {
        protected override void Run(IMethodDeclaration methodDeclaration, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            if (IsInterestring(methodDeclaration))
            {
                var declaredElement = methodDeclaration.DeclaredElement;
                if (declaredElement != null)
                {
                    if (declaredElement.ShortName.EndsWith("Async"))
                    {
                        return;
                    }

                    var returnType = declaredElement.ReturnType as IDeclaredType;
                    if (returnType != null)
                    {
                        if (returnType.IsTaskType())
                        {
                            consumer.AddHighlighting(new ConsiderUsingAsyncSuffixHighlighting(methodDeclaration));
                        }
                    }
                }
            }
        }

        private bool IsInterestring(IMethodDeclaration methodDeclaration)
        {
            if (!methodDeclaration.IsAsync || methodDeclaration.IsOverride)
            {
                return false;
            }
            return true;
        }
    }
}