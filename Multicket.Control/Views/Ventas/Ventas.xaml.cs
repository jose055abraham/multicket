using System.Windows.Controls;

namespace Multicket.Module.Views
{
    /// <summary>
    /// Lógica de interacción para Ventas.xaml
    /// </summary>
    public partial class Ventas : UserControl
    {
        public Ventas()
        {
            InitializeComponent();
        }

        private void KeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            ((TextBox)sender).Focus();
        }
    }
}
