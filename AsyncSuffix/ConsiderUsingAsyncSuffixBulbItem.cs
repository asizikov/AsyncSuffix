using System.Linq;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.Refactorings.Specific.Rename;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Refactorings.Rename;
using JetBrains.TextControl;

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
                var renames = RenameRefactoringService.Instance.CreateAtomicRenames(declared, newName, true).ToList();
                var workflow = RenameFromContexts.InitFromNameChanges(solution, renames);
                //                workflow.CreateRefactoring();
            }

            MethodDeclaration.GetPsiServices().Transactions.Execute(GetType().Name,
                () =>
                {
                    //                using (solution.GetComponent<IShellLocks>().UsingWriteLock())
                });
        }
    }
}