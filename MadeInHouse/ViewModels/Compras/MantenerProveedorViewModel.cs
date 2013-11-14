using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Views.Compras;
using System.Windows;
using System.Collections.ObjectModel;
using MadeInHouse.Models.Compras;
using MadeInHouse.Models;
using MadeInHouse.DataObjects.Compras;
using System.ComponentModel;

namespace MadeInHouse.ViewModels.Compras
{
    class MantenerProveedorViewModel: Screen , IDataErrorInfo
    {


        #region ATRIBUTOS

        int Id;
        BuscadorProveedorViewModel model;

        private MyWindowManager win = new MyWindowManager();
        

        private ProveedorSQL eM;

        private int indicador;
     
        public int Indicador
        {
            get { return indicador; }
        }

        private string txtCodigo="PROV-1XXXXXXXXX";

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

        private string txtEmail="";

        public string TxtEmail
        {
            get { return txtEmail; }
            set { txtEmail = value; NotifyOfPropertyChange(() => TxtEmail); }
        }

        #endregion


        public MantenerProveedorViewModel(Proveedor p, BuscadorProveedorViewModel model)
        {

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
        public MantenerProveedorViewModel(BuscadorProveedorViewModel model)
        {
            this.model = model;
            //Insertar
            indicador = 1;
        }
        public MantenerProveedorViewModel()
        {

            //Insertar
            indicador = 1;
        }

        private string EvaluarRuc() {

            if (String.IsNullOrEmpty(TxtRuc))
                return "No puede ser vacio el Ruc";

            if (TxtRuc.Length != 11)
                return "El Ruc es de 11 digitos";

            return string.Empty;
        }


public string this[string columnName]
{
    get
    {
        string result = string.Empty;
        switch (columnName)
        {
            case "TxtEmail": if (string.IsNullOrEmpty(TxtEmail)) result="El Email no puede ser vacio" ; break;
            case "TxtRuc":  result = EvaluarRuc(); break;
            case "TxtTelefono": if (string.IsNullOrEmpty(TxtTelefono)) result = "El telefono no debe ser vacio"; break;
            case "TxtDireccion": if (string.IsNullOrEmpty(TxtDireccion)) result = "La direccion no puede ser vacia"; break;
            case "TxtRazonSocial": if (string.IsNullOrEmpty(TxtRazonSocial)) result = "La Razon Social no puede ser vacia"; break;
            case "TxtTelefonoContacto": if (string.IsNullOrEmpty(TxtTelefonoContacto)) result = "El telefono del contacto no debe ser vacio"; break;
            case "TxtContacto": if (string.IsNullOrEmpty(TxtContacto)) result = "El nombre del contacto no debe ser vacio"; break;
            case "TxtFax": if (string.IsNullOrEmpty(TxtFax)) result = "El Fax no puede ser vacio"; break;
        };
        return result;
    }
}


        public Boolean validar() {

            if (string.IsNullOrEmpty(TxtEmail))
            {
               MessageBox.Show("El Email no puede ser vacio", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }

            if (string.IsNullOrEmpty(TxtRuc))
            {
                MessageBox.Show("El Ruc no puede ser vacio", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if ((TxtRuc.Length != 11))
            {
                MessageBox.Show("El Ruc es de 11 digitos", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrEmpty(TxtTelefono))
            {
                MessageBox.Show("El telefono no puede ser vacio", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(TxtDireccion))
            {
                MessageBox.Show("La direccion no puede ser vacia", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(TxtRazonSocial))
            {
                MessageBox.Show("La Razon social no puede ser vacio", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(TxtTelefonoContacto))
            {
                MessageBox.Show("El telefono del contacto no puede ser vacio", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(TxtContacto))
            {
                MessageBox.Show("El nombre del contacto no puede ser vacio", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(TxtFax))
            {
                MessageBox.Show("El Fax no puede ser vacio", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

             return true;

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

            if (validar()  )
            {
                p.IdProveedor = Id;
                p.Contacto = TxtContacto;
                p.Direccion = TxtDireccion;
                p.Email =TxtEmail; 
                p.Fax = TxtFax;
                p.RazonSocial = TxtRazonSocial;
                p.Ruc = TxtRuc;
                p.Telefono = TxtTelefono;
                p.TelefonoContacto = TxtTelefonoContacto;

               

                eM =  new ProveedorSQL();

                if (indicador == 1)
                {


                    k = eM.Agregar(p);

                    if (k == 0)
                        MessageBox.Show("Ocurrio un error","AVISO");
                    else
                    {
                        MessageBox.Show("La informacion del Proveedor fue Ingresada  satisfactoriamente .", "AVISO", MessageBoxButton.OK, MessageBoxImage.Information);


                       
                    }

                }

                if (indicador == 2)
                {

                    k = eM.Actualizar(p);

                    if (k == 0)
                        MessageBox.Show("Ocurrio un error");
                    else
                    {
                        MessageBox.Show("La informacion del Proveedor fue editada  satisfactoriamente .", "AVISO", MessageBoxButton.OK, MessageBoxImage.Information);
                      
                    }
                }

                if (model != null)
                {
                    model.ActualizarProveedor();
                  
                }

                this.TryClose();

            }
            
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
