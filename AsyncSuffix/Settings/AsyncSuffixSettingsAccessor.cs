using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Sizikov.AsyncSuffix.Settings
{
    public static class AsyncSuffixSettingsAccessor
    {
        [NotNull]
        public static readonly Expression<Func<AsyncSuffixSettings, bool>>
            ExcludeTestMethodsFromAnalysis = x => x.ExcludeTestMethodsFromAnalysis;
    }
}