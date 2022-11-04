using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows.Threading;

namespace Multicket.Module.ViewModels
{
    public class NotificationSuccessViewModel : BindableBase, IDialogAware
    {
        public event Action<IDialogResult> RequestClose;
        private DispatcherTimer _timer;
        private string _title;
        private string _message;

        public NotificationSuccessViewModel()
        {

        }

        public DelegateCommand Loaded
        {
            get
            {
                return new DelegateCommand(Close);
            }
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

        private DispatcherTimer Timer
        {
            get { return _timer; }
            set { SetProperty(ref _timer, value); }
        }

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
            Message = parameters.GetValue<string>("message").ToUpper();
        }

        private void Close()
        {
            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            Timer.Start();
            Timer.Tick += (s, o) =>
            {
                RaiseRequestClose(new DialogResult());
            };
        }
    }
}
