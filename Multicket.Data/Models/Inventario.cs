using System;

namespace Multicket.Data.Models
{
    public partial class Inventario
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual int NumeroProductos { get; set; }
        public virtual int NumeroMaximo { get; set; }
        public virtual int NumeroMinimo { get; set; }
        public virtual Producto Producto { get; set; }

        public Inventario()
        {
            OnCreated();
        }
    }
}
