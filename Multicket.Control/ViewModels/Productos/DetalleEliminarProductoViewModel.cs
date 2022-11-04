using Prism.Regions;
using Prism.Services.Dialogs;

namespace Multicket.Module.ViewModels.Productos
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class DetalleEliminarProductoViewModel
    {
        private readonly IRegionManager region;
        private readonly IDialogService dialog;

        public DetalleEliminarProductoViewModel(IRegionManager region,
                               IDialogService dialog)
        {
            this.region = region;
            this.dialog = dialog;
        }
    }
}
