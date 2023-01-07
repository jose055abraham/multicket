using Multicket.Data.Services;
using System;
using System.Collections.Generic;

namespace Multicket.Data.Models
{
    public partial class Paquete : Repository
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual int? Cantidad { get; set; }
        public virtual TipoVenta TipoVenta { get; set; }
        public virtual ISet<Producto> Producto { get; set; }


        public virtual void Add(Producto producto)
        {
            Producto.Add(producto);
        }

        public virtual bool Save()
        {
            return Add(this);
        }

        public Paquete()
        {
            Producto = new HashSet<Producto>();
            OnCreated();
        }

        //public override string ToString()
        //{
        //    return Codigo;
        //}
    }
}
