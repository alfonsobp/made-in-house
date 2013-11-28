using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Caliburn.Micro;
using MadeInHouse.Models.Ventas;
using MadeInHouse.DataObjects.Ventas;
using MadeInHouse.Models.Almacen;
using System.Windows.Controls;
using MadeInHouse.Models;
using MadeInHouse.ViewModels.Almacen;
using MadeInHouse.ViewModels.Compras;
using System.Windows;
using MadeInHouse.Validacion;
using System.Windows.Input;
using MadeInHouse.Models.Compras;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.Dictionary;
using MadeInHouse.DataObjects;
using System.Data.SqlClient;
using System.Data;
using MadeInHouse.DataObjects.Almacen;

namespace MadeInHouse.ViewModels.Ventas
{
    class VentaRegistrarViewModel : PropertyChangedBase
    {
        #region Atributos
        private double IGV { get; set; }
        private double PUNTO { get; set; }
        private double subt { get; set; }
        private double desc { get; set; }
        private double igv_total { get; set; }
        private double total { get; set; }
        private double montopago { get; set; }
        public int idTienda { get; set; }

        private Dictionary<string, int> tipoVenta = new Dictionary<string, int>()
        {
            { "Boleta", 0 }, { "Factura", 1 }
        };
        public BindableCollection<string> cmbTipoVenta
        {
            get
            {
                return new BindableCollection<string>(tipoVenta.Keys);
            }
        }

        private DetalleVenta detalleSeleccionado;
        public void SelectedItemChanged(object sender)
        {
            detalleSeleccionado = ((sender as DataGrid).SelectedItem as DetalleVenta);
        }

        private VentaPago detalleMontoSeleccionado;
        public void SelectedMontoChanged(object sender)
        {
            detalleMontoSeleccionado = ((sender as DataGrid).SelectedItem as VentaPago);
        }

        private DetalleVentaServicio detalleServicioSeleccionado;
        public void SelectedItemServicioChanged(object sender)
        {
            detalleServicioSeleccionado = ((sender as DataGrid).SelectedItem as DetalleVentaServicio);
        }

        private string txtCliente;
        public string TxtCliente
        {
            get { return txtCliente; }
            set { txtCliente = value; NotifyOfPropertyChange(() => TxtCliente); }
        }

        private Tarjeta cli;
        public Tarjeta Cli
        {
            get { return cli; }
            set
            {
                cli = value; NotifyOfPropertyChange(() => Cli);
                TxtCliente = cli.Cliente.RazonSocial;
                TxtRazonSocial = cli.Cliente.RazonSocial;
                TxtRuc = cli.Cliente.Ruc;
                TxtTelefono = cli.Cliente.Telefono;
                TxtDireccion = cli.Cliente.Direccion;
                TxtTarjetaCliente = cli.CodTarjeta;
            }
        }

        private string txtTarjetaCliente;
        public string TxtTarjetaCliente
        {
            get { return txtTarjetaCliente; }
            set { txtTarjetaCliente = value; NotifyOfPropertyChange(() => TxtTarjetaCliente); }
        }

        private string idCliente;
        public string IdCliente
        {
            get { return idCliente; }
            set { idCliente = value; NotifyOfPropertyChange(() => IdCliente); }
        }

        private string txtRazonSocial;
        public string TxtRazonSocial
        {
            get { return txtRazonSocial; }
            set { txtRazonSocial = value; NotifyOfPropertyChange(() => TxtRazonSocial); }
        }

        private string txtDireccion;
        public string TxtDireccion
        {
            get { return txtDireccion; }
            set { txtDireccion = value; NotifyOfPropertyChange(() => TxtDireccion); }
        }

        private string txtRuc;
        public string TxtRuc
        {
            get { return txtRuc; }
            set { txtRuc = value; NotifyOfPropertyChange(() => TxtRuc); }
        }

        private string txtTelefono;
        public string TxtTelefono
        {
            get { return txtTelefono; }
            set { txtTelefono = value; NotifyOfPropertyChange(() => TxtTelefono); }
        }

        private string txtServicio;
        public string TxtServicio
        {
            get { return txtServicio; }
            set { txtServicio = value; NotifyOfPropertyChange(() => TxtServicio); }
        }

        private string txtProducto;
        public string TxtProducto
        {
            get { return txtProducto; }
            set { txtProducto = value; NotifyOfPropertyChange(() => TxtProducto); }
        }

        private string txtCantidad;
        public string TxtCantidad
        {
            get { return txtCantidad; }
            set { txtCantidad = value; NotifyOfPropertyChange(() => TxtCantidad); }
        }

        private List<DetalleVenta> lstVenta;
        public List<DetalleVenta> LstVenta
        {
            get { return lstVenta; }
            set { lstVenta = value; NotifyOfPropertyChange(() => LstVenta); }
        }

        private List<DetalleVentaServicio> lstVentaServicios;
        public List<DetalleVentaServicio> LstVentaServicios
        {
            get { return lstVentaServicios; }
            set { lstVentaServicios = value; NotifyOfPropertyChange(() => LstVentaServicios); }
        }

