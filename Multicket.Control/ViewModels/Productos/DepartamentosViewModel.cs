using Multicket.Control.Mvvm;
using Multicket.Data.Models;
using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using NHibernate.Validator.Constraints;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class DepartamentosViewModel : Bind
    {
        private readonly IManagerService src;

        public DepartamentosViewModel(IManagerService service)
        {
            src = service;
            Initialization();
        }

        public string Buscar { get; set; }
        public int SelectedDepartamentoIndex { get; set; }
        public Departamento SelectedDepartamentoItem { get; set; }
        public ICollectionView DepartamentoFilterView { get; set; }

        public RelayCommand BuscarChangedCommand => new RelayCommand(action: OnBuscarChanged);
        public RelayCommand NuevoCommand => new RelayCommand(action: OnCanselar);
        public RelayCommand CancelarCommand => new RelayCommand(action: OnCanselar);
        public RelayCommand EliminarCommand => new RelayCommand(action: OnDelete, CanDelete);
        public RelayCommand GuardarCommand => new RelayCommand(action: OnGuardar, CanSave);
        public RelayCommand DepartamentosSelectionChanged => new RelayCommand(action: OnDepartamentoSelection);

        private void OnDepartamentoSelection(object sender)
        {
            if (SelectedDepartamentoItem is null) return;

            Id = SelectedDepartamentoItem.Id;
            Nombre = SelectedDepartamentoItem.Nombre;
        }


        private void OnGuardar(object sender)
        {
            var dep = new Departamento
            {
                Id = Id,
                Nombre = Nombre,
            };

            dep.OnVeryfi();
            dep.Save();

            OnRefrehs();
            OnClear();
        }

        private void OnBuscarChanged(object obj)
        {
            OnFilterData();
            return;
        }

        private void OnFilterData()
        {
            CollectionViewSource.GetDefaultView(DepartamentoItems).Refresh();
        }

        private void OnDelete(object sender)
        {
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
                        src.data.Delete(SelectedDepartamentoItem);
                        src.dialog.ShowDialog("NotificationSuccess", new DialogParameters
                        {
                            {"message","Registro eliminado con exito" }
                        }, null);
                    }
                    OnClear();
                    OnRefrehs();
                });
        }

        private void OnCanselar(object sender)
        {
            OnClear();
        }

        private bool CanSave(object sender)
        {
            return GetAllInvalidRules().Count == 0;
        }

        private bool CanDelete(object sender)
        {
            return SelectedDepartamentoIndex >= 0;
        }

        private void Initialization()
        {
            Buscar = "";
            Id = default;
            Nombre = default;
            SelectedDepartamentoIndex = 0;
            OnRefrehs();
        }

        private void OnRefrehs()
        {
            Nombre = default;
            DepartamentoItems = src.data.Find<Departamento>();
            DepartamentoFilterView = CollectionViewSource.GetDefaultView(DepartamentoItems);
            DepartamentoFilterView.Filter = (e) =>
            {
                if (e is Departamento dep)
                {
                    return dep.Nombre.Contains(Buscar);
                }
                return false;
            };

        }

        private void OnClear()
        {
            Id = default;
            Nombre = default;
        }

        public Guid Id
        {
            get => Get<Guid>();
            set => Set(value);
        }

        [NotNullNotEmpty]
        public string Nombre
        {
            get => Get<string>();
            set => Set(value);
        }

        public ISet<Departamento> DepartamentoItems
        {
            get => Get<ISet<Departamento>>();
            set => Set(value);
        }
    }
}
