using JetBrains.ReSharper.Psi;

namespace Sizikov.AsyncSuffix.Analyzer
{
    public static class TaskExtensions
    {
        public static bool IsTaskType(this IDeclaredType type)
        {
            return type.IsTask() || type.IsGenericTask();
        }
    }
}