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
	public class EliminarClienteViewModel : Bind
	{
		private readonly IManagerService src;
		private NavigationParameters parameters;

		public EliminarClienteViewModel(IManagerService service)
		{
			src = service;
			Initialization();
		}

		public ICollectionView ClienteFilterView { get; set; }
		public Cliente SelectedClienteItem { get; set; }
		public string Buscar { get; set; }

		public RelayCommand AceptarCommand => new RelayCommand(action: OnAceptar);
		public RelayCommand SearchChangedCommand => new RelayCommand(action: OnSearchChanged);

		private void OnSearchChanged(object sender)
		{
			OnFilterData();
		}

		private void OnAceptar(object sender)
		{
			if (SelectedClienteItem is null) return;
			parameters.Add(nameof(SelectedClienteItem), SelectedClienteItem);
			Navigate("Creditos", "DetalleEliminarCliente", parameters);
			return;
		}

		private void OnFilterData()
		{
			CollectionViewSource.GetDefaultView(ClienteItems).Refresh();
		}

		private void Initialization()
		{
			Buscar = "";
			parameters = new NavigationParameters();
			OnRefresh();

		}

		private void OnRefresh()
		{
			ClienteItems = src.data.Find<Cliente>();
			ClienteFilterView = CollectionViewSource.GetDefaultView(ClienteItems);
			ClienteFilterView.Filter = (e) =>
			{
				if (e is Cliente cli)
				{
					return cli.Nombre.Contains(Buscar);
				}
				return false;
			};
		}

		private void Navigate(string content, string view, NavigationParameters parasm)
		{
			src.region.RequestNavigate(content, view, parasm);
		}

		public ISet<Cliente> ClienteItems
		{
			get => Get<ISet<Cliente>>();
			set => Set(value);
		}
	}
}