        private string txtSubTotal;
        public string TxtSubTotal
        {
            get { return txtSubTotal; }
            set { txtSubTotal = value; NotifyOfPropertyChange(() => TxtSubTotal); }
        }

        private string txtDescuentoTotal;
        public string TxtDescuentoTotal
        {
            get { return txtDescuentoTotal; }
            set { txtDescuentoTotal = value; NotifyOfPropertyChange(() => TxtDescuentoTotal); }
        }

        private string txtIGVTotal;
        public string TxtIGVTotal
        {
            get { return txtIGVTotal; }
            set { txtIGVTotal = value; NotifyOfPropertyChange(() => TxtIGVTotal); }
        }

        private string txtTotal;
        public string TxtTotal
        {
            get { return txtTotal; }
            set { txtTotal = value; NotifyOfPropertyChange(() => TxtTotal); }
        }

        private string txtPagaCon;
        public string TxtPagaCon
        {
            get { return txtPagaCon; }
            set
            {
                txtPagaCon = value;
                NotifyOfPropertyChange(() => TxtPagaCon);

                try
                {
                    if (txtPagaCon != null && !txtPagaCon.Equals(""))
                    {
                        TxtVuelto = (Convert.ToDouble(txtPagaCon) - Convert.ToDouble(txtTotal)).ToString();
                        NotifyOfPropertyChange(() => TxtVuelto);
                    }
                    else
                    {
                        TxtVuelto = ""; NotifyOfPropertyChange(() => TxtVuelto);
                    }
                }
                catch (FormatException e)
                {
                    Console.Write(e.ToString());
                }
            }
        }

        private string txtVuelto;
        public string TxtVuelto
        {
            get { return txtVuelto; }
            set { txtVuelto = value; NotifyOfPropertyChange(() => TxtVuelto); }
        }

        private DateTime fechaDespacho = DateTime.Now;
        public DateTime FechaDespacho
        {
            get { return fechaDespacho; }
            set { fechaDespacho = value; NotifyOfPropertyChange(() => FechaDespacho); }
        }

        Producto prod;
        public Producto Prod
        {
            get { return prod; }
            set { prod = value; NotifyOfPropertyChange(() => Prod); TxtProducto = prod.Nombre; }
        }

        private List<VentaPago> lstPagos;
        public List<VentaPago> LstPagos
        {
            get { return lstPagos; }
            set { lstPagos = value; NotifyOfPropertyChange(() => LstPagos); }
        }

        private int selectedValue;
        public int SelectedValue
        {
            get { return selectedValue; }
            set { selectedValue = value; }
        }

        private string txtMonto;
        public string TxtMonto
        {
            get { return txtMonto; }
            set { txtMonto = value; NotifyOfPropertyChange(() => TxtMonto); }
        }

        private ServicioxProducto serv;
        public ServicioxProducto Serv
        {
            get { return serv; }
            set
            {
                serv = value; NotifyOfPropertyChange(() => Serv);
                ServicioxProductoSQL spsql = new ServicioxProductoSQL();
                TxtServicio = spsql.ServiciobyId(serv.IdServicio).Descripcion;
            }
        }

        #endregion

        #region Constructor
        public VentaRegistrarViewModel()
        {
            lstVenta = new List<DetalleVenta>();
            lstPagos = new List<VentaPago>();
            lstVentaServicios = new List<DetalleVentaServicio>();

            prod = new Producto();
            IGV = 0.18;
            PUNTO = 30;
            subt = 0;
            desc = 0;
            igv_total = 0;
            montopago = 0;

            TiendaSQL tiendSQL = new TiendaSQL();
            this.idTienda = tiendSQL.obtenerTienda(Int32.Parse(Thread.CurrentPrincipal.Identity.Name));

            ModoPagoSQL mpsql = new ModoPagoSQL();
            LstModosPago = mpsql.BuscarModosPago();
        }

        private BindableCollection<ModoPago> lstModosPago;

        public BindableCollection<ModoPago> LstModosPago
        {
            get { return lstModosPago; }
            set
            {
                if (this.lstModosPago == value)
                {
                    return;
                }
                this.lstModosPago = value;
                this.NotifyOfPropertyChange(() => this.lstModosPago);
            }
        }
        #endregion

