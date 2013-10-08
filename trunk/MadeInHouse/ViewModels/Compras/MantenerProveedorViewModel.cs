using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;
using MadeInHouse.Views.Compras;
using System.Windows;
using System.Collections.ObjectModel;

namespace MadeInHouse.ViewModels.Compras
{
    class MantenerProveedorViewModel:Screen
    {

        public MantenerProveedorViewModel(Proveedor p) {

            txtCodigo = p.Codigo;
            txtContacto = p.Contacto;
            txtDireccion = p.Direccion;
            txtFax = p.Fax;
            txtTelefono = p.Telefono;
            txtTelefonoContacto = p.TelefonoContacto;
            txtRuc = p.Ruc;
            txtRazonSocial = p.RazonSocial;
            txtEmail = p.Email;

            //Editar
            indicador = 2;
             
        }


        public MantenerProveedorViewModel()
        {
            //Insertar
            indicador = 1;
        }


        private int indicador;
        //indicador = 1 para insertar
        //indicador = 2 para editar

        public int Indicador
        {
            get { return indicador; }
        }

        private string txtCodigo;

        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }

        private string txtRazonSocial;

        public string TxtRazonSocial
        {
            get { return txtRazonSocial; }
            set { txtRazonSocial = value; NotifyOfPropertyChange(() => TxtRazonSocial); }
        }

        private string txtRuc;

        public string TxtRuc
        {
            get { return txtRuc; }
            set { txtRuc = value; NotifyOfPropertyChange(() => TxtRuc); }
        }

        private string txtTelefono;

        public string TxtTelefono
        {
            get { return txtTelefono; }
            set { txtTelefono = value; NotifyOfPropertyChange(() => TxtTelefono); }
        }

        private string txtFax;

        public string TxtFax
        {
            get { return txtFax; }
            set { txtFax = value; NotifyOfPropertyChange(() => TxtFax); }
        }

        private string txtContacto;

        public string TxtContacto
        {
            get { return txtContacto; }
            set { txtContacto = value; NotifyOfPropertyChange(() => TxtContacto); }
        }

        private string txtTelefonoContacto;

        public string TxtTelefonoContacto
        {
            get { return txtTelefonoContacto; }
            set { txtTelefonoContacto = value; NotifyOfPropertyChange(() => TxtTelefonoContacto); }
        }

        private string txtDireccion;

        public string TxtDireccion
        {
            get { return txtDireccion; }
            set { txtDireccion = value; NotifyOfPropertyChange(() => TxtDireccion); }
        }

        private string txtEmail;

        public string TxtEmail
        {
            get { return txtEmail; }
            set { txtEmail = value; NotifyOfPropertyChange(() => TxtEmail); }
        }

        public void GuardarProveedor()
        {
            if (indicador == 1)
                MessageBox.Show("Proveedor Registrado \n\nCodigo = " + txtCodigo + "\nRazon social = " + txtRazonSocial + "\nRuc = " + txtRuc +
                                "\nTelefono = " + txtTelefono + "\nFax = " + txtFax + "\nEmail = " + txtEmail + "\nContacto = " + txtContacto + 
                                "\nTelefono contacto = " + txtTelefonoContacto + "\nDireccion = " + txtDireccion); 
            else
                MessageBox.Show("Proveedor Editado \n\nCodigo = " + txtCodigo + "\nRazon social = " + txtRazonSocial + "\nRuc = " + txtRuc +
                                "\nTelefono = " + txtTelefono + "\nFax = " + txtFax + "\nEmail = " + txtEmail + "\nContacto = " + txtContacto + 
                                "\nTelefono contacto = " + txtTelefonoContacto + "\nDireccion = " + txtDireccion);
        }
    }
}
