 // ReSharper disable once CheckNamespace
namespace ClassLibrary1
{
    public class Class1
    {
        public GenericCustomAsyncType<string> Return()
        {
            return new GenericCustomAsyncType<string>();
        }

        public ICustomAsyncInterface<string> Foox()
        {
            return new CustomAsyncInterfaceIml();
        }

        public ICustomAsyncInterface<T> Foox<T>()
        {
            return null;
        }

        public CustomAsyncInterfaceIml Fooz()
        {
            return new CustomAsyncInterfaceIml();
        }

        public CustomAsyncType Fooy()
        {
            return new CustomAsyncType();
        }

        public IAsyncType Fooa()
        {
            return new AsyncType();
        }

        public AsyncType Fooyb()
        {
            return new AsyncType();
        }
    }

    public class CustomAsyncType
    {
    }

    public class GenericCustomAsyncType<T>
    {
    }

    public interface ICustomAsyncInterface<T>
    {
    }

    public interface IAsyncType
    {
    }

    public class AsyncType : IAsyncType
    {
    }

    public class CustomAsyncInterfaceIml : ICustomAsyncInterface<string>
    {
    }
}