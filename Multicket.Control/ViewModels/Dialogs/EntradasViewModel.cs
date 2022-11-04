using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows;

namespace Multicket.Module.ViewModels
{
    public class EntradasViewModel : BindableBase, IDialogAware
    {
        public event Action<IDialogResult> RequestClose;
        private Visibility _visibleStack;
        private Visibility _visible;
        private Visibility _collapse;
        private decimal _importe;
        private string _title = "";

        public EntradasViewModel()
        {
            VisibleStack = Visibility.Collapsed;
            Collapse = Visibility.Collapsed;
            Visible = Visibility.Visible;
        }

        #region Propiedades

        public decimal Importe
        {
            get => _importe;
            set
            {
                if (value.Equals(_importe))
                    return;
                _importe = value;
                RaisePropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                if (value.Equals(_title))
                    return;
                _title = value;
                RaisePropertyChanged();
            }
        }

        public Visibility VisibleStack
        {
            get => _visibleStack;
            set
            {
                if (value.Equals(_visibleStack))
                    return;
                _visibleStack = value;
                RaisePropertyChanged();
            }
        }

        public Visibility Visible
        {
            get => _visible;
            set
            {
                if (value.Equals(_visible))
                    return;
                _visible = value;
                RaisePropertyChanged();
            }
        }

        public Visibility Collapse
        {
            get => _collapse;
            set
            {
                if (value.Equals(_collapse))
                    return;
                _collapse = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands 

        public DelegateCommand VerEntradasCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    VisibleStack = VisibleStack.Equals(Visibility.Collapsed)
                            ? Visibility.Visible
                            : Visibility.Collapsed;
                    Visible = Visibility.Collapsed;
                    Collapse = Visibility.Visible;
                });
            }
        }

        public DelegateCommand OcultarEntradasCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    VisibleStack = VisibleStack.Equals(Visibility.Visible)
                            ? Visibility.Collapsed
                            : Visibility.Visible;
                    Visible = Visibility.Visible;
                    Collapse = Visibility.Collapsed;
                });
            }
        }

        #endregion

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
