using Multicket.Data.Services;
using System;
using System.Collections.Generic;

namespace Multicket.Data.Models
{
    public partial class Caja : Repository
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Serial { get; set; }
        public virtual ISet<Empleado> Empleados { get; set; }
        public virtual ISet<DetalleCaja> DetallesCaja { get; set; }
        public virtual ISet<DetalleCorteCaja> DetallesCorteCaja { get; set; }

        public virtual void Add(DetalleCaja detalle)
        {
            detalle.Caja = this;
        }

        public virtual void Add(DetalleCorteCaja corte)
        {
            corte.Caja = this;
        }

        public virtual void Add(Empleado empleado)
        {
            empleado.Caja = this;
        }

        public virtual bool Save()
        {
            return Add(this);
        }

        public Caja()
        {
            Empleados = new HashSet<Empleado>();
            DetallesCaja = new HashSet<DetalleCaja>();
            DetallesCorteCaja = new HashSet<DetalleCorteCaja>();
            OnCreated();
        }
    }
}