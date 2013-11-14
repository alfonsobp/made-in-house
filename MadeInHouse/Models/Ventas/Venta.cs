using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Ventas
{
    class Venta
    {
        int idVenta;
        int codTarjeta;
        double descuento;
        int estado;
        DateTime fechaMod;
        DateTime fechaReg;
        DateTime fechaDespacho;

        public int IdVenta
        {
            get { return idVenta; }
            set { idVenta = value; }
        }

        string tipoVenta;

        public string TipoVenta
        {
            get { return tipoVenta; }
            set { tipoVenta = value; }
        }

        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }


        public string TipoDocPago
        {
            get { return tipoDocPago; }
            set { tipoDocPago = value; }
        }

        public string NumDocPago
        {
            get { return numDocPago; }
            set { numDocPago = value; }
        }


        public double Monto
        {
            get { return monto; }
            set { monto = value; }
        }


        public DateTime FechaDespacho
        {
            get { return fechaDespacho; }
            set { fechaDespacho = value; }
        }

        int idUsuario;
        double igv;
        double monto;
        string numDocPago;
        string numDocPagoServicio;

        public string NumDocPagoServicio
        {
            get { return numDocPagoServicio; }
            set { numDocPagoServicio = value; }
        }

        private int idCliente;

        public int IdCliente
        {
            get { return idCliente; }
            set { idCliente = value; }
        }

        int ptosGanados;
        string tipoDocPago;
        List<DetalleVenta> lstDetalle;
        List<VentaPago> lstPagos;

        internal List<VentaPago> LstPagos
        {
            get { return lstPagos; }
            set { lstPagos = value; }
        }

        public int CodTarjeta
        {
            get { return codTarjeta; }
            set { codTarjeta = value; }
        }


        public double Descuento
        {
            get { return descuento; }
            set { descuento = value; }
        }


        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }


        public DateTime FechaMod
        {
            get { return fechaMod; }
            set { fechaMod = value; }
        }


        public DateTime FechaReg
        {
            get { return fechaReg; }
            set { fechaReg = value; }
        }

        public double Igv
        {
            get { return igv; }
            set { igv = value; }
        }



        public int PtosGanados
        {
            get { return ptosGanados; }
            set { ptosGanados = value; }
        }



        internal List<DetalleVenta> LstDetalle
        {
            get { return lstDetalle; }
            set { lstDetalle = value; }
        }

        List<DetalleVentaServicio> lstDetalleServicio;

        internal List<DetalleVentaServicio> LstDetalleServicio
        {
            get { return lstDetalleServicio; }
            set { lstDetalleServicio = value; }
        }


    }
}
