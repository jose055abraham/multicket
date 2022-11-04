using Multicket.Data.Enum;
using System;
using System.Collections.Generic;

namespace Multicket.Data.Models
{
    public partial class TipoVenta
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual VentaType VentaType { get; set; }
        public virtual ISet<Producto> Producto { get; set; }
        public virtual ISet<Agranel> Agranele { get; set; }
        public virtual ISet<Paquete> Paquete { get; set; }
        public virtual ISet<Pieza> Pieza { get; set; }


        public virtual void add(Producto producto)
        {
            producto.TipoVenta = this;
        }

        public virtual void add(Agranel agranel)
        {
            agranel.TipoVenta = this;
        }

        public virtual void add(Paquete paquete)
        {
            paquete.TipoVenta = this;
        }

        public virtual void add(Pieza pieza)
        {
            pieza.TipoVenta = this;
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
