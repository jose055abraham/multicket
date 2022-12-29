using Multicket.Data.Services;
using System;

namespace Multicket.Data.Models
{
    public partial class Ingreso : Repository
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual decimal? Importe { get; set; }
        public virtual string Nota { get; set; }
        public virtual DateTime? Created_At { get; set; }
        public virtual DateTime? Updated_At { get; set; }
        public virtual Empleado Empleado { get; set; }

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

        public Ingreso()
        {
            OnCreated();
        }
    }
}
