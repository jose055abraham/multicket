using Multicket.Data.Models;
using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = true)]
    public class LoginViewModel : BindableBase
    {
        private readonly IManagerService src;
        private Usuario _usuario;
        private string _nombre;
        private string _password;

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


            //if (src.Data.Verify(Usuario.Nombre, "12345678") is null)
            //{
            //    MessageBox.Show("Nombre o contraseña incorrecta!");

            //}
            //else
            //{
                OnNavigate("Main", "MainContent");
            //}

        }

        private void OnSalir(object sender)
        {
            SystemCommands.CloseWindow(Application.Current.MainWindow);
        }

        private void Initialization()
        {
            UsuarioItems = src.data.Find<Usuario>();
            Usuario = new Usuario();

        }

        public string Nombre
        {
            get => _nombre;
            set => SetProperty(ref _nombre, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public Usuario Usuario
        {
            get => _usuario;
            set => SetProperty(ref _usuario, value);
        }

        private void OnNavigate(string content, string view)
        {
            src.region.RequestNavigate(content, view);
        }
    }
}
