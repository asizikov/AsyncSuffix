using System.Collections.Generic;
using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.Intentions;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.Util;
using Sizikov.AsyncSuffix.Analyzer;

namespace Sizikov.AsyncSuffix
{
    [QuickFix]
    public sealed class ConsiderUsingAsyncSuffixQuickFix : IQuickFix
    {
        private ConsiderUsingAsyncSuffixHighlighting Highlighting { get; }

        public ConsiderUsingAsyncSuffixQuickFix([NotNull] ConsiderUsingAsyncSuffixHighlighting highlighting)
        {
            Highlighting = highlighting;
        }

        public IEnumerable<IntentionAction> CreateBulbItems()
            => new[] {new ConsiderUsingAsyncSuffixBulbItem(Highlighting.MethodDeclaration)}.ToQuickFixIntentions();

        public bool IsAvailable(IUserDataHolder cache) => Highlighting.IsValid();
    }
}