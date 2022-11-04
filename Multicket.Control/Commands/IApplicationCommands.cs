using Prism.Commands;

namespace Multicket.Module.Commands
{
    public interface IApplicationCommands
    {
        CompositeCommand GuardarProductoCommand { get; }
        CompositeCommand VisibleNuevoPaqueteViewCommand { get; }
        CompositeCommand CollapsedNuevoPaqueteViewCommand { get; }

    }
}