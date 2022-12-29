using Multicket.Data.Enum;
using Multicket.Data.Models;
using Multicket.Data.Validation;
using Multicket.Module.Commands;
using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using NHibernate.Validator.Constraints;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = true)]
    public class NuevoProductoViewModel : ValidatorBase, INavigationAware
    {
        private Guid _productoid;
        private Guid _proveedorid;
        private Guid _tipoventaid;
        private Guid _inventarioid;
        private int _pexistentes;
        private int _ganancia;
        private int _minimo;
        private int _maximo;
        private bool _isunidad;
        private bool _isagranel;
        private bool _eninventario;
        private bool _ispaquete;
        private string _depnombre;
        private string _descripcion;
        private string _codigo;
        private decimal _preciocosto;
        private decimal _precioventa;
        private decimal _preciomayoreo;
        private IApplicationCommands _cmd;
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
            get { return _cmd; }
            set { SetProperty(ref _cmd, value); }
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
                Id = ProductoId,
                Codigo = Codigo,
                Descripcion = Descripcion,
                PrecioCosto = PrecioCosto,
                PrecioVenta = PrecioVenta,
                PrecioMayoreo = PrecioMayoreo,
                Ganancia = Ganancia,
            };

            var inventario = new Inventario
            {
                Id = InventarioId,
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
            ProductoId = default;
            ProveedorId = default;
            TipoVentaId = default;
            InventarioId = default;
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
            ProductoId = default;
            ProveedorId = default;
            TipoVentaId = default;
            InventarioId = default;
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
            Producto producto = (Producto)navigationContext.Parameters["SelectrdProductoItem"];
            if (producto is null) return;

            ProductoId = producto.Id;
            Codigo = producto.Codigo;
            Ganancia = producto.Ganancia;
            Descripcion = producto.Descripcion;
            PrecioCosto = producto.PrecioCosto;
            PrecioVenta = producto.PrecioVenta;
            PrecioMayoreo = producto.PrecioMayoreo;
            DepNombre = producto.Departamento.Nombre;
            EnInventario = !(producto.Inventario is null);
            Minimo = producto.Inventario.NumeroMinimo;
            Maximo = producto.Inventario.NumeroMaximo;
            PExistentes = producto.Inventario.NumeroProductos;
            IsAgranel = producto.TipoVenta.VentaType.Equals(VentaType.Agranel);
            IsPaquete = producto.TipoVenta.VentaType.Equals(VentaType.Paquete);
            IsUnidad = producto.TipoVenta.VentaType.Equals(VentaType.Pieza);
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
            get => _pexistentes;
            set => SetProperty(ref _pexistentes, value);
        }

        public int Minimo
        {
            get => _minimo;
            set => SetProperty(ref _minimo, value);
        }

        public int Maximo
        {
            get => _maximo;
            set => SetProperty(ref _maximo, value);
        }

        [IsNumeric, NotNullNotEmpty]
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

        public decimal PrecioCosto
        {
            get => _preciocosto;
            set => SetProperty(ref _preciocosto, value);
        }

        public decimal PrecioVenta
        {
            get => _precioventa;
            set => SetProperty(ref _precioventa, value);
        }

        public decimal PrecioMayoreo
        {
            get => _preciomayoreo;
            set => SetProperty(ref _preciomayoreo, value);
        }

        public int Ganancia
        {
            get => _ganancia;
            set => SetProperty(ref _ganancia, value);
        }

        public bool EnInventario
        {
            get => _eninventario;
            set => SetProperty(ref _eninventario, value);
        }

        public bool IsPaquete
        {
            get => _ispaquete;
            set => SetProperty(ref _ispaquete, value);
        }

        public bool IsAgranel
        {
            get => _isagranel;
            set => SetProperty(ref _isagranel, value);
        }

        public bool IsUnidad
        {
            get => _isunidad;
            set => SetProperty(ref _isunidad, value);
        }

        public string DepNombre
        {
            get => _depnombre;
            set => SetProperty(ref _depnombre, value);
        }

        public Guid ProductoId
        {
            get => _productoid;
            set => SetProperty(ref _productoid, value);
        }

        public Guid ProveedorId
        {
            get => _proveedorid;
            set => SetProperty(ref _proveedorid, value);
        }

        public Guid TipoVentaId
        {
            get => _tipoventaid;
            set => SetProperty(ref _tipoventaid, value);
        }

        public Guid InventarioId
        {
            get => _inventarioid;
            set => SetProperty(ref _inventarioid, value);
        }
    }
}
