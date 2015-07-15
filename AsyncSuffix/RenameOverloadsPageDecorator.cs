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
    private RenameOverloadsPage OverloadsPage { get; set; }
    private NameCompletionEdit EditBox { get; set; }

    public IProperty<bool> ContinueEnabled
    {
      get
      {
        return OverloadsPage.ContinueEnabled;
      }
    }

    public string Description
    {
      get
      {
        return OverloadsPage.Description;
      }
    }

    public string Title
    {
      get
      {
        return OverloadsPage.Title;
      }
    }

    public EitherControl View
    {
      get
      {
        ShowSuggests();
        return OverloadsPage.View;
      }
    }

    public bool DoNotShow
    {
      get
      {
        return OverloadsPage.DoNotShow;
      }
    }

    public RenameOverloadsPageDecorator(RenameOverloadsPage overloadsPage)
    {
      OverloadsPage = overloadsPage;
    }

    public IRefactoringPage Commit(IProgressIndicator pi)
    {
      var irefactoringPage = OverloadsPage.Commit(pi);
      if (irefactoringPage is RenameInitialControl)
      {
        var renameInitialControl = irefactoringPage as RenameInitialControl;
        var field = typeof (RenameInitialControl).GetField("myEditboxName", BindingFlags.Instance | BindingFlags.NonPublic);
        if (field != null)
          EditBox = field.GetValue(renameInitialControl) as NameCompletionEdit;
      }
      return irefactoringPage;
    }

    public bool Initialize(IProgressIndicator pi)
    {
      return OverloadsPage.Initialize(pi);
    }

    public bool RefreshContents(IProgressIndicator pi)
    {
      return OverloadsPage.RefreshContents(pi);
    }

    private void ShowSuggests()
    {
      if (EditBox == null)
        return;
      var method = typeof (CompletionPickerEdit).GetMethod("CompletionListShow", BindingFlags.Instance | BindingFlags.NonPublic);
      var nameCompletionEdit = EditBox;
      method.Invoke(nameCompletionEdit,new[]{ (object)CompletionPickerEdit.CompletionListShowModeTransition.Soft});
    }
  }}