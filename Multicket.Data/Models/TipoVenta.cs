using Multicket.Data.Enum;
using Multicket.Data.Services;
using System;
using System.Collections.Generic;

namespace Multicket.Data.Models
{
    public partial class TipoVenta : Repository
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual VentaType VentaType { get; set; }
        public virtual ISet<Producto> Producto { get; set; }
        public virtual ISet<Agranel> Agranele { get; set; }
        public virtual ISet<Paquete> Paquete { get; set; }
        public virtual ISet<Pieza> Pieza { get; set; }


        public virtual void Add(Producto producto)
        {
            producto.TipoVenta = this;
        }

        public virtual void Add(Agranel agranel)
        {
            agranel.TipoVenta = this;
        }

        public virtual void Add(Paquete paquete)
        {
            paquete.TipoVenta = this;
        }

        public virtual void Add(Pieza pieza)
        {
            pieza.TipoVenta = this;
        }

        public virtual bool Save()
        {
            return Insert(this);
        }

        public TipoVenta()
        {
            Producto = new HashSet<Producto>();
            Agranele = new HashSet<Agranel>();
            Paquete = new HashSet<Paquete>();
            OnCreated();

        }
    }
}
