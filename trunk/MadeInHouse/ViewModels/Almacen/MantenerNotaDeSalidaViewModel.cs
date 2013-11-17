using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models;
using System.Windows;
using MadeInHouse.Models.Seguridad;
using System.Threading;
using MadeInHouse.ViewModels.RRHH;
using MadeInHouse.Views.RRHH;
using MadeInHouse.Models.RRHH;


namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerNotaDeSalidaViewModel:PropertyChangedBase
    {
         MyWindowManager win = new MyWindowManager();
        DataObjects.Almacen.ProductoxSolicitudAbSQL gateWay = new ProductoxSolicitudAbSQL();
        ProductoSQL pxaSQL;
        Usuario u = new Usuario();
        int idTienda;
        List<Usuario> responsable = new List<Usuario>();
        public MantenerNotaDeSalidaViewModel(){
            pxaSQL = new ProductoSQL();
            this.cmbMotivo = DataObjects.Almacen.MotivoSQL.BuscarMotivos(2);
            AlmacenSQL aGW = new AlmacenSQL();

            u = DataObjects.Seguridad.UsuarioSQL.buscarUsuarioPorIdUsuario(Int32.Parse(Thread.CurrentPrincipal.Identity.Name));
            idTienda = u.IdTienda;
            
            Models.Almacen.Almacenes a;
            if (idTienda != 0)
            {
                //1 deposito
                //2 anaquel
                //3 central va al else
                a = aGW.BuscarAlmacen(-1, idTienda, 1);
            }
            else
            {
                a = aGW.BuscarAlmacen(-1, idTienda, 3);
            }

            List <Models.Almacen.Almacenes> al = new List<Models.Almacen.Almacenes>();
            al.Add(a);

            List<Usuario> ul = new List<Usuario>();
            ul.Add(u);

            this.responsable = new List<Usuario>(ul);
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
        int txtDocId;

        public int TxtDocId
        {
            get { return txtDocId; }
            set { txtDocId = value; }
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
            NotifyOfPropertyChange(() => SelectedMotivo);
            DeshabilitarDoc(selectedMotivo);
            DeshabilitarPro(selectedMotivo);

            }
        }

        private void DeshabilitarDoc(string selectedMotivo)
        {

            if (selectedMotivo.Equals("Traslado Externo"))
            {
                Estado = false;
            }
            else
            {
                if (selectedMotivo.Equals("Orden de despacho"))
                {
                    Estado = true;
                }
                else
                {
                    if (selectedMotivo.Equals("Rotura"))
                    {
                        Estado = false;
                    }
                    else
                    {
                        if (selectedMotivo.Equals("Ajuste"))
                        {
                            Estado = false;
                        }
                        else
                        {
                            if (selectedMotivo.Equals("Perdida"))
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

        #region Orden de Despacho
/*        OrdenDeDespacho selectedOrden;

        public OrdenCompra SelectedOrden
        {
            get { return selectedOrden; }
            set
            {selectedOrden = value; 
                NotifyOfPropertyChange(() => SelectedOrden);
            }
        }
 */
        #endregion
        List<Models.Almacen.Almacenes> almacen;

        public List<Models.Almacen.Almacenes> Almacen
        {
            get { return almacen; }
            set { almacen = value; }
        }

        public List<Usuario> Responsable
        {
            get { return responsable; }
            set { responsable = value;
            NotifyOfPropertyChange("Responsable");
            }
       
        }
        
        string observaciones="";

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value;
            NotifyOfPropertyChange("Observaciones");
            }
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
             /*
               List<ProductoxOrdenCompra> poc = new List<ProductoxOrdenCompra>();
               poc = SelectedOrden.LstProducto;
               List<ProductoCant> lpcan = new List<ProductoCant>();
               for (int i = 0; i < poc.Count(); i++) {
                   ProductoCant pcan = new ProductoCant();
                   pcan.IdProducto = poc.ElementAt(i).Producto.IdProducto;
                   pcan.Can = poc.ElementAt(i).Cantidad;
                   pcan.CodPro = poc.ElementAt(i).Producto.CodigoProd;
                   pcan.ProNombre = poc.ElementAt(i).Producto.Nombre;
                   pcan.CanAtend = poc.ElementAt(i).CantAtendida.ToString();
                   pcan.CanAtender = poc.ElementAt(i).CantidadAtender;
                   lpcan.Add(pcan);
               }
                LstProductos = new List<ProductoCant>(lpcan);
            */
           }
            NotifyOfPropertyChange(() => LstProductos);
            EstadoMot = false;
            Estado = false;
        }

        private void DeshabilitarPro(string SelectedMotivo)
        {

            if (selectedMotivo.Equals("Traslado Externo"))
            {
                EstadoPro = true;
            }
            else
            {
                if (selectedMotivo.Equals("Orden de despacho"))
                {
                    EstadoPro = false;
                }
                else
                {
                    if (selectedMotivo.Equals("Rotura"))
                    {
                        EstadoPro = true;
                    }
                    else
                    {
                        if (selectedMotivo.Equals("Ajuste"))
                        {
                            EstadoPro = true;
                        }
                        else
                        {
                            if (selectedMotivo.Equals("Perdida"))
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

        }

        public void BuscarDocumento()
        {
            if (string.Compare(selectedMotivo, "Orden de despacho", true) == 0)
            {
                /*
                MadeInHouse.Models.MyWindowManager wm = new Models.MyWindowManager();
                wm.ShowWindow(new BuscarOrdenCompraViewModel(this, 1));
                */
            }
            else {
                if (string.Compare(selectedMotivo, "Rotura", true) == 0)
                {

                    MessageBox.Show("El motivo seleccionado no admite Documento de referencia");
                
                }
                else {
                    if (string.Compare(selectedMotivo, "Traslado Externo", true) == 0)
                    {
                        MessageBox.Show("El motivo seleccionado no admite Documento de referencia");
                    }
                    else {
                        if (string.Compare(selectedMotivo, "Perdida", true) == 0)
                        {
                            MessageBox.Show("El motivo seleccionado no admite Documento de referencia");
                        }
                        else
                        {
                            if (string.Compare(selectedMotivo, "Ajuste", true) == 0)
                            {
                                MessageBox.Show("El motivo seleccionado no admite Documento de referencia");
                            }
                            else
                            {
                                //cualquier otro motivo
                                MessageBox.Show("El motivo seleccionado no admite Documento de referencia");
                            }
                        }

                
                    }
                }
                

            }

        }

        public void BuscarProducto()
        {
            MadeInHouse.Models.MyWindowManager wm = new Models.MyWindowManager();
            wm.ShowWindow(new ProductoBuscarViewModel(this, 3));
        
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
                            pxa.IdProducto = lstAux.IdProducto;
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
                        pxa.IdProducto = lstAux.IdProducto;
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
            wm.ShowWindow(new Almacen.PosicionProductoViewModel(this,2));
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

        public void AgregarNota() {

            NotaISSQL ntgw = new NotaISSQL();
            NotaIS nota = new NotaIS();
            nota.IdAlmacen = Almacen.ElementAt(0).IdAlmacen;
            // Logica de  Referencia de documento
            if (Estado == false)
            {
                // no hay documento de referencia colocar 0;
                nota.IdDoc = 0;
            }
            else { 
                //Si existe documento de referencia colocar el ID
                nota.IdDoc = TxtDocId;
            }
            nota.IdMotivo = DataObjects.Almacen.MotivoSQL.BuscarMotivo(SelectedMotivo).Id;
            nota.IdResponsable = Responsable.ElementAt(0).IdUsuario;
            nota.Observaciones = Observaciones;
            nota.Tipo = 2;
            
            nota.LstProducto = this.LstProductos;
            
            nota.IdNota=ntgw.AgregarNota(nota);

            //Actualizar Documentos de Referencia para darlos por Terminados! :)

            //Actualizar Stock
            ProductoxTiendaSQL ptgw = new ProductoxTiendaSQL();
            List<ProductoCant> list = ntgw.BuscarNotas(nota.IdNota);

            ptgw.ActualizarStockSalida(list,u.IdTienda);

        }

    }



}
