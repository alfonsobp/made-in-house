using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Lógica de interacción para RegistrarEmpleadoView.xaml
    /// </summary>
    public partial class RegistrarEmpleadoView : UserControl
    {
        public RegistrarEmpleadoView()
        {
            InitializeComponent();
        }


        public void GuardarDatos(object sender, RoutedEventArgs e)
        {
            Empleado emp;
            emp = new Empleado();

            emp.Dni = TxtDni.Text;
            emp.Nombre = TxtNomb.Text;
            emp.ApePaterno = TxtApePat.Text;
            emp.ApeMaterno = TxtApeMat.Text;
            emp.FechNacimiento = TxtFechNac.Text;
            emp.Sexo = "M";
            if (RdbFem.IsChecked == true) emp.Sexo = "F";
            emp.Direccion = TxtDir.Text;
            emp.Referecia = TxtRef.Text;
            emp.Telefono = TxtTelef.Text;
            emp.Celular = TxtCel.Text;
            emp.EmailEmpleado = TxtEmail.Text;
            emp.Estado = 1;
                DateTime d = DateTime.Now;
            emp.FechaReg = d.ToString("dd/MM/yyyy ");
            emp.Puesto = CmbPuesto.Text;
            emp.Tienda = CmbTienda.Text;
            emp.Area = CmbArea.Text;
            emp.EmailEmpresa = TxtEmailEmpresa.Text;
            if (TxtSalario.Text != "") emp.Sueldo = decimal.Parse(TxtSalario.Text, NumberStyles.AllowThousands
                                        | NumberStyles.AllowDecimalPoint | NumberStyles.AllowCurrencySymbol);
            else emp.Sueldo = 0;
            emp.Banco = TxtBanco.Text;
            emp.CuentaBancaria = TxtCuentaBancaria.Text;

            if (emp.Dni == "") { MessageBox.Show("Inserte un DNI valido");}
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
                k = DataObjects.RrhhSQL.agregarEmpleado(emp);

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
            TxtCodEmp.Text = "";
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
            

        }
        



    }
}
