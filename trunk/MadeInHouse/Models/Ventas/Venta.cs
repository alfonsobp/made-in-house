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

        string estadoS="REALIZADA";

        public string EstadoS
        {
            get { return estadoS; }
            set { estadoS = value; }
        }

        string fechaRegS;

        public string FechaRegS
        {
            get { return fechaRegS; }
            set { fechaRegS = value; }
        }

        DateTime fechaReg;

        public DateTime FechaReg
        {
            get { return fechaReg; }
            set { fechaReg = value; }
        }

        DateTime fechaDespacho;
        public string dni { get; set; }

        

        string nombreCliente;

        public string NombreCliente
        {
            get { return nombreCliente; }
            set { nombreCliente = value; }
        }

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



        public List<DetalleVenta> LstDetalle
        {
            get { return lstDetalle; }
            set { lstDetalle = value; }
        }

        List<DetalleVentaServicio> lstDetalleServicio;

        public List<DetalleVentaServicio> LstDetalleServicio
        {
            get { return lstDetalleServicio; }
            set { lstDetalleServicio = value; }
        }

        string ruc;

        public string Ruc
        {
            get { return ruc; }
            set { ruc = value; }
        }

        string razonSocial;

        public string RazonSocial
        {
            get { return razonSocial; }
            set { razonSocial = value; }
        }

        string direccion;

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        string telefono;

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }
    }
}
