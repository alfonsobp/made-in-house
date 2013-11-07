using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Data;
using MadeInHouse.Views.Compras;
using System.Windows;
using System.Data.OleDb;
using System.Collections.ObjectModel;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.DataObjects;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Compras;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Collections;
using MadeInHouse.Models;


namespace MadeInHouse.ViewModels.Compras
{
    class registrarDocumentosViewModel:PropertyChangedBase
    {

        //Constructores
        public registrarDocumentosViewModel()
        {
            indicador = 1;
        }

        public registrarDocumentosViewModel(BuscarDocumentoViewModel m)
        {
            indicador = 1;
            model = m;
        }

        public registrarDocumentosViewModel(DocPagoProveedor d, BuscarDocumentoViewModel m)
        {
            Ord = d.OrdenCompra;
            TxtCodigo = "OC-" + (1000000 + d.IdDocPago).ToString();
            TxtFechaRec = d.FechaRecepcion;
            TxtFechaVen = d.FechaVencimiento;
            TxtDescuento = d.Descuentos;
            TxtIGV = d.Igv;
            TxtObservaciones = d.Observaciones;
            TxtSaldoPagado = d.SaldoPagado;
            TxtTotalBruto = d.TotalBruto;
            TxtTotalFinal = d.MontoTotal;
            indicador = 2;
            model = m;
        }


        //Atributos
        BuscarDocumentoViewModel model;
        int indicador; //1=insertar, 2=detalle
        UtilesSQL u = new UtilesSQL();
        int cant; double monto, importe;

        private OrdenCompra ord;
        public OrdenCompra Ord
        {
            get { return ord; }
            set { ord = value; NotifyOfPropertyChange(() => Ord); }
        }

        private string txtCodigo;
        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }

        private DateTime txtFechaVen = new DateTime(DateTime.Now.Year, 12, 31);
        public DateTime TxtFechaVen
        {
            get { return txtFechaVen; }
            set { txtFechaVen = value; NotifyOfPropertyChange(() => TxtFechaVen); }
        }

        private DateTime txtFechaRec = new DateTime(DateTime.Now.Year, 1, 1);
        public DateTime TxtFechaRec
        {
            get { return txtFechaRec; }
            set { txtFechaRec = value; NotifyOfPropertyChange(() => TxtFechaRec); }
        }

        private double txtTotalBruto;
        public double TxtTotalBruto
        {
            get { return txtTotalBruto; }
            set { txtTotalBruto = value; NotifyOfPropertyChange(() => TxtTotalBruto); }
        }

        private double txtTotalFinal;
        public double TxtTotalFinal
        {
            get { return txtTotalFinal; }
            set { txtTotalFinal = value; NotifyOfPropertyChange(() => TxtTotalFinal); }
        }

        private double txtIGV;
        public double TxtIGV
        {
            get { return txtIGV; }
            set { txtIGV = value; NotifyOfPropertyChange(() => TxtIGV); }
        }

        private double txtInteres;
        public double TxtInteres
        {
            get { return txtInteres; }
            set { txtInteres = value; NotifyOfPropertyChange(() => TxtInteres); }
        }

        private double txtDescuento;
        public double TxtDescuento
        {
            get { return txtDescuento; }
            set { txtDescuento = value; NotifyOfPropertyChange(() => TxtDescuento); }
        }

        private double txtSaldoPagado;
        public double TxtSaldoPagado
        {
            get { return txtSaldoPagado; }
            set { txtSaldoPagado = value; NotifyOfPropertyChange(() => TxtSaldoPagado); }
        }

        private string txtObservaciones;
        public string TxtObservaciones
        {
            get { return txtObservaciones; }
            set { txtObservaciones = value; NotifyOfPropertyChange(() => TxtObservaciones); }
        }


        private List<ProductoxOrdenCompra> lstProducto;
        public List<ProductoxOrdenCompra> LstProducto
        {
            get { return lstProducto; }
            set { lstProducto = value; NotifyOfPropertyChange(() => LstProducto); }
        }

        //Funciones
        public void BuscarOrden()
        {
            MyWindowManager w = new MyWindowManager();
            w.ShowWindow(new BuscarOrdenCompraViewModel(this));
        }

        OrdenCompraxProductoSQL eM = new OrdenCompraxProductoSQL();
        public void Refrescar()
        {
            if (ord != null)
                LstProducto = eM.Buscar(Ord.IdOrden) as List<ProductoxOrdenCompra>;

            int maxId = u.ObtenerMaximoID("DocPagoProveedor", "idDocPago");
            TxtCodigo = "DP-" + (100000 + maxId).ToString();

            cant = 0; monto = 0; importe = 0;
            for (int i = 0; i < LstProducto.Count; i++)
            {
                cant += Convert.ToInt32(LstProducto[i].Cantidad);
                monto += LstProducto[i].Monto;
                importe += LstProducto[i].Importe;
            }

            TxtIGV = (0.18) * monto;
            TxtTotalBruto = monto;
            TxtDescuento = importe;
            TxtTotalFinal = monto - TxtIGV - TxtDescuento - TxtInteres;
        }

        public void GuardarDocPago()
        {
            int k;
            DocPagoProveedor d = new DocPagoProveedor();
            d.OrdenCompra = new OrdenCompra();
            d.Proveedor = new Proveedor();

            d.FechaRecepcion = TxtFechaRec;
            d.OrdenCompra = Ord;
            d.Proveedor = Ord.Proveedor;
            d.FechaVencimiento = TxtFechaVen;
            d.SaldoPagado = 0;

            d.Observaciones = TxtObservaciones;
            d.TotalBruto = monto;
            d.CantProductos = cant;
            d.Descuentos = importe;
            d.Igv = (0.18) * (d.TotalBruto);
            d.MontoTotal = d.TotalBruto - d.Descuentos - d.Igv;

            if ((Ord != null) && (LstProducto != null))
            {
                if (indicador == 1)
                {
                    k = new DocPagoProveedorSQL().Agregar(d);

                    if (k == 0)
                        MessageBox.Show("Ocurrio un error");
                    else
                        MessageBox.Show("Documento Registrado \n\nCodigo = " + txtCodigo + "\nOC-correspondiente = OC-" + 
                                        (1000000+Ord.IdOrden).ToString());
                }

                if (indicador == 2)
                {

                }

            }

        }
    }
}
