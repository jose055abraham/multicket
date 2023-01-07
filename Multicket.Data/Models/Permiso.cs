using Multicket.Data.Models;
using Multicket.Data.Services;
using System;
using System.Collections.Generic;

namespace Multicket.Data.Models
{
    public partial class Permiso : Repository
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual PermisoType PermisoType { get; set; }
        public virtual ISet<Usuario> Usuario { get; set; }

        public virtual void Add(Usuario usuario)
        {
            Usuario.Add(usuario);
        }

        public virtual bool Save()
        {
            return Add(this);
        }

        public Permiso()
        {
            Usuario = new HashSet<Usuario>();
            OnCreated();
        }
    }
}
