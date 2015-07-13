using System;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Daemon.Stages.Dispatcher;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace Sizikov.AsyncSuffix.Analyzer
{
    [ElementProblemAnalyzer(typeof (IMethodDeclaration),
        HighlightingTypes = new[] {typeof (ConsiderUsingAsyncSuffixHighlighting)})]
    public sealed class ConsiderUsingAsyncSuffixAnalyzer : ElementProblemAnalyzer<IMethodDeclaration>
    {
        protected override void Run(IMethodDeclaration methodDeclaration, ElementProblemAnalyzerData data,
                                    IHighlightingConsumer consumer)
        {
            if (IsInterestring(methodDeclaration))
            {
                consumer.AddHighlighting(new ConsiderUsingAsyncSuffixHighlighting(methodDeclaration));
            }
        }

        private bool IsInterestring(IMethodDeclaration methodDeclaration)
        {
            if (methodDeclaration.IsOverride) return false;

            var declaredElement = methodDeclaration.DeclaredElement;
            if (declaredElement != null)
            {
                if (declaredElement.ShortName.EndsWith("Async", StringComparison.Ordinal))
                {
                    return false;
                }

                var returnType = declaredElement.ReturnType as IDeclaredType;
                if (returnType != null)
                {
                    if (returnType.IsTaskType())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}