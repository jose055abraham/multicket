using Prism.Commands;
using System.Windows;

namespace Multicket.Module.Commands
{
    public class SystemCommand : Window
    {
        public DelegateCommand Minimize
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    SystemCommands.MinimizeWindow(this);
                });
            }
        }

        public DelegateCommand Cloce
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    SystemCommands.CloseWindow(this);
                });
            }
        }
    }
}
