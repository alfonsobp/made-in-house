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

        public DateTime FechaDespacho
        {
            get { return fechaDespacho; }
            set { fechaDespacho = value; }
        }
        string tipoVenta;

        public string TipoVenta
        {
            get { return tipoVenta; }
            set { tipoVenta = value; }
        }
        int idUsuario;
        double igv;
        double monto;
        string numDocPago;
        int ptosGanados;
        string tipoDocPago;
        List<DetalleVenta> lstDetalle;

        public int IdVenta
        {
            get { return idVenta; }
            set { idVenta = value; }
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


        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }


        public double Igv
        {
            get { return igv; }
            set { igv = value; }
        }


        public double Monto
        {
            get { return monto; }
            set { monto = value; }
        }


        public string NumDocPago
        {
            get { return numDocPago; }
            set { numDocPago = value; }
        }


        public int PtosGanados
        {
            get { return ptosGanados; }
            set { ptosGanados = value; }
        }


        public string TipoDocPago
        {
            get { return tipoDocPago; }
            set { tipoDocPago = value; }
        }

        internal List<DetalleVenta> LstDetalle
        {
            get { return lstDetalle; }
            set { lstDetalle = value; }
        }

        private int idCliente;

        public int IdCliente
        {
            get { return idCliente; }
            set { idCliente = value; }
        }


    }
}
