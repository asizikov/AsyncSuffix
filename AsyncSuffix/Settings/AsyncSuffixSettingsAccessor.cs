using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using JetBrains.Application.Settings.Store;
using JetBrains.Metadata.Reader.API;

namespace Sizikov.AsyncSuffix.Settings
{
    public static class AsyncSuffixSettingsAccessor
    {
        [NotNull] public static readonly Expression<Func<AsyncSuffixSettings, bool>>
            ExcludeTestMethodsFromAnalysis = x => x.ExcludeTestMethodsFromAnalysis;

        [NotNull] public static readonly Expression<Func<AsyncSuffixSettings, IIndexedEntry<string,IClrTypeName>>>
            CustomAsyncTypes =
                x => x.CustomAsyncTypes;
    }
}