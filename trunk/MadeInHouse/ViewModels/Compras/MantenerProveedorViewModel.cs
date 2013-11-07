using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Views.Compras;
using System.Windows;
using System.Collections.ObjectModel;
using MadeInHouse.Models.Compras;
using MadeInHouse.Models;
using MadeInHouse.DataObjects.Compras;

namespace MadeInHouse.ViewModels.Compras
{
    class MantenerProveedorViewModel: Screen
    {

        public MantenerProveedorViewModel(Proveedor p, BuscadorProveedorViewModel model) {

            txtCodigo = p.CodProveedor;
            txtContacto = p.Contacto;
            txtDireccion = p.Direccion;
            txtFax = p.Fax;
            txtTelefono = p.Telefono;
            txtTelefonoContacto = p.TelefonoContacto;
            txtRuc = p.Ruc;
            txtRazonSocial = p.RazonSocial;
            txtEmail = p.Email;
            Id = p.IdProveedor;
            //Editar
            indicador = 2;
            this.model = model;
             
        }

        int Id;
        BuscadorProveedorViewModel model;

        public MantenerProveedorViewModel(BuscadorProveedorViewModel model)
        {
            this.model = model;
            //Insertar
            indicador = 1;
        }

        public MantenerProveedorViewModel() {

            //Insertar
            indicador = 1;
        }

        private int indicador;
        //indicador = 1 para insertar
        //indicador = 2 para editar

        private MyWindowManager win = new MyWindowManager();

        private ProveedorSQL eM;

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

        public Boolean validar(Proveedor p) {

            if (p.Telefono ==null || p.Contacto == null|| p.Direccion == null || p.Email==null || p.Fax == null || p.RazonSocial == null || p.Ruc == null || p.TelefonoContacto == null)
            {
                MessageBox.Show("Tiene campos incompletos , rellenar porfavor");
                return false;
            }
            else
            {
                return true;  
            }
        }

        public void AgregarServicio()
        {
            if (indicador == 1)
            {
                List<Proveedor> lstProveedor = new ProveedorSQL().Buscar() as List<Proveedor>;
                txtCodigo = lstProveedor[lstProveedor.Count - 1].CodProveedor;

                Compras.agregarServicioViewModel obj = new Compras.agregarServicioViewModel(txtCodigo);
                win.ShowWindow(obj);
            }
        }
        
        public void GuardarProveedor()
        {
            int k;
            Proveedor p = new Proveedor();

           
                p.IdProveedor = Id;
                p.Contacto = TxtContacto;
                p.Direccion = TxtDireccion;
                p.Email =TxtEmail;
                p.Fax = TxtFax;
                p.RazonSocial = TxtRazonSocial;
                p.Ruc = TxtRuc;
                p.Telefono = TxtTelefono;
                p.TelefonoContacto = TxtTelefonoContacto;

             if (validar(p) == true) {

                eM =  new ProveedorSQL();

                if (indicador == 1)
                {


                    k = eM.Agregar(p);

                    if (k == 0)
                        MessageBox.Show("Ocurrio un error");
                    else
                    {
                        MessageBox.Show("Proveedor Registrado \n\nCodigo = " + txtCodigo + "\nRazon social = " + txtRazonSocial + "\nRuc = " + txtRuc +
                                      "\nTelefono = " + txtTelefono + "\nFax = " + txtFax + "\nContacto = " + txtContacto + "\nTelefono contacto = " +
                                      txtTelefonoContacto + "\nDireccion = " + txtDireccion);


                       
                    }

                }

                if (indicador == 2)
                {

                    k = eM.Actualizar(p);

                    if (k == 0)
                        MessageBox.Show("Ocurrio un error");
                    else
                    {
                        MessageBox.Show("Proveedor Editado \n\nCodigo = " + txtCodigo + "\nRazon social = " + txtRazonSocial + "\nRuc = " + txtRuc +
                                          "\nTelefono = " + txtTelefono + "\nFax = " + txtFax + "\nEmail = " + txtEmail + "\nContacto = " + txtContacto +
                                          "\nTelefono contacto = " + txtTelefonoContacto + "\nDireccion = " + txtDireccion);

                      
                    }
                }

                if (model != null)
                {
                    model.ActualizarProveedor();
                    this.TryClose();
                }

            }
            
        }
    }
}
