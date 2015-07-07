using System;
using JetBrains.ReSharper.Psi;

namespace AsyncSuffix.Daemon
{
    public static class TaskExtensions
    {
        const string TaskTypeName = "System.Threading.Tasks.Task";
        const string TaskOfTTypeName = "System.Threading.Tasks.Task`1";

        public static bool IsTaskType(this IDeclaredType type)
        {
            var typeName = type.GetClrName().FullName;
            return string.Equals(typeName, TaskTypeName, StringComparison.Ordinal) ||
                   string.Equals(typeName, TaskOfTTypeName, StringComparison.Ordinal);
        }
    }
}