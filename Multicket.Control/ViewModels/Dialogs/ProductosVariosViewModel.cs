using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace Multicket.Module.ViewModels
{
    public class ProductosVariosViewModel : BindableBase, IDialogAware
    {
        public event Action<IDialogResult> RequestClose;


        public string Title { get; set; }

        #region Dialog

        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "true")
                result = ButtonResult.OK;
            else if (parameter?.ToLower() == "false")
                result = ButtonResult.Cancel;

            RaiseRequestClose(new DialogResult(result));
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }

        #endregion
    }
}
