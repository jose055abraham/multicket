using Multicket.Data.Services;
using System;

namespace Multicket.Data.Models
{
    public partial class DetalleCorteCaja : Repository
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual decimal? Ingresos { get; set; }
        public virtual decimal? Egresos { get; set; }
        public virtual decimal? Ganancia { get; set; }
        public virtual decimal? Diferencia { get; set; }
        public virtual decimal? MontoTotal { get; set; }
        public virtual DateTime? Created_At { get; set; }
        public virtual DateTime? Updated_At { get; set; }
        public virtual Caja Caja { get; set; }

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
            return Add(this);
        }

        public DetalleCorteCaja()
        {
            OnCreated();
        }
    }
}
