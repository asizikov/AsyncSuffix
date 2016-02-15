using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Impl;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using Sizikov.AsyncSuffix.Settings;

namespace Sizikov.AsyncSuffix.Analyzer
{
    internal static class AsyncMethodNameUtil
    {
        public static bool IsAsyncSuffixMissing(this IMethodDeclaration methodDeclaration)
        {
            if (methodDeclaration.IsOverride) return false;
            var declaredElement = methodDeclaration.DeclaredElement;
            if (declaredElement != null)
            {
                var memberInstances = declaredElement.GetAllSuperMembers();
                if (memberInstances.Count > 0)
                {
                    return false;
                }

                var settings = methodDeclaration.GetSettingsStore();
                var excludeTestMethods = settings.GetValue(AsyncSuffixSettingsAccessor.ExcludeTestMethodsFromAnalysis);
                if (excludeTestMethods)
                {
                    if (declaredElement.IsTestMethod() || methodDeclaration.IsAnnotatedWithKnownTestAttribute())
                    {
                        return false;
                    }
                }

                if (declaredElement.ShortName.EndsWith("Async", StringComparison.Ordinal))
                {
                    return false;
                }

                var returnType = declaredElement.ReturnType as IDeclaredType;
                if (returnType == null) return false;

                var customAsyncTypeNames = settings.EnumEntryIndices(AsyncSuffixSettingsAccessor.CustomAsyncTypes)
                    .ToArray();
                var customAsyncTypes = new List<IDeclaredType>();
                customAsyncTypeNames
                    .ForEach(type => customAsyncTypes.Add(TypeFactory.CreateTypeByCLRName(type, declaredElement.Module)));

                var returnTypeElement = returnType.GetTypeElement();
                var isCustomAsyncType = returnTypeElement != null && customAsyncTypes.Any(type => returnTypeElement.IsDescendantOf(type.GetTypeElement()));

                if (returnType.IsTaskType() || isCustomAsyncType)
                {
                    return true;
                }
            }
            return false;
        }
    }
}