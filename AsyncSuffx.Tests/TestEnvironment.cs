using System.Collections.Generic;
using System.Reflection;
using JetBrains.Application;
using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.ReSharper.TestFramework;
using JetBrains.TestFramework;
using JetBrains.TestFramework.Application.Zones;
using JetBrains.Threading;
using NUnit.Framework;
using Sizikov.AsyncSuffix.Analyzer;

[ZoneDefinition]
// ReSharper disable CheckNamespace
public class TestEnvironmentZone : ITestsZone, IRequire<PsiFeatureTestZone>
// ReSharper restore CheckNamespace
{
}

/// <summary>
///   Must be in the global namespace.
/// </summary>
[SetUpFixture]
// ReSharper disable CheckNamespace
public class ConfigureAwaitPluginTestEnvironmentAssembly : TestEnvironmentAssembly<TestEnvironmentZone>
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// Gets the assemblies to load into test environment.
    /// Should include all assemblies which contain components.
    /// </summary>
    private static IEnumerable<Assembly> GetAssembliesToLoad()
    {
        // Test assembly
        yield return Assembly.GetExecutingAssembly();

        // Plugin code
        yield return typeof(ConsiderUsingAsyncSuffixHighlighting).Assembly;
    }

    public override void SetUp()
    {
        base.SetUp();
        ReentrancyGuard.Current.Execute(
          "LoadAssemblies",
          () => Shell.Instance.GetComponent<AssemblyManager>().LoadAssemblies(
            GetType().Name, GetAssembliesToLoad()));
    }

    public override void TearDown()
    {
        ReentrancyGuard.Current.Execute(
          "UnloadAssemblies",
          () => Shell.Instance.GetComponent<AssemblyManager>().UnloadAssemblies(
            GetType().Name, GetAssembliesToLoad()));
        base.TearDown();
    }
}
