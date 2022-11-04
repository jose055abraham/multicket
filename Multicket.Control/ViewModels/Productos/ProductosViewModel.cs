using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using Prism.Regions;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class ProductosViewModel
    {
        private readonly IManagerService src;

        public ProductosViewModel(IManagerService service)
        {
            src = service;
        }

        public RelayCommand NuevoProductoCommand
        {
            get
            {
                return new RelayCommand((e) =>
                {
                    Navigate("Productos", "TabControlProducto");
                  
                });
            }
        }

        public RelayCommand DepartamentosCommand
        {
            get
            {
                return new RelayCommand((e) =>
                {
                    Navigate("Productos", "Departamentos");
                   
                });
            }
        }

        public RelayCommand EliminarProductoCommand
        {
            get
            {
                return new RelayCommand((e) =>
                {
                    Navigate("Productos", "EliminarProducto");
                  
                });
            }
        }

        public RelayCommand DetalleEliminarProductoCommand
        {
            get
            {
                return new RelayCommand((e) =>
                {
                    Navigate("Productos", "DetalleEliminarProducto");
                   
                });
            }
        }

        public RelayCommand ModificarProductoCommand
        {
            get
            {
                return new RelayCommand((e) =>
                {
                    Navigate("Productos", "ModificarProducto");
              
                });
            }
        }

        public RelayCommand PromocionesCommand
        {
            get
            {
                return new RelayCommand((e) =>
                {
                    Navigate("Productos", "Promociones");
         

                });
            }
        }

        public RelayCommand VentasPorPeriodoCommand
        {
            get
            {
                return new RelayCommand((e) =>
                {
                    Navigate("Productos", "VentasPorPeriodo");

                });
            }
        }

        public RelayCommand HistorialComprasCommand
        {
            get
            {
                return new RelayCommand((e) =>
                {
                    Navigate("Productos", "HistorialCompras");

                });
            }
        }

        public RelayCommand CatalogoCommand
        {
            get
            {
                return new RelayCommand((e) =>
                {
                    Navigate("Productos", "Catalogo");
                    return;

                });
            }
        }

        private void Navigate(string content, string view)
        {
            src.region.RequestNavigate(content, view);
        }
    }
}
