using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace Multicket.Module.ViewModels.Catalogo
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class CatalogoViewModel : BindableBase
    {
        private readonly IRegionManager region;
        private readonly IDialogService dialog;

        public CatalogoViewModel(IRegionManager region,
                               IDialogService dialog)
        {
            this.region = region;
            this.dialog = dialog;
        }
    }
}
