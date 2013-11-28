using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.DataObjects.Ventas;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Seguridad;
using MadeInHouse.Models.Ventas;
using MadeInHouse.ViewModels.Layouts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;


namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(MantenerNotaDeSalidaViewModel))]
    class MantenerNotaDeSalidaViewModel : PropertyChangedBase
    {
        #region constructores

        [ImportingConstructor]
        public MantenerNotaDeSalidaViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
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
                a = aGW.BuscarAlmacen(-1, -1, 3);
            }

            List<Models.Almacen.Almacenes> al = new List<Models.Almacen.Almacenes>();
            al.Add(a);

            List<Usuario> ul = new List<Usuario>();
            ul.Add(u);

            this.responsable = new List<Usuario>(ul);
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
        List<Usuario> responsable = new List<Usuario>();

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
            set { txtDocId = value; }
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

        #region selecteds

        OrdenDespacho selectedDespacho;

        public OrdenDespacho SelectedDespacho
        {
            get { return selectedDespacho; }
            set
            {
                selectedDespacho = value;
                NotifyOfPropertyChange("SelectedDespacho");
            }
        }


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
            set
            {
                observaciones = value;
                NotifyOfPropertyChange("Observaciones");
            }
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
            if (string.Compare(mot, "Orden de Despacho", true) == 0)
            {

                List<DetalleVenta> l = new DetalleVentaSQL().BuscarTodos();
                List<ProductoCant> lpcan = new List<ProductoCant>();
                for (int i = 0; i < l.Count; i++)
                    if (l[i].IdDetalleV == SelectedDespacho.Venta.IdVenta)
                    {
                        Producto p = new ProductoSQL().Buscar_por_CodigoProducto(l[i].IdProducto);
                        ProductoCant pcan = new ProductoCant();
                        pcan.IdProducto = p.IdProducto;
                        pcan.CodigoProd = p.CodigoProd;
                        pcan.Nombre = p.Nombre;
                        pcan.CanAtender = l.ElementAt(i).Cantidad.ToString();
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
                                if (selectedMotivo.Equals("Abastecimiento"))
                                {
                                    EstadoPro = false;
                                }
                                else
                                {
                                    //Cualquier otro motivo
                                    EstadoPro = false;
                                }
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
                _windowManager.ShowWindow(new BuscarOrdenDespachoViewModel(_windowManager, this));
            }
            else
            {
                if (string.Compare(selectedMotivo, "Rotura", true) == 0)
                {
                    _windowManager.ShowDialog(new AlertViewModel(_windowManager, "El motivo seleccionado no admite Documento de referencia"));
                }
                else
                {
                    if (string.Compare(selectedMotivo, "Traslado Externo", true) == 0)
                    {
                        _windowManager.ShowDialog(new AlertViewModel(_windowManager, "El motivo seleccionado no admite Documento de referencia"));
                    }
                    else
                    {
                        if (string.Compare(selectedMotivo, "Perdida", true) == 0)
                        {
                            _windowManager.ShowDialog(new AlertViewModel(_windowManager, "El motivo seleccionado no admite Documento de referencia"));
                        }
                        else
                        {
                            if (string.Compare(selectedMotivo, "Ajuste", true) == 0)
                            {
                                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "El motivo seleccionado no admite Documento de referencia"));
                            }
                            else
                            {
                                if (string.Compare(selectedMotivo, "Abastecimiento", true) == 0)
                                {
                                    _windowManager.ShowWindow(new SolicitudAbListadoViewModel(_windowManager, this,2));
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


            }

        }

        public void BuscarProducto()
        {
            _windowManager.ShowWindow(new ProductoBuscarViewModel(_windowManager, this, 3));
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
                            pxa.CodigoProd = lstAux.CodigoProd.ToString();
                            pxa.IdProducto = lstAux.IdProducto;
                            pxa.Nombre = lstAux.Nombre;
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
                        pxa.IdProducto = lstAux.IdProducto;
                        pxa.Nombre = lstAux.Nombre;
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
            _windowManager.ShowWindow(new PosicionProductoViewModel(_windowManager, this, 2));
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
            if (SelectedDespacho != null || SelectedSolicitud != null)
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
            nota.Tipo = 2;

            nota.LstProducto = this.LstProductos;

            nota.IdNota = ntgw.AgregarNota(nota);

            if (nota.IdNota > 0)
            {

                //Actualizar Documentos de Referencia para darlos por Terminados! :)

                //Actualizar Stock
                ProductoxTiendaSQL ptgw = new ProductoxTiendaSQL();
                ProductoSQL pgw = new ProductoSQL();

                List<ProductoCant> list = ntgw.BuscarNotas(nota.IdNota);
                if (SelectedSolicitud != null) {
                    pgw.ActualizarStockTemporalSalida(list);
                }
                else
                {
                    if (u.IdTienda != 0) ptgw.ActualizarStockSalida(list, u.IdTienda);
                    else pgw.ActualizarStockSalida(list);
                }
                //Actualizar Documento de Referencia

                if (SelectedDespacho != null)
                {

                    OrdenDespachoSQL osql = new OrdenDespachoSQL();
                    SelectedDespacho.Estado = 2;
                    osql.EditarOrdenDespacho(selectedDespacho);

                }
                if (SelectedSolicitud != null)
                {
                    CambiarEstadoSolicitud(SelectedSolicitud);
                }

                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Nota de Salida Creada"));
                //1: Agregar, 2: Editar, 3: Eliminar, 4: Recuperar, 5: Desactivar
                DataObjects.Seguridad.LogSQL.RegistrarActividad("Registrar Nota de Salida", nota.IdNota.ToString(), 1);
            }
            else
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No se pudo guardar"));
            }


        }

        private void CambiarEstadoSolicitud(Abastecimiento SelectedSolicitud)
        {

            ProductoxSolicitudAbSQL sasql = new ProductoxSolicitudAbSQL();
            selectedSolicitud.estado = 5;
            sasql.Atendida(selectedSolicitud);

        }
    }
}