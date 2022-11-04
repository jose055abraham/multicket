using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class DetalleCorteCajaMap : ClassMap<DetalleCorteCaja>
    {
        public DetalleCorteCajaMap()
        {
            Table("Detalle_Cote_Caja");
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.Ingresos).Not.Nullable();
            Map((e) => e.Egresos).Not.Nullable();
            Map((e) => e.Ganancia).Not.Nullable();
            Map((e) => e.Diferencia).Not.Nullable();
            Map((e) => e.MontoTotal).Not.Nullable();
            Map((e) => e.Created_At).Nullable();
            Map((e) => e.Updated_At).Nullable();
            References((e) => e.Caja).Column("Caja_Id");
        }
    }
}
