using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class PiezaMap : ClassMap<Pieza>
    {
        public PiezaMap()
        {
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.StockActual).Not.Nullable();
            Map((e) => e.StockMinimo).Not.Nullable();
            Map((e) => e.StockMaximo).Not.Nullable();
            References((e) => e.TipoVenta).Column("TipoVenta_Id");
        }
    }
}
