using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Compras
{
    class DocPagoProveedor
    {

        int idDocPago;

        public int IdDocPago
        {
            get { return idDocPago; }
            set { idDocPago = value; }
        }

        string codDoc;

        public string CodDoc
        {
            get { return codDoc; }
            set { codDoc = value; }
        }

        int cantProductos;

        public int CantProductos
        {
            get { return cantProductos; }
            set { cantProductos = value; }
        }
        double descuentos;

        public double Descuentos
        {
            get { return descuentos; }
            set { descuentos = value; }
        }
        DateTime fechaRecepcion;

        public DateTime FechaRecepcion
        {
            get { return fechaRecepcion; }
            set { fechaRecepcion = value; }
        }

        private DateTime fechaPago;

        public DateTime FechaPago
        {
            get { return fechaPago; }
            set { fechaPago = value; }
        }


        DateTime fechaVencimiento;

        public DateTime FechaVencimiento
        {
            get { return fechaVencimiento; }
            set { fechaVencimiento = value; }
        }
        OrdenCompra ordenCompra;

        public OrdenCompra OrdenCompra
        {
            get { return ordenCompra; }
            set { ordenCompra = value; }
        }
        Proveedor proveedor;

        public Proveedor Proveedor
        {
            get { return proveedor; }
            set { proveedor = value; }
        }
        double igv;

        public double Igv
        {
            get { return igv; }
            set { igv = value; }
        }
        double montoTotal;

        public double MontoTotal
        {
            get { return montoTotal; }
            set { montoTotal = value; }
        }
        string observaciones;

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }
        double totalBruto;

        public double TotalBruto
        {
            get { return totalBruto; }
            set { totalBruto = value; }
        }
        double saldoPagado;

        public double SaldoPagado
        {
            get { return saldoPagado; }
            set { saldoPagado = value; }
        }

        private int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }
    }
}
