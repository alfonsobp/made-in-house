using System.Windows;
using System.Windows.Controls;

namespace MadeInHouse.Views.Almacen
{
    /// <summary>
    /// Lógica de interacción para SolicitudAbRegistrarView.xaml
    /// </summary>
    public partial class SolicitudAbRegistrarView : UserControl
    {
        public SolicitudAbRegistrarView()
        {
            InitializeComponent();
        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }

            var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if (mainPanelBorder != null)
            {
                mainPanelBorder.Margin = new Thickness(0);
            }
        }
    }
}
