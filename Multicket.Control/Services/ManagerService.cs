using Multicket.Data.Services;
using Multicket.Module.Commands;
using Prism.Ioc;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace Multicket.Module.Services
{
    public class ManagerService : IManagerService
    {
        public ManagerService(IRegionManager region, IDialogService dialog, IBusinessBase data, ISessionStorage session, IApplicationCommands cmd, IContainerExtension container)
        {
            this.region = region;
            this.dialog = dialog;
            this.data = data;
            this.session = session;
            this.cmd = cmd;
            this.container = container;
        }

        public IRegionManager region { get; set; }
        public IDialogService dialog { get; set; }
        public IBusinessBase data { get; set; }
        public ISessionStorage session { get; set; }
        public IApplicationCommands cmd { get; set; }
        public IContainerExtension container { get; set; }
    }
}
