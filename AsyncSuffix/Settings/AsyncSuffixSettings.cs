using JetBrains.Application.Settings;
using JetBrains.Application.Settings.Store;
using JetBrains.Metadata.Reader.API;

namespace Sizikov.AsyncSuffix.Settings
{
    [SettingsKey(typeof (EnvironmentSettings), "AsyncSuffix plugin settings")]
    public sealed class AsyncSuffixSettings
    {
        [SettingsEntry(true, "Exclude test methods from analysis")]
        public bool ExcludeTestMethodsFromAnalysis;
        [SettingsEntry(false, "Apply analysis to custom types")]
        public bool CustomAsyncTypesEnabled { get; set; }

        [SettingsIndexedEntry("Custom types list")]
        public IIndexedEntry<string, string> CustomAsyncTypes { get; set; }
    }
}