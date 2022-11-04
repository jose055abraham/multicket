using Multicket.Module.Services;
using Prism.Regions;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class InventarioViewModel
    {
        private readonly IManagerService src;

        public InventarioViewModel(IManagerService service)
        {
            src = service;
        }
    }
}
