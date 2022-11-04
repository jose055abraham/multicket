using Multicket.Data.Models;
using Multicket.Data.Validation;
using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using NHibernate.Validator.Constraints;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class NuevoCreditoViewModel :  ValidatorBase, INavigationAware
    {
        private int _filio;
        private string _fill;
        private string _nombre;
        private decimal _credito;
        private string _telefono;
        private string _direccion;
        private Guid _clienteId;
        private Guid _creditoId;
        private Guid _direccionId;
        private readonly IManagerService src;

        public NuevoCreditoViewModel(IManagerService service)
        {
            src = service;
        }

        public Cliente Cliente { get; set; }

        public RelayCommand GuardarCommand => new RelayCommand(action: OnGuardar, CanSave);

        private bool CanSave(object obj)
        {
            return GetAllInvalidRules().Count == 0;
        }

        public RelayCommand CancelarClienteCommand => new RelayCommand(action: (e) => OnClear());

        private void OnGuardar(object args)
        {
            var cliente = new Cliente
            {
                Id = ClienteId,
                Nombre = Nombre,
                Telefono = Telefono,
                Folio = Folio,
                Fill = Fill
            };

            var direccion = new Direccion
            {
                Id = DireccionId,
                Domicilio1 = Direccion,
            };

            var credito = new Credito
            {
                Id = CreditoId,
                LCredito = LCredito
            };

            cliente.OnVeryfi();
            cliente.add(credito);
            cliente.add(direccion);
            src.data.Save(cliente);
            src.data.Save(credito);
            src.data.Save(direccion);
            Dialog("NotificationSuccess", "Datos actualizados");
            OnClear();
        }

        private void OnClear()
        {
            ClienteId = default;
            DireccionId = default;
            CreditoId = default;
            Nombre = default;
            Telefono = default;
            Direccion = default;
            LCredito = default;
            Folio = default;
            Fill = default;
            return;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Cliente cliente = (Cliente)navigationContext.Parameters["SelectedClienteItem"];
            if (cliente is null) return;

            ClienteId = cliente.Id;
            CreditoId = cliente.Credito.Id;
            DireccionId = cliente.Direccion.Id;
            Nombre = cliente.Nombre;
            Telefono = cliente.Telefono;
            Folio = cliente.Folio; 
            Fill = cliente.Fill;
            Direccion = cliente.Direccion.Domicilio1;
            LCredito = cliente.Credito.LCredito;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("SelectedClienteItem"))
            {
                Cliente cliente = navigationContext.Parameters["SelectedClienteItem"] as Cliente;
                if (cliente is null)
                {
                    return Cliente is null && Cliente.Nombre.Equals(cliente.Nombre);
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private void Dialog(string key, string message = "")
        {
            src.dialog.ShowDialog(key,
                new DialogParameters($"message={message}"), null);
        }

        [NotNullNotEmpty]
        public string Nombre
        {
            get => _nombre;
            set => SetProperty(ref _nombre, value);
        }

        [NotNullNotEmpty]
        public string Direccion
        {
            get => _direccion;
            set => SetProperty(ref _direccion, value);
        }
                
        [Length(10, 10)]
        public string Telefono
        {
            get => _telefono;
            set => SetProperty(ref _telefono, value);
        }
        
        public string Fill
        {
            get => _fill;
            set => SetProperty(ref _fill, value);
        }
        
        public decimal LCredito
        {
            get { return _credito; }
            set { SetProperty(ref _credito, value); }
        }
        
        public int Folio
        {
            get => _filio;
            set => SetProperty(ref _filio, value);
        }      

        public Guid ClienteId
        {
            get { return _clienteId; }
            set { SetProperty(ref _clienteId, value); }
        }

        public Guid DireccionId
        {
            get { return _direccionId; }
            set { SetProperty(ref _direccionId, value); }
        }

        public Guid CreditoId
        {
            get { return _creditoId; }
            set { SetProperty(ref _creditoId, value); }
        }

    }
}
