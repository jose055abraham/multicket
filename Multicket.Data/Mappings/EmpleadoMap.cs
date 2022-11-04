using FluentNHibernate.Mapping;
using Multicket.Data.Models;

namespace Multicket.Data.Mappings
{
    public class EmpleadoMap : ClassMap<Empleado>
    {
        public EmpleadoMap()
        {
            Table("Empleados");
            Id((e) => e.Id).Unique().Length(36).GeneratedBy.Guid();
            Map((e) => e.Nombre);
            Map((e) => e.Apellidos);
            Map((e) => e.Telefono);
            Map((e) => e.Correo);
            Map((e) => e.Created_At);
            Map((e) => e.Updated_At);
            References((e) => e.Usuario).Column("Usuario_Id");
            References((e) => e.Caja).Column("Caja_Id");
            HasMany((e) => e.Venta).Inverse().Cascade.All();
            HasMany((e) => e.Ingreso).Inverse().Cascade.All();
            HasMany((e) => e.Egreso).Inverse().Cascade.All();
            HasMany((e) => e.Abono).Inverse().Cascade.All();
        }
    }
}
