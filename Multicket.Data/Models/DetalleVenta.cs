using System;

namespace Multicket.Data.Models
{
    public partial class DetalleVenta
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual int? Cantidad { get; set; }
        public virtual decimal? Importe { get; set; }
        public virtual decimal? Descuento { get; set; }
        public virtual decimal? Cambio { get; set; }
        public virtual decimal? PrecioUnitario { get; set; }
        public virtual Venta Venta { get; set; }
        public virtual Producto Producto { get; set; }

        public DetalleVenta()
        {
            OnCreated();
        }
    }
}
