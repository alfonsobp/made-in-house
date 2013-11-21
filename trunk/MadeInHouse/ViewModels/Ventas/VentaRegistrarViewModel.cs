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
                    if (!txtPagaCon.Equals(""))
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

        private DateTime fechaDespacho = new DateTime(DateTime.Now.Year, 1, 1);
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
                c = csql.BuscarClienteByTarjeta(TxtCliente);
                TxtRazonSocial = c.RazonSocial;
                TxtRuc = c.Ruc.Trim();
                TxtCliente = c.NombreCompleto;
                cli.Cliente = c;
            }
        }

        public void AgregarDetalle()
        {
            Evaluador ev = new Evaluador();
            int nuevo = 1;
            int cant;

            List<DetalleVenta> aux = new List<DetalleVenta>();
            foreach (DetalleVenta item in LstVenta)
            {
                if (item.IdProducto == prod.IdProducto)
                {
                    if (ev.esNumeroEntero(TxtCantidad)) item.Cantidad += Int32.Parse(TxtCantidad);
                    else {
                        MessageBox.Show("Tiene que poner una cantidad");
                        return;
                    }
                    item.SubTotal = item.Cantidad * prod.Precio;
                    item.Descuento += 0;
                    if (subt > 0) subt += item.Cantidad; else subt = item.SubTotal;
                    TxtSubTotal = subt.ToString();
                    desc = item.Descuento;
                    TxtDescuentoTotal = desc.ToString();
                    igv_total = (subt - desc) * IGV;
                    TxtIGVTotal = igv_total.ToString();
                    total = (subt - desc) * (1 + IGV);
                    TxtTotal = total.ToString();
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

                if (ev.esNumeroEntero(TxtCantidad)) cant = Int32.Parse(TxtCantidad);
                else
                {
                    MessageBox.Show("Tiene que poner una cantidad");
                    return;
                }

                dv.SubTotal = prod.Precio * cant;
                dv.Cantidad = cant;
                aux.Add(dv);

                subt += dv.SubTotal;
                TxtSubTotal = subt.ToString();
                desc += dv.Descuento;
                TxtDescuentoTotal = desc.ToString();
                igv_total = (subt - desc) * IGV;
                TxtIGVTotal = igv_total.ToString();
                total = (subt - desc) * (1 + IGV);
                TxtTotal = total.ToString();
            }
            LstVenta = aux;
            
        }

        public void QuitarDetalleProducto()
        {
            DetalleVenta aux;
            aux = detalleSeleccionado;
            if (aux != null)
            {
                LstVenta.Remove(aux);
                LstVenta = new List<DetalleVenta>(LstVenta);
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

                subt += dv.Precio;
                TxtSubTotal = subt.ToString();
                igv_total = (subt) * IGV;
                TxtIGVTotal = igv_total.ToString();
                total = (subt) * (1 + IGV);
                TxtTotal = total.ToString();
            }
            LstVentaServicios = aux;
            
        }

        public void QuitarDetalleServicio()
        {
            DetalleVentaServicio aux;
            aux = detalleServicioSeleccionado;
            if (aux != null)
            {
                LstVentaServicios.Remove(aux);
                LstVentaServicios = new List<DetalleVentaServicio>(LstVentaServicios);
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
                        v.TipoDocPago = "Factura";

                    v.NumDocPago = null;
                    v.IdUsuario = Convert.ToInt32(Thread.CurrentPrincipal.Identity.Name);
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
                MessageBox.Show("Debe ingreasar datos de productos a la venta");
                return;
            }
        }

        private bool Validar()
        {
            Evaluador e = new Evaluador();
            if (!e.evalString(TxtRazonSocial))
            {
                MessageBox.Show("No ha ingresado la razon social del cliente", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
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
            w.ShowWindow(new ProductoBuscarViewModel(this,1));
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
            lstVenta = new List<DetalleVenta>();
            lstPagos = new List<VentaPago>();
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
                    vp.Nombre = selectedValue.ToString();

                    montopago += Double.Parse(TxtMonto);
                    TxtPagaCon = montopago.ToString();

                    List<VentaPago> aux = new List<VentaPago>();
                    foreach (VentaPago item in LstPagos)
                    {
                        aux.Add(item);
                    }
                    aux.Add(vp);
                    LstPagos = aux;
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
            if (String.IsNullOrEmpty(TxtMonto) && !e.esNumeroReal(TxtMonto))
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
            GenerarPDF pdf = new GenerarPDF();
            string body = formatoBP(v).ToString();
            pdf.createPDF(body, "\\BP.pdf",true);
        }

        public void GenerarPDFBoletaServicios(Venta v)
        {
            GenerarPDF pdf = new GenerarPDF();
            string body = formatoBS(v).ToString();
            pdf.createPDF(body, "\\BS.pdf",true);
        }

        public void GenerarPDFFacturaProductos(Venta v)
        {
            GenerarPDF pdf = new GenerarPDF();
            string body = formatoFP(v).ToString();
            pdf.createPDF(body, "\\FP.pdf",true);
        }

        public void GenerarPDFFacturaServicios(Venta v)
        {
            GenerarPDF pdf = new GenerarPDF();
            string body = formatoFS(v).ToString();
            pdf.createPDF(body, "\\FS.pdf",true);
        }

        public string formatoBP(Venta v)
        {
            string content = @"<HTML><BODY>";
            content += "<center> MadeInHouse  S.A. </center><br><br> ";
            content += "<center> Ruc. 99999999999 </center><br><br> ";
            content += "BOLETA " + v.NumDocPago + "<br><br>";
            content += "<br><br>";
            if (v.IdCliente != -1)
            {
                content += "Cliente : " + cli.Cliente.NombreCompleto + "<br><br>";
            }
            else
            {
                content += "Cliente : " + " " + "<br><br>";
            }
            content += "Fecha : " + v.FechaReg.ToString() + "<br><br>";
            content += "Detalle de Artículos: <br><br>";
            content += "<table border = 1 ><tr><th>COD</th><th>ARTICULO</th><th>PRECIO UNITARIO</th>" +
                        "<th>CANTIDAD</th><th>SUBTOTAL</th><tr>";
            double sumaAporte = 0;
            int i = 1;
            foreach (DetalleVenta dv in v.LstDetalle)
            {
                double parcial = dv.Precio * dv.Cantidad;
                content += "<tr><td>" + dv.IdProducto.ToString() + "</td>" +
                               "<td>" + dv.Descripcion + "</td>" +
                               "<td>" + dv.Precio.ToString() + "</td>" +
                               "<td>" + dv.Cantidad.ToString() + "</td>" +
                "<td>" + parcial.ToString() + "</td></tr>";
                i++;
                sumaAporte += parcial;
            }

            content += "<tr><td colspan = 4 > TOTAL</td><td>" + sumaAporte.ToString() + "</td> </tr></table>";
            content += "</BODY></HTML>";

            return content;
        }

        public string formatoBS(Venta v)
        {
            string content = @"<HTML><BODY>";
            content += "<center> MadeInHouse  S.A. </center><br><br> ";
            content += "<center> Ruc. 99999999999 </center><br><br> ";
            content += "BOLETA " + v.NumDocPagoServicio + "<br><br>";
            content += "<br><br>";
            if (v.IdCliente != -1)
            {
                content += "Cliente : " + cli.Cliente.NombreCompleto + "<br><br>";
            }
            else
            {
                content += "Cliente : " + " " + "<br><br>";
            }
            content += "Fecha : " + v.FechaReg.ToString() + "<br><br>";
            content += "Detalle de Artículos: <br><br>";
            content += "<table border = 1 ><tr><th>COD</th><th>ARTICULO</th><th>PRECIO UNITARIO</th><tr>";
            double sumaAporte = 0;
            int i = 1;
            foreach (DetalleVentaServicio dvs in v.LstDetalleServicio)
            {
                double parcial = dvs.Precio;
                content += "<tr><td>" + dvs.IdServicio.ToString() + "</td>" +
                               "<td>" + dvs.Descripcion + "</td>" +
                               "<td>" + dvs.Precio.ToString() + "</td></tr>";
                i++;
                sumaAporte += parcial;
            }

            content += "<tr><td colspan = 4 > TOTAL</td><td>" + sumaAporte.ToString() + "</td> </tr></table>";
            content += "</BODY></HTML>";

            return content;
        }

        public string formatoFP(Venta v)
        {
            string content = @"<HTML><BODY>";
            content += "<center> MadeInHouse  S.A. </center><br><br> ";
            content += "<center> Ruc. 99999999999 </center><br><br> ";
            content += "Factura " + v.NumDocPago + "<br><br>";
            content += "<br><br>";
            content += "Razón Social : " + cli.Cliente.RazonSocial + "<br>";
            content += "RUC : " + cli.Cliente.Ruc + "<br>";
            content += "Fecha : " + v.FechaReg.ToString() + "<br>";
            content += "Detalle de Artículos: <br><br>";
            content += "<table border = 1 ><tr><th>COD</th><th>ARTÍCULO</th><th>PRECIO UNITARIO</th>" +
                        "<th>CANTIDAD</th><th>SUBTOTAL</th><tr>";
            double sumaAporte = 0;
            int i = 1;
            foreach (DetalleVenta dv in v.LstDetalle)
            {
                double parcial = dv.Precio * dv.Cantidad;
                content += "<tr><td>" + dv.IdProducto.ToString() + "</td>" +
                               "<td>" + dv.Descripcion + "</td>" +
                               "<td>" + dv.Precio.ToString() + "</td>" +
                               "<td>" + dv.Cantidad.ToString() + "</td>" +
                "<td>" + parcial.ToString() + "</td></tr>";
                i++;
                sumaAporte += parcial;
            }

            content += "<tr><td colspan = 4 > SUBTOTAL</td><td>" + sumaAporte.ToString() + "</td> </tr>";
            content += "<tr><td colspan = 4 > IGV</td><td>" + (sumaAporte*0.18).ToString() + "</td> </tr>";
            content += "<tr><td colspan = 4 > TOTAL</td><td>" + (sumaAporte*1.18).ToString() + "</td> </tr></table>";
            content += "</BODY></HTML>";

            return content;
        }

        public string formatoFS(Venta v)
        {
            string content = @"<HTML><BODY>";
            content += "<center> MadeInHouse  S.A. </center><br><br> ";
            content += "<center> Ruc. 99999999999 </center><br><br> ";
            content += "FACTURA " + v.NumDocPagoServicio + "<br><br>";
            content += "<br><br>";
            content += "Razón Social : " + TxtRazonSocial + "<br>";
            content += "RUC : " + TxtRuc + "<br>";
            content += "Fecha : " + v.FechaReg.ToString() + "<br>";
            content += "Detalle de Artículos: <br><br>";
            content += "<table border = 1 ><tr><th>COD</th><th>ARTICULO</th><th>PRECIO UNITARIO</th><tr>";
            double sumaAporte = 0;
            int i = 1;
            foreach (DetalleVentaServicio dvs in v.LstDetalleServicio)
            {
                double parcial = dvs.Precio;
                content += "<tr><td>" + dvs.IdServicio.ToString() + "</td>" +
                               "<td>" + dvs.Descripcion + "</td>" +
                               "<td>" + dvs.Precio.ToString() + "</td></tr>";
                i++;
                sumaAporte += parcial;
            }

            content += "<tr><td colspan = 4 > SUBTOTAL</td><td>" + sumaAporte.ToString() + "</td> </tr>";
            content += "<tr><td colspan = 4 > IGV</td><td>" + (sumaAporte * 0.18).ToString() + "</td> </tr>";
            content += "<tr><td colspan = 4 > TOTAL</td><td>" + (sumaAporte * 1.18).ToString() + "</td> </tr></table>";
            content += "</BODY></HTML>";

            return content;
        }
        #endregion

    }
}
