using Multicket.Module.Commands;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Multicket.Module.Components
{
    public static class Click
    {
        private static readonly DependencyProperty ClickCommandBehaviorProperty = DependencyProperty.RegisterAttached(
            "ClickCommandBehavior",
            typeof(ButtonBaseClickCommandBehavior),
            typeof(Click),
            null);

        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(Click),
            new PropertyMetadata(OnSetCommandCallback));

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached(
            "CommandParameter",
            typeof(object),
            typeof(Click),
            new PropertyMetadata(OnSetCommandParameterCallback));

        public static void SetCommand(ButtonBase buttonBase, ICommand command)
        {
            if (buttonBase == null) throw new System.ArgumentNullException("buttonBase");
            buttonBase.SetValue(CommandProperty, command);
        }

        public static ICommand GetCommand(ButtonBase buttonBase)
        {
            if (buttonBase == null) throw new System.ArgumentNullException("buttonBase");
            return buttonBase.GetValue(CommandProperty) as ICommand;
        }

        public static void SetCommandParameter(ButtonBase buttonBase, object parameter)
        {
            if (buttonBase == null) throw new System.ArgumentNullException("buttonBase");
            buttonBase.SetValue(CommandParameterProperty, parameter);
        }

        public static object GetCommandParameter(ButtonBase buttonBase)
        {
            if (buttonBase == null) throw new System.ArgumentNullException("buttonBase");
            return buttonBase.GetValue(CommandParameterProperty);
        }

        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            ButtonBase buttonBase = dependencyObject as ButtonBase;
            if (buttonBase != null)
            {
                ButtonBaseClickCommandBehavior behavior = GetOrCreateBehavior(buttonBase);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        private static void OnSetCommandParameterCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            ButtonBase buttonBase = dependencyObject as ButtonBase;
            if (buttonBase != null)
            {
                ButtonBaseClickCommandBehavior behavior = GetOrCreateBehavior(buttonBase);
                behavior.CommandParameter = e.NewValue;
            }
        }

        private static ButtonBaseClickCommandBehavior GetOrCreateBehavior(ButtonBase buttonBase)
        {
            ButtonBaseClickCommandBehavior behavior = buttonBase.GetValue(ClickCommandBehaviorProperty) as ButtonBaseClickCommandBehavior;
            if (behavior == null)
            {
                behavior = new ButtonBaseClickCommandBehavior(buttonBase);
                buttonBase.SetValue(ClickCommandBehaviorProperty, behavior);
            }

            return behavior;
        }
    }
}
