using System.Collections.Generic;
using System.Linq;
using JetBrains.Application.DataContext;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Naming.Impl;
using JetBrains.ReSharper.Refactorings.Rename;

namespace Sizikov.AsyncSuffix.Workflows
{
    internal class MethodRenameWorkflow : RenameWorkflow
    {
        private List<string> Suggestions { get; }

        public MethodRenameWorkflow(List<string> suggestions, ISolution solution, string actionId)
            : base(solution, actionId)
        {
            Suggestions = suggestions;
        }

        //public override IRefactoringPage FirstPendingRefactoringPage
        //{
        //    get
        //    {
        //        var sequencePages = base.FirstPendingRefactoringPage as SequencePages;
        //        if (sequencePages != null)
        //        {
        //            return new RenameOverloadsPageDecorator(sequencePages.FirstPage, Suggestions);
        //        }
        //        return base.FirstPendingRefactoringPage;
        //    }
        //}

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