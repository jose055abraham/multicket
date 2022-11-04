using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class DetalleVentaMap : ClassMap<DetalleVenta>
    {
        public DetalleVentaMap()
        {
            Table("Detalle_Venta");
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.Importe).Not.Nullable();
            Map((e) => e.Cantidad).Not.Nullable();
            Map((e) => e.PrecioUnitario).Not.Nullable();
            Map((e) => e.Descuento).Nullable();
            Map((e) => e.Cambio).Nullable();
            References((e) => e.Producto).Column("Producto_Id");
            References((e) => e.Venta).Column("Venta_Id");
        }
    }
}
