using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class IngresoMap : ClassMap<Ingreso>
    {
        public IngresoMap()
        {
            Table("Ingresos");
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.Importe).Nullable();
            Map((e) => e.Nota).Nullable();
            Map((e) => e.Created_At).Nullable();
            Map((e) => e.Updated_At).Nullable();
            References((e) => e.Empleado);
        }
    }
}
