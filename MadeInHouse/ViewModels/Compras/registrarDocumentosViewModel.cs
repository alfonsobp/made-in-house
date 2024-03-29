﻿using System;
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
        OrdenCompraxProductoSQL eM = new OrdenCompraxProductoSQL();
        List<ProductoxOrdenCompra> list = new List<ProductoxOrdenCompra>();

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

        public void Limpiar()
        {
            TxtCodigo = null;
            Ord = null;
            TxtDescuento = 0;
            TxtIGV = 0;
            TxtTotalBruto = 0;
            TxtSaldoPagado = 0;
            TxtTotalFinal = 0;
            TxtObservaciones = null;
        }

        public void Refrescar()
        {
            if (ord != null)
                list = eM.Buscar(Ord.IdOrden) as List<ProductoxOrdenCompra>;

            int maxId = u.ObtenerMaximoID("DocPagoProveedor", "idDocPago");
            TxtCodigo = "DP-" + (100000 + maxId).ToString();

            //Productos cotizados por el proveedor
            Cotizacion c = obtenerCotizacion(Ord.Proveedor.IdProveedor);
            List<CotizacionxProducto> lstProdCot = null;
            
            if (c != null)
                lstProdCot = new CotizacionxProductoSQL().Buscar(c.IdCotizacion) as List<CotizacionxProducto>;

            cant = 0; monto = 0; importe = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (lstProdCot != null)
                {
                    for (int z = 0; z < lstProdCot.Count; z++)
                    {
                        if (list[i].Producto.IdProducto == lstProdCot[z].Producto.IdProducto)
                        {
                            int catCot = (int)((Convert.ToInt32(list[i].Cantidad))/lstProdCot[z].Cantidad);
                            int resto = (Convert.ToInt32(list[i].Cantidad)) - (catCot*lstProdCot[z].Cantidad);

                            if (lstProdCot[z].Precio != 0)
                                list[i].Importe = catCot * lstProdCot[z].Precio + resto * list[i].PrecioUnitario;

                        }
                        
                    }

                    if (list[i].Importe == 0)
                        list[i].Importe = list[i].Monto;
                }

                cant += Convert.ToInt32(list[i].Cantidad);
                monto += list[i].Monto;
                importe += list[i].Importe;
            }

            TxtIGV = (0.18) * monto;
            TxtTotalBruto = monto;
            TxtDescuento = monto - importe;
            TxtTotalFinal = importe;

            LstProducto = new List<ProductoxOrdenCompra>(list);
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

            if (d.Observaciones != null)
                d.Observaciones = TxtObservaciones;
            else
                d.Observaciones = "NN";
            
            d.TotalBruto = monto;
            d.CantProductos = cant;
            d.Descuentos = monto - importe;
            d.Igv = (0.18) * (importe);
            d.MontoTotal = importe;

            Boolean noPagado = true;
            List<DocPagoProveedor> listDocs = new DocPagoProveedorSQL().Buscar() as List<DocPagoProveedor>;
            
            for (int i = 0; i < listDocs.Count; i++)
                if ((listDocs[i].Proveedor.IdProveedor == d.Proveedor.IdProveedor) && (listDocs[i].OrdenCompra.IdOrden == d.OrdenCompra.IdOrden))
                    noPagado = false;


            if ((Ord != null) && (LstProducto != null) && (noPagado))
            {
                if (indicador == 1)
                {
                    k = new DocPagoProveedorSQL().Agregar(d);

                    if (k == 0)
                        MessageBox.Show("Ocurrio un error");
                    else
                        MessageBox.Show("Documento Registrado \n\nCodigo = " + txtCodigo + "\nOC-correspondiente = OC-" +
                                        (1000000 + Ord.IdOrden).ToString());
                }

                if (indicador == 2)
                {
                    MessageBox.Show("Los documentos de pago no son editables");
                }

            }
            else
            {
                MessageBox.Show("Orden de compra no válida a pagar \nRevisar si actualmente está en proceso de pago");
            }

        }

        public Cotizacion obtenerCotizacion(int idProv)
        {
            List<Cotizacion> lstCot = new CotizacionSQL().Buscar() as List<Cotizacion>;

            if (lstCot == null)
                return null;
            else
            {
                for (int i = 0; i < lstCot.Count; i++)
                {
                    if (lstCot[i].Proveedor.IdProveedor == idProv)
                        return lstCot[i];
                }

            }

            return null;
        }
    }
}
