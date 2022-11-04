using Prism.Regions;
using Prism.Services.Dialogs;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class PromocionesViewModel
    {
        private readonly IRegionManager region;
        private readonly IDialogService dialog;

        public PromocionesViewModel(IRegionManager region,
                               IDialogService dialog)
        {
            this.region = region;
            this.dialog = dialog;
        }
    }
}
