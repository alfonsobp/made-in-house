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

        public void GuardarProveedor()
        {
            MessageBox.Show("proveedor :  Codigo = " + txtCodigo + ", Razon social = " + txtRazonSocial + ", Ruc = " + txtRuc +
                            ", Telefono = " + txtTelefono + ", Direccion = " + txtDireccion); 
        }
    }
}
