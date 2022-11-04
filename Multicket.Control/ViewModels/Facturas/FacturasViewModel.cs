using Multicket.Module.Services;
using Prism.Regions;

namespace Multicket.Module.ViewModels.Facturas
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class FacturasViewModel
    {
        private readonly IManagerService src;

        public FacturasViewModel(IManagerService service)
        {
            src = service;
        }
    }
}
