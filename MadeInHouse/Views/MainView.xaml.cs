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
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace MadeInHouse.Views
{
    /// <summary>
    /// Lógica de interacción para MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void CloseWin_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void cambiarExpander(object sender, RoutedEventArgs e)
        {
            if (sender == butAlmacen)
            {
                comeIn(expanderRRHH);
                comeIn(expanderSeguridad);
                comeIn(expanderClima);
                comeIn(expanderCompras);
                comeIn(expanderReportes);
                comeIn(expanderVentas);
                comeIn(expanderConfiguracion);
                goOut(expanderAlmacen);
            }
            if (sender == butRRHH)
            {
                comeIn(expanderAlmacen);
                comeIn(expanderSeguridad);
                comeIn(expanderClima);
                comeIn(expanderCompras);
                comeIn(expanderReportes);
                comeIn(expanderVentas);
                comeIn(expanderConfiguracion);
                goOut(expanderRRHH);
            }
            if (sender == butSeguridad)
            {
                comeIn(expanderRRHH);
                comeIn(expanderAlmacen);
                comeIn(expanderClima);
                comeIn(expanderCompras);
                comeIn(expanderReportes);
                comeIn(expanderVentas);
                comeIn(expanderConfiguracion);
                goOut(expanderSeguridad);
            }
            if (sender == butCompras)
            {
                comeIn(expanderRRHH);
                comeIn(expanderAlmacen);
                comeIn(expanderClima);
                comeIn(expanderSeguridad);
                comeIn(expanderReportes);
                comeIn(expanderVentas);
                comeIn(expanderConfiguracion);
                goOut(expanderCompras);
            }
            if (sender == butVentas)
            {
                comeIn(expanderRRHH);
                comeIn(expanderAlmacen);
                comeIn(expanderClima);
                comeIn(expanderCompras);
                comeIn(expanderReportes);
                comeIn(expanderSeguridad);
                comeIn(expanderConfiguracion);
                goOut(expanderVentas);
            }
            if (sender == butReportes)
            {
                comeIn(expanderRRHH);
                comeIn(expanderSeguridad);
                comeIn(expanderClima);
                comeIn(expanderCompras);
                comeIn(expanderAlmacen);
                comeIn(expanderVentas);
                comeIn(expanderConfiguracion);
                goOut(expanderReportes);
            }
            if (sender == butConfiguracion)
            {
                comeIn(expanderRRHH);
                comeIn(expanderSeguridad);
                comeIn(expanderClima);
                comeIn(expanderCompras);
                comeIn(expanderAlmacen);
                comeIn(expanderVentas);
                comeIn(expanderReportes);
                goOut(expanderConfiguracion);
            }
            if (sender == butClima)
            {
                comeIn(expanderRRHH);
                comeIn(expanderSeguridad);
                comeIn(expanderReportes);
                comeIn(expanderCompras);
                comeIn(expanderAlmacen);
                comeIn(expanderVentas);
                comeIn(expanderConfiguracion);
                goOut(expanderClima);
            }
        }
        private void agrandar(object sender, RoutedEventArgs e)
        {

            Button B = (Button)sender;
            B.Width = 120;
            B.Margin = new Thickness(B.Margin.Left - 10, B.Margin.Top - 20, 0, 0);
            B.Height = 120;


        }
        private void achicar(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender;
            B.Width = 100;
            B.Margin = new Thickness(B.Margin.Left + 10, B.Margin.Top + 20, 0, 0);
            B.Height = 100;
        }

        private void comeIn(object expander)
        {
            Expander E = (Expander)expander;
            DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.25));
            E.BeginAnimation(Expander.WidthProperty, animation);
        }
        private void goOut(object expander)
        {
            Expander E = (Expander)expander;
            DoubleAnimation animation = new DoubleAnimation(300, TimeSpan.FromSeconds(0.25));
            E.BeginAnimation(Expander.WidthProperty, animation);
        }

    }
}
