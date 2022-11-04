using System;
using System.Collections.Generic;

namespace Multicket.Data.Models
{
    public partial class Empleado
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Apellidos { get; set; }
        public virtual string Telefono { get; set; }
        public virtual string Correo { get; set; }
        public virtual DateTime? Created_At { get; set; }
        public virtual DateTime? Updated_At { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Caja Caja { get; set; }
        public virtual ISet<Abono> Abono { get; set; }
        public virtual ISet<Venta> Venta { get; set; }
        public virtual ISet<Ingreso> Ingreso { get; set; }
        public virtual ISet<Egreso> Egreso { get; set; }

        public virtual void add(Venta venta)
        {
            venta.Empleado = this;
        }

        public virtual void add(Ingreso ingreso)
        {
            ingreso.Empleado = this;
        }

        public virtual void add(Egreso egreso)
        {
            egreso.Empleado = this;
        }

        public virtual void add(Abono abono)
        {
            abono.Empleado = this;
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

        public Empleado()
        {
            Abono = new HashSet<Abono>();
            Venta = new HashSet<Venta>();
            Ingreso = new HashSet<Ingreso>();
            Egreso = new HashSet<Egreso>();
            OnCreated();
        }
    }
}
