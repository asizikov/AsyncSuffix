using System.Collections.Generic;
using System.Linq;
using JetBrains.ActionManagement;
using JetBrains.Application.DataContext;
using JetBrains.DataFlow;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.DataContext;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.Refactorings;
using JetBrains.ReSharper.Feature.Services.Refactorings.Specific.Rename;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl;
using DataConstants = JetBrains.ProjectModel.DataContext.DataConstants;

namespace Sizikov.AsyncSuffix
{
    public sealed class ConsiderUsingAsyncSuffixBulbItem : IBulbAction
    {
        private IMethodDeclaration MethodDeclaration { get; set; }

        public ConsiderUsingAsyncSuffixBulbItem(IMethodDeclaration methodDeclaration)
        {
            MethodDeclaration = methodDeclaration;
        }

        public string Text
        {
            get { return "Add \'Async\' suffix to method name"; }
        }

        public void Execute(ISolution solution, ITextControl textControl)
        {
            if (!MethodDeclaration.IsValid())
            {
                return;
            }
            var declared = MethodDeclaration.DeclaredElement;
            if (declared != null)
            {
                var newName = declared.ShortName + "Async";
                var suggests = new List<string> {newName};
                var workflow = (IRefactoringWorkflow)new MethodRenameWorkflow(suggests, RenameRefactoringService.Instance, solution, "TypoRename");
                Lifetimes.Using(lifetime =>
                {
                    var dataRules = DataRules.AddRule("DoTypoRenameWorkflow", ProjectModelDataConstants.SOLUTION, solution);
                    var dataContext = solution.GetComponent<IActionManager>().DataContexts.CreateOnSelection(lifetime, dataRules);
                    RefactoringActionUtil.ExecuteRefactoring(dataContext, workflow);
                });
            }
        }
    }
}