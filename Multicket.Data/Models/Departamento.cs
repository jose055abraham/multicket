using Multicket.Data.Services;
using System;
using System.Collections.Generic;

namespace Multicket.Data.Models
{
    public partial class Departamento : Repository
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual string Nombre { get; set; }
        public virtual DateTime? Created_At { get; set; }
        public virtual DateTime? Updated_At { get; set; }
        public virtual ISet<Producto> Producto { get; set; }

        public virtual void Add(Producto producto)
        {
            producto.Departamento = this;
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
            return Insert(this);
        }

        public Departamento()
        {
            Producto = new HashSet<Producto>();
            OnCreated();
        }
    }
}
