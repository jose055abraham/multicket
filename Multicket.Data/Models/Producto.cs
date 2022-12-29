using Multicket.Data.Services;
using System;
using System.Collections.Generic;

namespace Multicket.Data.Models
{
    public partial class Producto : Repository
    {

        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual string Codigo { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual decimal PrecioCosto { get; set; }
        public virtual decimal PrecioVenta { get; set; }
        public virtual decimal PrecioMayoreo { get; set; }
        public virtual int Ganancia { get; set; }
        public virtual bool? Favorito { get; set; }
        public virtual DateTime? Created_At { get; set; }
        public virtual DateTime? Updated_At { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual TipoVenta TipoVenta { get; set; }
        public virtual Inventario Inventario { get; set; }
        public virtual ISet<DetalleVenta> DetalleVenta { get; set; }
        public virtual ISet<Paquete> Paquete { get; set; }

        public virtual void Add(DetalleVenta detalle)
        {
            detalle.Producto = this;
        }

        public virtual void Add(Inventario inventario)
        {
            inventario.Producto = this;
        }

        public virtual void Add(Paquete paquete)
        {
            Paquete.Add(paquete);
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

        public Producto()
        {
            Paquete = new HashSet<Paquete>();
            DetalleVenta = new HashSet<DetalleVenta>();
            OnCreated();
        }
    }
}
