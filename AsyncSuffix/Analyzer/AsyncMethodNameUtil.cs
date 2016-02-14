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
                var settings = methodDeclaration.GetSettingsStore();
                var excludeTestMethods = settings.GetValue(AsyncSuffixSettingsAccessor.ExcludeTestMethodsFromAnalysis);
                if (excludeTestMethods)
                {
                    if (declaredElement.IsTestMethod())
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
                var conversionRule = new CSharpTypeConversionRule(returnType.Module);
                var isCustomAsyncType = customAsyncTypes.Any(type => returnType.IsSubtypeOf(type) || returnType.IsImplicitlyConvertibleTo(type, conversionRule));
                if (!isCustomAsyncType)
                {
                    var declaredTypes = returnType.GetSuperTypes();
                    var clrNames = declaredTypes.Select(declaredType => declaredType.GetClrName()).ToList();
                    clrNames.Add(returnType.GetClrName());

                    if (clrNames.Any(clrTypeName => customAsyncTypeNames.Contains(clrTypeName.FullName)))
                    {
                        isCustomAsyncType = true;
                    }
                }

                if (returnType.IsTaskType() || isCustomAsyncType)
                {
                    return true;
                }
            }
            return false;
        }
    }
}