using System;

namespace Multicket.Data.Models
{
    public partial class Agranel
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual decimal? StockActual { get; set; }
        public virtual decimal? StockMinimo { get; set; }
        public virtual decimal? StockMaximo { get; set; }
        public virtual TipoVenta TipoVenta { get; set; }

        public Agranel()
        {
            OnCreated();
        }
    }
}
