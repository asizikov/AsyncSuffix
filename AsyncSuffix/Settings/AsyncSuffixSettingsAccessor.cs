using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using JetBrains.Application.Settings.Store;

namespace Sizikov.AsyncSuffix.Settings
{
    internal static class AsyncSuffixSettingsAccessor
    {
        [NotNull] public static readonly Expression<Func<AsyncSuffixSettings, bool>>
            ExcludeTestMethodsFromAnalysis = x => x.ExcludeTestMethodsFromAnalysis;

        [NotNull] public static readonly Expression<Func<AsyncSuffixSettings, IIndexedEntry<string,string>>>
            CustomAsyncTypes =
                x => x.CustomAsyncTypes;
    }
}