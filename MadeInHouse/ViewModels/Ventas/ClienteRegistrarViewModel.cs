using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Ventas;
using MadeInHouse.Views.Ventas;
using System.Windows;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using MadeInHouse.DataObjects.Ventas;
using MadeInHouse.Models;

namespace MadeInHouse.ViewModels.Ventas
{
    class ClienteRegistrarViewModel : PropertyChangedBase
    {

        private MyWindowManager win = new MyWindowManager();

        private int ind = 0;

        private string txtDNI;

        public string TxtDNI
        {
            get { return txtDNI; }
            set { txtDNI = value; NotifyOfPropertyChange(() => TxtDNI); }
        }

        public ClienteRegistrarViewModel(ClienteBuscarViewModel m, int idcliente)
        {
            Tarjeta t = DataObjects.Ventas.ClienteSQL.BuscarClientePorId(idcliente);
            //LstRol = clienteSQL.ListarRol();
            txtDNI = t.Cliente.Dni;
            txtNombre = t.Cliente.Nombre;
            txtApPat = t.Cliente.ApePaterno;
            txtApMat = t.Cliente.ApeMaterno;
            txtRUC = t.Cliente.Ruc;
            txtRazonSocial = t.Cliente.RazonSocial;
            txtTelf = t.Cliente.Telefono;
            txtDist = t.Cliente.Distrito;
            txtDireccion = t.Cliente.Direccion;
            fecNacimiento = t.Cliente.FechaNacimiento;
            txtCelular = t.Cliente.Celular;
            txtMail = t.Cliente.Email;

            //txtCodUsuario = t.CodEmpleado.ToString();
            //Deshabilitar escritura en txtCodUsuario
            //BTxtCodUsuario = u.CodEmpleado;
            ind = 2;
        }

        public ClienteRegistrarViewModel()
        {
            // TODO: Complete member initialization
            ind = 1;
        }

        public ClienteRegistrarViewModel(VentaRegistrarViewModel ventaRegistrarViewModel)
        {
            // TODO: Complete member initialization
            this.ventaRegistrarViewModel = ventaRegistrarViewModel;
        }

        private string txtNombre;

        public string TxtNombre
        {
            get { return txtNombre; }
            set { txtNombre = value; NotifyOfPropertyChange(() => TxtNombre); }
        }

        private string txtApPat;

        public string TxtApPat
        {
            get { return txtApPat; }
            set { txtApPat = value; NotifyOfPropertyChange(() => TxtApPat); }
        }

        /* private string txtSexo;

         public string TxtSexo
         {
             get { return txtSexo; }
             set { txtSexo = value; NotifyOfPropertyChange(() => TxtSexo); }
         }*/

        private string txtRUC;

        public string TxtRUC
        {
            get { return txtRUC; }
            set { txtRUC = value; NotifyOfPropertyChange(() => TxtRUC); }
        }

        private string txtRazonSocial;

        public string TxtRazonSocial
        {
            get { return txtRazonSocial; }
            set { txtRazonSocial = value; NotifyOfPropertyChange(() => TxtRazonSocial); }
        }

        private string txtApMat;

        public string TxtApMat
        {
            get { return txtApMat; }
            set { txtApMat = value; NotifyOfPropertyChange(() => TxtApMat); }
        }

        private string txtTelf;

        public string TxtTelf
        {
            get { return txtTelf; }
            set { txtTelf = value; NotifyOfPropertyChange(() => TxtTelf); }
        }

        private string txtCelular;

        public string TxtCelular
        {
            get { return txtCelular; }
            set { txtCelular = value; NotifyOfPropertyChange(() => TxtCelular); }
        }

        private string txtMail;

        public string TxtMail
        {
            get { return txtMail; }
            set { txtMail = value; NotifyOfPropertyChange(() => TxtMail); }
        }

        private string txtDist;

        public string TxtDist
        {
            get { return txtDist; }
            set { txtDist = value; NotifyOfPropertyChange(() => TxtDist); }
        }

        private string txtDireccion;

        public string TxtDireccion
        {
            get { return txtDireccion; }
            set { txtDireccion = value; NotifyOfPropertyChange(() => TxtDireccion); }
        }

        private DateTime fecNacimiento;
        private VentaRegistrarViewModel ventaRegistrarViewModel;

        public DateTime FecNacimiento
        {
            get { return fecNacimiento; }
            set { fecNacimiento = value; NotifyOfPropertyChange(() => FecNacimiento); }
        }

        private Dictionary<string, int> sexo = new Dictionary<string, int>()
        {
            { "Varón", 0 }, { "Mujer", 1 }
        };

        public BindableCollection<string> cmbSexo
        {
            get
            {
                return new BindableCollection<string>(sexo.Keys);
            }
        }

        public void GuardarCliente(String cmbSexo)
        {
            Cliente a = new Cliente();

            a.Dni = txtDNI;
            a.Nombre = txtNombre;
            a.ApePaterno = txtApPat;
            a.ApeMaterno = txtApMat;
            a.Sexo = sexo[cmbSexo];
            a.Direccion = txtDireccion;
            a.Telefono = txtTelf;
            a.Ruc = txtRUC;
            a.RazonSocial = txtRazonSocial;
            a.Estado = 1;
            a.Distrito = txtDist;
            a.FechaNacimiento = fecNacimiento;
            a.FecNacimiento = fecNacimiento.ToString();
            a.Celular = txtCelular;
            a.Email = txtMail;
            if (String.IsNullOrEmpty(txtRazonSocial))
            {
                a.TipoCliente = 0;
            }
            else
            {
                a.TipoCliente = 1;
            }
            MessageBox.Show(a.FecNacimiento + " " + a.Dni);

            if (ind == 1)
            {
                int k = DataObjects.Ventas.ClienteSQL.agregarCliente(a, DateTime.Today);
                if (k == 0)
                    MessageBox.Show("Ocurrio un error al grabar el cliente");
                else
                {
                    Int32 id = DataObjects.Ventas.ClienteSQL.BuscarIDCliente(a);
                    //MessageBox.Show("Cliente Registrado \n\n Nombre = " + id);
                    int j = DataObjects.Ventas.ClienteSQL.RegistrarTarjeta(id, DateTime.Today, a.Dni);
                    MessageBox.Show("Cliente Registrado \n\n Nombre = " + txtNombre);
                }
            }
            else
            {
                Int32 id = DataObjects.Ventas.ClienteSQL.BuscarIDCliente(a);
                int k = DataObjects.Ventas.ClienteSQL.editarCliente(a, id);
                if (k == 0)
                    MessageBox.Show("Ocurrio un error");
                else
                {
                    //MessageBox.Show("Cliente Registrado \n\n Nombre = " + id);
                    //int j = DataObjects.Ventas.ClienteSQL.ActualizarEstadoTarjeta(id);
                    MessageBox.Show("Cliente Actualizado \n\n Nombre = " + txtNombre);
                }
            }
        }

        public string DisplayName { get; set; }
    }
}
