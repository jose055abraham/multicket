using Multicket.Data.Services;
using System;

namespace Multicket.Data.Models
{
    public partial class Agranel : Repository
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual decimal? StockActual { get; set; }
        public virtual decimal? StockMinimo { get; set; }
        public virtual decimal? StockMaximo { get; set; }
        public virtual TipoVenta TipoVenta { get; set; }

        public virtual bool Save()
        {
            return Add(this);
        }

        public Agranel()
        {
            OnCreated();
        }
    }
}