        #region Metodos
        public void ExecuteFilterView(KeyEventArgs keyArgs)
        {
            Cliente c = new Cliente();
            if (keyArgs.Key == Key.Enter)
            {
                //buscar al cliente por la tarjeta
                ClienteSQL csql = new ClienteSQL();
                c = csql.BuscarClienteByTarjeta(TxtTarjetaCliente);
                if (c == null)
                {
                    MessageBox.Show("La Tarjeta ingresada no esta registrata en el sistema", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                    TxtTarjetaCliente = "";
                    return;
                }
                TxtRazonSocial = c.RazonSocial;
                TxtRuc = c.Ruc.Trim();
                TxtCliente = c.NombreCompleto;
                cli.Cliente = c;
            }
        }

        private void ActualizaCampos(DetalleVenta dv, int tipo, List<DetalleVenta> lst = null)
        {
            //tipo = 1 -> aumenta detalle de venta
            if (tipo == 1)
            {
                desc += dv.Descuento;
                total += Math.Round(dv.SubTotal - desc, 2);
                subt = Math.Round(total / (1 + IGV), 2);
                igv_total = Math.Round(subt * IGV, 2);
                //TxtDescuentoTotal = desc.ToString();
                TxtTotal = total.ToString();
                TxtSubTotal = subt.ToString();
                TxtIGVTotal = igv_total.ToString();
            }
            //tipo 2 es para descontar al quitar una linea del detalle
            if (tipo == 2)
            {
                desc -= dv.Descuento;
                total -= Math.Round(dv.SubTotal - dv.Descuento, 2);
                subt = Math.Round(total / (1 + IGV), 2);
                igv_total = Math.Round(subt * IGV, 2);
                //TxtDescuentoTotal = desc.ToString();
                TxtTotal = total.ToString();
                TxtSubTotal = subt.ToString();
                TxtIGVTotal = igv_total.ToString();
                /*int c = lst.Count();
                if (c == 0)
                {
                    TxtDescuentoTotal = "";
                    TxtTotal = "";
                    TxtSubTotal = "";
                    TxtIGVTotal = "";
                }*/
            }

            TxtPagaCon = txtPagaCon;
        }

        private double CalculaDescuento(int idProd, int cant)
        {
            return 0;
        }

        public void AgregarDetalle()
        {
            Evaluador ev = new Evaluador();
            int nuevo = 1;
            int cant;

            if (String.IsNullOrEmpty(TxtProducto))
            {
                MessageBox.Show("No ha ingresado ningún producto", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<DetalleVenta> aux = new List<DetalleVenta>();
            foreach (DetalleVenta item in LstVenta)
            {
                if (item.IdProducto == prod.IdProducto)
                {
                    if (ev.esNumeroEntero(TxtCantidad) && ev.esPositivo(Convert.ToInt32(TxtCantidad))) item.Cantidad += Int32.Parse(TxtCantidad);
                    else {
                        MessageBox.Show("Tiene que poner una cantidad");
                        return;
                    }
                    item.SubTotal = item.Cantidad * prod.Precio;
                    //item.Descuento += CalculaDescuento(prod.IdProducto, item.Cantidad);

                    desc = 0;// item.Descuento;
                    //TxtDescuentoTotal = desc.ToString();

                    total += Math.Round(Int32.Parse(TxtCantidad) * prod.Precio, 2);
                    TxtTotal = total.ToString();

                    subt = Math.Round(total / (1 + IGV), 2);
                    TxtSubTotal = subt.ToString();

                    igv_total = Math.Round(subt * IGV, 2);
                    TxtIGVTotal = igv_total.ToString();

                    TxtPagaCon = txtPagaCon;

                    nuevo = 0;
                }
                aux.Add(item);
            }

            if (nuevo == 1)
            {
                DetalleVenta dv = new DetalleVenta();
                //Producto p = new DetalleVentaSQL().Buscar(TxtProducto);
                dv.IdProducto = prod.IdProducto;
                dv.CodProducto = prod.CodigoProd;
                dv.Descripcion = prod.Nombre;
                dv.Descuento = 0;
                dv.Precio = prod.Precio;
                dv.Unidad = prod.UnidadMedida;

                if (ev.esNumeroEntero(TxtCantidad)) cant = Int32.Parse(TxtCantidad);
                else
                {
                    MessageBox.Show("Tiene que poner una cantidad");
                    return;
                }

                dv.Descuento = CalculaDescuento(prod.IdProducto, cant);
                dv.SubTotal = prod.Precio * cant;
                dv.Cantidad = cant;
                aux.Add(dv);
                ActualizaCampos(dv, 1);
            }
            LstVenta = aux;
            TxtCantidad = "";
        }

        public void QuitarDetalleProducto()
        {
            DetalleVenta aux;
            aux = detalleSeleccionado;
            if (aux != null)
            {
                LstVenta.Remove(aux);
                LstVenta = new List<DetalleVenta>(LstVenta);
                ActualizaCampos(aux, 2, LstVenta);
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningun producto");
            }
        }

        public void AgregarDetalleServicio()
        {
            Evaluador ev = new Evaluador();
            int nuevo = 1;

            List<DetalleVentaServicio> aux = new List<DetalleVentaServicio>();
            foreach (DetalleVentaServicio item in LstVentaServicios)
            {
                if (item.IdServicio == serv.IdServicio)
                {
                    nuevo = 0;
                }
                aux.Add(item);
            }

            if (nuevo == 1)
            {
                DetalleVentaServicio dv = new DetalleVentaServicio();
                ServicioxProductoSQL sxpsql = new ServicioxProductoSQL();
                dv.Servicio = sxpsql.ServiciobyId(serv.IdServicio);
                dv.IdProducto = serv.Producto.IdProducto;
                dv.IdServicio = serv.IdServicio;
                dv.Descripcion = dv.Servicio.Descripcion;
                dv.Precio = serv.Precio;

                aux.Add(dv);

                total += Math.Round(dv.Precio, 2);
                TxtTotal = total.ToString();
                subt = Math.Round(total / (1 + IGV), 2);
                TxtSubTotal = subt.ToString();
                igv_total = Math.Round((subt) * IGV, 2);
                TxtIGVTotal = igv_total.ToString();
                TxtPagaCon = txtPagaCon;
            }
            LstVentaServicios = aux;
            TxtServicio = "";
        }

        public void QuitarDetalleServicio()
        {
            DetalleVentaServicio aux;
            aux = detalleServicioSeleccionado;
            if (aux != null)
            {
                LstVentaServicios.Remove(aux);
                LstVentaServicios = new List<DetalleVentaServicio>(LstVentaServicios);
                total -= Math.Round(aux.Precio, 2);
                subt = Math.Round(total / (1 + IGV), 2);
                igv_total = Math.Round(subt * IGV, 2);
                //TxtDescuentoTotal = desc.ToString();
                TxtTotal = total.ToString();
                TxtSubTotal = subt.ToString();
                TxtIGVTotal = igv_total.ToString();
                TxtPagaCon = txtPagaCon;
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningun servicio");
            }
        }

        public void GuardarVenta(string cmbTipoVenta)
        {
            int numFilas = LstVenta.Count();
            if (numFilas > 0)
            {
                if (Validar())
                {
                    Venta v = new Venta();
                    v.LstDetalle = new List<DetalleVenta>();
                    v.LstPagos = new List<VentaPago>();
                    v.LstDetalleServicio = new List<DetalleVentaServicio>();
                    //guardar datos de la venta

                    if (tipoVenta[cmbTipoVenta] == 0)
                        v.TipoDocPago = "Boleta";
                    else
                    {
                        v.TipoDocPago = "Factura";
                        if (String.IsNullOrEmpty(TxtRazonSocial) || String.IsNullOrEmpty(TxtRuc))
                        {
                            MessageBox.Show("Se requiere Razón Social y ruc para la factura", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }


                    v.NumDocPago = null;
                    v.IdUsuario = Convert.ToInt32(Thread.CurrentPrincipal.Identity.Name);
                    v.Direccion = TxtDireccion;
                    v.Telefono = TxtTelefono;
                    v.IdCliente = cli.Cliente.Id;
                    v.CodTarjeta = Convert.ToInt32(cli.CodTarjeta);

                    v.FechaReg = System.DateTime.Now;
                    v.FechaDespacho = fechaDespacho;
                    v.TipoVenta = "Obra";
                    v.Estado = 1;

                    //guardar detalle de productos de la venta
                    foreach (DetalleVenta dv in lstVenta)
                    {
                        v.LstDetalle.Add(dv);
                    }

                    //guardar detalle de servicios de la venta, si es que hay
                    if (LstVentaServicios.Count() > 0)
                    {
                        foreach (DetalleVentaServicio dvs in LstVentaServicios)
                        {
                            v.LstDetalleServicio.Add(dvs);
                        }
                    }

                    v.Monto = total;
                    v.Descuento = desc;
                    v.Igv = igv_total;
                    v.PtosGanados = Convert.ToInt32(v.Monto / PUNTO);

                    //guardar el pago de la venta
                    foreach (VentaPago vp in lstPagos)
                    {
                        if (vp.Nombre.Equals("Efectivo"))
                        {
                            vp.Monto -= Double.Parse(txtVuelto);
                        }
                        v.LstPagos.Add(vp);
                    }

                    //insertar la venta en la base de datos
                    DBConexion db = new DBConexion();
                    db.conn.Open();
                    SqlTransaction trans = db.conn.BeginTransaction(IsolationLevel.Serializable);
                    db.cmd.Transaction = trans;
                    VentaSQL vsql = new VentaSQL(db);
                    int k = vsql.AgregarVentaObra(v);
                    if (k != 0)
                    {
                        trans.Commit();
                        MessageBox.Show("Venta Realizada con Exito");
                        Limpiar();
                        if (v.TipoDocPago.Equals("Boleta"))
                        {
                            GenerarPDFBoletaProductos(v);
                            if (v.LstDetalleServicio.Count() > 0)
                                GenerarPDFBoletaServicios(v);
                        }
                        else
                        {
                            GenerarPDFFacturaProductos(v);
                            if (v.LstDetalleServicio.Count() > 0)
                                GenerarPDFFacturaServicios(v);
                        }
                    }
                    else
                    {
                        trans.Rollback();
                        MessageBox.Show("Ocurrio un Error en el proceso");
                    }
                }

            }
            else
            {
                MessageBox.Show("Falta ingresar produtos", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private bool Validar()
        {
            Evaluador e = new Evaluador();
            if (String.IsNullOrEmpty(TxtRazonSocial) || String.IsNullOrEmpty(TxtTelefono))
            {
                MessageBox.Show("Los datos de direccion y/o telefono son requeridos", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!e.evalString(TxtDireccion))
            {
                MessageBox.Show("No ha ingresado la direccion del cliente", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            int numFilas = LstPagos.Count();
            if (numFilas == 0)
            {
                MessageBox.Show("No se ha ingresado el monto de pago", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        public void BuscarCliente()
        {
            MyWindowManager wc = new MyWindowManager();
            wc.ShowWindow(new ClienteBuscarViewModel(this,1));
        }

        public void AgregarCliente()
        {
            MyWindowManager wc = new MyWindowManager();
            wc.ShowWindow(new ClienteRegistrarViewModel(this));
        }

        public void BuscarProducto()
        {
            MyWindowManager w = new MyWindowManager();
            w.ShowWindow(new ProductoBuscarViewModel(w,this,1));
        }

        public void BuscarServicio()
        {
            MyWindowManager ws = new MyWindowManager();
            ws.ShowWindow(new BuscadorServicioViewModel(this,1));
        }

        public void Limpiar()
        {
            TxtIGVTotal = "";
            TxtSubTotal = "";
            TxtTotal = "";
            TxtPagaCon = "";
            TxtProducto = "";
            TxtCantidad = "";
            TxtCliente = "";
            TxtDescuentoTotal = "";
            TxtDireccion = "";
            TxtRazonSocial = "";
            TxtRuc = "";
            TxtTelefono = "";
            LstVenta = null;
            subt = 0;
            desc = 0;
            igv_total = 0;
            total = 0;
            lstVenta = new List<DetalleVenta>();
            LstPagos = new List<VentaPago>();
            TxtMonto = "";
            TxtServicio = "";
            TxtTarjetaCliente = "";
            LstVentaServicios = new List<DetalleVentaServicio>();
        }

        public void AgregarMonto()
        {
            int numFilas = LstVenta.Count();
            int numFilasServicios = LstVentaServicios.Count();
            if (ValidaMonto())
            {
                if (((numFilas > 0) || (numFilasServicios > 0)))
                {
                    VentaPago vp = new VentaPago();
                    vp.IdModoPago = selectedValue;
                    vp.Monto = Double.Parse(TxtMonto);
                    if (selectedValue.ToString().Equals("1")) vp.Nombre = "Efectivo";
                    else vp.Nombre = "Tarjeta";

                    montopago += Double.Parse(TxtMonto);
                    TxtPagaCon = montopago.ToString();

                    List<VentaPago> aux = new List<VentaPago>();
                    foreach (VentaPago item in LstPagos)
                    {
                        aux.Add(item);
                    }
                    aux.Add(vp);
                    LstPagos = aux;
                    TxtMonto = "";
                }
                else
                {
                    MessageBox.Show("No ha ingresado productos o servicios", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void QuitarMonto()
        {
            VentaPago aux;
            aux = detalleMontoSeleccionado;
            if (aux != null)
            {
                LstPagos.Remove(aux);
                LstPagos = new List<VentaPago>(LstPagos);
            }
            else
            {
                MessageBox.Show("No se ha seleccionado Monto");
            }
        }

        private bool ValidaMonto()
        {
            Evaluador e = new Evaluador();
            if (String.IsNullOrEmpty(TxtMonto))
            {
                MessageBox.Show("Debe ingresar un monto", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!e.esNumeroReal(TxtMonto) && e.esPositivo(Convert.ToInt32(TxtMonto)))
            {
                MessageBox.Show("No ha ingresado un valor correcto en el Monto de pago", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (selectedValue == 0)
            {
                MessageBox.Show("No ha seleccionado una forma de pago", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            return true;
        }

        public void GenerarPDFBoletaProductos(Venta v)
        {
            string pathString = Environment.CurrentDirectory + @"\Archivos\DocumentosPago";
            System.IO.Directory.CreateDirectory(pathString);
            GenerarPDF pdf = new GenerarPDF();
            string body = formatoBP(v).ToString();
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            pdf.createPDF(body, "\\Archivos\\DocumentosPago\\BP" + date + ".pdf", true);
        }

        public void GenerarPDFBoletaServicios(Venta v)
        {
            string pathString = Environment.CurrentDirectory + @"\Archivos\DocumentosPago";
            System.IO.Directory.CreateDirectory(pathString);
            GenerarPDF pdf = new GenerarPDF();
            string body = formatoBS(v).ToString();
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            pdf.createPDF(body, "\\Archivos\\DocumentosPago\\BS" + date + ".pdf", true);
        }

        public void GenerarPDFFacturaProductos(Venta v)
        {
            string pathString = Environment.CurrentDirectory + @"\Archivos\DocumentosPago";
            System.IO.Directory.CreateDirectory(pathString);
            GenerarPDF pdf = new GenerarPDF();
            string body = formatoFP(v).ToString();
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            pdf.createPDF(body, "\\Archivos\\DocumentosPago\\FP" + date + ".pdf", true);
        }

        public void GenerarPDFFacturaServicios(Venta v)
        {
            string pathString = Environment.CurrentDirectory + @"\Archivos\DocumentosPago";
            System.IO.Directory.CreateDirectory(pathString);
            GenerarPDF pdf = new GenerarPDF();
            string body = formatoFS(v).ToString();
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            pdf.createPDF(body, "\\Archivos\\DocumentosPago\\FS" + date + ".pdf", true);
        }

        public string formatoBP(Venta v)
        {
            double valorVenta = 0;
            double precioReal;
            string content =
                @"<html>
                    <body>
                        <table>
                            <tr>
                                <td>
                                    <span style='text-align: center; text-decoration: underline; font-size: 1em; font-weight: bold'>MadeInHouse S.A.</span><br>
                                    <span style='text-align: center; font-size: 0.5em'>
                                        Av. Priority N° xxx - San Miguel - Lima<br>
                                        Telf: 999-9999<br>
                                        www.MadeInHouse.com Email: info@mih.com
                                    </span>
                                </td>
                                <td></td>
                                <td border=1>
                                    <span style='text-align: center; font-size: 1em'>
                                        R.U.C. N° XXXXXXXXXXX<br>
                                        BOLETA<br>
                                        001 - N° " + v.NumDocPago.ToString().PadLeft(10, '0');
            content += "</span>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br>" +
                        "<table width=100%>" +
                            "<tr>" +
                                "<td width=200 border=1>";
            if (v.IdCliente != -1)
            {
                ClienteSQL cSQL = new ClienteSQL();
                Cliente cli = cSQL.BuscarClienteByIdCliente(v.IdCliente);
                content += "<span style='font-size: 0.5em'>" +
                                        "Señor (es): " + cli.ApePaterno + " " + cli.ApeMaterno + ", " + cli.Nombre + "<br>" +
                                        "Dirección : " + cli.Direccion + "<br>" +
                                    "</span>";
            }
            else
            {
                content += "<span style='font-size: 0.5em'>" +
                                        "DNI: " + v.dni + "<br>" +
                                    "</span>";
            }
            content += "</td>" +
                                "<td width=100>" +
                                    "<table border=1 width=150 align=center>" +
                                        "<tr><td><span style='text-align: center; font-size: 0.5em'>FECHA DE EMISIÓN</span></td></tr>" +
                                        "<tr><td><span style='text-align: center; font-size: 0.5em'>" + DateTime.Now.ToString("dd/MM/yyyy") + "</span></td></tr>" +
                                    "</table>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br>" +
                        "<table border=1 height=700>" +
                            "<tr>" +
                                "<th><span style='font-size: 0.5em'>CODIGO</span></th>" +
                                "<th><span style='font-size: 0.5em'>CANTIDAD</span></th>" +
                                "<th><span style='font-size: 0.5em'>DESCRIPCION</span></th>" +
                                "<th><span style='font-size: 0.5em'>PRECIO (S/.)</span></th>" +
                                "<th><span style='font-size: 0.5em'>SUB TOTAL (S/.)</span></th>" +
                            "</tr>";
            if (v.LstDetalle != null)
            {
                foreach (DetalleVenta dv in v.LstDetalle)
                {
                    content += "<tr>" +
                                "<td><span style='font-size: 0.5em'>" + dv.CodProducto + "</span></th>" +
                                "<td><span style='font-size: 0.5em'>" + dv.Cantidad + "</span></th>" +
                                "<td><span style='font-size: 0.5em'>" + dv.Descripcion + "</span></th>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(precioReal = (dv.Precio / 1.18), 2) + "</span></th>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(valorVenta += dv.Cantidad * precioReal, 2) + "</span></th>" +
                                "</tr>";
                }
            }
            content += "</table>" +
                        "<br>" +
                        "<table border=1>" +
                            "<tr>" +
                                "<td><span style='font-size: 0.5em'>VALOR VENTA (S/.)</span></td>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(valorVenta, 2) + "</span></td>" +
                                "<td><span style='font-size: 0.5em'>IGV (S/.)</span></td>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(0.18 * valorVenta, 2) + "</span></td>" +
                                "<td><span style='font-size: 0.5em'>TOTAL A PAGAR (S/.)</span></td>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(1.18 * valorVenta, 2) + "</span></td>" +
                            "</tr>" +
                        "</table>" +
                    "</body>" +
                "</html>";

            return content;
        }

        public string formatoBS(Venta v)
        {
            double valorVenta = 0;
            string content =
                @"<html>
                    <body>
                        <table>
                            <tr>
                                <td>
                                    <span style='text-align: center; text-decoration: underline; font-size: 1em; font-weight: bold'>MadeInHouse S.A.</span><br>
                                    <span style='text-align: center; font-size: 0.5em'>
                                        Av. Priority N° xxx - San Miguel - Lima<br>
                                        Telf: 999-9999<br>
                                        www.MadeInHouse.com Email: info@mih.com
                                    </span>
                                </td>
                                <td></td>
                                <td border=1>
                                    <span style='text-align: center; font-size: 1em'>
                                        R.U.C. N° XXXXXXXXXXX<br>
                                        BOLETA<br>
                                        001 - N° " + v.NumDocPagoServicio.ToString().PadLeft(10, '0');
            content += "</span>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br>" +
                        "<table width=100%>" +
                            "<tr>" +
                                "<td width=200 border=1>";
            if (v.IdCliente != -1)
            {
                ClienteSQL cSQL = new ClienteSQL();
                Cliente cli = cSQL.BuscarClienteByIdCliente(v.IdCliente);
                content += "<span style='font-size: 0.5em'>" +
                                        "Señor (es): " + cli.ApePaterno + " " + cli.ApeMaterno + ", " + cli.Nombre + "<br>" +
                                        "Dirección : " + cli.Direccion + "<br>" +
                                    "</span>";
            }
            else
            {
                content += "<span style='font-size: 0.5em'>" +
                                        "DNI: " + v.dni + "<br>" +
                                    "</span>";
            }
            content += "</td>" +
                                "<td width=100>" +
                                    "<table border=1 width=150 align=center>" +
                                        "<tr><td><span style='text-align: center; font-size: 0.5em'>FECHA DE EMISIÓN</span></td></tr>" +
                                        "<tr><td><span style='text-align: center; font-size: 0.5em'>" + DateTime.Now.ToString("dd/MM/yyyy") + "</span></td></tr>" +
                                    "</table>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br>" +
                        "<table border=1 height=700>" +
                            "<tr>" +
                                "<th><span style='font-size: 0.5em'>DESCRIPCION</span></th>" +
                                "<th><span style='font-size: 0.5em'>VALOR (S/.)</span></th>" +
                            "</tr>";
            if (v.LstDetalle != null)
            {
                foreach (DetalleVentaServicio dvs in v.LstDetalleServicio)
                {
                    content += "<tr>" +
                                "<td><span style='font-size: 0.5em'>" + dvs.Descripcion + "</span></th>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(valorVenta += (dvs.Precio / 1.18), 2) + "</span></th>" +
                                "</tr>";
                }
            }
            content += "</table>" +
                        "<br>" +
                        "<table border=1>" +
                            "<tr>" +
                                "<td><span style='font-size: 0.5em'>VALOR VENTA (S/.)</span></td>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(valorVenta, 2) + "</span></td>" +
                                "<td><span style='font-size: 0.5em'>IGV (S/.)</span></td>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(0.18 * valorVenta, 2) + "</span></td>" +
                                "<td><span style='font-size: 0.5em'>TOTAL A PAGAR (S/.)</span></td>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(1.18 * valorVenta, 2) + "</span></td>" +
                            "</tr>" +
                        "</table>" +
                    "</body>" +
                "</html>";

            return content;
        }

        public string formatoFP(Venta v)
        {
            double valorVenta = 0;
            double precioReal;
            string content =
                @"<html>
                    <body>
                        <table>
                            <tr>
                                <td>
                                    <span style='text-align: center; text-decoration: underline; font-size: 1em; font-weight: bold'>MadeInHouse S.A.</span><br>
                                    <span style='text-align: center; font-size: 0.5em'>
                                        Av. Priority N° xxx - San Miguel - Lima<br>
                                        Telf: 999-9999<br>
                                        www.MadeInHouse.com Email: info@mih.com
                                    </span>
                                </td>
                                <td></td>
                                <td border=1>
                                    <span style='text-align: center; font-size: 1em'>
                                        R.U.C. N° XXXXXXXXXXX<br>
                                        FACTURA<br>
                                        001 - N° " + v.NumDocPago.ToString().PadLeft(10, '0');
            content += "</span>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br>" +
                        "<table width=100%>" +
                            "<tr>" +
                                "<td width=200 border=1>";
            if (v.IdCliente != -1)
            {
                ClienteSQL cSQL = new ClienteSQL();
                Cliente cli = cSQL.BuscarClienteByIdCliente(v.IdCliente);
                content += "<span style='font-size: 0.5em'>" +
                                        "Señor (es): " + cli.RazonSocial + "<br>" +
                                        "Dirección : " + cli.Direccion + "<br>" +
                                        "R.U.C. N° : " + cli.Ruc +
                                    "</span>";
            }
            else
            {
                content += "<span style='font-size: 0.5em'>" +
                                        "Señor (es): " + v.RazonSocial + "<br>" +
                                        "Dirección : " + v.Direccion + "<br>" +
                                        "R.U.C. N° : " + v.Ruc +
                                    "</span>";
            }
            content += "</td>" +
                                "<td width=100>" +
                                    "<table border=1 width=150 align=center>" +
                                        "<tr><td><span style='text-align: center; font-size: 0.5em'>FECHA DE EMISIÓN</span></td></tr>" +
                                        "<tr><td><span style='text-align: center; font-size: 0.5em'>" + DateTime.Now.ToString("dd/MM/yyyy") + "</span></td></tr>" +
                                    "</table>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br>" +
                        "<table border=1 height=700>" +
                            "<tr>" +
                                "<th><span style='font-size: 0.5em'>CODIGO</span></th>" +
                                "<th><span style='font-size: 0.5em'>CANTIDAD</span></th>" +
                                "<th><span style='font-size: 0.5em'>DESCRIPCION</span></th>" +
                                "<th><span style='font-size: 0.5em'>PRECIO (S/.)</span></th>" +
                                "<th><span style='font-size: 0.5em'>SUB TOTAL (S/.)</span></th>" +
                            "</tr>";
            if (v.LstDetalle != null)
            {
                foreach (DetalleVenta dv in v.LstDetalle)
                {
                    content += "<tr>" +
                                "<td><span style='font-size: 0.5em'>" + dv.CodProducto + "</span></th>" +
                                "<td><span style='font-size: 0.5em'>" + dv.Cantidad + "</span></th>" +
                                "<td><span style='font-size: 0.5em'>" + dv.Descripcion + "</span></th>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(precioReal = (dv.Precio / 1.18), 2) + "</span></th>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(valorVenta += dv.Cantidad * precioReal, 2) + "</span></th>" +
                                "</tr>";
                }
            }
            content += "</table>" +
                        "<br>" +
                        "<table border=1>" +
                            "<tr>" +
                                "<td><span style='font-size: 0.5em'>VALOR VENTA (S/.)</span></td>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(valorVenta, 2) + "</span></td>" +
                                "<td><span style='font-size: 0.5em'>IGV (S/.)</span></td>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(0.18 * valorVenta, 2) + "</span></td>" +
                                "<td><span style='font-size: 0.5em'>TOTAL A PAGAR (S/.)</span></td>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(1.18 * valorVenta, 2) + "</span></td>" +
                            "</tr>" +
                        "</table>" +
                    "</body>" +
                "</html>";

            return content;
        }

        public string formatoFS(Venta v)
        {
            double valorVenta = 0;
            string content =
                @"<html>
                    <body>
                        <table>
                            <tr>
                                <td>
                                    <span style='text-align: center; text-decoration: underline; font-size: 1em; font-weight: bold'>MadeInHouse S.A.</span><br>
                                    <span style='text-align: center; font-size: 0.5em'>
                                        Av. Priority N° xxx - San Miguel - Lima<br>
                                        Telf: 999-9999<br>
                                        www.MadeInHouse.com Email: info@mih.com
                                    </span>
                                </td>
                                <td></td>
                                <td border=1>
                                    <span style='text-align: center; font-size: 1em'>
                                        R.U.C. N° XXXXXXXXXXX<br>
                                        FACTURA<br>
                                        001 - N° " + v.NumDocPagoServicio.ToString().PadLeft(10, '0');
            content += "</span>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br>" +
                        "<table width=100%>" +
                            "<tr>" +
                                "<td width=200 border=1>";
            if (v.IdCliente != -1)
            {
                ClienteSQL cSQL = new ClienteSQL();
                Cliente cli = cSQL.BuscarClienteByIdCliente(v.IdCliente);
                content += "<span style='font-size: 0.5em'>" +
                                        "Señor (es): " + cli.RazonSocial + "<br>" +
                                        "Dirección : " + cli.Direccion + "<br>" +
                                        "R.U.C. N° : " + cli.Ruc +
                                    "</span>";
            }
            else
            {
                content += "<span style='font-size: 0.5em'>" +
                                        "Señor (es): " + v.RazonSocial + "<br>" +
                                        "Dirección : " + v.Direccion + "<br>" +
                                        "R.U.C. N° : " + v.Ruc +
                                    "</span>";
            }
            content += "</td>" +
                                "<td width=100>" +
                                    "<table border=1 width=150 align=center>" +
                                        "<tr><td><span style='text-align: center; font-size: 0.5em'>FECHA DE EMISIÓN</span></td></tr>" +
                                        "<tr><td><span style='text-align: center; font-size: 0.5em'>" + DateTime.Now.ToString("dd/MM/yyyy") + "</span></td></tr>" +
                                    "</table>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br>" +
                        "<table border=1 height=700>" +
                            "<tr>" +
                                "<th><span style='font-size: 0.5em'>DESCRIPCION</span></th>" +
                                "<th><span style='font-size: 0.5em'>VALOR (S/.)</span></th>" +
                            "</tr>";
            if (v.LstDetalle != null)
            {
                foreach (DetalleVentaServicio dvs in v.LstDetalleServicio)
                {
                    content += "<tr>" +
                                "<td><span style='font-size: 0.5em'>" + dvs.Descripcion + "</span></th>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(valorVenta += (dvs.Precio / 1.18), 2) + "</span></th>" +
                                "</tr>";
                }
            }
            content += "</table>" +
                        "<br>" +
                        "<table border=1>" +
                            "<tr>" +
                                "<td><span style='font-size: 0.5em'>VALOR VENTA (S/.)</span></td>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(valorVenta, 2) + "</span></td>" +
                                "<td><span style='font-size: 0.5em'>IGV (S/.)</span></td>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(0.18 * valorVenta, 2) + "</span></td>" +
                                "<td><span style='font-size: 0.5em'>TOTAL A PAGAR (S/.)</span></td>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(1.18 * valorVenta, 2) + "</span></td>" +
                            "</tr>" +
                        "</table>" +
                    "</body>" +
                "</html>";

            return content;
        }
        #endregion
    }
}
