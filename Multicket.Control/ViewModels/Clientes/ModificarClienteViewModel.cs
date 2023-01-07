using Multicket.Control.Mvvm;
using Multicket.Data.Models;
using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using Prism.Regions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace Multicket.Module.ViewModels
{
	[RegionMemberLifetime(KeepAlive = false)]
	public class ModificarClienteViewModel : Bind
	{
		private readonly IManagerService src;
		private NavigationParameters Parameters;

		public ModificarClienteViewModel(IManagerService service)
		{
			src = service;
			Initialization();
		}

		public ISet<Cliente> ClienteItems
		{
			get => Get<ISet<Cliente>>();
			set => Set(value);

		}

		public string Buscar { get; set; }
		public ICollectionView ClienteFilterView { get; set; }
		public Cliente SelectedClienteItem { get; set; }

		public RelayCommand AceptarCommand => new RelayCommand(action: OnAceptar);
		public RelayCommand SearchChangedCommand => new RelayCommand(action: OnSearchChanged);

		private void OnSearchChanged(object sender)
		{
			OnFilterData();
		}

		private void OnFilterData()
		{
			CollectionViewSource.GetDefaultView(ClienteItems).Refresh();
		}

		private void OnAceptar(object sender)
		{
			if (SelectedClienteItem is null) return;
			Parameters.Add(nameof(SelectedClienteItem), SelectedClienteItem);
			Navigate("Creditos", "NuevoCredito", Parameters);
			return;
		}

		private void Initialization()
		{
			Buscar = "";
			Parameters = new NavigationParameters();
			ClienteItems = src.data.Find<Cliente>();
			ClienteFilterView = CollectionViewSource.GetDefaultView(ClienteItems);
			ClienteFilterView.Filter = (e) =>
			{
				if (e is Cliente cli)
				{
					return cli.Nombre.ToUpper().ToLower().Contains(Buscar)
						|| cli.Folio.ToString().Contains(Buscar);
				}
				return false;
			};
		}

		private void Navigate(string content, string view, NavigationParameters parasm)
		{
			src.region.RequestNavigate(content, view, parasm);
		}
	}
}
