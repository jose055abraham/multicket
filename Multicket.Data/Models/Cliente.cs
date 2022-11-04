using Multicket.Data.Common;
using System;
using System.Collections.Generic;

namespace Multicket.Data.Models
{
    public partial class Cliente
    {
        private string _avatar;
        partial void OnCreated();

        public virtual Guid Id { get; set; }
        public virtual int Folio { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Apellidos { get; set; }
        public virtual string Telefono { get; set; }
        public virtual string Correo { get; set; }
        public virtual string Fill { get; set; }
        public virtual DateTime? Created_At { get; set; }
        public virtual DateTime? Updated_At { get; set; }
        public virtual Direccion Direccion { get; set; }
        public virtual Credito Credito { get; set; }
        public virtual ISet<Venta> Venta { get; set; }

        public virtual string Avatar
        {
            get => _avatar;

            set
            {
                if (!string.IsNullOrEmpty(Nombre) && !string.IsNullOrEmpty(Apellidos))
                {
                    _avatar = string.Format("{0}{1}",
                         Nombre.Substring(0, 1),
                         Apellidos.Substring(0, 1));
                }
                _avatar = Nombre.Substring(0, 2);
            }
        }

        public virtual void add(Credito credito)
        {
            credito.Cliente = this;
        }

        public virtual void add(Direccion direccion)
        {
            direccion.Cliente = this;
        }

        public virtual void add(Venta venta)
        {
            venta.Cliente = this;
        }

        public virtual void OnVeryfi()
        {
            if (Id == Guid.Empty)
            {
                Created_At = DateTime.Now;
                Folio = Generate.Folio;
                Fill = Generate.Colors;
            }
            else
            {
                Updated_At = DateTime.Now;
            }
        }

        public Cliente()
        {
            Venta = new HashSet<Venta>();
            OnCreated();
        }
    }
}
