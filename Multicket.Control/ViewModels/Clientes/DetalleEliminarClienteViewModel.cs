using Multicket.Data.Models;
using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class DetalleEliminarClienteViewModel : BindableBase, INavigationAware
    {
        private Guid _id;
        private string _nombre;
        private string _telefono;
        private string _direccion;
        private decimal? _lcredito;
        private readonly IManagerService src;

        public DetalleEliminarClienteViewModel(IManagerService service)
        {
            src = service;
        }

        public Cliente SelectedClienteItem { get; set; }

        public RelayCommand CancelarCommand => new RelayCommand(action: OnCancelar);
        public RelayCommand EliminarCommand => new RelayCommand(action: OnEliminar);

        private void OnEliminar(object sender)
        {
            if (SelectedClienteItem is null) return;

            src.dialog.ShowDialog("Warning", 
                parameters: new DialogParameters
                {
                    { "message", "Esta seguro de eliminar la siguiente información?" },
                    { "title", "Advertencia" },
                    { "caption", "Eliminar registro" }
                }, callback: (action) =>
                {
                    if (action.Result == ButtonResult.OK)
                    {
                        src.data.Delete(SelectedClienteItem);
                        src.dialog.ShowDialog("NotificationSuccess", new DialogParameters
                        {
                            {"message","Registro eliminado con exito" }
                        }, null);
                    }
                    OnClear();
                    Navigate("Creditos", "EliminarCliente");
                });

        }


        private void OnCancelar(object sender)
        {
            Navigate("Creditos", "EliminarCliente");
            OnClear();
            return;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Cliente cliente = (Cliente)navigationContext.Parameters["SelectedClienteItem"];
            if (cliente is null) return;

            SelectedClienteItem = cliente;
            Id = cliente.Id;
            Nombre = cliente.Nombre;
            Telefono = cliente.Telefono;
            Direccion = cliente.Direccion.Domicilio1;
            LCredito = cliente.Credito.LCredito;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            Cliente cliente = navigationContext.Parameters["SelectedClienteItem"] as Cliente;
            if (cliente is null)
            {
                return SelectedClienteItem is null && SelectedClienteItem.Nombre.Equals(cliente.Nombre);
            }
            else
            {
                return false;
            }

        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private void OnClear()
        {
            Id = default;
            Nombre = default;
            Direccion = default;
            Telefono = default;
            LCredito = default;
        }

        private void Navigate(string content, string view)
        {
            src.region.RequestNavigate(content, view);
        }

        public string Nombre
        {
            get { return _nombre; }
            set { SetProperty(ref _nombre, value); }
        }

        public string Direccion
        {
            get { return _direccion; }
            set { SetProperty(ref _direccion, value); }
        }

        public string Telefono
        {
            get { return _telefono; }
            set { SetProperty(ref _telefono, value); }
        }

        public decimal? LCredito
        {
            get { return _lcredito; }
            set { SetProperty(ref _lcredito, value); }
        }

        public Guid Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
    }
}
