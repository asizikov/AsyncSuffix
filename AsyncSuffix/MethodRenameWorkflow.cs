using System.Collections.Generic;
using System.Reflection;
using JetBrains.Application;
using JetBrains.Application.DataContext;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Refactorings;
using JetBrains.ReSharper.Feature.Services.Refactorings.Specific.Rename;
using JetBrains.ReSharper.Psi.Naming.Impl;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Refactorings.Rename;

namespace Sizikov.AsyncSuffix
{
  internal class MethodRenameWorkflow : RenameWorkflow
  {
    private readonly List<string> Suggestions;

    public override IRefactoringPage FirstPendingRefactoringPage
    {
      get
      {
        var overloadsPage = base.FirstPendingRefactoringPage as RenameOverloadsPage;
        if (overloadsPage != null)
          return new RenameOverloadsPageDecorator(overloadsPage);
        return base.FirstPendingRefactoringPage;
      }
    }

    public MethodRenameWorkflow(List<string> suggestions, IShellLocks locks, SearchDomainFactory searchDomainFactory, RenameRefactoringService renameRefactoringService, ISolution solution, string actionId)
            :base(locks, searchDomainFactory, renameRefactoringService, solution, actionId)
    {
      Suggestions = suggestions;
    }

    public override bool Initialize(IDataContext context)
    {
      var flag = base.Initialize(context);
      var list1 = typeof (RenameWorkflow).GetField("myRoots", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(this) as List<NameRoot>;
      if (list1 != null)
      {
        list1.Clear();
        foreach (var str in Suggestions)
        {
          var list2 = list1;
            var list3 = new List<NameInnerElement> {new NameWord(str, str)};
            var nameRoot = new NameRoot(list3, (PluralityKinds) 1, true);
          list2.Add(nameRoot);
        }
      }
      return flag;
    }
  }
}
