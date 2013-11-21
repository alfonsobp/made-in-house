using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para EditarEmpleadoView.xaml
    /// </summary>
    public partial class EditarEmpleadoView : UserControl
    {
        private List<Empleado> grid;

        public EditarEmpleadoView()
        {            
            InitializeComponent();
            grid = new List<Empleado>();
            grid = DataObjects.RRHH.EmpleadoSQL.BuscarEmpleado("", "",RRHH.MantenerEmpleadoView.INDICEMP, "", "", "");

            TxtCodEmp.Text = grid[0].CodEmpleado;
            TxtDni.Text = grid[0].Dni;
            TxtNomb.Text = grid[0].Nombre;
            TxtApePat.Text = grid[0].ApePaterno;
            TxtApeMat.Text = grid[0].ApeMaterno;
            TxtFechNac.Text = grid[0].FechNacimiento.ToString();
            if (grid[0].Sexo == "M") RdbMasc.IsChecked = true; else RdbFem.IsChecked = true;
            TxtDir.Text = grid[0].Direccion;
            TxtRef.Text = grid[0].Referecia;
            TxtTelef.Text = grid[0].Telefono;
            TxtCel.Text = grid[0].Celular;
            TxtEmail.Text = grid[0].EmailEmpleado;

            CmbPuesto.Text = grid[0].Puesto;
            
            CmbArea.Text = grid[0].Area;
            TxtEmailEmpresa.Text = grid[0].EmailEmpresa;
            TxtSalario.Text = grid[0].Sueldo + "";
            TxtBanco.Text = grid[0].Banco;
            TxtCuentaBancaria.Text = grid[0].CuentaBancaria;

            List<string> tiendas = new List<string>();
            tiendas = DataObjects.RRHH.EmpleadoSQL.Tiendas();
            CmbTienda.ItemsSource = tiendas;
            CmbTienda.Text = grid[0].Tienda;
        }

        public void GuardarDatos(object sender, RoutedEventArgs e)
        {

            Empleado emp;
            emp = new Empleado();
            emp.CodEmpleado = TxtCodEmp.Text;
            emp.Dni = TxtDni.Text;
            emp.Nombre = TxtNomb.Text;
            emp.ApePaterno = TxtApePat.Text;
            emp.ApeMaterno = TxtApeMat.Text;
            emp.FechNacimiento = DateTime.Parse(TxtFechNac.Text);
            emp.Sexo = "M";
            if (RdbFem.IsChecked == true) emp.Sexo = "F";
            emp.Direccion = TxtDir.Text;
            emp.Referecia = TxtRef.Text;
            emp.Telefono = TxtTelef.Text;
            emp.Celular = TxtCel.Text;
            emp.EmailEmpleado = TxtEmail.Text;
            emp.Estado = 1;
            emp.FechaReg = DateTime.Today;
            emp.Puesto = CmbPuesto.Text;
            emp.Tienda = CmbTienda.Text;    //esto es el nombre de la Tienda seleccionada
            emp.IdTienda = DataObjects.RRHH.EmpleadoSQL.GetIdTienda(emp.Tienda);
            DataObjects.RRHH.EmpleadoSQL.ActualizarTiendaEnUsuario(emp.CodEmpleado, emp.IdTienda);

            emp.Area = CmbArea.Text;
            emp.EmailEmpresa = TxtEmailEmpresa.Text;
            if (TxtSalario.Text != "") emp.Sueldo = decimal.Parse(TxtSalario.Text, NumberStyles.AllowThousands
                                        | NumberStyles.AllowDecimalPoint | NumberStyles.AllowCurrencySymbol);
            else emp.Sueldo = 0;
            emp.Banco = TxtBanco.Text;
            emp.CuentaBancaria = TxtCuentaBancaria.Text;

            if (!Regex.IsMatch(emp.Dni, "^[0-9]{8}$")) { MessageBox.Show("Inserte un DNI valido"); }
            else if (emp.CodEmpleado == "") { MessageBox.Show("Inserte un codigo valido"); }
            else if (emp.Nombre == "") { MessageBox.Show("Inserte un nombre valido"); }
            else if (emp.ApePaterno == "") { MessageBox.Show("Inserte un apellido paterno valido"); }
            else if (emp.ApeMaterno == "") { MessageBox.Show("Inserte un apellido materno valido"); }
            else if (emp.Direccion == "") { MessageBox.Show("Inserte una direccion valida"); }
            else if (emp.Telefono == "") { MessageBox.Show("Inserte un telefono valido"); }
            else if (emp.Tienda == "") { MessageBox.Show("Inserte un nombre valido"); }
            else if (emp.Puesto == "") { MessageBox.Show("Inserte un puesto valido"); }
            else if (emp.Area == "") { MessageBox.Show("Inserte una area valida"); }
            else if (emp.Banco == "") { MessageBox.Show("Inserte un banco valido"); }
            else if (emp.CuentaBancaria == "") { MessageBox.Show("Inserte una cuenta bancaria valida"); }

            else
            {
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result = MessageBox.Show("Esta seguro que desea actualizar los datos de " + emp.Nombre + " " + emp.ApePaterno + " " + emp.ApeMaterno, "Alerta", button, icon);

                if (result == MessageBoxResult.Yes)
                {
                    int k;
                    k = DataObjects.RRHH.EmpleadoSQL.editarEmpleado(emp);

                    if (k == 0)
                        MessageBox.Show("Ocurrio un error");
                    else
                    {
                        MessageBox.Show("Los datos han sido guardados exitosamente");
                        Limpiar(sender, e);
                    }
                }
            }

        }


        public void Limpiar(object sender, RoutedEventArgs e)
        {
           
            TxtNomb.Text = "";
            TxtApePat.Text = "";
            TxtApeMat.Text = "";
            TxtDir.Text = "";

            TxtRef.Text = "";
            TxtRutFoto.Text = "";
            TxtCel.Text = "";

            TxtEmail.Text = "";
            TxtTelef.Text = "";

            TxtEmailEmpresa.Text = "";
            TxtSalario.Text = "";

            TxtBanco.Text = "";
            TxtBanco.Text = "";
            CmbArea.Text = "";
            CmbPuesto.Text = "";
            CmbTienda.Text = "";
            TxtCuentaBancaria.Text = "";

        }
        


    }
}
