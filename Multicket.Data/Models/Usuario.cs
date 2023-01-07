using Multicket.Data.Services;
using System;
using System.Collections.Generic;

namespace Multicket.Data.Models
{
	public partial class Usuario : Repository

	{
		partial void OnCreated();
		public virtual Guid Id { get; set; }
		public virtual string Nombre { get; set; }
		public virtual string Password { get; set; }
		public virtual Empleado Empleado { get; set; }
		public virtual ISet<Permiso> Permiso { get; set; }

		public virtual void Add(Empleado empleado)
		{
			empleado.Usuario = this;
		}

		/// <summary>
		/// Relación uno a muchos 
		/// </summary>
		/// <param name="permisos"></param>
		public virtual void Add(ISet<Permiso> permisos)
		{
			foreach (var item in permisos)
			{
				Permiso.Add(item);
				item.Add(this);
				item.Save();
			}
		}

		public virtual bool Save()
		{
			return Add(this);
		}

		public Usuario()
		{
			Permiso = new HashSet<Permiso>();
			OnCreated();
		}
	}
}
