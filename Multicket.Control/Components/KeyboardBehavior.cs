using Microsoft.Xaml.Behaviors;
using System.Windows.Input;
using System.Windows;

namespace Multicket.Module.Components
{
    public class KeyboardBehavior : Behavior<FrameworkElement>
    {

        public Key KeyType
        {
            get { return (Key)GetValue(KeyTypeProperty); }
            set { SetValue(KeyTypeProperty, value); }
        }

        public static readonly DependencyProperty KeyTypeProperty =  
            DependencyProperty.Register(
                nameof(KeyType), 
                typeof(Key), 
                typeof(KeyboardBehavior), 
                new PropertyMetadata());

        public ICommand KeyDown
        {
            get { return (ICommand)GetValue(KeyDownProperty); }
            set { SetValue(KeyDownProperty, value); }
        }

        public static readonly DependencyProperty KeyDownProperty =
            DependencyProperty.Register(
                nameof(KeyDown), 
                typeof(ICommand), 
                typeof(KeyboardBehavior),
                new PropertyMetadata());

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.KeyDown += OnKeyDown;
            //var window = Application.Current.MainWindow;
            //if (window == null) return;
            //window.KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var isSystemKey = e.SystemKey == KeyType;
            var isKeyType = e.Key == KeyType;
            var canExecute = KeyDown.CanExecute(null);

            if (isSystemKey && canExecute)
            {
                KeyDown.Execute(null);
            }

            if (isKeyType && canExecute)
            {
                KeyDown.Execute(null);
            } 
            
            
        }
    }
}
