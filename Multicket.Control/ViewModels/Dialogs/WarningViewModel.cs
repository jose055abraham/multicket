using Multicket.Module.Mvvm;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace Multicket.Module.ViewModels
{
    /// <summary>
    /// <para>parameters { title , message, caption}</para>
    /// </summary>
    public class WarningViewModel : BindableBase, IDialogAware
    {
        private string _title;
        private string _message;
        private string _caption;

        public RelayCommand AceptarCancelarCommand => new RelayCommand((e) => CloseDialog(e.ToString()));

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result;
            if (parameter?.ToLower() == "true")
            {
                result = ButtonResult.OK;
                RaiseRequestClose(new DialogResult(result));
            }
            else if (parameter?.ToLower() == "false")
            {
                result = ButtonResult.Cancel;
                RaiseRequestClose(new DialogResult(result));
            }
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>("message");
            Title = parameters.GetValue<string>("title");
            Caption = parameters.GetValue<string>("caption");
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string Caption
        {
            get { return _caption; }
            set { SetProperty(ref _caption, value); }
        }
    }
}
