using Multicket.Data.Services;
using System;

namespace Multicket.Data.Models
{
    public partial class Pieza : Repository
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual int? StockActual { get; set; }
        public virtual int? StockMinimo { get; set; }
        public virtual int? StockMaximo { get; set; }
        public virtual TipoVenta TipoVenta { get; set; }

        public virtual bool Save()
        {
            return Insert(this);
        }

        public Pieza()
        {
            OnCreated();
        }
    }
}
