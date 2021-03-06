﻿using JetBrains.ActionManagement;
using JetBrains.Application.DataContext;
using JetBrains.DataFlow;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.DataContext;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.Refactorings;
using JetBrains.ReSharper.Feature.Services.Refactorings.Specific.Rename;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.TextControl;
using Sizikov.AsyncSuffix.Workflows;

namespace Sizikov.AsyncSuffix
{
    public sealed class ConsiderUsingAsyncSuffixBulbItem : IBulbAction
    {
        private IMethodDeclaration MethodDeclaration { get; }

        public ConsiderUsingAsyncSuffixBulbItem(IMethodDeclaration methodDeclaration)
        {
            MethodDeclaration = methodDeclaration;
        }

        public string Text => "Add 'Async' suffix to method name";

        public void Execute(ISolution solution, ITextControl textControl)
        {
            if (!MethodDeclaration.IsValid())
            {
                return;
            }
            var declared = MethodDeclaration.DeclaredElement;
            if (declared != null)
            {
                var suggests = AsyncMethodNameSuggestions.Get(MethodDeclaration);
                var workflow =
                    (IRefactoringWorkflow)
                        new MethodRenameWorkflow(suggests, solution.GetComponent<RenameRefactoringService>(), solution, "AsyncSuffixMethodRename");
                Lifetimes.Using(lifetime =>
                {
                    var dataRules = DataRules.AddRule("DoAsyncMethodRenameWorkflow", ProjectModelDataConstants.SOLUTION, solution);
                    var dataContext = solution.GetComponent<IActionManager>().DataContexts.CreateOnSelection(lifetime, dataRules);
                    RefactoringActionUtil.ExecuteRefactoring(dataContext, workflow);
                });
            }
        }
    }
}