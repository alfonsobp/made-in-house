using MadeInHouse.Models.Almacen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MadeInHouse.Models.Compras
{
    class SolicitudAdquisicion
    {

        int idSolicitudAD;

        public int IdSolicitudAD
        {
            get { return idSolicitudAD; }
            set { idSolicitudAD = value; }
        }
        int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        DateTime fechaAtencion;

        public DateTime FechaAtencion
        {
            get { return fechaAtencion; }
            set { fechaAtencion = value; }
        }
        DateTime fechaReg;

        public DateTime FechaReg
        {
            get { return fechaReg; }
            set { fechaReg = value; }
        }
        Almacen.Almacen almacen;

        public Almacen.Almacen Almacen
        {
            get { return almacen; }
            set { almacen = value; }
        }
        List<ProductoxSolicitudAd> lstProductos;

        internal List<ProductoxSolicitudAd> LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value; }
        }
    }
}
