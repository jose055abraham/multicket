using System;
using System.Collections.Generic;

namespace Multicket.Data.Models
{
    public partial class Caja
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Serial { get; set; }
        public virtual ISet<Empleado> Empleados { get; set; }
        public virtual ISet<DetalleCaja> DetallesCaja { get; set; }
        public virtual ISet<DetalleCorteCaja> DetallesCorteCaja { get; set; }

        public virtual void add(DetalleCaja detalle)
        {
            detalle.Caja = this;
        }

        public virtual void add(DetalleCorteCaja corte)
        {
            corte.Caja = this;
        }

        public virtual void add(Empleado empleado)
        {
            empleado.Caja = this;
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