using Multicket.Data.Models;
using Multicket.Data.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Multicket.Data.Test
{
    public class Tests
    {
        IBusinessBase business;

        [SetUp]
        public void Setup()
        {
            business = new BusinessBase();

        }

        [Test]
        public void new_usuario_permisos()
        {
            ISet<Permiso> permisos = new HashSet<Permiso>()
            {
                new Permiso { PermisoType = Enum.PermisoType.Catalogo},
                new Permiso { PermisoType = Enum.PermisoType.Apartamentos},
                new Permiso { PermisoType = Enum.PermisoType.Venta},
                new Permiso { PermisoType = Enum.PermisoType.Usuarios},
                new Permiso { PermisoType = Enum.PermisoType.Ventas},
            };

            var usuario = new Usuario();
            usuario.Nombre = "José";
            usuario.Password = "12345";

            usuario.Add(permisos);
            usuario.Save();


        }

        [Test]
        public void usuario_all_permisos()
        {
            var usuarios = business.Find<Usuario>();
            var usuario = usuarios.FirstOrDefault();
            Assert.NotNull(usuario.Permiso);

        }

        [Test]
        public void add_tipo_ventas()
        {
            var tipo_ventas = new List<TipoVenta>()
            {
                new TipoVenta { VentaType = Enum.VentaType.Paquete },
                new TipoVenta { VentaType = Enum.VentaType.Pieza },
                new TipoVenta { VentaType = Enum.VentaType.Agranel },
            };

            foreach (var item in tipo_ventas)
            {
                item.Save();
            }

        }

        [Test]
        public void add_productos()
        {
            var tipo_ventas = new List<Producto>()
            {
                new Producto{Codigo="111110000011111", Descripcion="Pepsi de 600ml"}
            };

            foreach (var item in tipo_ventas)
            {
                item.Save();
            }

        }


        [Test]
        public void query_over()
        {
        }
    }
}