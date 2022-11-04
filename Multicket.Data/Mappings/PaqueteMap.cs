using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class PaqueteMap : ClassMap<Paquete>
    {
        public PaqueteMap()
        {
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.Descripcion).Not.Nullable();
            Map((e) => e.Cantidad).Not.Nullable();
            References((e) => e.TipoVenta).Column("TipoVenta_Id");
            HasManyToMany((e) => e.Producto)
                .Cascade.All()
                .Inverse().Not.LazyLoad()
                .Table("Paquete_Has_Productos");
        }
    }
}
