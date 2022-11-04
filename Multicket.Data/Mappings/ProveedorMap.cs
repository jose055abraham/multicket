using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class ProveedorMap : ClassMap<Proveedor>
    {
        public ProveedorMap()
        {
            Table("Proveedores");
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.Nombre).Not.Nullable();
            Map((e) => e.Representante).Nullable();
            Map((e) => e.Correo).Nullable();
            Map((e) => e.Telefono).Nullable();
            Map((e) => e.DatosDireccion).Nullable();
            Map((e) => e.Created_At).Nullable();
            Map((e) => e.Updated_At).Nullable();
            HasMany((e) => e.Producto).Inverse().Cascade.All();
        }
    }
}
