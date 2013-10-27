using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Views.Compras;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using MadeInHouse.Manager;
using MadeInHouse.Model;


namespace MadeInHouse.ViewModels.Compras
{
    class BuscadorProveedorViewModel : PropertyChangedBase
    {

        public BuscadorProveedorViewModel(){

            ActualizarProveedor();
        
        }

        private MyWindowManager win = new MyWindowManager();

        EntityManager eM = new TableManager().getInstance(EntityName.Proveedor);

        private string txtRuc;

        public string TxtRuc
        {
           
            get { return txtRuc; }
            set { txtRuc = value; NotifyOfPropertyChange(() => TxtRuc); }
        }

        private string txtRazonSocial;

        public string TxtRazonSocial
        {
            get { return txtRazonSocial; }
            set { txtRazonSocial = value; NotifyOfPropertyChange(() => TxtRazonSocial); }
        }

        private string txtCodigo;

        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }

        private DateTime fechaIni=new DateTime(DateTime.Now.Year, 1,1);

        public DateTime FechaIni
        {
            get { return fechaIni; }
            set { fechaIni = value; NotifyOfPropertyChange(() => FechaIni); }
        }


        private DateTime fechaFin=new DateTime(DateTime.Now.Year,12,31);

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; NotifyOfPropertyChange(() => FechaFin); }
        }

        

        private List<Proveedor> lstProveedor;

        public  List<Proveedor> LstProveedor
        {
            get { return lstProveedor; }
            set { lstProveedor = value; NotifyOfPropertyChange(() => LstProveedor); }
        }

        private Proveedor proveedorSeleccionado;
        
        public void SelectedItemChanged(object sender)
        {
            proveedorSeleccionado = ((sender as DataGrid).SelectedItem as Proveedor);
            
        }



        public void test() {

            MessageBox.Show("El proveedor tiene Codigo = " + proveedorSeleccionado.CodProveedor + " , Ruc = " + proveedorSeleccionado.Ruc + 
                            " , Razon Social = " + proveedorSeleccionado.RazonSocial);
        }
     
        public void NuevoProveedor()
        {        
            Compras.MantenerProveedorViewModel obj = new Compras.MantenerProveedorViewModel();   
            win.ShowWindow(obj);  
        }

        public void EditarProveedor()
        {
            Compras.MantenerProveedorViewModel obj = new Compras.MantenerProveedorViewModel(proveedorSeleccionado);
            win.ShowWindow(obj);
        }

        public void EliminarProveedor()
        {
            MessageBox.Show("Proveedor Eliminado \n\nCodigo = " + proveedorSeleccionado.CodProveedor + "\nRazon social = " + proveedorSeleccionado.RazonSocial + 
                            "\nRuc = " + proveedorSeleccionado.Ruc + "\nTelefono = " + proveedorSeleccionado.Telefono + "\nFax = " + proveedorSeleccionado.Fax + 
                            "\nContacto = " + proveedorSeleccionado.Contacto + "\nTelefono contacto = " + proveedorSeleccionado.TelefonoContacto + 
                            "\nDireccion = " + proveedorSeleccionado.Direccion);
            eM.Eliminar(proveedorSeleccionado);
            ActualizarProveedor();
        }

        public void BuscarProveedor() 
        {
            
    
            
            LstProveedor = eM.Buscar(TxtCodigo,TxtRuc,TxtRazonSocial,FechaIni,FechaFin) as List<Proveedor>;
           
 

        }

        public void ActualizarProveedor()
        {
            LstProveedor = eM.Buscar() as List<Proveedor>;
           
        }

    }
}
