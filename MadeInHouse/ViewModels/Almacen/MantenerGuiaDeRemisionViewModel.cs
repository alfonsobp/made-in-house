using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Views.Almacen;
using System.Windows;
using System.Collections.ObjectModel;
using MadeInHouse.ViewModels.Ventas;
using MadeInHouse.ViewModels.Compras;
using MadeInHouse.Models.Ventas;
using MadeInHouse.Models.Seguridad;
using MadeInHouse.DataObjects;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.DataObjects.Ventas;
using MadeInHouse.DataObjects.Seguridad;
using MadeInHouse.Dictionary;
using System.ComponentModel.Composition;
using MadeInHouse.ViewModels.Layouts;

namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(MantenerGuiaDeRemisionViewModel))]
    class MantenerGuiaDeRemisionViewModel : PropertyChangedBase
    {
        #region constructores

        public MantenerGuiaDeRemisionViewModel(IWindowManager windowmanager, GuiaRemision g):this(windowmanager) {

            if (g.Nota != null)
            {
                Nota = g.Nota;
                Alm = g.Almacen;
            }

            if (g.Orden != null)
            {
                Orden = g.Orden;
                Orden.CodOrden = "OD-" + (1000000 + Orden.IdOrdenDespacho).ToString();
            }

            TxtCodigo = g.CodGuiaRem;
            TxtFechaReg = g.FechaReg;
            SeleccionadoTipo=g.Tipo;
            TxtConductor = g.Conductor;
            SeleccionadoCamion = g.Camion;
            TxtObservaciones = g.Observaciones;

            if (g.Estado == 1)
                TxtEstado = "EMITIDA";
            else
                TxtEstado = "ATENDIDA";

            indicador = 2;
        }

        [ImportingConstructor]
        public MantenerGuiaDeRemisionViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
            int k=0;
            indicador = 1;

            k = new UtilesSQL().ObtenerMaximoID("GuiaRemision", "idGuia");
            TxtCodigo = "GR-" + (1000000 + k + 1).ToString();
            TxtEstado = "POR EMITIR";
        }

        #endregion

        //Atributos
        private readonly IWindowManager _windowManager;

        GuiaRemision g = new GuiaRemision();
        string tiendaOrigen = null;
        private int indicador; //1=insertar, 2=detalle;
        public int Indicador
        {
            get { return indicador; }
        }

        private string txtCodigo;
        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }

        private List<string> cbTipo = new List<string>() { "GR-ORDEN DESPACHO", "GR-TRASLADO EXTERNO" };
        public List<string> CbTipo
        {
            get { return cbTipo; }
            set { cbTipo = value; NotifyOfPropertyChange(() => CbTipo); }
        }

        private DateTime txtFechaReg = DateTime.Now;
        public DateTime TxtFechaReg
        {
            get { return txtFechaReg; }
            set { txtFechaReg = value; NotifyOfPropertyChange(() => TxtFechaReg); }
        }


        private string txtCodigoCT;
        public string TxtCodigoCT
        {
            get { return txtCodigoCT; }
            set { txtCodigoCT = value; NotifyOfPropertyChange(() => TxtCodigoCT); }
        }

        private string txtTienda;
        public string TxtTienda
        {
            get { return txtTienda; }
            set { txtTienda = value; NotifyOfPropertyChange(() => TxtTienda); }
        }


        private string txtDirPartida;
        public string TxtDirPartida
        {
            get { return txtDirPartida; }
            set { txtDirPartida = value; NotifyOfPropertyChange(() => TxtDirPartida); }
        }

        private string txtConductor;
        public string TxtConductor
        {
            get { return txtConductor; }
            set { txtConductor = value; NotifyOfPropertyChange(() => TxtConductor); }
        }

        private List<string> cbCamion = new List<string>() { "CAMION RH-A1", "CAMION RT-V5", "CAMION HL-H8" };
        public List<string> CbCamion
        {
            get { return cbCamion; }
            set { cbCamion = value; NotifyOfPropertyChange(() => CbCamion); }
        }

        private string txtObservaciones;
        public string TxtObservaciones
        {
            get { return txtObservaciones; }
            set { txtObservaciones = value; NotifyOfPropertyChange(() => TxtObservaciones); }
        }

        private string txtAlmacenOrigen;
        public string TxtAlmacenOrigen
        {
            get { return txtAlmacenOrigen; }
            set { txtAlmacenOrigen = value; NotifyOfPropertyChange(() => TxtAlmacenOrigen); }
        }

        private string seleccionadoTipo;
        public string SeleccionadoTipo
        {
            get { return seleccionadoTipo; }
            set { seleccionadoTipo = value; NotifyOfPropertyChange(() => SeleccionadoTipo); }
        }

        private string seleccionadoCamion;
        public string SeleccionadoCamion
        {
            get { return seleccionadoCamion; }
            set { seleccionadoCamion = value; NotifyOfPropertyChange(() => SeleccionadoCamion); }
        }

        private string selectedNotaOrden;
        public string SelectedNotaOrden
        {
            get { return selectedNotaOrden; }
            set { selectedNotaOrden = value; NotifyOfPropertyChange(() => SelectedNotaOrden); }
        }

        private string txtDirLlegada;
        public string TxtDirLlegada
        {
            get { return txtDirLlegada; }
            set { txtDirLlegada = value; NotifyOfPropertyChange(() => TxtDirLlegada); }
        }

        private string txtEstado;
        public string TxtEstado
        {
            get { return txtEstado; }
            set { txtEstado = value; NotifyOfPropertyChange(() => TxtEstado); }
        }

        private int txtCantidad;
        public int TxtCantidad
        {
            get { return txtCantidad; }
            set { txtCantidad = value; NotifyOfPropertyChange(() => TxtCantidad); }
        }

        private string txtRSCliente;
        public string TxtRSCliente
        {
          get { return txtRSCliente; }
            set { txtRSCliente = value; NotifyOfPropertyChange(() => TxtRSCliente); }
        }

        private string txtDirCliente;
        public string TxtDirCliente
        {
            get { return txtDirCliente; }
            set { txtDirCliente = value; NotifyOfPropertyChange(() => TxtDirCliente); }
        }

        private NotaIS nota;
        public NotaIS Nota
        {
            get { return nota; }
            set { nota = value; NotifyOfPropertyChange(() => Nota); }
        }

        private OrdenDespacho orden;
        public OrdenDespacho Orden
        {
            get { return orden; }
            set { orden = value; NotifyOfPropertyChange(() => Orden); }
        }

        private Almacenes alm;
        public Almacenes Alm
        {
            get { return alm; }
            set { alm = value; NotifyOfPropertyChange(() => Alm); }
        }

        private List<GuiaRemxProducto> lstProductos;
        public List<GuiaRemxProducto> LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value; NotifyOfPropertyChange(() => LstProductos); }
        }

        public void RefrescarNotas()
        {
            if (Nota.Tipo == 2)
            {

                Almacenes almOr = new GuiaDeRemisionSQL().BuscarALMfromID(Nota.IdAlmacen);
                Tienda a = new GuiaDeRemisionSQL().BuscarTIENfromID(almOr.IdTienda);

                if (a == null)
                    tiendaOrigen = "ALMACEN CENTRAL";
                else
                    tiendaOrigen = a.Nombre;
                
                TxtDirPartida = almOr.Direccion;
                TxtAlmacenOrigen = almOr.Nombre;

                List<ProductoxNotaIS> l = new NotaISSQL().BuscarNotasXProductos();
                List<GuiaRemxProducto> lAux = new List<GuiaRemxProducto>();
                int cantTotal = 0;

                for (int i = 0; i < l.Count; i++)
                    if (l[i].IdNota == Nota.IdNota)
                    {
                        GuiaRemxProducto gp = new GuiaRemxProducto();
                        Producto p = new ProductoSQL().Buscar_por_CodigoProducto(l[i].IdProducto);
                        gp.CodProd = p.CodigoProd;
                        gp.Nombre = p.Nombre;
                        gp.Cantidad = l[i].Cantidad;
                        cantTotal += l[i].Cantidad;
                        lAux.Add(gp);
                    }

                LstProductos = new List<GuiaRemxProducto>(lAux);
                TxtCantidad = cantTotal;
            }

            else
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Transacción única para NOTAS DE SALIDA"));
            }


        }

        public void RefrescarOrden()
        {
            Usuario u = getUsuariofromID(Orden.Venta.IdUsuario);
            Cliente c = getClientefromID(Orden.Venta.IdCliente);
            Tienda a = new GuiaDeRemisionSQL().BuscarTIENfromID(u.IdTienda);
            Almacenes alm = new GuiaDeRemisionSQL().BuscarALMDEPfromIdTienda(u.IdTienda);

            tiendaOrigen = a.Nombre;
            TxtAlmacenOrigen = alm.Nombre;
            TxtDirPartida = a.Direccion;
            TxtDirLlegada = Orden.Direccion;
            TxtTienda = c.RazonSocial;

            List<DetalleVenta> l = new DetalleVentaSQL().BuscarTodos();
            List<GuiaRemxProducto> lAux = new List<GuiaRemxProducto>();
            int cantTotal = 0;

            for (int i = 0; i < l.Count; i++)
                if (l[i].IdDetalleV == Orden.Venta.IdVenta)
                {
                    GuiaRemxProducto gp = new GuiaRemxProducto();
                    Producto p = new ProductoSQL().Buscar_por_CodigoProducto(l[i].IdProducto);
                    gp.CodProd = p.CodigoProd;
                    gp.Nombre = p.Nombre;
                    gp.Cantidad = l[i].Cantidad;
                    cantTotal += l[i].Cantidad;
                    lAux.Add(gp);
                }

            LstProductos = new List<GuiaRemxProducto>(lAux);
            TxtCantidad = cantTotal;
        }

        public void RefrescarAlm()
        {
            TxtDirLlegada = Alm.Direccion;
            TxtTienda = (new GuiaDeRemisionSQL().BuscarTIENfromID(Alm.IdTienda)).Nombre;
        }

        public void BuscarAlmacen()
        {
            if (seleccionadoTipo != null)
                if (SeleccionadoTipo.Equals("GR-TRASLADO EXTERNO"))
                {
                    _windowManager.ShowWindow(new BuscarAlmacenViewModel(_windowManager, this));
                }

                else
                {
                    _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Es un traslado por ORDEN DE DESPACHO \nNo entre ALMACENES"));
                }
            else 
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No se Selecciona tipo de la Guia"));
            }
        }

        public void BuscarNota()
        {
            _windowManager.ShowWindow(new BuscarNotasViewModel(_windowManager, this));
        }

        public void BuscarOrden()
        {
            _windowManager.ShowWindow(new BuscarOrdenDespachoViewModel(_windowManager, this));
        }

        public void BuscarNotaOrden()
        {
            if (String.IsNullOrEmpty(SeleccionadoTipo))
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Seleccione el TIPO de traslado"));
            }

            else
            {
                if (SeleccionadoTipo.Equals("GR-ORDEN DESPACHO"))
                {
                    BuscarOrden();
                }

                if (SeleccionadoTipo.Equals("GR-TRASLADO EXTERNO"))
                {
                    BuscarNota();
                }
            }


        }

        public void GuardarGuiaDeRemision()
        {

            int k = 1;

            if ((Nota != null) && (Alm != null))
            {
                g.Almacen = Alm;
                g.Nota = Nota;
            }

            if (Orden != null)
                g.Orden = Orden;
            
            g.CodGuiaRem = txtCodigo;
            g.FechaTraslado = txtFechaReg;
            g.FechaReg = DateTime.Now;
            g.Tipo = SeleccionadoTipo;
            g.Conductor = txtConductor;
            g.Camion = SeleccionadoCamion;
            g.Observaciones = txtObservaciones;

            if (indicador == 1)
            {
                if ((LstProductos != null) || (!String.IsNullOrEmpty(TxtConductor)) || (!String.IsNullOrEmpty(SeleccionadoCamion)))
                {
                    if (((Nota != null) && (TxtTienda != null)) || (Orden != null))
                    {
                        if (String.IsNullOrEmpty(TxtObservaciones)) TxtObservaciones = "NN";

                        k = new GuiaDeRemisionSQL().agregarGuiaDeRemision(g);
                        indicador = 0;

                        if (k == 0)
                            _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Ocurrio un error"));
                        else
                        {
                            _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Guia de Remision Registrada \n\nCodigo = " + txtCodigo + "\nFecha de Registro = " + txtFechaReg + "\nDireccion Partida = " + txtDirPartida +
                                            "\nTipo Guia = " + seleccionadoTipo + "\nConductor = " + txtConductor + "\nCamion= " +
                                            seleccionadoCamion + "\nObservaciones = " + txtObservaciones));
                        }
                    }

                    else
                    {
                        if (TxtTienda == null)
                            _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Ingrese DATOS faltantes"));

                        if (Orden == null)
                            _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Ingrese DATOS faltantes"));
                    }

                }

                else
                {
                    _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Ingrese datos VALIDOS, por favor REVISAR"));
                }
            }

            if (indicador == 2)
            {

                k = new GuiaDeRemisionSQL().editarGuiaDeRemision(g);

                if (k == 0)
                    _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Ocurrio un error"));
                else
                    _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Guia de Remision Actualizada \n\nCodigo = " + txtCodigo + "\nFecha de Registro = " + txtFechaReg + "\nDireccion Partida = " + txtDirPartida +
                                    "\nTipo Guia = " + cbTipo + "\nConductor = " + txtConductor + "\nCamion= " +
                                     cbCamion + "\nObservaciones = " + txtObservaciones));
            }
            
        }

        public Tienda getTiendafromID(int id)
        {
            List<Tienda> list = new TiendaSQL().BuscarTienda();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].IdTienda == id)
                    return list[i];
            }

            return null;
        }

        public Venta getVentafromID(int id)
        {
            Venta c = new VentaSQL().buscarVentaPorId(id);
            return c;
        }

        public Cliente getClientefromID(int id)
        {
            Cliente c = new ClienteSQL().BuscarClienteByIdCliente(id);
            return c;
        }

        public Usuario getUsuariofromID(int id)
        {
            Usuario u = UsuarioSQL.buscarUsuarioPorIdUsuario(id);
            return u;
        }

        public Almacenes getAlmacenfromIDTienda(int id)
        {
            List<Almacenes> list = new AlmacenSQL().BuscarAlmacen();
            for (int i = 0; i < list.Count; i++)
                if (list[i].IdTienda == id)
                {
                    return list[i];
                }

            return null;
        }



        public void GenerarPDF()
        {

            if (g != null)
            {
                    try
                    {
                        GenerarPDF pdf = new GenerarPDF();
                        Correo c = new Correo();
                        //m.coloma@pucp.pe
                        string path = "//GuiaRemision-" + g.CodGuiaRem + ".pdf";
                        pdf.Borrar(Environment.CurrentDirectory + path);
                        string body = formato().ToString();
                        pdf.createPDF(body, path, true);
                        
                        //c.EnviarCorreo("ORDEN DE COMPRA AL " + DateTime.Now.ToString(), OrdenSelected.Proveedor.Email, msg, Environment.CurrentDirectory + path);


                    }
                    catch (Exception e)
                    {
                        _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No se pudo imprimir Guia \nRevisar conexiones"));
                    }
                
            }
        }

        public string formato()
        {

            string content = @"<HTML><BODY>";
            content += "<center> MadeInHouse  S.A. </center><br><br> ";
            content += "Guia de Remision  " + TxtCodigo + "<br>";
            content += "Tipo de traslado  " + SeleccionadoTipo + "<br>";
            content += "<br><br>";
            content += "DATOS DE ORIGEN: <br>";
            content += "Tienda : " + tiendaOrigen + "<br>";
            content += "Almacen : " + TxtAlmacenOrigen + "<br>";
            content += "Direccion : " + TxtDirPartida + "<br>";
            content += "<br><br>";
            content += "DATOS DESTINO: <br>";

            if (Orden != null)
            {
                content += "Cliente : " + TxtTienda + "<br>";
                content += "Telefono : " + Orden.Telefono + "<br>";
            }

            if (Alm != null)
                content += "Almacen : " + TxtTienda + "<br>";

            content += "Direccion : " + TxtDirLlegada + "<br>";
            content += "<br><br>";
            content += "DATOS GENERALES: <br>";
            content += "Fecha de traslado : " + TxtFechaReg.ToString() + "<br>";
            content += "Conductor asignado : " + TxtConductor + "<br>";
            content += "Camion asignado : " + SeleccionadoCamion + "<br>";
            content += "Terminos de entrega : Se deberá firmar la Guia recibida para confirmar llegada, en caso <br>";
            content += "traslado externo, registrar los productos enviados en dicha tienda<br><br>";
            content += "<table border = 1 ><tr><th>NRO</th><th>ARTICULO</th><th>CANTIDAD</th><tr>";
          
            int i = 1;
            foreach (GuiaRemxProducto o in LstProductos)
            {

                content += "<tr><td>" + i.ToString() + "</td>" +
                               "<td>" + o.Nombre + "</td>" +
                               "<td>" + o.Cantidad.ToString() + "</td></tr>";
                i++;
            }

            content += "<tr><td colspan = 2 > TOTAL</td><td>" + TxtCantidad.ToString() + "</td> </tr></table>";
            content += "<br>";
            content += "Observaciones :" + TxtObservaciones;
            content += "<br><br>";
            content += "<br><br>";
            content += "____________________________<br>";

            if (Alm != null)
                content += "Firma del encargado";
            
            if (Orden != null)
                content += "Firma del Cliente";

            content += "</BODY></HTML>";

            return content;
        }

    }
}
