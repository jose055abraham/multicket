using Multicket.Module.Mvvm;
using Multicket.Module.Services;

namespace Multicket.Module.ViewModels
{
    public class ComprasViewModel
    {
        private readonly IManagerService src;

        public ComprasViewModel(IManagerService service)
        {
            src = service;
        }

        public RelayCommand ComprasSugeridasCommand => new RelayCommand(action: OnComprasSugeridas);

        private void OnComprasSugeridas(object sender)
        {

        }

        private void Navigate(string content, string view)
        {
            src.region.RequestNavigate(content, view);
        }
    }
}
