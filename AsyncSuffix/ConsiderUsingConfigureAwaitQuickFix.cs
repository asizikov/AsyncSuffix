using System.Collections.Generic;
using AsyncSuffix.Daemon;
using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.Intentions;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.Util;

namespace AsyncSuffix
{
    [QuickFix]
    public sealed class ConsiderUsingConfigureAwaitQuickFix : IQuickFix
    {
        private readonly ConsiderUsingAsyncSuffixHighlighting _highlighting;

        public ConsiderUsingConfigureAwaitQuickFix([NotNull] ConsiderUsingAsyncSuffixHighlighting highlighting)
        {
            _highlighting = highlighting;
        }

        public IEnumerable<IntentionAction> CreateBulbItems()
        {
            return new[]
            {
                new ConsiderUsingConfigureAwaitBulbItem(_highlighting.MethodDeclaration)
            }.ToQuickFixAction();
        }

        public bool IsAvailable(IUserDataHolder cache)
        {
            return _highlighting.IsValid();
        }

    }
}