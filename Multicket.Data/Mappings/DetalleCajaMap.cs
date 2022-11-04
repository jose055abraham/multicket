using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class DetalleCajaMap : ClassMap<DetalleCaja>
    {
        public DetalleCajaMap()
        {
            Table("Detalle_Caja");
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.MontoInicial);
            Map((e) => e.MontoCierre);
            Map((e) => e.Estado);
            Map((e) => e.Apertura_At).Not.Nullable();
            Map((e) => e.Cierre_At).Not.Nullable();
            References((e) => e.Caja).Column("Caja_Id");
        }
    }
}
