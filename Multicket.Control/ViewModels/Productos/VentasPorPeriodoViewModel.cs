using Prism.Regions;
using Prism.Services.Dialogs;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class VentasPorPeriodoViewModel
    {
        private readonly IRegionManager region;
        private readonly IDialogService dialog;

        public VentasPorPeriodoViewModel(IRegionManager region,
                               IDialogService dialog)
        {
            this.region = region;
            this.dialog = dialog;
        }
    }
}
