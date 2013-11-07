﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Compras;
using MadeInHouse.ViewModels.Compras;

namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerNotaDeIngresoViewModel:PropertyChangedBase
    {
        MyWindowManager win = new MyWindowManager();
        DataObjects.Almacen.ProductoxSolicitudAbSQL gateWay = new ProductoxSolicitudAbSQL();
        ProductoSQL pxaSQL;
        public MantenerNotaDeIngresoViewModel(){
            pxaSQL = new ProductoSQL();
            this.cmbMotivo = DataObjects.Almacen.MotivoSQL.BuscarMotivos();
            AlmacenSQL aGW = new AlmacenSQL();
            Models.Almacen.Almacenes a = aGW.BuscarAlmacen(4);
            List <Models.Almacen.Almacenes> al = new List<Models.Almacen.Almacenes>();
            al.Add(a);
            this.almacen = al;
        }

        string txtDoc;

        public string TxtDoc
        {
            get { return txtDoc; }
            set { txtDoc = value;
            NotifyOfPropertyChange(() => TxtCodPro);
            }
        }

        int txtCod;

        public int TxtCod
        {
            get { return txtCod; }
            set { txtCod = value; }
        }
        List<Motivo> cmbMotivo = null;

        public List<Motivo> CmbMotivo
        {
            get { return cmbMotivo; }
            set { cmbMotivo = value; }
        }

        string selectedMotivo;

        public string SelectedMotivo
        {
            get { return selectedMotivo; }
            set { selectedMotivo = value; }
        }

        Producto selectedProducto;
        
        public Producto SelectedProducto
        {
            get { return selectedProducto; }
            set { selectedProducto = value;
            NotifyOfPropertyChange(() => SelectedProducto);
            }
        }


        OrdenCompra selectedOrden;

        public OrdenCompra SelectedOrden
        {
            get { return selectedOrden; }
            set
            {selectedOrden = value; 
                NotifyOfPropertyChange(() => SelectedOrden);
            }
        }

        List<Models.Almacen.Almacenes> almacen;

        public List<Models.Almacen.Almacenes> Almacen
        {
            get { return almacen; }
            set { almacen = value; }
        }
        string responsable;

        public string Responsable
        {
            get { return responsable; }
            set { responsable = value; }
        }
        string observaciones;

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        //Estado (Documento Referencia);

        List<ProductoCant> lstProductos;

        public List<ProductoCant> LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value;
                  NotifyOfPropertyChange(() => LstProductos);
            }
        }

        string txtCodPro="";

        public string TxtCodPro
        {
            get { return txtCodPro; }
            set
            {
                txtCodPro = value;
                NotifyOfPropertyChange(() => TxtCodPro);
            }
        }

        string txtCantPro;

        public string TxtCantPro
        {
            get { return txtCantPro; }
            set { txtCantPro = value;
            NotifyOfPropertyChange(() => TxtCantPro);
            }
        }

        //Botones:

        public void CargarProductos() {
        
            string referencia = TxtDoc;
            string mot = this.selectedMotivo;
           if ( string.Compare(mot,"Orden de Compra",true)==0){
               List<ProductoxOrdenCompra> poc = new List<ProductoxOrdenCompra>();
               poc = SelectedOrden.LstProducto;
               List<ProductoCant> lpcan = new List<ProductoCant>();
               for (int i = 0; i < poc.Count(); i++) {
                   ProductoCant pcan = new ProductoCant();
                   pcan.Can = poc.ElementAt(i).Cantidad;
                   pcan.CodPro = poc.ElementAt(i).Producto.CodigoProd;
                   pcan.ProNombre = poc.ElementAt(i).Producto.Nombre;
                   pcan.CanAtend = poc.ElementAt(i).CantAtendida.ToString();
                   pcan.CanAtender = poc.ElementAt(i).CantidadAtender;
                   lpcan.Add(pcan);
               }
                LstProductos = new List<ProductoCant>(lpcan);
  
           }
            NotifyOfPropertyChange(() => LstProductos);
        }

        public void BuscarDocumento()
        {
            if (string.Compare(selectedMotivo, "Orden de Compra", true) == 0)
            {
                MadeInHouse.Models.MyWindowManager wm = new Models.MyWindowManager();
                wm.ShowWindow(new BuscarOrdenCompraViewModel(this, 1));
            }
            else {
                MessageBox.Show("El motivo seleccionado no admite Documento de referencia");
            }
        }

        public void BuscarProducto()
        {
            MadeInHouse.Models.MyWindowManager wm = new Models.MyWindowManager();
            wm.ShowWindow(new ProductoBuscarViewModel(this, 4));
        }

        public void AgregarProducto() {
            if (TxtCodPro == null || TxtCantPro == null)
            {
                System.Windows.MessageBox.Show("Debe completar todos los campos");
            }
            else
            {
                ProductoCant pxa;
                List<Producto> lstAux = null;
                lstAux = pxaSQL.BuscarProducto(TxtCodPro);
                
                if ( (lstAux != null))
                {
                    if (LstProductos != null)
                    {
                        if ((pxa = LstProductos.Find(x => x.CodPro == lstAux[0].CodigoProd)) == null)
                        {

                            pxa = new ProductoCant();
                            pxa.Can = TxtCantPro;
                            pxa.CodPro = lstAux[0].CodigoProd.ToString();
                            pxa.ProNombre = lstAux[0].Nombre;
                            LstProductos.Add(pxa);
                            LstProductos = new List<ProductoCant>(LstProductos);
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("El producto que se quiere registrar ya esta siendo ingresado","Error");
                        }
                    }
                    else {
                        pxa = new ProductoCant();
                        pxa.Can = TxtCantPro;
                        pxa.CodPro = lstAux[0].CodigoProd.ToString();
                        pxa.ProNombre = lstAux[0].Nombre;
                        LstProductos = new List<ProductoCant>();
                        LstProductos.Add(pxa);
                        LstProductos = new List<ProductoCant>(LstProductos);
                    }

                }
                else
                {
                    System.Windows.MessageBox.Show("El código proporcionado no existe");
                }
            }
        }
        public void AbrirPosicionProducto()
        {

            MadeInHouse.Models.MyWindowManager wm = new Models.MyWindowManager();
            wm.ShowWindow(new Almacen.PosicionProductoViewModel(this,1));
        }

        public void AbrirListarOrdenesCompra()
        {

            Almacen.ListaOrdenCompraViewModel abrirListaOrden = new Almacen.ListaOrdenCompraViewModel();
            win.ShowWindow(abrirListaOrden);
        }

    }
}
