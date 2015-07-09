using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Daemon;
using JetBrains.ReSharper.Psi;
using NUnit.Framework;
using Sizikov.AsyncSuffix.Analyzer;

namespace AsyncSuffx.Tests.Analyzer
{
    [TestFixture]
    public class ConsiderUsingAsyncSuffixHighlightingTest : CSharpHighlightingTestBase
    {
        protected override bool HighlightingPredicate(IHighlighting highlighting, IPsiSourceFile sourceFile)

        {
            return highlighting is ConsiderUsingAsyncSuffixHighlighting;
        }

        protected override string RelativeTestDataPath
        {
            get { return @"\Analyzer\ConsiderUsingAwaitSuffix"; }
        }

        [Test]
        [TestCase("Case1.cs")]
        public void Test(string testName)
        {
            DoTestSolution(testName);
        }
    }
}