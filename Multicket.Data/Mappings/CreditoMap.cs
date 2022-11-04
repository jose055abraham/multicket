using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class CreditoMap : ClassMap<Credito>
    {
        public CreditoMap()
        {
            Table("Creditos");
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.LCredito).Not.Nullable();
            Map((e) => e.Created_At).Nullable();
            Map((e) => e.Updated_At).Nullable();
            References((e) => e.Cliente);
            HasMany((e) => e.VentaACredito).Inverse().Cascade.All();

        }
    }
}
