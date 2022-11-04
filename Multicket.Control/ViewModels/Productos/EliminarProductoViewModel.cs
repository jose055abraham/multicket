using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace Multicket.Module.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class EliminarProductoViewModel
    {
        private readonly IRegionManager region;
        private readonly IDialogService dialog;

        public EliminarProductoViewModel(IRegionManager region,
                               IDialogService dialog)
        {
            this.region = region;
            this.dialog = dialog;
        }

        public DelegateCommand AceptarCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Navigate("Productos", "DetalleEliminarProducto");
                    return;
                });
            }
        }


        private void Navigate(string content, string view)
        {
            region.RequestNavigate(content, view);
        }
    }
}
