using System;

namespace Multicket.Module.Commands
{
    public interface IInteractionRequest
    {
        event EventHandler<InteractionRequestedEventArgs> Raised;
    }

}
