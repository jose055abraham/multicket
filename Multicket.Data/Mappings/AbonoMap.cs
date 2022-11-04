using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class AbonoMap : ClassMap<Abono>
    {
        public AbonoMap()
        {
            Table("Abonos");
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.Importe).Nullable();
            Map((e) => e.Nota).Nullable();
            Map((e) => e.Created_At).Nullable();
            Map((e) => e.Updated_At).Nullable();
            References((e) => e.Empleado).Column("Empleado_Id");
            References((e) => e.VentaACredito).Column("Venta_A_Credito_Id");

        }
    }
}
