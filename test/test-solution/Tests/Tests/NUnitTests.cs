using System.Threading.Tasks;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class NUnitTests
    {
        [Test]
        public Task MyMethod()
        {
            return Task.FromResult<object>(null);
        }

        [TestCase(1)]
        public Task TestCase(int x)
        {
            return Task.FromResult<object>(null);
        }
    }
}
