using Multicket.Data.Models;
using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Multicket.Module.ViewModels
{
    public class BusquedaProductoViewModel : BindableBase, IDialogAware
    {
        private bool _focus;
        public string Title => "Buscar...";
        private readonly IManagerService src;
        private NavigationParameters Parameters;
        public event Action<IDialogResult> RequestClose;


        public BusquedaProductoViewModel(IManagerService service)
        {
            src = service;
            Initialization();
        }

        public string Buscar { get; set; }
        public ISet<Producto> ProductoItems { get; set; }
        public int SelectedProductoIndex { get; set; }
        public Producto SelectedProductoItem { get; set; }

        public RelayCommand BuscarChangedCommand => new RelayCommand(OnBuscarChanged);
        public RelayCommand AgregarProductoCommand => new RelayCommand(OnAgregarProducto);

        private void OnAgregarProducto(object sender)
        {
            if (SelectedProductoItem is null) return;
            Parameters.Add("ProductoID", SelectedProductoItem.Id);
            RaiseRequestClose(new DialogResult());
            OnNavigate("MainContent", "Ventas", Parameters);
        }

        private void OnBuscarChanged(object sender)
        {
            ProductoItems = src.data.Query<Producto>((e) => e.Descripcion == Buscar).ToHashSet();
            SelectedProductoIndex = 0;
        }

        private void Initialization()
        {
            SelectedProductoIndex = -1;
            ProductoItems = new HashSet<Producto>();
            Parameters = new NavigationParameters();
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

        }

        private void OnNavigate(string content, string view, NavigationParameters parasm)
        {
            src.region.RequestNavigate(content, view, parasm);
        }

        public bool Focus
        {
            get => _focus;
            set => SetProperty(ref _focus, value);
        }
    }
}
