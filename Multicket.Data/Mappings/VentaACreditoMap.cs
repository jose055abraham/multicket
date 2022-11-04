using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class VentaACreditoMap : ClassMap<VentaACredito>
    {
        public VentaACreditoMap()
        {
            Table("Venta_A_Credito");
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.Nota).Nullable();
            Map((e) => e.Estado).Nullable();
            Map((e) => e.Created_At).Nullable();
            Map((e) => e.Updated_At).Nullable();
            References((e) => e.Credito).Column("Credito_Id");
            References((e) => e.Venta).Column("Venta_Id");
            HasMany((e) => e.Abono).Inverse().Cascade.All();
        }
    }
}
