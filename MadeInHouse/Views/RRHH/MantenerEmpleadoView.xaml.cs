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
using MadeInHouse.Models.RRHH;

namespace MadeInHouse.Views.RRHH
{
    /// <summary>
    /// Lógica de interacción para MantenerEmpleadoView.xaml
    /// </summary>
    /// 


    public partial class MantenerEmpleadoView : UserControl
    {
        public MantenerEmpleadoView()
        {
            InitializeComponent();

            List<Empleado> grid = new List<Empleado>();

            grid = DataObjects.RRHH.EmpleadoSQL.BuscarEmpleado("","","","","","");


            Lista.ItemsSource = grid;
            Lista.Items.Refresh();            
        }

        public void Refrescar(object sender, RoutedEventArgs e)
        {
            List<Empleado> grid = new List<Empleado>();

            grid = DataObjects.RRHH.EmpleadoSQL.BuscarEmpleado("","","","","","");


            Lista.ItemsSource = grid;
            Lista.Items.Refresh();
        }

        public void Buscar(object sender, RoutedEventArgs e)
        {
            List<Empleado> grid = new List<Empleado>();

            grid = DataObjects.RRHH.EmpleadoSQL.BuscarEmpleado(TxtNombre.Text, TxtApePaterno.Text,TxtDni.Text,CmbArea.Text,CmbPuesto.Text,CmbTienda.Text);


            Lista.ItemsSource = grid;
            Lista.Items.Refresh();
        }

        public void Limpiar(object sender, RoutedEventArgs e)
        {
            TxtNombre.Text = "";
            TxtApePaterno.Text = "";
            TxtDni.Text = "";
            CmbArea.Text = "";
            CmbPuesto.Text = "";
            CmbTienda.Text = "";
                
        }
        
        public void EliminarEmpleado(object sender, RoutedEventArgs e)
        {
            List<Empleado> grid = new List<Empleado>();
            int indice, k;

            grid = DataObjects.RRHH.EmpleadoSQL.BuscarEmpleado("", "", "", "", "", "");
            indice = Lista.SelectedIndex;

            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show("Esta seguro que desea eliminar a " + grid[indice].Nombre + " " + grid[indice].ApePaterno + " " +grid[indice].ApeMaterno, "Alerta", button, icon);

            if (result == MessageBoxResult.Yes)
            {
                k = DataObjects.RRHH.EmpleadoSQL.EliminarEmpleado(grid[indice].Dni);
                Refrescar(sender, e);
            }
        }

    }
}
