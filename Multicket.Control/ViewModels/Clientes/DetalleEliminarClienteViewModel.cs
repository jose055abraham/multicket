using Multicket.Control.Mvvm;
using Multicket.Data.Models;
using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;

namespace Multicket.Module.ViewModels
{
	[RegionMemberLifetime(KeepAlive = false)]
	public class DetalleEliminarClienteViewModel : Bind, INavigationAware
	{
		private Cliente cliente;
		private Credito credito;
		private Direccion direccion;
		private readonly IManagerService src;

		public DetalleEliminarClienteViewModel(IManagerService service)
		{
			src = service;
		}

		public RelayCommand CancelarCommand => new RelayCommand(action: OnCancelar);
		public RelayCommand EliminarCommand => new RelayCommand(action: OnEliminar);

		private void OnEliminar(object sender)
		{
			if (cliente is null) return;

			src.dialog.ShowDialog(
				name: "Warning",
				parameters: new DialogParameters
				{
					{ "message", "Esta seguro de eliminar la siguiente información?" },
					{ "title", "Advertencia" },
					{ "caption", "Eliminar registro" }
				},
				callback: (action) =>
				{
					if (action.Result == ButtonResult.OK)
					{
						src.data.Delete(cliente);

						src.dialog.ShowDialog(
							name: "NotificationSuccess",
							parameters: new DialogParameters
						{
							{"message","Registro eliminado con exito" }
						}, null);
					}
					OnClear();
					Navigate("Creditos", "EliminarCliente");
				});

		}


		private void OnCancelar(object sender)
		{
			Navigate("Creditos", "EliminarCliente");
			OnClear();
			return;
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			cliente = (Cliente)navigationContext.Parameters["SelectedClienteItem"];

			if (cliente is null) return;

			credito = cliente.Credito;
			direccion = cliente.Direccion;

			Id = cliente.Id;
			Nombre = cliente.Nombre;
			Telefono = cliente.Telefono;
			Direccion = direccion.Domicilio1;
			LCredito = credito.Importe;
		}

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			cliente = navigationContext.Parameters["SelectedClienteItem"] as Cliente;

			if (cliente is null)
			{
				return cliente is null;
			}
			else
			{
				return false;
			}

		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
		}

		private void OnClear()
		{
			Id = default;
			Nombre = default;
			Direccion = default;
			Telefono = default;
			LCredito = default;
		}

		private void Navigate(string content, string view)
		{
			src.region.RequestNavigate(content, view);
		}

		public string Nombre
		{
			get => Get<string>();
			set => Set(value);
		}

		public string Direccion
		{
			get => Get<string>();
			set => Set(value);
		}

		public string Telefono
		{
			get => Get<string>();
			set => Set(value);
		}

		public decimal? LCredito
		{
			get => Get<decimal?>();
			set => Set(value);
		}

		public Guid Id
		{
			get => Get<Guid>();
			set => Set(value);
		}
	}
}
