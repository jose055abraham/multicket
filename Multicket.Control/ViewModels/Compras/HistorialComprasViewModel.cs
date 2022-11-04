using Multicket.Module.Services;
using Prism.Regions;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class HistorialComprasViewModel
    {
        private readonly IManagerService src;

        public HistorialComprasViewModel(IManagerService service)
        {
            src = service;
        }
    }
}
