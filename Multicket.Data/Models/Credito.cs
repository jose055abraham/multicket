using Multicket.Data.Services;
using System;
using System.Collections.Generic;

namespace Multicket.Data.Models
{

    public partial class Credito : Repository
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual decimal LCredito { get; set; }
        public virtual DateTime? Created_At { get; set; }
        public virtual DateTime? Updated_At { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ISet<VentaACredito> VentaACredito { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", Id, LCredito);
        }

        public virtual void Add(VentaACredito credito)
        {
            credito.Credito = this;
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

        public Credito()
        {
            VentaACredito = new HashSet<VentaACredito>();
            OnCreated();
        }

    }
}
