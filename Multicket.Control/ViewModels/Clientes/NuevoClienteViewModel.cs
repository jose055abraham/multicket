using Multicket.Control.Mvvm;
using Multicket.Data;
using Multicket.Data.Models;
using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using NHibernate.Validator.Constraints;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class NuevoClienteViewModel : Bind
    {
        private readonly IManagerService src;

        public NuevoClienteViewModel(IManagerService service)
        {
            src = service;
            Initialization();
        }

        #region Propiedades 
        public string Buscar { get; set; }
        public Credito Credito { get; set; }
        public Direccion Direccion { get; set; }
        public CTCreditos TipoCreditoItems { get; set; }
        public Cliente SelectedClienteItem { get; set; }
        public ICollectionView ClienteFilterView { get; set; }
        public int SelectedClienteIndex { get; set; }

        #endregion

        #region Commands

        public RelayCommand CheckedCommand => new RelayCommand(action: OnChecked);
        public RelayCommand UncheckedCommand => new RelayCommand(action: OnUnchecked);
        public RelayCommand GuardarCommand => new RelayCommand(action: OnGuardar, CanSave);
        public RelayCommand NombreChangedCommand => new RelayCommand(action: (e) => { });
        public RelayCommand ApellidosChangedCommand => new RelayCommand(action: (e) => { });
        public RelayCommand NuevoClienteCommand => new RelayCommand(action: OnNuevoCliente);
        public RelayCommand SearchChangedCommand => new RelayCommand(action: OnSearchChanged);
        public RelayCommand TCreditosSelectionChangedCommand => new RelayCommand(action: OnTCreditoSelection);
        public RelayCommand ClientesSelectionChangedCommand => new RelayCommand(action: OnClienteSelection);
        public RelayCommand EliminarClienteCommand => new RelayCommand(action: OnEliminarCliente, CanDelete);

        private bool CanDelete(object obj)
        {
            return SelectedClienteIndex >= 0;
        }

        private void OnGuardar(object sender)
        {
            Cliente cliente = new Cliente
            {
                Id = ClienteId,
                Nombre = Nombre,
                Apellidos = Apellidos,
                Telefono = Telefono,
                Correo = Correo,
                Folio = Folio,
                Fill = Fill
            };

            Credito credito = new Credito
            {
                Id = CreditoId,
                Importe = LCredito
            };

            Direccion direccion = new Direccion
            {
                Id = DireccionId,
                Domicilio1 = Domicilio1,
                Domicilio2 = Domicilio2,
                Colonia = Colonia,
                Municipio = Municipio,
                Estado = Estado,
                CodigoPostal = CP,
                Nota = Nota,
            };

            cliente.OnVeryfi();
            cliente.Add(credito);
            cliente.Add(direccion);
            cliente.Save();
            credito.Save();
            direccion.Save();

            src.dialog.ShowDialog("NotificationSuccess", new DialogParameters
            {
                {"caption", "!El usuario o la contraseña son incorrectos!" },
                {"message","Registro realizado con éxito" },
                {"title", "No autorizado" }
            }, null);
            OnRefresh();
            OnClear();
            return;
        }

        private bool CanSave(object obj)
        {
            return GetAllInvalidRules().Count == 0;
        }

        private void OnNuevoCliente(object sender)
        {
            OnClear();
            return;
        }

        private void OnEliminarCliente(object sender)
        {
            if (ClienteItems.Count <= 0)
            {
                SelectedClienteIndex = -1;
            }

            src.dialog.ShowDialog("Warning",
                parameters: new DialogParameters
                {
                    { "message", "Esta seguro de eliminar la siguiente información?" },
                    { "title", "Advertencia" },
                    { "caption", "Eliminar registro" }
                }, callback: (action) =>
                {
                    if (action.Result == ButtonResult.OK)
                    {
                        src.data.Delete(SelectedClienteItem);
                        src.dialog.ShowDialog("NotificationSuccess", new DialogParameters
                        {
                            {"message","Registro eliminado con éxito" }
                        }, null);
                    }
                    OnClear();
                });
            OnClear();
            OnRefresh();
            return;
        }

        private void OnClienteSelection(object sender)
        {
            if (SelectedClienteItem is null) return;

            Credito = SelectedClienteItem.Credito;
            Direccion = SelectedClienteItem.Direccion;

            IsChecked = Credito.Importe >= 0;
            SelectedTipoCreditoIndex = Credito.Importe.Equals(0) ? 0 : 1;
            SelectedTipoCreditoIndex = Credito.Importe > 0 ? 1 : 0;


            ClienteId = SelectedClienteItem.Id;
            CreditoId = SelectedClienteItem.Credito.Id;
            DireccionId = SelectedClienteItem.Direccion.Id;
            Folio = SelectedClienteItem.Folio;
            Nombre = SelectedClienteItem.Nombre;
            Apellidos = SelectedClienteItem.Apellidos;
            Telefono = SelectedClienteItem.Telefono;
            Correo = SelectedClienteItem.Correo;
            Avatar = SelectedClienteItem.Avatar;
            Fill = SelectedClienteItem.Fill;
            Domicilio1 = Direccion.Domicilio1;
            Domicilio2 = Direccion.Domicilio2;
            Colonia = Direccion.Colonia;
            Municipio = Direccion.Municipio;
            Estado = Direccion.Estado;
            CP = Direccion.CodigoPostal;
            Nota = Direccion.Nota;
            LCredito = Credito.Importe;
            return;
        }

        private void OnTCreditoSelection(object sender)
        {
            if (SelectedTipoCreditoIndex.Equals(0))
            {
                LimiteVisible = Visibility.Collapsed;
                LCredito = 0;
            }
            if (SelectedTipoCreditoIndex.Equals(1))
            {
                LimiteVisible = Visibility.Visible;
            }
            return;
        }

        private void OnChecked(object sender)
        {
            LCredito = 0;
            CreditoVisible = CreditoVisible.Equals(Visibility.Collapsed)
                 ? Visibility.Visible
                 : Visibility.Collapsed;
            return;
        }

        private void OnUnchecked(object sender)
        {
            LCredito = -1;
            CreditoVisible = CreditoVisible.Equals(Visibility.Collapsed)
                 ? Visibility.Visible
                 : Visibility.Collapsed;
            return;
        }

        private void OnSearchChanged(object sender)
        {
            OnFilterData();
            return;
        }

        private void OnFilterData()
        {
            CollectionViewSource.GetDefaultView(ClienteItems).Refresh();
        }


        #endregion

        #region Métodos  

        private void Initialization()
        {
            Buscar = "";
            SelectedClienteIndex = -1;
            SelectedClienteItem = new Cliente();
            TipoCreditoItems = new CTCreditos();
            Credito = new Credito();
            Direccion = new Direccion();
            LimiteVisible = Visibility.Collapsed;
            CreditoVisible = Visibility.Collapsed;
            OnRefresh();
        }

        private void OnClear()
        {
            ClienteId = default;
            DireccionId = default;
            CreditoId = default;
            Folio = default;
            Nombre = default;
            Apellidos = default;
            Telefono = default;
            Correo = default;
            Avatar = default;
            Domicilio1 = default;
            Domicilio2 = default;
            Colonia = default;
            Municipio = default;
            CP = default;
            Nota = default;
            LCredito = default;
            Fill = default;
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

        #endregion

        #region Dependency

        public Visibility LimiteVisible
        {
            get => Get<Visibility>();
            set => Set(value);
        }

        public Visibility CreditoVisible
        {
            get => Get<Visibility>();
            set => Set(value);
        }

        [NotNullNotEmpty]
        public string Nombre
        {
            get => Get<string>();
            set => Set(value);
        }

        [NotNullNotEmpty]
        public string Apellidos
        {
            get => Get<string>();
            set => Set(value);
        }

        [Length(10, 10)]
        public string Telefono
        {
            get => Get<string>();
            set => Set(value);
        }

        [Email]
        public string Correo
        {
            get => Get<string>();
            set => Set(value);
        }

        [NotNullNotEmpty]
        public string Domicilio1
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Domicilio2
        {
            get => Get<string>();
            set => Set(value);
        }

        [NotNull]
        public string Colonia
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Municipio
        {
            get => Get<string>();
            set => Set(value);
        }

        public string CP
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Nota
        {
            get => Get<string>();
            set => Set(value);
        }

        public decimal LCredito
        {
            get => Get<decimal>();
            set => Set(value);
        }

        public string Fill
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Estado
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Avatar
        {
            get => Get<string>();
            set => Set(value);
        }

        public Guid ClienteId
        {
            get => Get<Guid>();
            set => Set(value);
        }

        public bool IsChecked
        {
            get => Get<bool>();
            set => Set(value);
        }

        public int Folio
        {
            get => Get<int>();
            set => Set(value);
        }

        public Guid DireccionId
        {
            get => Get<Guid>();
            set => Set(value);
        }

        public Guid CreditoId
        {
            get => Get<Guid>();
            set => Set(value);
        }

        public ISet<Cliente> ClienteItems
        {
            get => Get<ISet<Cliente>>();
            set => Set(value);
        }

        public int SelectedTipoCreditoIndex
        {
            get => Get<int>();
            set => Set(value);
        }

        #endregion
    }
}
