using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using Prism.Regions;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class CreditosViewModel
    {
        private readonly IManagerService src;

        public CreditosViewModel(IManagerService service)
        {
            src = service;
        }

        public RelayCommand NuevoCreditoCommand => new RelayCommand(action: OnNuevoCredito);
        public RelayCommand EstadoCuentaCommand => new RelayCommand(action: OnEstadoCuenta);
        public RelayCommand EliminarClienteCommand => new RelayCommand(action: OnEliminarCliente);
        public RelayCommand ModificarClienteCommand => new RelayCommand(action: OnModificarCliente);

        private void OnNuevoCredito(object sender) => OnNavigate("Creditos", "NuevoCredito");
        private void OnEstadoCuenta(object sender) => OnNavigate("Creditos", "EstadoCuenta");
        private void OnEliminarCliente(object sender) => OnNavigate("Creditos", "EliminarCliente");
        private void OnModificarCliente(object sender) => OnNavigate("Creditos", "ModificarCliente");

        private void OnNavigate(string content, string view)
        {
            src.region.RequestNavigate(content, view);
        }
    }
}
