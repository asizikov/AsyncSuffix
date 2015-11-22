using JetBrains.DataFlow;
using JetBrains.Metadata.Reader.API;
using JetBrains.Metadata.Reader.Impl;

namespace Sizikov.AsyncSuffix.Settings
{
    public sealed class CustomAsyncTypeViewModel
    {
        public IProperty<string> ClrName { get; set; }

        public IClrTypeName ClrTypeName
        {
            get { return new ClrTypeName(ClrName.Value); }
        }
    }
}
