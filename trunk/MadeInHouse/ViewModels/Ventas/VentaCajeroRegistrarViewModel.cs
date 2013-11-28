using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Ventas;
using MadeInHouse.DataObjects.Ventas;
using System.Windows.Controls;
using System.Windows;
using MadeInHouse.Validacion;
using System.Windows.Input;
using System.Threading;
using MadeInHouse.Models.Compras;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.Models;
using MadeInHouse.ViewModels.Compras;
using MadeInHouse.Dictionary;
using MadeInHouse.DataObjects;
using System.Data.SqlClient;
using System.Data;
using MadeInHouse.DataObjects.Almacen;

namespace MadeInHouse.ViewModels.Ventas
{
    class VentaCajeroRegistrarViewModel : PropertyChangedBase
    {
        #region Atributos
        private double IGV { get; set; }
        private double PUNTO { get; set; }
        private double subt { get; set; }
        private double desc { get; set; }
        private double igv_total { get; set; }
        private double total { get; set; }
        private double montopago { get; set; }
        private Cliente cliente { get; set; }
        private int idTienda { get; set; }

        private List<DetalleVentaServicio> lstVentaServicios;
        public List<DetalleVentaServicio> LstVentaServicios
        {
            get { return lstVentaServicios; }
            set { lstVentaServicios = value; NotifyOfPropertyChange(() => LstVentaServicios); }
        }

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

        private string txtDNI;
        public string TxtDNI
        {
            get { return txtDNI; }
            set { txtDNI = value; NotifyOfPropertyChange(() => TxtDNI); }
        }
        
