using System.Reflection;
using JetBrains.Application.Progress;
using JetBrains.DataFlow;
using JetBrains.ReSharper.Feature.Services.Refactorings;
using JetBrains.ReSharper.Feature.Services.UI.CompletionPicker;
using JetBrains.ReSharper.Refactorings.Rename;
using JetBrains.UI.CrossFramework;

namespace Sizikov.AsyncSuffix
{
  internal class RenameOverloadsPageDecorator : IRefactoringPage
  {
    private readonly RenameOverloadsPage myOverloadsPage;
    private NameCompletionEdit EditBox;

    public IProperty<bool> ContinueEnabled
    {
      get
      {
        return myOverloadsPage.ContinueEnabled;
      }
    }

    public string Description
    {
      get
      {
        return myOverloadsPage.Description;
      }
    }

    public string Title
    {
      get
      {
        return myOverloadsPage.Title;
      }
    }

    public EitherControl View
    {
      get
      {
        ShowSuggests();
        return myOverloadsPage.View;
      }
    }

    public bool DoNotShow
    {
      get
      {
        return myOverloadsPage.DoNotShow;
      }
    }

    public RenameOverloadsPageDecorator(RenameOverloadsPage overloadsPage)
    {
      myOverloadsPage = overloadsPage;
    }

    public IRefactoringPage Commit(IProgressIndicator pi)
    {
      IRefactoringPage irefactoringPage = myOverloadsPage.Commit(pi);
      if (irefactoringPage is RenameInitialControl)
      {
        var renameInitialControl = irefactoringPage as RenameInitialControl;
        var field = typeof (RenameInitialControl).GetField("myEditboxName", BindingFlags.Instance | BindingFlags.NonPublic);
        if (field != null)
          this.EditBox = field.GetValue(renameInitialControl) as NameCompletionEdit;
      }
      return irefactoringPage;
    }

    public bool Initialize(IProgressIndicator pi)
    {
      return myOverloadsPage.Initialize(pi);
    }

    public bool RefreshContents(IProgressIndicator pi)
    {
      return myOverloadsPage.RefreshContents(pi);
    }

    private void ShowSuggests()
    {
      if (EditBox == null)
        return;
      var method = typeof (CompletionPickerEdit).GetMethod("CompletionListShow", BindingFlags.Instance | BindingFlags.NonPublic);
      var nameCompletionEdit = EditBox;
      var parameters = new object[1];
      int index = 0;
      var showModeTransition = CompletionPickerEdit.CompletionListShowModeTransition.Soft;
      parameters[index] = showModeTransition;
      method.Invoke(nameCompletionEdit, parameters);
    }
  }}