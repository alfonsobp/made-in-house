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
    /// Lógica de interacción para RegistrarEmpleadoView.xaml
    /// </summary>
    public partial class RegistrarEmpleadoView : UserControl
    {
        private List<Empleado> grid;

        public RegistrarEmpleadoView()
        {
            InitializeComponent();
            TxtFechNac.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
            grid = new List<Empleado>();
            grid = DataObjects.RRHH.EmpleadoSQL.BuscarEmpleado("", "", "", "", "", "");
            TxtCodEmp.Text = TxtCodEmp.Text = "EMP-" + grid.Count;
            List<string> tiendas = new List<string>();
            tiendas = DataObjects.RRHH.EmpleadoSQL.Tiendas();
            string almCentral = "ALMACEN CENTRAL";
            tiendas.Insert(0,almCentral);
            CmbTienda.ItemsSource = tiendas;
            
            
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
            if (TxtFechNac.Text != "") emp.FechNacimiento = DateTime.Parse(TxtFechNac.Text);
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
            //emp.IdTienda = DataObjects.RRHH.EmpleadoSQL.GetIdTienda(emp.Tienda);
            if (String.Compare("ALMACEN CENTRAL", emp.Tienda) == 0) { 
                emp.IdTienda=0;
            }
            else{
                emp.IdTienda = DataObjects.RRHH.EmpleadoSQL.GetIdTienda(emp.Tienda);
            }

            MessageBox.Show("IDTIENDA: "+emp.IdTienda);

            emp.Area = CmbArea.Text;
            emp.EmailEmpresa = TxtEmailEmpresa.Text;
            if (TxtSalario.Text != "") emp.Sueldo = decimal.Parse(TxtSalario.Text, NumberStyles.AllowThousands
                                        | NumberStyles.AllowDecimalPoint | NumberStyles.AllowCurrencySymbol);
            else emp.Sueldo = 0;
            emp.Banco = TxtBanco.Text;
            emp.CuentaBancaria = TxtCuentaBancaria.Text;
            
            if (!Regex.IsMatch(emp.Dni, "^[0-9]{8}$")) { MessageBox.Show("Inserte un DNI valido"); }
            else if (revisarDNI(emp.Dni)) { MessageBox.Show("El DNI ya existe"); }
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
                int k;
                k = DataObjects.RRHH.EmpleadoSQL.agregarEmpleado(emp);

                if (k == 0)
                    MessageBox.Show("Ocurrio un error");
                else
                {
                    MessageBox.Show("Los datos han sido guardados exitosamente");
                    Limpiar(sender,e);
                }
            }

        }


        public void Limpiar(object sender, RoutedEventArgs e)
        {
            grid = DataObjects.RRHH.EmpleadoSQL.BuscarEmpleado("", "", "", "", "", "");
            TxtCodEmp.Text = TxtCodEmp.Text = "EMP-" + grid.Count;
            TxtCuentaBancaria.Text = "";
            TxtDni.Text = "";
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

        }
        
        
        private bool revisarDNI(string dni)
        {
            grid = DataObjects.RRHH.EmpleadoSQL.BuscarEmpleado("", "", "", "", "", "");
            int i = 0;
            for (i = 0; i<grid.Count ;i++ )
            {                
                if (grid[i].Dni == dni)
                {
                    
                    return true;
                }
            }
            
            return false;
        }


    }
}
