using Multicket.Control.Mvvm;
using Multicket.Data.Models;
using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.Generic;
using System.Windows;

namespace Multicket.Module.ViewModels
{
	[RegionMemberLifetime(KeepAlive = true)]
	public class LoginViewModel : BindableBase
	{
		private readonly IManagerService src;

		public LoginViewModel(IManagerService service)
		{
			src = service;
			Initialization();
		}

		public ISet<Usuario> UsuarioItems { get; set; }

		public RelayCommand SalirCommand => new RelayCommand(action: OnSalir);
		public RelayCommand AceptarCommand => new RelayCommand(action: OnAceptar);

		private void OnAceptar(object sender)
		{
			Nombre = SelectedUsuarioItem.Nombre;

			if (src.data.Verify(Nombre, Password))
			{
				src.dialog.ShowDialog(
					name: "Information",
					parameters: new DialogParameters
					{
						{"title","Warning" },
						{"caption", "Usuario no encontrado" },
						{"message" , "!El nombre o contraseña son incorrectos!"},
					},
					callback: null);

			}
			else
			{
				OnNavigate("Main", "MainContent");
			}

		}

		private void OnSalir(object sender)
		{
			SystemCommands.CloseWindow(Application.Current.MainWindow);
		}

		private void Initialization()
		{
			UsuarioItems = src.data.Find<Usuario>();
			SelectedUsuarioItem = new Usuario();

		}

		public string Nombre
		{
			get => Get<string>();
			set => Set(value);
		}

		public string Password
		{
			get => Get<string>();
			set => Set(value);
		}

		public Usuario SelectedUsuarioItem
		{
			get => Get<Usuario>();
			set => Set(value);
		}

		private void OnNavigate(string content, string view)
		{
			src.region.RequestNavigate(content, view);
		}
	}
}
