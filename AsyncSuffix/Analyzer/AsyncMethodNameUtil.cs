using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Application.Settings;
using JetBrains.Metadata.Reader.API;
using JetBrains.Metadata.Reader.Impl;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using Sizikov.AsyncSuffix.Settings;

namespace Sizikov.AsyncSuffix.Analyzer
{
    public sealed class AsyncMethodNameUtil
    {
        private static readonly JetHashSet<IClrTypeName> TestMethodClrAttributes = new JetHashSet<IClrTypeName>();

        static AsyncMethodNameUtil()
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

        public static bool IsAsyncSuffixMissing(IMethodDeclaration methodDeclaration)
        {
            if (methodDeclaration.IsOverride) return false;

            var declaredElement = methodDeclaration.DeclaredElement;
            if (declaredElement != null)
            {
                var settings = methodDeclaration.GetSettingsStore();
                var excludeTestMethods = settings.GetValue(AsyncSuffixSettingsAccessor.ExcludeTestMethodsFromAnalysis);
                if (excludeTestMethods)
                {
                    if (IsAnnotatedWithKnownTestAttribute(methodDeclaration))
                    {
                        return false;
                    }
                }

                if (declaredElement.ShortName.EndsWith("Async", StringComparison.Ordinal))
                {
                    return false;
                }

                var returnType = declaredElement.ReturnType as IDeclaredType;
                if (returnType != null)
                {
                    var customAsyncTypes = new List<IDeclaredType>();
                    settings.EnumEntryIndices(AsyncSuffixSettingsAccessor.CustomAsyncTypes)
                .ToArray()
                .ForEach(type => customAsyncTypes.Add(TypeFactory.CreateTypeByCLRName(type, declaredElement.Module)));

                    var isCustomAsyncType = customAsyncTypes.Any(type => returnType.IsSubtypeOf(type));

                   
                    if (returnType.IsTaskType() || isCustomAsyncType)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool IsAnnotatedWithKnownTestAttribute(IMethodDeclaration methodDeclaration)
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