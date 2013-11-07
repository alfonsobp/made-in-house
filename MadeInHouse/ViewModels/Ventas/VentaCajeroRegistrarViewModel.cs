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

        public VentaCajeroRegistrarViewModel()
        {
           lstVenta = new List<DetalleVenta>();
           LstPagos = new List<VentaPago>();
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
            Cliente c = new Cliente();
            if (keyArgs.Key == Key.Enter)
            {
                //buscar al cliente por la tarjeta
                ClienteSQL csql = new ClienteSQL();
                c = csql.BuscarClienteByTarjeta(TxtCliente);
                TxtRazonSocial = c.RazonSocial;
                TxtRuc = c.Ruc;
                TxtDNI = c.Dni;
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
            { "Seleccionar", -1 }, { "Boleta", 0 }, { "Factura", 1 }
        };

        public BindableCollection<string> cmbTipoVenta
        {
            get
            {
                return new BindableCollection<string>(tipoVenta.Keys);
            }
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

        public void GuardarVenta()
        {
            Venta v = new Venta();
            v.LstDetalle = new List<DetalleVenta>();
            v.LstPagos = new List<VentaPago>();
            //guardar datos de la venta
            //completar
            v.NumDocPago = null;
            v.TipoDocPago = null;
            v.Estado = 1;
            v.FechaReg = System.DateTime.Now;
            //idCliente desde la tarjeta de este si es que hay
            
            v.IdCliente = Convert.ToInt32(TxtCliente);

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

            //insertar en la base de datos
            VentaSQL vsql = new VentaSQL();
            int k = vsql.Agregar(v,"tienda");
            if (k != 0)
            {
                MessageBox.Show("Venta Realizada con Exito");
                Limpiar();
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
