using Multicket.Data.Models;
using Multicket.Data.Validation;
using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using NHibernate.Validator.Constraints;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace Multicket.Module.ViewModels
{
	[RegionMemberLifetime(KeepAlive = false)]
	public class NuevoCreditoViewModel : ValidatorBase, INavigationAware
	{
		private string _nombre;
		private decimal _credito;
		private string _telefono;
		private string _ndireccion;
		private Cliente cliente;
		private Credito credito;
		private Direccion direccion;
		private readonly IManagerService src;

		public NuevoCreditoViewModel(IManagerService service)
		{
			src = service;
		}


		public RelayCommand GuardarCommand => new RelayCommand(action: OnGuardar, CanSave);

		private bool CanSave(object obj)
		{
			return GetAllInvalidRules().Count == 0;
		}

		public RelayCommand CancelarClienteCommand => new RelayCommand(action: (e) => OnClear());

		private void OnGuardar(object args)
		{
			if (cliente is null)
			{
				cliente = new Cliente();
				cliente.Nombre = Nombre;
				cliente.Telefono = Telefono;

				direccion = new Direccion();
				direccion.Domicilio1 = Direccion;

				credito = new Credito();
				credito.LCredito = LCredito;
			}
			else
			{
				cliente.Nombre = Nombre;
				cliente.Telefono = Telefono;
				credito.LCredito = LCredito;
				direccion.Domicilio1 = Direccion;
			}

			cliente.OnVeryfi();
			cliente.Add(credito);
			cliente.Add(direccion);
			cliente.Save();
			credito.Save();

			if (direccion.Save())
			{
				Dialog("NotificationSuccess", "Datos actualizados");
				OnClear();
			}
		}

		private void OnClear()
		{
			Nombre = default;
			Telefono = default;
			Direccion = default;
			LCredito = default;
			cliente = default;
			credito = default;
			direccion = default;
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			cliente = (Cliente)navigationContext.Parameters["SelectedClienteItem"];
			if (cliente is null) return;

			direccion = cliente.Direccion;
			credito = cliente.Credito;

			Nombre = cliente.Nombre;
			Telefono = cliente.Telefono;
			Direccion = direccion.Domicilio1;
			LCredito = credito.LCredito;
		}

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			if (navigationContext.Parameters.ContainsKey("SelectedClienteItem"))
			{
				cliente = navigationContext.Parameters["SelectedClienteItem"] as Cliente;

				if (cliente is null)
				{
					return cliente is null /*&& this.cliente.Nombre.Equals(cliente.Nombre)*/;
				}
				return true;
			}
			else
			{
				return false;
			}

		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
		}

		private void Dialog(string key, string message = "")
		{
			src.dialog.ShowDialog(key,
				new DialogParameters($"message={message}"), null);
		}

		[NotNullNotEmpty]
		public string Nombre
		{
			get => _nombre;
			set => SetProperty(ref _nombre, value);
		}

		[NotNullNotEmpty]
		public string Direccion
		{
			get => _ndireccion;
			set => SetProperty(ref _ndireccion, value);
		}

		[Length(10, 10)]
		public string Telefono
		{
			get => _telefono;
			set => SetProperty(ref _telefono, value);
		}

		public decimal LCredito
		{
			get { return _credito; }
			set { SetProperty(ref _credito, value); }
		}
	}
}
