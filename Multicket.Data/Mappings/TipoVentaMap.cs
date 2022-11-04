using FluentNHibernate.Mapping;
using Multicket.Data.Enum;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class TipoVentaMap : ClassMap<TipoVenta>
    {
        public TipoVentaMap()
        {
            Table("Tipo_Venta");
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.VentaType).CustomType<VentaType>();
            HasMany((e) => e.Pieza).Inverse().Cascade.All();
            HasMany((e) => e.Producto).Inverse().Cascade.All();
            HasMany((e) => e.Paquete).Inverse().Cascade.All();
            HasMany((e) => e.Agranele).Inverse().Cascade.All();
        }
    }
}
