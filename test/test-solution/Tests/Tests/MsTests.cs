using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class MsTests
    {
        [TestMethod]
        public Task MyMethod()
        {
            return Task.FromResult<object>(null);
        }

        [TestMethod]
        public Task TestCase(int x)
        {
            return Task.FromResult<object>(null);
        }
    }
}