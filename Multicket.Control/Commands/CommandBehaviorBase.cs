using System;
using System.Windows;
using System.Windows.Input;

namespace Multicket.Module.Commands
{
    public class CommandBehaviorBase<T>
#if SILVERLIGHT
        where T : Control
#else
        where T : UIElement
#endif
    {
        private ICommand command;
        private object commandParameter;
        private readonly WeakReference targetObject;
        private readonly EventHandler commandCanExecuteChangedHandler;


        /// <summary>
        /// Constructor specifying the target object.
        /// </summary>
        /// <param name="targetObject">The target object the behavior is attached to.</param>
        public CommandBehaviorBase(T targetObject)
        {
            this.targetObject = new WeakReference(targetObject);

            // In Silverlight, unlike WPF, this is strictly not necessary since the Command properties
            // in Silverlight do not expect their CanExecuteChanged handlers to be weakly held,
            // but holding on to them in this manner should do no harm.
            commandCanExecuteChangedHandler = new EventHandler(CommandCanExecuteChanged);
        }

        /// <summary>
        /// Corresponding command to be execute and monitored for <see cref="ICommand.CanExecuteChanged"/>
        /// </summary>
        public ICommand Command
        {
            get { return command; }
            set
            {
                if (command != null)
                {
                    command.CanExecuteChanged -= commandCanExecuteChangedHandler;
                }

                command = value;
                if (command != null)
                {
                    command.CanExecuteChanged += commandCanExecuteChangedHandler;
                    UpdateEnabledState();
                }
            }
        }

        /// <summary>
        /// The parameter to supply the command during execution
        /// </summary>
        public object CommandParameter
        {
            get { return commandParameter; }
            set
            {
                if (commandParameter != value)
                {
                    commandParameter = value;
                    UpdateEnabledState();
                }
            }
        }

        /// <summary>
        /// Object to which this behavior is attached.
        /// </summary>
        protected T TargetObject
        {
            get
            {
                return targetObject.Target as T;
            }
        }


        /// <summary>
        /// Updates the target object's IsEnabled property based on the commands ability to execute.
        /// </summary>
        protected virtual void UpdateEnabledState()
        {
            if (TargetObject == null)
            {
                Command = null;
                CommandParameter = null;
            }
            else if (Command != null)
            {
                TargetObject.IsEnabled = Command.CanExecute(CommandParameter);
            }
        }

        private void CommandCanExecuteChanged(object sender, EventArgs e)
        {
            UpdateEnabledState();
        }

        /// <summary>
        /// Executes the command, if it's set, providing the <see cref="CommandParameter"/>
        /// </summary>
        protected virtual void ExecuteCommand()
        {
            if (Command != null)
            {
                Command.Execute(CommandParameter);
            }
        }
    }
}
