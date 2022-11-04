using System;
using System.Collections.Generic;

namespace Multicket.Data.Models
{
    public partial class Usuario
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Password { get; set; }
        public virtual Empleado Empleado { get; set; }
        public virtual ISet<Permiso> Permiso { get; set; }

        public virtual void add(Empleado empleado)
        {
            empleado.Usuario = this;
        }

        public virtual void add(Permiso permiso)
        {
            Permiso.Add(permiso);
        }

        public Usuario()
        {
            Permiso = new HashSet<Permiso>();
            OnCreated();
        }
    }
}
