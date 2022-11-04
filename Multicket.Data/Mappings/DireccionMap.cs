using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class DireccionMap : ClassMap<Direccion>
    {
        public DireccionMap()
        {
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.Domicilio1).Nullable();
            Map((e) => e.Domicilio2).Nullable();
            Map((e) => e.Colonia).Nullable();
            Map((e) => e.Municipio).Nullable();
            Map((e) => e.Estado).Nullable();
            Map((e) => e.CodigoPostal).Nullable();
            Map((e) => e.Nota).Nullable();
            References((e) => e.Cliente);
        }
    }
}
