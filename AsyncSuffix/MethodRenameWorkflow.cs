using System.Collections.Generic;
using System.Linq;
using JetBrains.Application.DataContext;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Refactorings;
using JetBrains.ReSharper.Feature.Services.Refactorings.Specific.Rename;
using JetBrains.ReSharper.Psi.Naming.Impl;
using JetBrains.ReSharper.Refactorings.Rename;

namespace Sizikov.AsyncSuffix
{
  internal class MethodRenameWorkflow : RenameWorkflow
  {
    private List<string> Suggestions { get; set; }

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

    public MethodRenameWorkflow(List<string> suggestions, RenameRefactoringService renameRefactoringService, ISolution solution, string actionId)
            :base(renameRefactoringService, solution, actionId)
    {
      Suggestions = suggestions;
    }

    public override bool Initialize(IDataContext context)
    {
        var flag = base.Initialize(context);
        var roots =
              Suggestions.Select(str => new List<NameInnerElement> {new NameWord(str, str)})
                  .Select(nameElement => new NameRoot(nameElement, PluralityKinds.Single, true));
        DataModel.Roots = roots;

        return flag;
    }
  }
}
