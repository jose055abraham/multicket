using Multicket.Data.Common;
using Multicket.Data.Services;
using System;
using System.Collections.Generic;

namespace Multicket.Data.Models
{
    public partial class Cliente : Repository
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
                if (string.IsNullOrEmpty(Nombre) && string.IsNullOrEmpty(Apellidos))
                {
                    _avatar = string.Format("{0}{1}", Nombre.Substring(0, 1), Apellidos.Substring(0, 1)).ToUpper();
                }
                _avatar = Nombre.Substring(0, 2).ToUpper();
            }
        }

        public virtual void Add(Credito credito)
        {
            credito.Cliente = this;
        }

        public virtual void Add(Direccion direccion)
        {
            direccion.Cliente = this;
        }

        public virtual void Add(Venta venta)
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

        public virtual bool Save()
        {
            return Insert(this);
        }

        public Cliente()
        {
            Venta = new HashSet<Venta>();
            OnCreated();
        }
    }
}
