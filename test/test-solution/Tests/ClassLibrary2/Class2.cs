using System.Threading.Tasks;

namespace ClassLibrary2
{
    public interface IInterface
    {
        Task Foo();
    }
    public abstract class Abstract
    {
        public abstract Task Do();
    }

    public class ExcludeOverridesAndInterfaceImlementation : Abstract, IInterface
    {
        //should not be hilighted
        public override Task Do()
        {
            return Task.FromResult<object>(null);
        }

        //should not be hilighted
        public Task Foo()
        {
            return Task.FromResult<object>(null);
        }


        //should be hilighted
        public Task Bar()
        {

            return Task.FromResult<object>(null);
        }
    }
}
