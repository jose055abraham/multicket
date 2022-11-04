using Multicket.Data.Models;
using System.Collections.ObjectModel;

namespace Multicket.Data
{
    ///// <summary>
    ///// Colección de <see cref="Usuario"/>
    ///// </summary>
    //public class CUsuarios : ObservableCollection<Usuario> { }
    ///// <summary>
    ///// Colección de <see cref="Empleado"/>
    ///// </summary>
    //public class CEmpleados : ObservableCollection<Empleado> { }
    ///// <summary>
    ///// Colección de <see cref="Caja"/>
    ///// </summary>
    //public class CCajas : ObservableCollection<Caja> { }
    ///// <summary>
    ///// Colección de <see cref="Cliente"/>
    ///// </summary>
    //public class CClientes : ObservableCollection<Cliente> { }
    ///// <summary>
    ///// Colección de <see cref="Departamento"/>
    ///// </summary>
    //public class CDepartamentos : ObservableCollection<Departamento> { }
    ///// <summary>
    ///// Colección de <see cref="DetalleCaja"/>
    ///// </summary>
    //public class CDetalleCajas : ObservableCollection<DetalleCaja> { }
    ///// <summary>
    ///// Colección de <see cref="DetalleVenta"/>
    ///// </summary>
    //public class CDetalleVentas : ObservableCollection<DetalleVenta> { }
    ///// <summary>
    ///// Colección de <see cref="UsuarioPermisos"/>
    ///// </summary>
    //public class CEmpleadoPermisos : ObservableCollection<UsuarioPermisos> { }
    ///// <summary>
    ///// Colección de <see cref="Permiso"/>
    ///// </summary>
    //public class CPermisos : ObservableCollection<Permiso> { }
    ///// <summary>
    ///// Colección de <see cref="Producto"/>
    ///// </summary>
    //public class CProductos : ObservableCollection<Producto> { }
    ///// <summary>
    ///// Colección de <see cref="Proveedor"/>
    ///// </summary>
    //public class CProveedores : ObservableCollection<Proveedor> { }
    ///// <summary>
    ///// Colección de <see cref="Venta"/>
    ///// </summary>
    //public class CVentas : ObservableCollection<Venta> { }
    ///// <summary>
    ///// Colección de <see cref="VentaACredito"/>
    ///// </summary>
    //public class CVentasACredito : ObservableCollection<VentaACredito> { }
    ///// <summary>
    ///// Colección de <see cref="Abono"/>
    ///// </summary>
    //public class CAbonos : ObservableCollection<Abono> { }
    ///// <summary>
    ///// Colección de nombres de <see cref="Usuario"/>
    ///// </summary>
    //public class CNUsuarios : ObservableCollection<Usuario> { }
    ///// <summary>
    ///// Colección de Productos en Venta <see cref="Orden"/>
    ///// </summary>
    ////public class CCVentas: ObservableCollection<CompraVenta> { }
    //public class CTVentas : ObservableCollection<TipoVenta> { }

    public class CTCreditos : ObservableCollection<TCredito>
    {
        public CTCreditos()
        {
            Items.Add(new TCredito { Tag = "Ilimitado" });
            Items.Add(new TCredito { Tag = "De:" });
        }
    }
}