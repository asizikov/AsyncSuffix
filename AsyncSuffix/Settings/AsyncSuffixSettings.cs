using JetBrains.Application.Settings;

namespace Sizikov.AsyncSuffix.Settings
{
    [SettingsKey(typeof (EnvironmentSettings), "AsyncSuffix plugin settings")]
    public sealed class AsyncSuffixSettings
    {
        [SettingsEntry(true, "Exclude test methods from analysis")] public bool ExcludeTestMethodsFromAnalysis;
    }
}