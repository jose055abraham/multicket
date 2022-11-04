using Multicket.Data.Models;
using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using Prism.Regions;
using System.Collections.Generic;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class ModificarProductoViewModel
    {
        private NavigationParameters Parameters;
        private readonly IManagerService manager;

        public ModificarProductoViewModel(IManagerService manager)
        {
            this.manager = manager;
            Initialization();
        }

        public Producto SelectedProductoItem { get; set; }
        public ISet<Producto> ProductoItems { get; set; }

        public RelayCommand AceptarCommand => new RelayCommand(action: OnAceptar);

        private void OnAceptar(object sender)
        {
            if (SelectedProductoItem is null) return;
            Parameters.Add(nameof(SelectedProductoItem), SelectedProductoItem);
            Navigate("Productos", "NuevoProducto", Parameters);
            return;
        }

        private void Initialization()
        {
            Parameters = new NavigationParameters();
            ProductoItems = new HashSet<Producto>();
            SelectedProductoItem = new Producto();
            ProductoItems = manager.data.Find<Producto>();
        }

        private void Navigate(string content, string view, NavigationParameters parasm)
        {
            manager.region.RequestNavigate(content, view, parasm);
        }
    }
}
