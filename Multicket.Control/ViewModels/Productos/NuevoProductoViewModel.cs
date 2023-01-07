using Multicket.Control.Mvvm;
using Multicket.Data.Models;
using Multicket.Data.Models;
using Multicket.Module.Commands;
using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using NHibernate.Validator.Constraints;
using Prism.Regions;
using System.Collections.Generic;
using System.Linq;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = true)]
    public class NuevoProductoViewModel : Bind, INavigationAware
    {
        private TipoVenta tventa;
        private Producto producto;
        private Inventario inventario;
        private Departamento departamento;
        private readonly IManagerService src;


        public NuevoProductoViewModel(IManagerService service)
        {
            src = service;
            Initialization();
        }

        public NuevoProductoViewModel() { }

        public TipoVenta SelectedTipoVentaItem { get; set; }
        public Producto Producto { get; set; }
        public Departamento SelectedDepartamentoItem { get; set; }
        public ISet<TipoVenta> TipoVentaItems { get; set; }
        public ISet<Departamento> DepartamentoItems { get; set; }

        public IApplicationCommands Cmd
        {
            get { return Get<IApplicationCommands>(); }
            set { Set(value); }
        }

        public RelayCommand GuardarCommand => new RelayCommand(action: OnGuardar);
        public RelayCommand CancelarCommand => new RelayCommand(action: (e) => { });
        public RelayCommand PrecioVentaChangedCommand => new RelayCommand(action: (e) => { });
        public RelayCommand GananciaChangedCommand => new RelayCommand(action: OnGananciaChanged);

        private void OnGuardar(object sender)
        {
            if (IsUnidad) SelectedTipoVentaItem = TipoVentaItems.Single((tv) => tv.VentaType == VentaType.Pieza);
            if (IsPaquete) SelectedTipoVentaItem = TipoVentaItems.Single((tv) => tv.VentaType == VentaType.Paquete);
            if (IsAgranel) SelectedTipoVentaItem = TipoVentaItems.Single((tv) => tv.VentaType == VentaType.Agranel);

            var paquete = sender as Paquete;

            var producto = new Producto
            {
                Codigo = Codigo,
                Descripcion = Descripcion,
                PrecioCosto = PrecioCosto,
                PrecioVenta = PrecioVenta,
                PrecioMayoreo = PrecioMayoreo,
                Ganancia = Ganancia,
            };

            var inventario = new Inventario
            {
                NumeroMaximo = Maximo,
                NumeroMinimo = Minimo,
                NumeroProductos = PExistentes
            };

            SelectedDepartamentoItem.Add(producto);
            SelectedTipoVentaItem.Add(producto);
            producto.Add(paquete);
            paquete.Add(producto);
            producto.OnVeryfi();

            producto.Save();
            paquete.Save();

            if (EnInventario)
            {
                producto.Add(inventario);
                inventario.Save();
            }


            Clear();
            return;
        }

        private void OnGananciaChanged(object sender)
        {
        }

        private void Initialization()
        {
            IsUnidad = true;
            EnInventario = false;
            PExistentes = default;
            Minimo = default;
            Maximo = default;
            Codigo = default;
            Ganancia = default;
            Descripcion = default;
            PrecioCosto = default;
            PrecioVenta = default;
            PrecioMayoreo = default;
            Cmd = src.cmd;
            TipoVentaItems = src.data.Find<TipoVenta>();
            DepartamentoItems = src.data.Find<Departamento>();
            src.cmd.GuardarProductoCommand.RegisterCommand(GuardarCommand);
        }

        private void Clear()
        {
            Codigo = default;
            DepNombre = default;
            Descripcion = default;
            Minimo = default;
            Maximo = default;
            PExistentes = default;
            Ganancia = default;
            PrecioCosto = default;
            PrecioVenta = default;
            PrecioMayoreo = default;
            IsUnidad = true;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            producto = (Producto)navigationContext.Parameters["SelectrdProductoItem"];

            if (producto is null) return;

            tventa = producto.TipoVenta;
            inventario = producto.Inventario;
            departamento = producto.Departamento;

            Codigo = producto.Codigo;
            Ganancia = producto.Ganancia;
            Descripcion = producto.Descripcion;
            PrecioCosto = producto.PrecioCosto;
            PrecioVenta = producto.PrecioVenta;
            PrecioMayoreo = producto.PrecioMayoreo;
            DepNombre = departamento.Nombre;
            EnInventario = !(producto.Inventario is null);
            Minimo = inventario.NumeroMinimo;
            Maximo = inventario.NumeroMaximo;
            PExistentes = inventario.NumeroProductos;
            IsAgranel = tventa.VentaType.Equals(VentaType.Agranel);
            IsPaquete = tventa.VentaType.Equals(VentaType.Paquete);
            IsUnidad = tventa.VentaType.Equals(VentaType.Pieza);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("SelectedProductoItem"))
            {
                Producto producto = (Producto)navigationContext.Parameters["SelectedProductoItem"];
                if (producto is null)
                {
                    return Producto is null && Producto.Descripcion.Equals(producto.Descripcion);
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

        private void Navigate(string content, string view)
        {
            src.region.RequestNavigate(content, view);
        }

        public int PExistentes
        {
            get => Get<int>();
            set => Set(value);
        }

        public int Minimo
        {
            get => Get<int>();
            set => Set(value);
        }

        public int Maximo
        {
            get => Get<int>();
            set => Set(value);
        }

        [IsNumeric, NotNullNotEmpty]
        public string Codigo
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Descripcion
        {
            get => Get<string>();
            set => Set(value);
        }

        public decimal PrecioCosto
        {
            get => Get<decimal>();
            set => Set(value);
        }

        public decimal PrecioVenta
        {
            get => Get<decimal>();
            set => Set(value);
        }

        public decimal PrecioMayoreo
        {
            get => Get<decimal>();
            set => Set(value);
        }

        public int Ganancia
        {
            get => Get<int>();
            set => Set(value);
        }

        public bool EnInventario
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsPaquete
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsAgranel
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsUnidad
        {
            get => Get<bool>();
            set => Set(value);
        }

        public string DepNombre
        {
            get => Get<string>();
            set => Set(value);
        }
    }
}
