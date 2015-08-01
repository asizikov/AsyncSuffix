using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace Sizikov.AsyncSuffix.Analyzer
{
    public sealed class AsyncMethodNameUtil
    {
        private static readonly JetHashSet<string> TestMethodAttributes = new JetHashSet<string>();

        static AsyncMethodNameUtil()
        {
            TestMethodAttributes.Add("Test");
            TestMethodAttributes.Add("Fact");
            TestMethodAttributes.Add("TestMethod");
        }

        public static bool IsAsyncSuffixMissing(IMethodDeclaration methodDeclaration)
        {
            if (methodDeclaration.IsOverride) return false;

            var declaredElement = methodDeclaration.DeclaredElement;
            if (declaredElement != null)
            {
                if (methodDeclaration.AttributeSectionList != null)
                {
                    foreach (var attribute in methodDeclaration.AttributeSectionList.AttributesEnumerable)
                    {
                        var referenceName = attribute.Name;
                        if (referenceName != null)
                        {
                            if (referenceName.ShortName != null)
                            {
                                if (TestMethodAttributes.Contains(referenceName.ShortName))
                                {
                                    return false;
                                }

                            }
                        }
                    }
                }
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