using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class DocPagoProveedor
    {
        int idDocPago;

        public int IdDocPago
        {
            get { return idDocPago; }
            set { idDocPago = value; }
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
        DateTime fechaVencimiento;

        public DateTime FechaVencimiento
        {
            get { return fechaVencimiento; }
            set { fechaVencimiento = value; }
        }
        OrdenCompra ordenCompra;

        internal OrdenCompra OrdenCompra
        {
            get { return ordenCompra; }
            set { ordenCompra = value; }
        }
        Proveedor proveedor;

        internal Proveedor Proveedor
        {
            get { return proveedor; }
            set { proveedor = value; }
        }
        int igv;

        public int Igv
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

    }
}
