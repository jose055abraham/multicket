using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class VentaMap : ClassMap<Venta>
    {
        public VentaMap()
        {
            Table("Ventas");
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.Importe).Not.Nullable();
            Map((e) => e.Impuesto).Not.Nullable();
            Map((e) => e.TipoComprobante).Nullable();
            Map((e) => e.NumComprobante).Nullable();
            Map((e) => e.SerieComprobante).Nullable();
            Map((e) => e.Estatus).Not.Nullable();
            Map((e) => e.FechaVenta).Not.Nullable();
            Map((e) => e.Created_At).Nullable();
            Map((e) => e.Updated_At).Nullable();
            HasMany((e) => e.DetalleVenta).Inverse().Cascade.All();
            HasMany((e) => e.VentaACredito).Inverse().Cascade.All();
            References((e) => e.Empleado)/*.Column("Empleado_Id")*/.Not.Nullable();
            References((e) => e.Cliente)/*.Column("Cliente_Id")*/.Nullable();
        }
    }
}
