using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Compras;
using MadeInHouse.Models.Seguridad;
using MadeInHouse.ViewModels.Compras;
using MadeInHouse.ViewModels.Ventas;
using MadeInHouse.DataObjects.Compras;

namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerNotaDeIngresoViewModel:PropertyChangedBase
    {
        MyWindowManager win = new MyWindowManager();
        DataObjects.Almacen.ProductoxSolicitudAbSQL gateWay = new ProductoxSolicitudAbSQL();
        ProductoSQL pxaSQL;
        Usuario u = new Usuario();
        int idTienda;
        public MantenerNotaDeIngresoViewModel(){
            pxaSQL = new ProductoSQL();
            this.cmbMotivo = DataObjects.Almacen.MotivoSQL.BuscarMotivos(1);
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

            List<Usuario> ul = new List<Usuario>();
            ul.Add(u);
            this.responsable = new List<Usuario>(ul);

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
        
        int txtDocId;

        public int TxtDocId { 
            get{ return txtDocId; }
            set { txtDocId = value;
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
            DeshabilitarPro(selectedMotivo);
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
                            if (selectedMotivo.Equals("Abastecimiento"))
                            {
                                Estado = true;
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


        OrdenCompra selectedOrden=null;

        public OrdenCompra SelectedOrden
        {
            get { return selectedOrden; }
            set
            {selectedOrden = value; 
                NotifyOfPropertyChange(() => SelectedOrden);
            }
        }

        GuiaRemision selectedGuia;

        public GuiaRemision SelectedGuia
        {
            get { return selectedGuia; }
            set { selectedGuia = value;
            NotifyOfPropertyChange("SelectedGuia");
            }
        }

        

        List<Models.Almacen.Almacenes> almacen;

        public List<Models.Almacen.Almacenes> Almacen
        {
            get { return almacen; }
            set { almacen = value; }
        }


        List<Usuario> responsable = new List<Usuario>();

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
               for (int i = 0; i < poc.Count; i++) {
                   ProductoCant pcan = new ProductoCant();
                   pcan.IdProducto = poc.ElementAt(i).Producto.IdProducto;
                   pcan.Can = poc.ElementAt(i).Cantidad;
                   pcan.CodigoProd = poc.ElementAt(i).Producto.CodigoProd;
                   pcan.Nombre = poc.ElementAt(i).Producto.Nombre;
                   pcan.CanAtend = poc.ElementAt(i).CantAtendida.ToString();
                   pcan.CanAtender = poc.ElementAt(i).CantidadAtender;
                   pcan.Ubicaciones = new List<Ubicacion>();
                   lpcan.Add(pcan);
               }
                LstProductos = new List<ProductoCant>(lpcan);
  
           }
           else
           {
               if (string.Compare(mot, "Traslado Externo", true) == 0)
               {

                   LstProductos = selectedGuia.Nota.LstProducto;

               }
               else
               {
                   if (string.Compare(mot, "Devolucion", true) == 0)
                   {
                       LstProductos = new List<ProductoCant>();
                   }
                   else {

                       if (string.Compare(mot, "Abastecimiento", true) == 0) {

                            LstProductos = new List<ProductoCant>();
                       }
                   }
               }
           }
           

            NotifyOfPropertyChange(() => LstProductos);
            EstadoMot = false;
            Estado = false;
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
                            if (selectedMotivo.Equals("Abastecimiento"))
                            {
                                EstadoPro = false;
                            }
                            else
                            {

                                //Cualquier otro motivo
                                Estado = true;
                            }
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
                    wm.ShowWindow(new DevolucionesBuscarViewModel(wm, this,1));
                
                }
                else {
                    if (string.Compare(selectedMotivo, "Traslado Externo", true) == 0)
                    {
                        MadeInHouse.Models.MyWindowManager wm = new Models.MyWindowManager();
                        wm.ShowWindow(new BuscarGuiasRemisionViewModel(this, 1));
                    }
                    else {
                        if (string.Compare(selectedMotivo, "Abastecimiento", true) == 0)
                        {
                            MadeInHouse.Models.MyWindowManager wm = new Models.MyWindowManager();
                            wm.ShowWindow(new SolicitudAbListadoViewModel(this, 1));
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
                        if ((pxa = LstProductos.Find(x => x.CodigoProd == lstAux.CodigoProd)) == null)
                        {
                            pxa = new ProductoCant();
                            pxa.CanAtender = TxtCantPro;
                            pxa.CanAtend = "0";
                            pxa.Can = "0";
                            pxa.IdProducto=lstAux.IdProducto;
                            pxa.CodigoProd = lstAux.CodigoProd.ToString();
                            pxa.Nombre = lstAux.Nombre;
                            pxa.Ubicaciones = new List<Ubicacion>();
                            LstProductos.Add(pxa);
                            LstProductos = new List<ProductoCant>(LstProductos);
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("El producto que se quiere registrar ya esta siendo ingresado","Error");
                        }
                    }
                    else 
                    {
                        pxa = new ProductoCant();
                        pxa.CanAtender = TxtCantPro;
                        pxa.CanAtend = "0";
                        pxa.Can = "0";
                        pxa.CodigoProd = lstAux.CodigoProd.ToString();
                        pxa.Nombre = lstAux.Nombre;
                        pxa.IdProducto = lstAux.IdProducto;
                        pxa.Ubicaciones = new List<Ubicacion>();
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

        public void AgregarNota()
        {

            NotaISSQL ntgw = new NotaISSQL();
            NotaIS nota = new NotaIS();
            nota.IdAlmacen = Almacen.ElementAt(0).IdAlmacen;
            // Logica de  Referencia de documento
            if (Estado == false)
            {
                // no hay documento de referencia colocar 0;
                nota.IdDoc = 0;
            }
            else
            {
                //Si existe documento de referencia colocar el ID
                nota.IdDoc = TxtDocId;
            }
            nota.IdMotivo = DataObjects.Almacen.MotivoSQL.BuscarMotivo(SelectedMotivo).Id;
            nota.IdResponsable = Responsable.ElementAt(0).IdUsuario;
            nota.Observaciones = Observaciones;
            nota.Tipo = 1;

            nota.LstProducto = this.LstProductos;

            nota.IdNota = ntgw.AgregarNota(nota);

            ProductoxTiendaSQL ptgw = new ProductoxTiendaSQL();
            ProductoSQL pgw = new ProductoSQL();

            List<ProductoCant> list = ntgw.BuscarNotas(nota.IdNota);
            if (u.IdTienda != 0) ptgw.ActualizarStockEntrada(list, u.IdTienda);
            else pgw.ActualizarStockEntrada(list);

            //Actualizar Documentos de Referencia para darlos por Terminados! :)

            if (SelectedOrden != null)
            {
                guardarOrden(list);
            }

            if (SelectedGuia != null) {

                CambiarEstadoGuia(SelectedGuia);

            }

            MessageBox.Show("Nota Creada");

        }

        private void CambiarEstadoGuia(GuiaRemision SelectedGuia)
        {
            GuiaDeRemisionSQL grsql = new GuiaDeRemisionSQL();
            selectedGuia.Estado = 2;
            grsql.editarGuiaDeRemision(selectedGuia);

        }


        public void guardarOrden(List<ProductoCant> list) {

            OrdenCompraxProductoSQL ocSQL = new OrdenCompraxProductoSQL();
            
            foreach (ProductoxOrdenCompra op in SelectedOrden.LstProducto) 
            {
                foreach (ProductoCant pc in list) {

                    if (pc.IdProducto == op.Producto.IdProducto) {
                        MessageBox.Show(" cant atendida = " + pc.CanAtender);
                        op.CantAtendida += Convert.ToInt32(pc.CanAtender);
                        ocSQL.Actualizar(op);
                    }
                
                }
            
            }

            
        
        }



    }
}