        private string txtCliente;
        public string TxtCliente
        {
            get { return txtCliente; }
            set { txtCliente = value; NotifyOfPropertyChange(() => TxtCliente); }
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
                        txtVuelto = (Convert.ToDouble(txtPagaCon) - Convert.ToDouble(txtTotal)).ToString();
                        NotifyOfPropertyChange(() => TxtVuelto);
                    }
                    else
                    {
                        txtVuelto = ""; NotifyOfPropertyChange(() => TxtVuelto);
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

        private string txtServicio;
        public string TxtServicio
        {
            get { return txtServicio; }
            set { txtServicio = value; NotifyOfPropertyChange(() => TxtServicio); }
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
        #endregion

        #region Constructor
        public VentaCajeroRegistrarViewModel()
        {
           lstVenta = new List<DetalleVenta>();
           LstPagos = new List<VentaPago>();
           lstVentaServicios = new List<DetalleVentaServicio>();
           cliente = new Cliente();
           IGV = 0.18;
           PUNTO = 30;
           subt = 0;
           desc = 0;
           igv_total = 0;
           total = 0;
           montopago = 0;
           ModoPagoSQL mpsql = new ModoPagoSQL();
           LstModosPago = mpsql.BuscarModosPago();
           idTienda = new TiendaSQL().obtenerTienda(Convert.ToInt32(Thread.CurrentPrincipal.Identity.Name));
        }
        #endregion

        #region Metodos
        //sacara datos del cliente por tarjeta
        public void ExecuteFilterView(KeyEventArgs keyArgs)
        {
            if (keyArgs.Key == Key.Enter)
            {
                //buscar al cliente por la tarjeta
                ClienteSQL csql = new ClienteSQL();
                cliente = csql.BuscarClienteByTarjeta(TxtCliente);
                if (cliente != null)
                {
                    TxtRazonSocial = cliente.RazonSocial;
                    TxtRuc = cliente.Ruc;
                    TxtDNI = cliente.Dni;
                }
                else
                {
                    MessageBox.Show("La tarjeta ingresada no se encuentra en el sistema", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                    TxtCliente = "";
                }
            }
        }

        //sacara datos del cliente por DNI
        public void ExecuteFilterViewDNI(KeyEventArgs keyArgs)
        {
            if (keyArgs.Key == Key.Enter)
            {
                //buscar al cliente por la tarjeta
                ClienteSQL csql = new ClienteSQL();
                cliente = csql.BuscarClienteByDNI(TxtDNI);
                if (cliente != null)
                {
                    TxtCliente = csql.BuscarTarjetaByIdCliente(cliente.Id);
                    TxtRazonSocial = cliente.RazonSocial;
                    TxtRuc = cliente.Ruc;
                    TxtDNI = cliente.Dni;
                }
                else
                {
                    MessageBox.Show("El DNI ingresado no corresponde a ningun cliente registrado", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                    TxtDNI = "";
                }
            }
        }

        public void BuscarServicio()
        {
            MyWindowManager ws = new MyWindowManager();
            ws.ShowWindow(new BuscadorServicioViewModel(this, 2));
        }
        private double CalculaDescuento(int idProd, int cantidad)
        {
            return 0;
        }

        private void ActualizaCampos(DetalleVenta dv, int tipo,List<DetalleVenta> lst = null)
        {
            //tipo = 1 -> aumenta detalle de venta
            if (tipo == 1)
            {
                desc += dv.Descuento;
                total += (dv.SubTotal - desc);
                subt = (total * (1 - IGV));
                igv_total = total * IGV;
                TxtDescuentoTotal = desc.ToString();
                TxtTotal = total.ToString();
                TxtSubTotal = subt.ToString();
                TxtIGVTotal = igv_total.ToString();
            }
            //tipo 2 es para descontar al quitar una linea del detalle
            if (tipo == 2)
            {
                desc -= dv.Descuento;
                total -= dv.SubTotal - dv.Descuento;
                igv_total -= total * IGV;
                subt -= (total * (1 - IGV));
                TxtDescuentoTotal = desc.ToString();
                TxtTotal = total.ToString();
                TxtSubTotal = subt.ToString();
                TxtIGVTotal = igv_total.ToString();
                int c = lst.Count();
                if (c == 0)
                {
                    TxtDescuentoTotal = "";
                    TxtTotal = "";
                    TxtSubTotal = "";
                    TxtIGVTotal = "";
                }
            }
        }

        public void AgregarDetalle()
        {
            if (String.IsNullOrEmpty(TxtProducto))
            {
                MessageBox.Show("No ha ingresado ningún producto", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DetalleVentaSQL dvsql = new DetalleVentaSQL();
            Producto p = dvsql.Buscar(TxtProducto,idTienda);
            if (p == null)
            {
                return;
            }
            Evaluador ev = new Evaluador();
            int nuevo = 1;
            int cant;

            List<DetalleVenta> aux = new List<DetalleVenta>();
            foreach (DetalleVenta item in LstVenta)
            {
                if (item.IdProducto == p.IdProducto)
                {
                    if (ev.esNumeroEntero(TxtCantidad) && ev.esPositivo(Convert.ToInt32(TxtCantidad))) item.Cantidad += Int32.Parse(TxtCantidad);
                    else
                    {
                        MessageBox.Show("Tiene que poner una cantidad");
                        return;
                    }
                    item.SubTotal = item.Cantidad * p.Precio;
                    item.Descuento += CalculaDescuento(p.IdProducto, item.Cantidad);

                    desc = item.Descuento;
                    TxtDescuentoTotal = desc.ToString();

                    total = item.SubTotal - desc;
                    TxtTotal = total.ToString();

                    subt = total * (1 - IGV);
                    TxtSubTotal = subt.ToString();
                    
                    igv_total = total * IGV;
                    TxtIGVTotal = igv_total.ToString();
                    
                    nuevo = 0;
                }
                aux.Add(item);
            }

            if (nuevo == 1)
            {
                DetalleVenta dv = new DetalleVenta();
                dv.IdProducto = p.IdProducto;
                dv.CodProducto = p.CodigoProd;
                dv.Descripcion = p.Nombre;
                dv.Unidad = p.UnidadMedida;
                
                dv.Precio = p.Precio;
                if (ev.esNumeroEntero(TxtCantidad)) cant = Int32.Parse(TxtCantidad);
                else
                {
                    MessageBox.Show("Tiene que poner una cantidad");
                    return;
                }

                dv.Descuento = CalculaDescuento(p.IdProducto,cant);
                dv.SubTotal = p.Precio * cant;
                dv.Cantidad = cant;
                aux.Add(dv);
                ActualizaCampos(dv,1);
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
                ActualizaCampos(aux, 2,LstVenta);
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

                total += dv.Precio;
                TxtTotal = total.ToString();
                subt += total/(1+IGV);
                TxtSubTotal = subt.ToString();
                igv_total = (total) * IGV;
                TxtIGVTotal = igv_total.ToString();
                
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
                Venta v = new Venta();
                v.LstDetalle = new List<DetalleVenta>();
                v.LstPagos = new List<VentaPago>();
                v.LstDetalleServicio = new List<DetalleVentaServicio>();
                //guardar datos de la venta
                //completar
                if (tipoVenta[cmbTipoVenta] == 0)
                    v.TipoDocPago = "Boleta";
                else
                {
                    v.TipoDocPago = "Factura";
                    //validar que los datos de ruc y razon social
                    if (!string.IsNullOrEmpty(TxtRuc) && !string.IsNullOrEmpty(TxtRazonSocial))
                    {
                        v.Ruc = TxtRuc;
                        v.RazonSocial = TxtRazonSocial;
                    }
                    else
                    {
                        MessageBox.Show("Falta ingresar Ruc o Razón Social", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                v.NumDocPago = null;
                v.TipoVenta = "Tienda";
                v.Estado = 1;
                v.FechaReg = System.DateTime.Now;
                v.IdUsuario = Convert.ToInt32(Thread.CurrentPrincipal.Identity.Name);
                //idCliente desde la tarjeta de este si es que hay
                if (!string.IsNullOrEmpty(TxtCliente))
                {
                    v.IdCliente = Convert.ToInt32(cliente.Id);
                    v.CodTarjeta = Convert.ToInt32(TxtCliente);
                }
                else
                {
                    v.IdCliente = -1;
                    v.CodTarjeta = -1;
                }

                //guardar detalle de la venta
                foreach (DetalleVenta dv in lstVenta)
                {
                    v.LstDetalle.Add(dv);
                }
                v.Monto = total;
                v.Descuento = desc;
                v.Igv = igv_total;

                v.PtosGanados = Convert.ToInt32(v.Monto / PUNTO);

                foreach (VentaPago vp in lstPagos)
                {
                    if (vp.Nombre.Equals("Efectivo"))
                    {
                        vp.Monto -= Double.Parse(txtVuelto);
                    }
                    v.LstPagos.Add(vp);
                }

                //guardar detalle de servicios de la venta, si es que hay
                if (LstVentaServicios.Count() > 0)
                {
                    foreach (DetalleVentaServicio dvs in LstVentaServicios)
                    {
                        v.LstDetalleServicio.Add(dvs);
                    }
                }


                //insertar en la base de datos
                DBConexion db = new DBConexion();
                db.conn.Open();
                SqlTransaction trans = db.conn.BeginTransaction(IsolationLevel.Serializable);
                db.cmd.Transaction = trans;
                VentaSQL vsql = new VentaSQL(db);
                if (v.IdCliente == -1)
                {
                    int k = vsql.AgregarSinCliente(v);
                    if (k != 0)
                    {

                        NotaISSQL ntgw = new NotaISSQL();
                        NotaIS nota = new NotaIS();
                        AlmacenSQL asql = new AlmacenSQL();
                        nota.IdAlmacen = asql.BuscarAlmacen(-1, idTienda, 2).IdAlmacen;
                        // Logica de  Referencia de documento

                        //Si existe documento de referencia colocar el ID
                        nota.IdDoc = v.IdVenta;

                        nota.IdMotivo = 9;
                        nota.IdResponsable = v.IdUsuario;
                        nota.Observaciones = "Venta en Cajero";
                        nota.Tipo = 2;
                        List<ProductoCant> LstProductos = new List<ProductoCant>();
                        List<ProductoCant> lpcan = new List<ProductoCant>();

                        for (int i = 0; i < v.LstDetalle.Count; i++)
                        {
                            Producto p = new ProductoSQL().Buscar_por_CodigoProducto(v.LstDetalle.ElementAt(i).IdProducto);
                            ProductoCant pcan = new ProductoCant();
                            pcan.IdProducto = p.IdProducto;
                            pcan.CodigoProd = p.CodigoProd;
                            pcan.Nombre = p.Nombre;
                            pcan.CanAtender = v.LstDetalle.ElementAt(i).Cantidad.ToString();
                            lpcan.Add(pcan);

                        }
                        LstProductos = new List<ProductoCant>(lpcan);


                        nota.LstProducto = LstProductos;

                        nota.IdNota = ntgw.AgregarNota(nota);
                        
                        

                        trans.Commit();
                        ntgw.AgregarNotaxSector(nota);
                        MessageBox.Show("Venta Realizada con Exito");
                        Limpiar();
                    }
                    else
                    {
                        trans.Rollback();
                        MessageBox.Show("Ocurrio un Error en el proceso");
                    }
                }
                else
                {
                    int k = vsql.Agregar(v);
                    if (k != 0)
                    {

                        NotaISSQL ntgw = new NotaISSQL();
                        NotaIS nota = new NotaIS();
                        AlmacenSQL asql = new AlmacenSQL();
                        nota.IdAlmacen = asql.BuscarAlmacen(-1, idTienda, 2).IdAlmacen;
                        // Logica de  Referencia de documento

                        //Si existe documento de referencia colocar el ID
                        nota.IdDoc = v.IdVenta;

                        nota.IdMotivo = 9;
                        nota.IdResponsable = v.IdUsuario;
                        nota.Observaciones = "Venta en Cajero";
                        nota.Tipo = 2;
                        List<ProductoCant> LstProductos = new List<ProductoCant>();
                        List<ProductoCant> lpcan = new List<ProductoCant>();

                        for (int i = 0; i < v.LstDetalle.Count; i++)
                        {
                            Producto p = new ProductoSQL().Buscar_por_CodigoProducto(v.LstDetalle.ElementAt(i).IdProducto);
                            ProductoCant pcan = new ProductoCant();
                            pcan.IdProducto = p.IdProducto;
                            pcan.CodigoProd = p.CodigoProd;
                            pcan.Nombre = p.Nombre;
                            pcan.CanAtender = v.LstDetalle.ElementAt(i).Cantidad.ToString();
                            lpcan.Add(pcan);

                        }
                        LstProductos = new List<ProductoCant>(lpcan);


                        nota.LstProducto = LstProductos;

                        nota.IdNota = ntgw.AgregarNota(nota);

                        trans.Commit();

                        ntgw.AgregarNotaxSector(nota);
                        MessageBox.Show("Venta Realizada con Exito");
                        Limpiar();
                    }
                    else
                    {
                        trans.Rollback();
                        MessageBox.Show("Ocurrio un Error en el proceso");
                    }
                }

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
                MessageBox.Show("Debe ingreasar datos de la venta");
                return;
            }
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
            LstVenta = null;
            desc = 0;
            total = 0;
            subt = 0;
            igv_total = 0;
            lstVenta = new List<DetalleVenta>();
            lstVentaServicios = new List<DetalleVentaServicio>();
            LstPagos = new List<VentaPago>();
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
            if (String.IsNullOrEmpty(TxtMonto) && !e.esNumeroReal(TxtMonto) && e.esPositivo(Convert.ToInt32(TxtMonto)))
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
                content += "Cliente : " + cliente.Nombre + " " + cliente.ApePaterno + " " + cliente.ApeMaterno + "<br><br>";
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
                content += "Cliente : " + cliente.Nombre + " " + cliente.ApePaterno + " " + cliente.ApeMaterno + "<br><br>";
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
            content += "Razón Social : " + TxtRazonSocial + "<br>";
            content += "RUC : " + TxtRuc + "<br>";
            content += "Fecha : " + v.FechaReg.ToString() + "<br>";
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

            content += "<tr><td colspan = 4 > SUBTOTAL</td><td>" + sumaAporte.ToString() + "</td> </tr>";
            content += "<tr><td colspan = 4 > IGV</td><td>" + (sumaAporte * 0.18).ToString() + "</td> </tr>";
            content += "<tr><td colspan = 4 > TOTAL</td><td>" + (sumaAporte * 1.18).ToString() + "</td> </tr></table>";
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
