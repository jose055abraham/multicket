using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class EgresoMap : ClassMap<Egreso>
    {
        public EgresoMap()
        {
            Table("Egresos");
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.Importe).Not.Nullable();
            Map((e) => e.Nota).Nullable();
            Map((e) => e.Created_At).Nullable();
            Map((e) => e.Updated_At).Nullable();
            References((e) => e.Empleado);
        }
    }
}
