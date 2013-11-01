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
        private ClienteSQL VentasSQ;
        private MyWindowManager win = new MyWindowManager();

        private string txtDNI;

        public string TxtDNI
        {
            get { return txtDNI; }
            set { txtDNI = value; NotifyOfPropertyChange(() => TxtDNI); }
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

        public DateTime FecNacimiento
        {
            get { return fecNacimiento; }
            set { fecNacimiento = value; NotifyOfPropertyChange(() => FecNacimiento); }
        }

        public void GuardarCliente()
        {
            Cliente a = new Cliente();

            a.dni = txtDNI;
            a.nombre = txtNombre;
            a.apePaterno = txtApPat;
            a.apeMaterno = txtApMat;
            //a.sexo = Int32.Parse(txtSexo);
            a.direccion = txtDireccion;
            a.telefono = txtTelf;
            a.ruc = txtRUC;
            a.razonSocial = txtRazonSocial;
            a.estado = 1;
            a.distrito = txtDist;
            a.fechaNacimiento = fecNacimiento;
            a.fecNacimiento = fecNacimiento.ToString();
            MessageBox.Show(a.fecNacimiento);

            int k = DataObjects.Ventas.ClienteSQL.agregarCliente(a, DateTime.Today);
            if (k == 0)
                MessageBox.Show("Ocurrio un error");
            else
            {
                Int32 id = DataObjects.Ventas.ClienteSQL.buscarIDCliente(a);
                //MessageBox.Show("Cliente Registrado \n\n Nombre = " + id);
                int j = DataObjects.Ventas.ClienteSQL.RegistrarTarjeta(id,DateTime.Today,a.dni);
                MessageBox.Show("Cliente Registrado \n\n Nombre = " + txtNombre);
            }
        }
    }
}
