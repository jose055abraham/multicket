using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class ClienteMap : ClassMap<Cliente>
    {
        public ClienteMap()
        {
            Table("Clientes");
            Id((e) => e.Id).Unique().GeneratedBy.Guid().Length(36);
            Map((e) => e.Folio).Not.Nullable();
            Map((e) => e.Nombre).Nullable();
            Map((e) => e.Apellidos).Nullable();
            Map((e) => e.Telefono).Nullable();
            Map((e) => e.Correo).Nullable();
            Map((e) => e.Avatar).Nullable();
            Map((e) => e.Fill).Nullable();
            Map((e) => e.Created_At).Nullable();
            Map((e) => e.Updated_At).Nullable();
            HasMany((e) => e.Venta).Inverse().Cascade.All();
            HasOne((e) => e.Credito).PropertyRef((e) => e.Cliente).Cascade.All();
            HasOne((e) => e.Direccion).PropertyRef((e) => e.Cliente).Cascade.All();
            //Join("Creditos",(join) =>
            //{
            //    join.KeyColumn("Cliente_id");
            //    join.HasOne((e) => e.Credito).PropertyRef((e) => e.Cliente);
            //});
        }
    }
}
