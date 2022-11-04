using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class CajaMap : ClassMap<Caja>
    {
        public CajaMap()
        {
            Table("Cajas");
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.Nombre);
            Map((e) => e.Serial);
            HasMany((e) => e.DetallesCaja).Inverse().Cascade.All();
            HasMany((e) => e.DetallesCorteCaja).Inverse().Cascade.All();
        }
    }
}
