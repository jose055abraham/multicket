using Multicket.Data.Enum;
using System;
using System.Collections.Generic;

namespace Multicket.Data.Models
{
    public partial class Permiso
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual PermisoType PermisoType { get; set; }
        public virtual ISet<Usuario> Usuario { get; set; }

        public virtual void add(Usuario usuario)
        {
            Usuario.Add(usuario);
        }

        public Permiso()
        {
            Usuario = new HashSet<Usuario>();
            OnCreated();
        }
    }
}
