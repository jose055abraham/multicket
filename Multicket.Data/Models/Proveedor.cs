using Multicket.Data.Services;
using System;
using System.Collections.Generic;

namespace Multicket.Data.Models
{
    public partial class Proveedor : Repository
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Representante { get; set; }
        public virtual string Correo { get; set; }
        public virtual string Telefono { get; set; }
        public virtual string DatosDireccion { get; set; }
        public virtual DateTime? Created_At { get; set; }
        public virtual DateTime? Updated_At { get; set; }
        public virtual ISet<Producto> Producto { get; set; }

        public virtual void add(Producto producto)
        {
            producto.Proveedor = this;
        }

        public virtual void OnVeryfi()
        {
            if (Id == Guid.Empty)
            {
                Created_At = DateTime.Now;
            }
            else
            {
                Updated_At = DateTime.Now;
            }
        }

        public virtual bool Save()
        {
            return Add(this);
        }

        public Proveedor()
        {
            Producto = new HashSet<Producto>();
            OnVeryfi();
            OnCreated();
        }
    }
}
