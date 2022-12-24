using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = true)]
    public class MainContentViewModel : BindableBase, IConfirmNavigationRequest
    {
        private string _tiempo;
        private string _horaLocal;
        private string _usuario;
        private DispatcherTimer _timer;
        private readonly IManagerService src;

        public MainContentViewModel(IManagerService service)
        {
            src = service;
            Initialization();
        }

        public RelayCommand LoginCommand => new RelayCommand(action: OnLogin);
        public RelayCommand MinimizeCommand => new RelayCommand(action: OnMinimize);


        public RelayCommand VentasCommand => new RelayCommand(action: OnVentas/*, (a) => IsVentas*/);
        public RelayCommand ComprasCommand => new RelayCommand(action: OnCompras/*, (a) => IsCompras*/);
        public RelayCommand CreditosCommand => new RelayCommand(action: OnCreditos/*, (a) => IsCreditos*/);
        public RelayCommand FacturasCommand => new RelayCommand(action: OnFacturas/*, (a) => IsFacturas*/);
        public RelayCommand ProductosCommand => new RelayCommand(action: OnProductos/*, (a) => IsProductos*/);
        public RelayCommand ClientesCommand => new RelayCommand(action: OnClientes/*, (a) => IsNuevoCliente*/);
        public RelayCommand InventarioCommand => new RelayCommand(action: OnInventario/*, (a) => IsInventario*/);

        private void OnLogin(object sender) => OnNavigate("Main", "Login");
        private void OnCompras(object sender) => OnNavigate("MainContent", "Compras");
        private void OnVentas(object sender) => OnNavigate("MainContent", "Ventas");
        private void OnFacturas(object sender) => OnNavigate("MainContent", "Facturas");
        private void OnCreditos(object sender) => OnNavigate("MainContent", "Creditos");
        private void OnProductos(object sender) => OnNavigate("MainContent", "Productos");
        private void OnClientes(object sender) => OnNavigate("MainContent", "NuevoCliente");
        private void OnInventario(object sender) => OnNavigate("MainContent", "Inventario");
        private void OnMinimize(object sender)
        {
            SystemCommands.MinimizeWindow(Application.Current.MainWindow);
        }

        private void Initialization()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            //SelectedUsuarioItem = src.session.SelectedUsuarioItem;
            _timer.Tick += Tick;
            _timer.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            HoraLocal = DateTime.Now.ToShortTimeString().ToUpper();
            Tiempo = DateTime.Now.ToLongDateString().ToUpper();
        }

        public void ConfirmNavigationRequest(NavigationContext nav, Action<bool> callback)
        {
            if (nav.NavigationService.Region.Name.Equals("Main"))
            {
                MessageBoxResult result = MessageBox.Show(
                    "Seguro que quiere serrar la sesión?",
                    "Salir",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result.Equals(MessageBoxResult.No))
                {
                    return;
                }
            }
            callback(true);
        }

        public bool IsNavigationTarget(NavigationContext nav)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext nav)
        {
        }

        public void OnNavigatedTo(NavigationContext nav)
        {
        }

        private void OnNavigate(string content, string view)
        {
            src.region.RequestNavigate(content, view);
        }

        public string Tiempo
        {
            get { return _tiempo; }
            set { SetProperty(ref _tiempo, value); }
        }

        public string HoraLocal
        {
            get { return _horaLocal; }
            set { SetProperty(ref _horaLocal, value); }
        }

        public string Usuario
        {
            get { return _usuario; }
            set { SetProperty(ref _usuario, value); }
        }
    }
}
