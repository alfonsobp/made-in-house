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

namespace MadeInHouse.ViewModels.Ventas
{
    class VentaCajeroRegistrarViewModel : PropertyChangedBase
    {
        private double IGV { get; set; }
        private double PUNTO { get; set; }
        private double subt { get; set; }
        private double desc { get; set; }
        private double igv_total { get; set; }
        private double total { get; set; }
        private double montopago { get; set; }
        private Cliente cliente { get; set; }

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
        }

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

        //sacara datos del cliente por tarjeta
        private string txtCliente;

        public string TxtCliente
        {
            get { return txtCliente; }
            set { txtCliente = value; NotifyOfPropertyChange(() => TxtCliente); }
        }

        public void ExecuteFilterView(KeyEventArgs keyArgs)
        {
            if (keyArgs.Key == Key.Enter)
            {
                //buscar al cliente por la tarjeta
                ClienteSQL csql = new ClienteSQL();
                cliente = csql.BuscarClienteByTarjeta(TxtCliente);
                TxtRazonSocial = cliente.RazonSocial;
                TxtRuc = cliente.Ruc;
                TxtDNI = cliente.Dni;
            }
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
            set {
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

        public void BuscarServicio()
        {
            MyWindowManager ws = new MyWindowManager();
            ws.ShowWindow(new BuscadorServicioViewModel(this, 2));
        }
        
        public void AgregarDetalle()
        {
            Producto p = new DetalleVentaSQL().Buscar(TxtProducto);
            Evaluador ev = new Evaluador();
            int nuevo = 1;
            int cant;

            List<DetalleVenta> aux = new List<DetalleVenta>();
            foreach (DetalleVenta item in LstVenta)
            {
                if (item.IdProducto == p.IdProducto)
                {
                    if (ev.esNumeroEntero(TxtCantidad)) item.Cantidad += Int32.Parse(TxtCantidad);
                    else
                    {
                        MessageBox.Show("Tiene que poner una cantidad");
                        return;
                    }
                    item.SubTotal = item.Cantidad * p.Precio;
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
                dv.IdProducto = p.IdProducto;
                dv.CodProducto = p.CodigoProd;
                dv.Descripcion = p.Nombre;
                dv.Descuento = 0;
                dv.Precio = p.Precio;
                if (ev.esNumeroEntero(TxtCantidad)) cant = Int32.Parse(TxtCantidad);
                else
                {
                    MessageBox.Show("Tiene que poner una cantidad");
                    return;
                }
                dv.SubTotal = p.Precio * cant;
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
                    v.TipoDocPago = "Factura";

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
                VentaSQL vsql = new VentaSQL();
                if (v.IdCliente == -1)
                {
                    int k = vsql.AgregarSinCliente(v);
                    if (k != 0)
                    {
                        MessageBox.Show("Venta Realizada con Exito");
                        Limpiar();
                    }
                }
                else
                {
                    int k = vsql.Agregar(v);
                    if (k != 0)
                    {
                        MessageBox.Show("Venta Realizada con Exito");
                        Limpiar();
                    }
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
            subt = 0;
            desc = 0;
            igv_total = 0;
            lstVenta = new List<DetalleVenta>();
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

        public void AgregarMonto()
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

    }
}
