using Multicket.Data.Models;
using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using Prism.Regions;
using System.ComponentModel;
using System.Windows.Data;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class EliminarClienteViewModel
    {
        private readonly IManagerService src;
        private NavigationParameters Parameters;

        public EliminarClienteViewModel(IManagerService service)
        {
            src = service;
            Initialization();
        }

        public ICollectionView Clientes { get; set; }
        public Cliente SelectedCliente { get; set; }
        public string Buscar { get; set; }

        public RelayCommand AceptarCommand => new RelayCommand(action: OnAceptar);
        public RelayCommand SearchChangedCommand => new RelayCommand(action: OnSearchChanged);

        private void OnSearchChanged(object sender)
        {
            Clientes.Refresh();
            return;
        }

        private void OnAceptar(object sender)
        {
            if (SelectedCliente is null) return;
            Parameters.Add(nameof(SelectedCliente), SelectedCliente);
            Navigate("Creditos", "DetalleEliminarCliente", Parameters);
            return;
        }

        private void Initialization()
        {
            Buscar = "";
            Parameters = new NavigationParameters();
            Clientes = new CollectionView(src.data.Find<Cliente>())
            {
                Filter = Filter
            };
        }

        private bool Filter(object obj)
        {
            if (obj is Cliente cli)
            {
                return cli.Nombre.ToUpper().ToLower().Contains(Buscar)
                    || cli.Folio.ToString().Contains(Buscar);
            }
            return false;
        }

        private void Navigate(string content, string view, NavigationParameters parasm)
        {
            src.region.RequestNavigate(content, view, parasm);
        }
    }
}
