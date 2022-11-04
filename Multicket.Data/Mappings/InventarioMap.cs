using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class InventarioMap : ClassMap<Inventario>
    {
        public InventarioMap()
        {
            Table("Inventarios");
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.NumeroProductos).Not.Nullable();
            Map((e) => e.NumeroMinimo).Not.Nullable();
            Map((e) => e.NumeroMaximo).Not.Nullable();
            References((e) => e.Producto);
        }
    }
}
