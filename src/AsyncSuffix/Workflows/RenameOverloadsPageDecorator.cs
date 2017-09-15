using System.Collections.Generic;
using System.Reflection;
using JetBrains.Application.Progress;
using JetBrains.DataFlow;
using JetBrains.ReSharper.Feature.Services.Refactorings;
using JetBrains.ReSharper.Feature.Services.UI.CompletionPicker;
using JetBrains.ReSharper.Refactorings.Rename;
using JetBrains.ReSharper.Refactorings.UI.CommonUI.RefactoringWindows.WinFormsControls;

namespace Sizikov.AsyncSuffix.Workflows
{
    internal class RenameOverloadsPageDecorator : IRefactoringPage
    {
        private RenameOverloadsPage OverloadsPage { get; }
        private List<string> Suggestions { get; }
        private NameCompletionEdit EditBox { get; set; }

        public RenameOverloadsPageDecorator(RenameOverloadsPage overloadsPage, List<string> suggestions)
        {
            OverloadsPage = overloadsPage;
            Suggestions = suggestions;
        }

        public IProperty<bool> ContinueEnabled => OverloadsPage.ContinueEnabled;
        public string Description => OverloadsPage.Description;
        public string Title => OverloadsPage.Title;

        public bool DoNotShow => OverloadsPage.DoNotShow;

        public IRefactoringPage Commit(IProgressIndicator pi)
        {
            var irefactoringPage = OverloadsPage.Commit(pi);
            var originPage = irefactoringPage as RenameInitialControlViewModel;
            if (originPage == null)
                return irefactoringPage;

            if (originPage?.View != null)
            {
                var renameInitialControl = originPage.View.Control as RenameInitialControl;
                renameInitialControl.
            }

            return (IRefactoringPage)new RenameInitialControlDecorator(originPage, this.mySpellService, this.myReSpellerSettings);

            var refactoringPage = OverloadsPage.Commit(pi);
            var controlViewModel = refactoringPage as RenameInitialControlViewModel;
            if (controlViewModel?.View != null)
            {
                var renameInitialControl = controlViewModel.View.Control as RenameInitialControl;
                var field = typeof(RenameInitialControl).GetField("myEditboxName", BindingFlags.Instance | BindingFlags.NonPublic);
                if (field != null)
                {
                    EditBox = field.GetValue(renameInitialControl) as NameCompletionEdit;
                    
                    EditBox.GotFocus += (sender, args) => ShowSuggests();
                }
            }
            return refactoringPage;
        }

        public bool Initialize(IProgressIndicator pi) => OverloadsPage.Initialize(pi);

        public bool RefreshContents(IProgressIndicator pi) => OverloadsPage.RefreshContents(pi);

        private void ShowSuggests()
        {
            if (EditBox == null)
                return;
            var method = typeof(CompletionPickerEdit).GetMethod("CompletionListShow", BindingFlags.Instance | BindingFlags.NonPublic);
            var nameCompletionEdit = EditBox;
            method.Invoke(nameCompletionEdit, new[] {(object) CompletionPickerEdit.CompletionListShowModeTransition.Soft});
        }
    }
}