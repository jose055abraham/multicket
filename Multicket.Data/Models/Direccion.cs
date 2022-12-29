using Multicket.Data.Services;
using System;

namespace Multicket.Data.Models
{
    public partial class Direccion : Repository
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

        public virtual bool Save()
        {
            return Insert(this);
        }

        public Direccion()
        {
            OnCreated();
        }
    }
}
