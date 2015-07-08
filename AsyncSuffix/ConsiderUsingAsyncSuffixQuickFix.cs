﻿using System.Collections.Generic;
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
        private readonly ConsiderUsingAsyncSuffixHighlighting highlighting;

        public ConsiderUsingAsyncSuffixQuickFix([NotNull] ConsiderUsingAsyncSuffixHighlighting highlighting)
        {
            this.highlighting = highlighting;
        }

        public IEnumerable<IntentionAction> CreateBulbItems()
        {
            return new[] { new ConsiderUsingAsyncSuffixBulbItem(highlighting.MethodDeclaration) }.ToQuickFixAction();
        }

        public bool IsAvailable(IUserDataHolder cache)
        {
            return highlighting.IsValid();
        }
    }
}