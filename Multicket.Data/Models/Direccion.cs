using System;

namespace Multicket.Data.Models
{
    public partial class Direccion
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual string Domicilio1 { get; set; }
        public virtual string Domicilio2 { get; set; }
        public virtual string Colonia { get; set; }
        public virtual string Municipio { get; set; }
        public virtual string Estado { get; set; }
        public virtual string CodigoPostal { get; set; }
        public virtual string Nota { get; set; }
        public virtual Cliente Cliente { get; set; }

        public override string ToString()
        {
            return string.Format(
                "{0} {1} {2} {3} {4} {5} {6} {7}",
                Id, Domicilio1, Domicilio2, Colonia, Municipio, Estado, CodigoPostal, Nota
                );
        }

        public Direccion()
        {
            OnCreated();
        }
    }
}
