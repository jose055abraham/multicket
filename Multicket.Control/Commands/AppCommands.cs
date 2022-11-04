using Prism.Commands;

namespace Multicket.Module.Commands
{
    public class AppCommands : IApplicationCommands
    {
        private CompositeCommand _guardarProductoCommand = new CompositeCommand();
        private CompositeCommand _nuevoPaqueteViewCommand = new CompositeCommand();
        private CompositeCommand _collapsedNuevoPaqueteViewCommand = new CompositeCommand();

        public CompositeCommand GuardarProductoCommand
        {
            get
            {
                return _guardarProductoCommand;
            }
        }

        public CompositeCommand VisibleNuevoPaqueteViewCommand
        {
            get
            {
                return _nuevoPaqueteViewCommand;
            }
        }

        public CompositeCommand CollapsedNuevoPaqueteViewCommand
        {
            get
            {
                return _collapsedNuevoPaqueteViewCommand;
            }
        }
    }
}
