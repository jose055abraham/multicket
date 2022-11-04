using Multicket.Module.Services;

namespace Multicket.Module.ViewModels
{
    public class ComprasSugeridasViewModel
    {
        private readonly IManagerService src;

        public ComprasSugeridasViewModel(IManagerService service)
        {
            src = service;
        }



        private void Navigate(string content, string view)
        {
            src.region.RequestNavigate(content, view);
        }
    }
}
