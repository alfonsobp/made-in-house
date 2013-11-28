using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.DataObjects.Ventas;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Compras;
using MadeInHouse.Models.Seguridad;
using MadeInHouse.Models.Ventas;
using MadeInHouse.ViewModels.Compras;
using MadeInHouse.ViewModels.Layouts;
using MadeInHouse.ViewModels.Ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Windows;

namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(MantenerNotaDeIngresoViewModel))]
    class MantenerNotaDeIngresoViewModel : PropertyChangedBase
    {
        #region constructores

        [ImportingConstructor]
        public MantenerNotaDeIngresoViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
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
                a = aGW.BuscarAlmacen(-1, -1, 3);
            }

            List<Usuario> ul = new List<Usuario>();
            ul.Add(u);
            this.responsable = new List<Usuario>(ul);

            List<Models.Almacen.Almacenes> al = new List<Models.Almacen.Almacenes>();
            al.Add(a);
            this.almacen = al;
            Estado = true;
            EstadoMot = true;
            EstadoPro = true;
        }

        #endregion

        private readonly IWindowManager _windowManager;
        DataObjects.Almacen.ProductoxSolicitudAbSQL gateWay = new ProductoxSolicitudAbSQL();
        ProductoSQL pxaSQL;
        Usuario u = new Usuario();
        int idTienda;


        string txtDoc = null;

        public string TxtDoc
        {
            get { return txtDoc; }
            set
            {
                txtDoc = value;
                NotifyOfPropertyChange(() => TxtDoc);
            }
        }

        int txtDocId;

        public int TxtDocId
        {
            get { return txtDocId; }
            set
            {
                txtDocId = value;
            }

        }

        bool estadoPro;

        public bool EstadoPro
        {
            get { return estadoPro; }
            set
            {
                estadoPro = value;
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
            set
            {
                selectedMotivo = value;
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
            set
            {
                selectedProducto = value;
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


        OrdenCompra selectedOrden = null;

        public OrdenCompra SelectedOrden
        {
            get { return selectedOrden; }
            set
            {
                selectedOrden = value;
                NotifyOfPropertyChange(() => SelectedOrden);
            }
        }

        Abastecimiento selectedSolicitud = null;
        public Abastecimiento SelectedSolicitud
        {
            get { return selectedSolicitud; }
            set
            {
                selectedSolicitud = value;
                NotifyOfPropertyChange("SelectedSolicitud");
            }
        }

        GuiaRemision selectedGuia = null;

        public GuiaRemision SelectedGuia
        {
            get { return selectedGuia; }
            set
            {
                selectedGuia = value;
                NotifyOfPropertyChange("SelectedGuia");
            }
        }

        Devolucion selectedDevolucion;

        public Devolucion SelectedDevolucion
        {
            get { return selectedDevolucion; }
            set
            {
                selectedDevolucion = value;
                NotifyOfPropertyChange("SelectedDevolucion");
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
            set
            {
                responsable = value;
                NotifyOfPropertyChange("Responsable");
            }
        }
        string observaciones = "";

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        bool estado;

        public bool Estado
        {
            get { return estado; }
            set
            {
                estado = value;
                NotifyOfPropertyChange("Estado");
            }
        }

        List<ProductoCant> lstProductos;

        public List<ProductoCant> LstProductos
        {
            get { return lstProductos; }
            set
            {
                lstProductos = value;
                NotifyOfPropertyChange("LstProductos");
            }
        }

        string txtCantPro;

        public string TxtCantPro
        {
            get { return txtCantPro; }
            set
            {
                txtCantPro = value;
                NotifyOfPropertyChange(() => TxtCantPro);
            }
        }

        //Botones:

        public void CargarProductos()
        {

            string referencia = TxtDoc;
            if (string.IsNullOrWhiteSpace(referencia))
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No se ha ingresado ningun Documento de Referencia"));
                return;
            }
            string mot = this.selectedMotivo;
            if (string.Compare(mot, "Orden de Compra", true) == 0)
            {
                List<ProductoxOrdenCompra> poc = new List<ProductoxOrdenCompra>();
                poc = SelectedOrden.LstProducto;
                List<ProductoCant> lpcan = new List<ProductoCant>();
                for (int i = 0; i < poc.Count; i++)
                {
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
                    if (selectedGuia == null)
                    {
                        _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Ingrese un código de guía de remisión valido"));
                    }
                    else
                    {
                        LstProductos = selectedGuia.Nota.LstProducto;
                    }
                }
                else
                {
                    if (string.Compare(mot, "Devolucion", true) == 0)
                    {
                        List<DevolucionProducto> dv = new List<DevolucionProducto>();
                        DevolucionSQL dsql = new DevolucionSQL();

                        dv = dsql.BuscarProductos(-1, -1, null, SelectedDevolucion.IdDevolucion);
                        List<ProductoCant> lpcan = new List<ProductoCant>();
                        for (int i = 0; i < dv.Count; i++)
                        {
                            ProductoCant pcan = new ProductoCant();
                            Producto p = new Producto();
                            ProductoSQL pgw = new ProductoSQL();
                            p = pgw.Buscar_por_CodigoProducto(dv.ElementAt(i).IdProducto);
                            pcan.IdProducto = p.IdProducto;
                            pcan.Nombre = p.Nombre;
                            pcan.Can = "0";
                            pcan.CanAtend = "0";
                            pcan.CanAtender = dv.ElementAt(i).Devuelto.ToString();
                            pcan.Ubicaciones = new List<Ubicacion>();
                            pcan.CodigoProd = p.CodigoProd;
                            lpcan.Add(pcan);
                        }
                        LstProductos = new List<ProductoCant>(lpcan);
                    }
                    else
                    {

                        if (string.Compare(mot, "Abastecimiento", true) == 0)
                        {

                            List<ProductoCant> psa = new List<ProductoCant>();
                            ProductoxSolicitudAbSQL pasql = new ProductoxSolicitudAbSQL();

                            psa = pasql.ListaProductos(SelectedSolicitud.idSolicitudAB.ToString());

                            List<ProductoCant> lpcan = new List<ProductoCant>();
                            for (int i = 0; i < psa.Count; i++)
                            {
                                ProductoCant pcan = new ProductoCant();
                                pcan.IdProducto = psa.ElementAt(i).IdProducto;
                                pcan.Can = psa.ElementAt(i).Can;
                                pcan.CodigoProd = psa.ElementAt(i).CodigoProd;
                                pcan.Nombre = psa.ElementAt(i).Nombre;
                                pcan.CanAtend = "0";
                                pcan.CanAtender = psa.ElementAt(i).CanAtend; 
                                pcan.Ubicaciones = new List<Ubicacion>();
                                lpcan.Add(pcan);
                            }

                            LstProductos = new List<ProductoCant>(lpcan);

                        }
                    }
                }
            }


            NotifyOfPropertyChange(() => LstProductos);
            if (LstProductos != null)
            {
                EstadoMot = false;
                Estado = false;
            }
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
                                EstadoPro = true;
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
                if (u.IdTienda == 0)
                {
                    _windowManager.ShowWindow(new BuscarOrdenCompraViewModel(this, 1));
                }
                else
                {
                    _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Ordenes de Compra solo pueden ser ingresadas por Almacen Central"));
                }
            }
            else
            {
                if (string.Compare(selectedMotivo, "Devolucion", true) == 0)
                {
                    _windowManager.ShowWindow(new DevolucionesBuscarViewModel(_windowManager, this, 2));
                }
                else
                {
                    if (string.Compare(selectedMotivo, "Traslado Externo", true) == 0)
                    {
                        _windowManager.ShowWindow(new BuscarGuiasRemisionViewModel(_windowManager, this, 1));
                    }
                    else
                    {
                        if (string.Compare(selectedMotivo, "Abastecimiento", true) == 0)
                        {
                            _windowManager.ShowWindow(new SolicitudAbListadoViewModel(_windowManager, this, 1));
                        }
                        else
                        {
                            //cualquier otro motivo
                            _windowManager.ShowDialog(new AlertViewModel(_windowManager, "El motivo seleccionado no admite Documento de referencia"));
                        }

                    }
                }
            }
        }

        public void BuscarProducto()
        {
            _windowManager.ShowWindow(new ProductoBuscarViewModel(_windowManager, this, 4));
        }

        public void AgregarProducto()
        {
            if (SelectedProducto == null)
            {
                return;
            }

            Validacion.Evaluador eval = new Validacion.Evaluador();
            if (SelectedProducto.CodigoProd == null || TxtCantPro == null || !eval.esNumeroEntero(TxtCantPro) || Convert.ToInt64(TxtCantPro) <= 0)
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Debe completar todos los campos y la cantidad debe ser mayor a 0"));
            }
            else
            {
                ProductoCant pxa;
                Producto lstAux = null;
                lstAux = pxaSQL.Buscar_por_CodigoProducto(SelectedProducto.CodigoProd);

                if ((lstAux != null))
                {
                    if (LstProductos != null)
                    {
                        if ((pxa = LstProductos.Find(x => x.CodigoProd == lstAux.CodigoProd)) == null)
                        {
                            pxa = new ProductoCant();
                            pxa.CanAtender = TxtCantPro;
                            pxa.CanAtend = "0";
                            pxa.Can = "0";
                            pxa.IdProducto = lstAux.IdProducto;
                            pxa.CodigoProd = lstAux.CodigoProd.ToString();
                            pxa.Nombre = lstAux.Nombre;
                            pxa.Ubicaciones = new List<Ubicacion>();
                            LstProductos.Add(pxa);
                            LstProductos = new List<ProductoCant>(LstProductos);
                        }
                        else
                        {
                            _windowManager.ShowDialog(new AlertViewModel(_windowManager, "El producto que se quiere registrar ya esta siendo ingresado"));
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
                    _windowManager.ShowDialog(new AlertViewModel(_windowManager, "El código proporcionado no existe"));
                }
            }
        }

        public void AbrirPosicionProducto()
        {
            _windowManager.ShowWindow(new PosicionProductoViewModel(_windowManager, this, 1));
        }

        public void AbrirListarOrdenesCompra()
        {
            _windowManager.ShowWindow(new ListaOrdenCompraViewModel());
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
            if (SelectedOrden!=null || SelectedGuia!=null || SelectedDevolucion!=null || SelectedSolicitud !=null)
            {
                // si hay documento de referencia colocar id;
                nota.IdDoc = TxtDocId;
            }
            else
            {
                //Si no existe documento de referencia colocar 0
                nota.IdDoc = 0;
            }
            if (String.IsNullOrEmpty(SelectedMotivo))
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No se pudo guardar"));
                return;
            }

  
            nota.IdMotivo = DataObjects.Almacen.MotivoSQL.BuscarMotivo(SelectedMotivo).Id;
            nota.IdResponsable = Responsable.ElementAt(0).IdUsuario;
            nota.Observaciones = Observaciones;
            nota.Tipo = 1;

            nota.LstProducto = this.LstProductos;

            nota.IdNota = ntgw.AgregarNota(nota);

            if (nota.IdNota > 0)
            {

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

                if (SelectedGuia != null)
                {

                    CambiarEstadoGuia(SelectedGuia);

                }

                if (SelectedDevolucion != null)
                {

                    CambiarEstadoDevolucion(SelectedDevolucion);

                }

                if (SelectedSolicitud != null) {
                    CambiarEstadoSolicitud(SelectedSolicitud);
                }
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Nota Creada"));
                //1: Agregar, 2: Editar, 3: Eliminar, 4: Recuperar, 5: Desactivar
                DataObjects.Seguridad.LogSQL.RegistrarActividad("Registrar Nota de Ingreso",  nota.IdNota.ToString(), 1);

            }
            else
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No se pudo guardar"));
            }
        }

        private void CambiarEstadoSolicitud(Abastecimiento SelectedSolicitud)
        {
            ProductoxSolicitudAbSQL sasql = new ProductoxSolicitudAbSQL();
            selectedSolicitud.estado = 6;
            sasql.Atendida(selectedSolicitud);

        }

        private void CambiarEstadoDevolucion(Devolucion SelectedDevolucion)
        {
            DevolucionSQL dsql = new DevolucionSQL();
            dsql.cambiarEstado(SelectedDevolucion.IdDevolucion, 2);
        }

        private void CambiarEstadoGuia(GuiaRemision SelectedGuia)
        {
            GuiaDeRemisionSQL grsql = new GuiaDeRemisionSQL();
            selectedGuia.Estado = 2;
            grsql.editarGuiaDeRemision(selectedGuia);
        }


        public void guardarOrden(List<ProductoCant> list)
        {
            OrdenCompraxProductoSQL ocSQL = new OrdenCompraxProductoSQL();

            foreach (ProductoxOrdenCompra op in SelectedOrden.LstProducto)
            {
                foreach (ProductoCant pc in list)
                {

                    if (pc.IdProducto == op.Producto.IdProducto)
                    {
                       // MessageBox.Show(" cant atendida = " + pc.CanAtender);
                        op.CantAtendida += Convert.ToInt32(pc.CanAtender);
                        ocSQL.Actualizar(op);
                    }
                }
            }
        }

    }
}