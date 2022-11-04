using Multicket.App.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;

namespace Multicket.App.Bootstrap
{
    public class Bootstrapper : PrismBootstrapper
    {
        static Bootstrapper instance = null;

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var module = base.CreateModuleCatalog();
            module.AddModule(typeof(Module.Control));
            return module;
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog module)
        {
            //module.AddModule<Module.Control>();
        }

        public static Bootstrapper Build
        {
            get
            {
                if (instance is null)
                {
                    return instance = new Bootstrapper();
                }

                return instance;
            }
        }
    }
}
