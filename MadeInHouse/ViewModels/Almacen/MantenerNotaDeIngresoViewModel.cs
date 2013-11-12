using System;
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
using MadeInHouse.ViewModels.Ventas;

namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerNotaDeIngresoViewModel:PropertyChangedBase
    {
        MyWindowManager win = new MyWindowManager();
        DataObjects.Almacen.ProductoxSolicitudAbSQL gateWay = new ProductoxSolicitudAbSQL();
        ProductoSQL pxaSQL;
        public MantenerNotaDeIngresoViewModel(){
            pxaSQL = new ProductoSQL();
            this.cmbMotivo = DataObjects.Almacen.MotivoSQL.BuscarMotivos(1);
            AlmacenSQL aGW = new AlmacenSQL();
            Models.Almacen.Almacenes a = aGW.BuscarAlmacen(3);
            List <Models.Almacen.Almacenes> al = new List<Models.Almacen.Almacenes>();
            al.Add(a);
            this.almacen = al;
            Estado = true;
            EstadoMot = true;
            EstadoPro = true;
        }

        string txtDoc;

        public string TxtDoc
        {
            get { return txtDoc; }
            set { txtDoc = value;
            NotifyOfPropertyChange(() => TxtDoc);
            }
        }
        bool estadoPro;

        public bool EstadoPro
        {
            get { return estadoPro; }
            set { estadoPro = value;
            NotifyOfPropertyChange("EstadoPro");
            }
        }
        bool estadoMot;

        public bool EstadoMot
        {
            get { return estadoMot; }
            set
            {
                estadoMot = value;
                NotifyOfPropertyChange("EstadoMot");

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
            set { selectedMotivo = value;
            DeshabilitarDoc(selectedMotivo);
            NotifyOfPropertyChange(() => SelectedMotivo);
            }
        }

        private void DeshabilitarDoc(string selectedMotivo)
        {

            if (selectedMotivo.Equals("Traslado Externo"))
            {
                Estado = true;
            }
            else
            {
                if (selectedMotivo.Equals("Orden de Compra"))
                {
                    Estado = true;
                }
                else
                {
                    if (selectedMotivo.Equals("Devolucion"))
                    {
                        Estado = true;
                    }
                    else
                    {
                        if (selectedMotivo.Equals("Ajuste"))
                        {
                            Estado = false;
                        }
                        else
                        {
                        //Cualquier otro motivo
                            Estado = false;
                        }

                    }
                }
            }

        }

        Producto selectedProducto;
        
        public Producto SelectedProducto
        {
            get { return selectedProducto; }
            set { selectedProducto = value;
            NotifyOfPropertyChange(() => SelectedProducto);
            }
        }

        ProductoCant selectedProductoCant;

        public ProductoCant SelectedProductoCant
        {
            get { return selectedProductoCant; }
            set
            {
                selectedProductoCant = value;
                NotifyOfPropertyChange(() => SelectedProductoCant);
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

        bool estado;

        public bool Estado
        {
            get { return estado; }
            set { estado = value; 
            NotifyOfPropertyChange("Estado");
            }
        }

        List<ProductoCant> lstProductos;

        public List<ProductoCant> LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value;
                  NotifyOfPropertyChange("LstProductos");
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
            EstadoMot = false;
            Estado = false;
            DeshabilitarPro(SelectedMotivo);
        }

        private void DeshabilitarPro(string SelectedMotivo)
        {

            if (selectedMotivo.Equals("Traslado Externo"))
            {
                EstadoPro = false;
            }
            else
            {
                if (selectedMotivo.Equals("Orden de Compra"))
                {
                    EstadoPro = false;
                }
                else
                {
                    if (selectedMotivo.Equals("Devolucion"))
                    {
                        EstadoPro = false;
                    }
                    else
                    {
                        if (selectedMotivo.Equals("Ajuste"))
                        {
                            EstadoPro = true;
                        }
                        else
                        {
                            //Cualquier otro motivo
                            EstadoPro = true;
                        }

                    }
                }
            }

        }

        public void BuscarDocumento()
        {
            if (string.Compare(selectedMotivo, "Orden de Compra", true) == 0)
            {
                MadeInHouse.Models.MyWindowManager wm = new Models.MyWindowManager();
                wm.ShowWindow(new BuscarOrdenCompraViewModel(this, 1));
            }
            else {
                if (string.Compare(selectedMotivo, "Devolucion", true) == 0) {
            
                    MadeInHouse.Models.MyWindowManager wm = new Models.MyWindowManager();
                    wm.ShowWindow(new DevolucionesBuscarViewModel(this,1));
                
                }
                else {
                    if (string.Compare(selectedMotivo, "Traslado Externo", true) == 0)
                    {
                        MadeInHouse.Models.MyWindowManager wm = new Models.MyWindowManager();
                        wm.ShowWindow(new BuscarGuiasRemisionViewModel(this, 1));
                    }
                    else {

                        //cualquier otro motivo
                        MessageBox.Show("El motivo seleccionado no admite Documento de referencia");
                
                    }
                }
                

            }

        }

        public void BuscarProducto()
        {
            MadeInHouse.Models.MyWindowManager wm = new Models.MyWindowManager();
            wm.ShowWindow(new ProductoBuscarViewModel(this, 4));
        }

        public void AgregarProducto() {
            if (SelectedProducto.CodigoProd == null || TxtCantPro == null)
            {
                System.Windows.MessageBox.Show("Debe completar todos los campos");
            }
            else
            {
                ProductoCant pxa;
                Producto lstAux = null;
                lstAux = pxaSQL.Buscar_por_CodigoProducto(SelectedProducto.CodigoProd);
                
                if ( (lstAux != null))
                {
                    if (LstProductos != null)
                    {
                        if ((pxa = LstProductos.Find(x => x.CodPro == lstAux.CodigoProd)) == null)
                        {
                            pxa = new ProductoCant();
                            pxa.CanAtender = TxtCantPro;
                            pxa.CanAtend = "0";
                            pxa.Can = "0";
                            pxa.CodPro = lstAux.CodigoProd.ToString();
                            pxa.ProNombre = lstAux.Nombre;
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
                        pxa.CanAtender = TxtCantPro;
                        pxa.CanAtend = "0";
                        pxa.Can = "0";
                        pxa.CodPro = lstAux.CodigoProd.ToString();
                        pxa.ProNombre = lstAux.Nombre;
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

        public void Quitar()
        {
            if (SelectedProductoCant != null)
            {
                LstProductos.Remove(SelectedProductoCant);
                LstProductos = new List<ProductoCant>(LstProductos);
                
            }
        }

    }
}
