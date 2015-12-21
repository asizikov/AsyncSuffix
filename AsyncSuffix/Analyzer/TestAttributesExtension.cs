using System.Collections.Generic;
using JetBrains.Metadata.Reader.API;
using JetBrains.Metadata.Reader.Impl;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace Sizikov.AsyncSuffix.Analyzer
{
    public static class TestAttributesExtension
    {
        private static readonly JetHashSet<IClrTypeName> TestMethodClrAttributes = new JetHashSet<IClrTypeName>();

        static TestAttributesExtension()
        {
            TestMethodClrAttributes.Add(
                new ClrTypeName("Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute"));

            TestMethodClrAttributes.Add(
                new ClrTypeName("Xunit.FactAttribute"));

            TestMethodClrAttributes.Add(
                new ClrTypeName("Xunit.TheoryAttribute"));

            TestMethodClrAttributes.Add(
                new ClrTypeName("NUnit.Framework.TestAttribute"));
            TestMethodClrAttributes.Add(
                new ClrTypeName("NUnit.Framework.TestCaseAttribute"));
        }

        public static bool IsAnnotatedWithKnownTestAttribute(this IMethodDeclaration methodDeclaration)
        {
            if (methodDeclaration.AttributeSectionList != null)
            {
                foreach (var attribute in methodDeclaration.AttributeSectionList.AttributesEnumerable)
                {
                    var attributeClass = attribute.Name.Reference.Resolve().DeclaredElement as IClass;
                    if (attributeClass == null)
                    {
                        return false;
                    }
                    var clrTypeName = attributeClass.GetClrName();
                    if (TestMethodClrAttributes.Contains(clrTypeName))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}