using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Multicket.Module.Components
{
    public class KeyUpCommand : Behavior<UIElement>
    {
        public KeyUpCommand()
        {
            AssociatedObject.KeyUp += KyeUpEvent;
        }

        private void KyeUpEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                ((TextBox)sender).MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
            }

            else if (e.Key == Key.Down)
            {
                ((TextBox)sender).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
    }
}
