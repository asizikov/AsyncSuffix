using System.Collections.Generic;
using JetBrains.Metadata.Reader.API;
using JetBrains.Metadata.Reader.Impl;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.UnitTestFramework;
using JetBrains.ProjectModel;

namespace Sizikov.AsyncSuffix.Analyzer
{
    public static class TestAttributesExtension
    {
        public static bool IsTestMethod(this IDeclaredElement declaredElement)
        {
            var unitTestElement = declaredElement.GetSolution().GetComponent<IUnitTestElementStuff>();
            return (declaredElement is IMethod || declaredElement is IProperty) && unitTestElement.IsElementOfKind(declaredElement, UnitTestElementKind.Test);
        }
    }
}