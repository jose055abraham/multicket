using System.Collections.Generic;

namespace Multicket.Data.Models
{
    public class UsuarioPermisos
    {
        public ISet<Usuario> Usuario { get; set; }
        public ISet<Permiso> Permisos { get; set; }
    }
}
