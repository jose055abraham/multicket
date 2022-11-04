using System;
using System.Collections.Generic;

namespace Multicket.Data.Models
{
    public partial class VentaACredito
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual string Nota { get; set; }
        public virtual string Estado { get; set; }
        public virtual DateTime? Created_At { get; set; }
        public virtual DateTime? Updated_At { get; set; }
        public virtual Venta Venta { get; set; }
        public virtual Credito Credito { get; set; }
        public virtual ISet<Abono> Abono { get; set; }

        public virtual void add(Abono abono)
        {
            abono.VentaACredito = this;
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

        public VentaACredito()
        {
            Abono = new HashSet<Abono>();
            OnCreated();
        }
    }
}
