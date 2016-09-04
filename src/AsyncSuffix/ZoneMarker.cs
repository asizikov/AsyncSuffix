using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Feature.Services;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;

namespace Sizikov.AsyncSuffix
{
    [ZoneMarker]
    public class ZoneMarker  : IRequire<ICodeEditingZone>, IRequire<ILanguageCSharpZone>, IRequire<DaemonZone>
    {
    }
}