using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class XUnitTests
    {
        [Fact]
        public Task MyMethod()
        {
            return Task.FromResult<object>(null);
        }

        [Theory]
        public Task TestCase(int x)
        {
            return Task.FromResult<object>(null);
        }
    }
}