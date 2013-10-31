using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MadeInHouse.Views.Seguridad
{
    /// <summary>
    /// Lógica de interacción para ConfigurarUsuarioView.xaml
    /// </summary>
    public partial class ConfigurarUsuarioView : UserControl
    {
        public ConfigurarUsuarioView()
        {
            InitializeComponent();
        }

        private void psbPassActual_PasswordChanged(object sender, RoutedEventArgs e)
        {
            TxtPassActual.Text = psbPassActual.Password;
        }

        private void psbPassNuevo1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            TxtPassNuevo1.Text = psbPassNuevo1.Password;
        }

        private void psbPassNuevo2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            TxtPassNuevo2.Text = psbPassNuevo2.Password;
        }
    }
}
