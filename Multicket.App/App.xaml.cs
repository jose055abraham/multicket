using System.Windows;

namespace Multicket.App
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstrap.Bootstrapper.Build.Run();
        }
    }
}
