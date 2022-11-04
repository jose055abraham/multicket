using Multicket.Data.Services;
using Multicket.Module.Commands;
using Prism.Ioc;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace Multicket.Module.Services
{
    public interface IManagerService
    {

        ISessionStorage session { get; set; }
        IRegionManager region { get; set; }
        IDialogService dialog { get; set; }
        IBusinessBase data { get; set; }
        IApplicationCommands cmd { get; set; }
        IContainerExtension container { get; set; }
    }
}
