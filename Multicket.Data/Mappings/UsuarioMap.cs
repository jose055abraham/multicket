using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Table("Usuarios");
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.Nombre).Not.Nullable();
            Map((e) => e.Password).Not.Nullable();
            HasManyToMany((e) => e.Permiso)
                .Cascade.All().Not.LazyLoad()
                .Table("Usuario_Has_Permisos");
        }
    }
}
