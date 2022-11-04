using System;

namespace Multicket.Data.Models
{
    public partial class Pieza
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual int? StockActual { get; set; }
        public virtual int? StockMinimo { get; set; }
        public virtual int? StockMaximo { get; set; }
        public virtual TipoVenta TipoVenta { get; set; }

        public Pieza()
        {
            OnCreated();
        }
    }
}
