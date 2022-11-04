using Multicket.Data.Models;
using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.Generic;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = true)]
    public class VentasViewModel : BindableBase, INavigationAware
    {
        private string _codigo;
        private int _numproductos;
        private decimal _importetotal;
        private readonly IManagerService src;

        public VentasViewModel(IManagerService service)
        {
            src = service;
            OnInitialization();
        }

        public ISet<Venta> VentaItems
        {
            get => src.data.Find<Venta>();
        }

        public RelayCommand CodigoChangedCommand => new RelayCommand(action: (e) => { });
        public RelayCommand SalidasCommand => new RelayCommand(action: OnSalidas);
        public RelayCommand EntradasCommand => new RelayCommand(action: OnEntradas);
        public RelayCommand VentasDelDiaCommand => new RelayCommand(action: OnVentasDelDia);
        public RelayCommand BuscarProductoCommand => new RelayCommand(action: OnBuscarProducto);
        public RelayCommand ProductosVariosCommand => new RelayCommand(action: OnProductosVarios);
        public RelayCommand AgregarCommand => new RelayCommand(action: OnCompraVenta);

        private void OnSalidas(object sender) => Dialog("Salidas");
        private void OnEntradas(object sender) => Dialog("Entradas");
        private void OnVentasDelDia(object sender) => Dialog("VentasDelDia");
        private void OnBuscarProducto(object sender) => Dialog("BusquedaProducto");
        private void OnProductosVarios(object sender) => Dialog("ProductosVarios");

        private void OnInitialization()
        {
            Codigo = default;
        }

        private void OnCompraVenta(object sender)
        {
            if (sender is null || sender as string == "") return;

            string codigo = (string)sender;
            //Producto producto = src.data.Find<Producto>("Codigo", codigo);

            //if (!(producto is null))
            //{
            //    var venta = new Venta
            //    {

            //    };

            //    var detalle = new DetalleVenta
            //    {

            //    };

            //    venta.add(detalle);
            //}



            //if (!(Tickets is null))
            //{
            //    OnSumImporte();
            //    OnClear();
            //}
        }

        private void OnClear()
        {
            Codigo = default;

        }

        private void OnSumImporte()
        {
            //ImporteTotal = Tickets.Sum((e) => e.Total);
            //NumProductos = Tickets.Sum((e) => e.Cantidad);
        }

        private void Navigate(string content, string view)
        {
            src.region.RequestNavigate(content, view);
        }

        private void Dialog(string key, string message = "")
        {
            src.dialog.ShowDialog(key, new DialogParameters($"message={message}"), null);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            OnCompraVenta((string)navigationContext.Parameters["ProductoID"]);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("ProductoID"))
            {
                return !((string)navigationContext.Parameters["ProductoID"] is null);
            }
            else
            {
                return false;
            }
        }

        public void OnNavigatedFrom(NavigationContext navigationContext) { }

        public string Codigo
        {
            get { return _codigo; }
            set { SetProperty(ref _codigo, value); }
        }

        public decimal ImporteTotal
        {
            get { return _importetotal; }
            set { SetProperty(ref _importetotal, value); }
        }

        public int NumProductos
        {
            get { return _numproductos; }
            set { SetProperty(ref _numproductos, value); }
        }
    }
}
