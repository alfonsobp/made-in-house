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
using System.Windows.Controls;


namespace MadeInHouse.ViewModels.Compras
{
    class BuscadorProveedorViewModel : Screen
    {
        private MyWindowManager win = new MyWindowManager();

      
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

        private string fechaIni;

        public string FechaIni
        {
            get { return fechaIni; }
            set { fechaIni = value; NotifyOfPropertyChange(() => FechaIni); }
        }

        private string fechaFin;

        public string FechaFin
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

            MessageBox.Show("El proveedor tiene Codigo = " + proveedorSeleccionado.Codigo + " , Ruc = " + proveedorSeleccionado.Ruc + 
                            " , Razon Social = " + proveedorSeleccionado.RazonSocial);
        }
     
        public void NuevoProveedor()
        {        
            Compras.MantenerProveedorViewModel obj = new Compras.MantenerProveedorViewModel { DisplayName = "Nuevo Proveedor" };   
            win.ShowWindow(obj);  
        }

        public void EditarProveedor()
        {
            Compras.MantenerProveedorViewModel obj = new Compras.MantenerProveedorViewModel(proveedorSeleccionado);
            win.ShowWindow(obj);
        }

        public void EliminarProveedor()
        {
            MessageBox.Show("Proveedor Eliminado \n\nCodigo = " + proveedorSeleccionado.Codigo + "\nRazon social = " + proveedorSeleccionado.RazonSocial + 
                            "\nRuc = " + proveedorSeleccionado.Ruc + "\nTelefono = " + proveedorSeleccionado.Telefono + "\nFax = " + proveedorSeleccionado.Fax + 
                            "\nContacto = " + proveedorSeleccionado.Contacto + "\nTelefono contacto = " + proveedorSeleccionado.TelefonoContacto + 
                            "\nDireccion = " + proveedorSeleccionado.Direccion);
            DataObjects.ComprasSQL.eliminarProveedor(proveedorSeleccionado);
        }

        public void BuscarProveedor() 
        {

          //  MessageBox.Show("Proveedor Buscado \n\nCodigo =" + txtCodigo + "\nRazon social = " + txtRazonSocial + "\nRuc = " + txtRuc + 
          //                "\n Fecha Inicial = " + fechaIni + "\n Fecha Fin = " + fechaFin);

          //  List<Proveedor> e = new List<Proveedor>();
          //  e.Add(new Proveedor("121212", "Ladrillos San Jorge", "999999991", "986689107", "Fax", "KK@gmail", "Carloncho", "555-555", "Jr Hola"));
         //  e.Add(new Proveedor("121212", "Ladrillos San Jorge", "999999991", "986689107", "Fax", "KK@gmail", "Carloncho", "555-555", "Jr Hola"));
          //  e.Add(new Proveedor("121212", "Ladrillos San Jorge", "999999991", "986689107", "Fax", "KK@gmail", "Carloncho", "555-555", "Jr Hola"));
          //  e.Add(new Proveedor("121212", "Ladrillos San Jorge", "999999991", "986689107", "Fax", "KK@gmail", "Carloncho", "555-555", "Jr Hola"));

         //   lstProveedor = e;

            lstProveedor = DataObjects.ComprasSQL.BuscarProveedor(null,null,null,null,null);
            NotifyOfPropertyChange("LstProveedor");

        }

        public void ActualizarProveedor()
        {
            lstProveedor = DataObjects.ComprasSQL.BuscarProveedor(null, null, null, null, null);
            NotifyOfPropertyChange("LstProveedor");
        }

    }
}
