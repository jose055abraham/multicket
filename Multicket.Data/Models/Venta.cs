using Multicket.Data.Services;
using System;
using System.Collections.Generic;

namespace Multicket.Data.Models
{
    public partial class Venta : Repository
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual decimal? Importe { get; set; }
        public virtual decimal? Impuesto { get; set; }
        public virtual string TipoComprobante { get; set; }
        public virtual string NumComprobante { get; set; }
        public virtual string SerieComprobante { get; set; }
        public virtual string Estatus { get; set; }
        public virtual DateTime? FechaVenta { get; set; }
        public virtual DateTime? Created_At { get; set; }
        public virtual DateTime? Updated_At { get; set; }
        public virtual Empleado Empleado { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ISet<DetalleVenta> DetalleVenta { get; set; }
        public virtual ISet<VentaACredito> VentaACredito { get; set; }

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

        public virtual void Add(DetalleVenta detalle)
        {
            detalle.Venta = this;
        }

        public virtual void Add(VentaACredito credito)
        {
            credito.Venta = this;
        }

        public virtual bool Save()
        {
            return Insert(this);
        }

        public Venta()
        {
            VentaACredito = new HashSet<VentaACredito>();
            DetalleVenta = new HashSet<DetalleVenta>();
            OnCreated();
        }
    }
}
