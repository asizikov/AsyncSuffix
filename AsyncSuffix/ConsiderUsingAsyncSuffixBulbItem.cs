using System.Collections.Generic;
using System.Linq;
using JetBrains.ActionManagement;
using JetBrains.Application;
using JetBrains.Application.DataContext;
using JetBrains.DataFlow;
using JetBrains.ProjectModel;
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

            var containingFile = MethodDeclaration.GetContainingFile();
            var psiModule = MethodDeclaration.GetPsiModule();
            var elementFactory = CSharpElementFactory.GetInstance(psiModule);
            var declared = MethodDeclaration.DeclaredElement;
            if (declared != null)
            {
                var newName = declared.ShortName + "Async";


                var refactoringService = solution.GetComponent<RenameRefactoringService>();
                var suggests = new List<string> {newName};
                SearchDomainFactory searchDomainFactory = solution.GetComponent<SearchDomainFactory>();
                var workflow = (IRefactoringWorkflow)new MethodRenameWorkflow(suggests, solution.GetComponent<IShellLocks>(), searchDomainFactory, refactoringService, solution, "TypoRename");

                var renames = RenameRefactoringService.Instance.CreateAtomicRenames(declared, newName, true).ToList();
               // var workflow = RenameFromContexts.InitFromNameChanges(solution, renames);
                //                workflow.CreateRefactoring();
                Lifetimes.Using(lifetime => RefactoringActionUtil.ExecuteRefactoring(solution.GetComponent<IActionManager>().DataContexts.CreateOnSelection(lifetime, DataRules.AddRule("DoTypoRenameWorkflow", DataConstants.SOLUTION, solution)), workflow));
            }
        }
    }
}