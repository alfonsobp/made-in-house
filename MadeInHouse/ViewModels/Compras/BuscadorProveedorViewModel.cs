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
            set { fechaIni = value; NotifyOfPropertyChange(() => FechaFin); }
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



     
        public void NuevoProveedor()
        {

            
            Compras.MantenerProveedorViewModel obj = new Compras.MantenerProveedorViewModel { DisplayName = "Nuevo Proveedor" };
            win.ShowWindow(obj);
           
           

        }
        public void EditarProveedor()
        {


            Compras.MantenerProveedorViewModel obj = new Compras.MantenerProveedorViewModel { DisplayName = "Editar Proveedor" };
            win.ShowWindow(obj);
        }

        public void BuscarProveedor() {

            MessageBox.Show("proveedor :  Codigo = " + txtCodigo + ", Razon social = " + txtRazonSocial + ", Ruc = "+txtRuc + ", Fecha Inicial = "+fechaIni + ", Fecha Fin = "+fechaFin);

            List<Proveedor> e = new List<Proveedor>();
            e.Add(new Proveedor("121212","Ladrillos San Jorge","999999999"));
            e.Add(new Proveedor("121212","Ladrillos San Jorge","999999999"));
            e.Add(new Proveedor("121212","Ladrillos San Jorge","999999999"));
            e.Add(new Proveedor("121212","Ladrillos San Jorge","999999999"));

            lstProveedor = e;
            NotifyOfPropertyChange("LstProveedor");
        }

    }
}
