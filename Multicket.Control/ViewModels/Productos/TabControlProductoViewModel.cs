using Multicket.Module.Commands;
using Multicket.Module.Mvvm;
using Multicket.Module.Services;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = true)]
    public class TabControlProductoViewModel : BindableBase
    {
        private readonly IManagerService src;

        public TabControlProductoViewModel(IManagerService service)
        {
            src = service;
            Initialization();
        }

        public IApplicationCommands Cmd { get; set; }

        public RelayCommand VisibleNuevoPaqueteViewCommand => new RelayCommand(OnVisibleView);

        private void OnVisibleView(object obj)
        {
            src.dialog.ShowDialog("ContenidoPaquete",
                new DialogParameters(), callback: (e) =>
            {

            });
        }

        private void Initialization()
        {
            src.cmd.VisibleNuevoPaqueteViewCommand.RegisterCommand(VisibleNuevoPaqueteViewCommand);
            Cmd = src.cmd;
        }

    }
}
