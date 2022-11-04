using Multicket.Data.Models;
using Multicket.Module.Commands;
using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;

namespace Multicket.Module.ViewModels
{
    public class ContenidoPaqueteViewModel : BindableBase, IDialogAware
    {
        private ISet<Paquete> _paqueteitems;
        private IManagerService src;
        private string _descripcion;
        private string _codigo;
        private decimal _precioventa;
        private int _cantidad;

        public ContenidoPaqueteViewModel(IManagerService service)
        {
            src = service;
            OnInitialization();
        }
        public Producto Producto { get; set; }

        public IApplicationCommands Cmd { get; set; }
        public RelayCommand BuscarCommand => new RelayCommand(OnBuscarProducto);
        public RelayCommand AgregarCommand => new RelayCommand(OnAgregar);

        private void OnAgregar(object obj)
        {
            var paquete = new Paquete();
            paquete.Cantidad = Cantidad;
            paquete.Descripcion = Producto.Descripcion;

            //Cmd.GuardarProductoCommand.Execute(paquete);
        }

        private void OnBuscarProducto(object obj)
        {
            //Producto = src.data.Find<Producto>("Codigo", Codigo);
            if (Producto == null)
            {
                return;
            }

            Codigo = Producto.Codigo;
            Descripcion = Producto.Descripcion;
            PrecioVenta = Producto.PrecioVenta;
        }

        private void OnInitialization()
        {
            OnClear();
            Cmd = src.cmd;
            //Cmd.GuardarProductoCommand.Execute(new Paquete { Codigo = "332323323" });
        }

        private void OnClear()
        {
            Codigo = default;
            Descripcion = default;
            PrecioVenta = default;
        }

        public string Title => "";

        public event Action<IDialogResult> RequestClose;

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

        public ISet<Paquete> PaqueteItems
        {
            get => _paqueteitems;
            set => SetProperty(ref _paqueteitems, value);
        }

        public string Codigo
        {
            get => _codigo;
            set => SetProperty(ref _codigo, value);
        }

        public string Descripcion
        {
            get => _descripcion;
            set => SetProperty(ref _descripcion, value);
        }

        public decimal PrecioVenta
        {
            get => _precioventa;
            set => SetProperty(ref _precioventa, value);
        }

        public int Cantidad
        {
            get => _cantidad;
            set => SetProperty(ref _cantidad, value);
        }
    }
}
