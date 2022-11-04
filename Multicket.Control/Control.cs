using Multicket.Data.Services;
using Multicket.Module.Commands;
using Multicket.Module.Services;
using Multicket.Module.ViewModels;
using Multicket.Module.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Multicket.Module
{
    public class Control : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegionManager regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion<Login>("Main");
            regionManager.RegisterViewWithRegion<Ventas>("MainContent");
            regionManager.RegisterViewWithRegion<EstadoCuenta>("Creditos");
            regionManager.RegisterViewWithRegion<TabControlProducto>("Productos");
            regionManager.RegisterViewWithRegion<ComprasSugeridas>("Compras");

            //regionManager.RegisterViewWithRegion("TabRegion", typeof(NuevoProducto));
            //regionManager.RegisterViewWithRegion("TabRegion", typeof(ContenidoPaquete));


        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainContent>();
            containerRegistry.RegisterForNavigation<Login>();

            /*** Vistas de Ventas ***/
            containerRegistry.RegisterForNavigation<Ventas>();

            /*** Vistas de Productos ***/
            containerRegistry.RegisterForNavigation<DetalleEliminarProducto>();
            containerRegistry.RegisterForNavigation<ModificarProducto>();
            //containerRegistry.RegisterForNavigation<ContenidoPaquete>();
            containerRegistry.RegisterForNavigation<EliminarProducto>();
            containerRegistry.RegisterForNavigation<VentasPorPeriodo>();
            containerRegistry.RegisterForNavigation<Departamentos>();
            containerRegistry.RegisterForNavigation<Promociones>();
            containerRegistry.RegisterForNavigation<Productos>();
            containerRegistry.RegisterForNavigation<Catalogo>();
            containerRegistry.RegisterForNavigation<TabControlProducto>();
            /*** Vistas Tab Productos ***/
            containerRegistry.RegisterForNavigation<NuevoProducto>();

            /*** Vistas de Cliente ***/
            containerRegistry.RegisterForNavigation<DetalleEliminarCliente>();
            containerRegistry.RegisterForNavigation<EstadoCuentaCliente>();
            containerRegistry.RegisterForNavigation<ModificarCliente>();
            containerRegistry.RegisterForNavigation<EliminarCliente>();
            containerRegistry.RegisterForNavigation<EstadoCuenta>();
            containerRegistry.RegisterForNavigation<NuevoCredito>();
            containerRegistry.RegisterForNavigation<NuevoCliente>();
            containerRegistry.RegisterForNavigation<Creditos>();

            /*** Vistas de Facturas ***/
            containerRegistry.RegisterForNavigation<Inventario>();
            containerRegistry.RegisterForNavigation<Facturas>();

            /*** Vistas de Compras ***/
            containerRegistry.RegisterForNavigation<Compras>();
            containerRegistry.RegisterForNavigation<ComprasSugeridas>();
            containerRegistry.RegisterForNavigation<HistorialCompras>();

            /*** Dialogs ***/
            containerRegistry.RegisterDialog<NotificationSuccess, NotificationSuccessViewModel>();
            containerRegistry.RegisterDialog<BusquedaProducto, BusquedaProductoViewModel>();
            containerRegistry.RegisterDialog<ProductosVarios, ProductosVariosViewModel>();
            containerRegistry.RegisterDialog<VentasDelDia, VentasDelDiaViewModel>();
            containerRegistry.RegisterDialog<ContenidoPaquete, ContenidoPaqueteViewModel>();
            containerRegistry.RegisterDialog<Entradas, EntradasViewModel>();
            containerRegistry.RegisterDialog<Salidas, SalidasViewModel>();
            containerRegistry.RegisterDialog<Warning, WarningViewModel>();
            containerRegistry.RegisterDialog<Information, InformationViewModel>();

            /*** Services ***/
            containerRegistry.RegisterSingleton<IBusinessBase, BusinessBase>();
            containerRegistry.RegisterSingleton<ISessionStorage, SessionStorage>();
            containerRegistry.RegisterSingleton<IManagerService, ManagerService>();
            containerRegistry.RegisterSingleton<IApplicationCommands, AppCommands>();
        }


    }
}
