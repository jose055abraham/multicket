using FluentNHibernate.Mapping;
using Multicket.Data.Enum;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class PermisoMap : ClassMap<Permiso>
    {
        public PermisoMap()
        {
            Table("Permisos");
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.PermisoType).CustomType<PermisoType>();
            HasManyToMany((e) => e.Usuario)
                .Cascade.All()
                .Inverse().Not.LazyLoad()
                .Table("Usuario_Has_Permisos");
        }
    }
}
