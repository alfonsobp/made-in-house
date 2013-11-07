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
using System.Windows;
using MadeInHouse.Validacion;

namespace MadeInHouse.ViewModels.Ventas
{
    class VentaRegistrarViewModel : PropertyChangedBase
    {

        private double IGV { get; set; }
        private double PUNTO { get; set; }
        private double subt { get; set; }
        private double desc { get; set; }
        private double igv_total { get; set; }
        private double total { get; set; }
        private double montopago { get; set; }

        public VentaRegistrarViewModel()
        {
            lstVenta = new List<DetalleVenta>();
            LstPagos = new List<VentaPago>();
            List<string> ListaTiposVenta = new List<string>();
            ListaTiposVenta.Add("Boleta");
            ListaTiposVenta.Add("Factura");

            prod = new Producto();
            IGV = 0.18;
            PUNTO = 30;
            subt = 0;
            desc = 0;
            igv_total = 0;
            montopago = 0;

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


        private DetalleVenta detalleSeleccionado;

        public void SelectedItemChanged(object sender)
        {
            detalleSeleccionado = ((sender as DataGrid).SelectedItem as DetalleVenta);
        }


        private string txtCliente;

        public string TxtCliente
        {
            get { return txtCliente; }
            set { txtCliente = value; NotifyOfPropertyChange(() => TxtCliente); }
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

        public string TxtRazolSocial
        {
            get { return txtRazonSocial; }
            set { txtRazonSocial = value; NotifyOfPropertyChange(() => TxtRazolSocial); }
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

        private string tipoVenta;

        public string TipoVenta
        {
            get { return tipoVenta; }
            set { tipoVenta = value; NotifyOfPropertyChange(() => TipoVenta); }
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

        public void GuardarVenta()
        {
            int numFilas = LstVenta.Count();
            if (numFilas > 0)
            {
                Venta v = new Venta();
                v.LstDetalle = new List<DetalleVenta>();
                v.LstPagos = new List<VentaPago>();
                //guardar datos de la venta
                //completar
                v.NumDocPago = null;
                v.TipoDocPago = null;
                v.IdUsuario = Convert.ToInt32(Thread.CurrentPrincipal.Identity.Name);
                v.IdCliente = cli.Cliente.Id;

                //guardar detalle de la venta
                foreach (DetalleVenta dv in lstVenta)
                {
                    v.LstDetalle.Add(dv);
                }
                v.Monto = total;
                v.Descuento = desc;
                v.Igv = igv_total;

                v.PtosGanados = Convert.ToInt32(v.Monto / PUNTO);

                v.FechaReg = System.DateTime.Now;
                v.FechaDespacho = fechaDespacho;

                v.Estado = 1;


                foreach (VentaPago vp in lstPagos)
                {
                    if (vp.Nombre.Equals("Efectivo"))
                    {
                        vp.Monto -= Double.Parse(txtVuelto);
                    }
                    v.LstPagos.Add(vp);
                }

                //insertar en la base de datos
                VentaSQL vsql = new VentaSQL();
                int k = vsql.Agregar(v, "obra");
                if (k != 0)
                {
                    MessageBox.Show("Venta Realizada con Exito");
                    Limpiar();
                }
            }
            else
            {
                MessageBox.Show("Debe ingreasar datos de la venta");
                return;
            }
        }

        private Tarjeta cli;

        public Tarjeta Cli
        {
            get { return cli; }
            set { 
                cli = value; NotifyOfPropertyChange(() => Cli); 
                TxtCliente = cli.Cliente.RazonSocial;
                TxtRazolSocial = cli.Cliente.RazonSocial;
                TxtRuc = cli.Cliente.Ruc;
                TxtTelefono = cli.Cliente.Telefono;
                TxtDireccion = cli.Cliente.Direccion;
                //TxtTarjetaCliente = cli.Cliente.IdTarjeta;
                }
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

        Producto prod;

        public Producto Prod
        {
            get { return prod; }
            set { prod = value; NotifyOfPropertyChange(() => Prod); TxtProducto = prod.Nombre; }
        }

        public void BuscarProducto()
        {
            MyWindowManager w = new MyWindowManager();
            w.ShowWindow(new ProductoBuscarViewModel(this,1));
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
            TxtRazolSocial = "";
            TxtRuc = "";
            TxtTelefono = "";
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
