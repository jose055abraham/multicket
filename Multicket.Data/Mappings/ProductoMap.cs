using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class ProductoMap : ClassMap<Producto>
    {
        public ProductoMap()
        {
            Table("Productos");
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.Codigo).Nullable();
            Map((e) => e.Descripcion).Nullable();
            Map((e) => e.PrecioCosto).Not.Nullable();
            Map((e) => e.PrecioVenta).Not.Nullable();
            Map((e) => e.PrecioMayoreo).Not.Nullable();
            Map((e) => e.Ganancia).Not.Nullable();
            Map((e) => e.Favorito).Nullable();
            Map((e) => e.Created_At).Nullable();
            Map((e) => e.Updated_At).Nullable();
            References((e) => e.TipoVenta).Column("TipoVenta_Id").Not.Nullable();
            References((e) => e.Proveedor).Column("Proveedor_Id").Nullable();
            References((e) => e.Departamento).Column("Departamento_Id").Nullable();
            HasOne((e) => e.Inventario).PropertyRef((e) => e.Producto).Cascade.All();
            HasMany((e) => e.DetalleVenta).Inverse().Cascade.All();
            HasManyToMany((e) => e.Paquete)
                .Cascade.All().Not.LazyLoad()
                .Table("Paquete_Has_Productos");


        }
    }
}
