using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Application;
using JetBrains.Application.Progress;
using JetBrains.DocumentManagers;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.Refactorings.Specific.Rename;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.CodeStyle;
using JetBrains.ReSharper.Psi.CSharp.ConstantValue;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Refactorings.Rename;
using JetBrains.TextControl;

namespace AsyncSuffix
{
    public sealed class ConsiderUsingConfigureAwaitBulbItem : IBulbAction
    {
        private readonly IMethodDeclaration _literalExpression;

        public ConsiderUsingConfigureAwaitBulbItem(IMethodDeclaration literalExpression)
        {
            _literalExpression = literalExpression;
        }

        public string Text
        {
            get { return "Add \'Async\' suffix to method name"; }
        }

        public void Execute(ISolution solution, ITextControl textControl)
        {
            if (!_literalExpression.IsValid())
                return;

            var containingFile = _literalExpression.GetContainingFile();
            var psiModule = _literalExpression.GetPsiModule();
            var elementFactory = CSharpElementFactory.GetInstance(psiModule);
            var declared =_literalExpression.DeclaredElement;
            if (declared != null)
            {
               var newName= declared.ShortName + "Async";
                var renames =RenameRefactoringService.Instance.CreateAtomicRenames(declared, newName, true).ToList();
                var workflow = RenameFromContexts.InitFromNameChanges(solution, renames);
                workflow.CreateRefactoring()
            }
            


            IExpression newExpression = null;
            _literalExpression.GetPsiServices().Transactions.Execute(GetType().Name, () =>
            {
//                using (solution.GetComponent<IShellLocks>().UsingWriteLock())
//                    newExpression = ModificationUtil.ReplaceChild(
//                      _literalExpression.Task, elementFactory.CreateExpression("$0.ConfigureAwait($1)", _literalExpression.Task,
//                        elementFactory.CreateExpressionByConstantValue(CSharpConstantValueFactory.CreateBoolValue(_value, psiModule, null))));
            });

            if (newExpression != null)
            {
                IRangeMarker marker = newExpression.GetDocumentRange().CreateRangeMarker(solution.GetComponent<DocumentManager>());
                containingFile.OptimizeImportsAndRefs(marker, false, true, NullProgressIndicator.Instance);
            }
        }
    }
}
