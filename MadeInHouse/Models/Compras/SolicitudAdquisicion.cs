using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Compras
{
    class SolicitudAdquisicion
    {
        int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        int idSolicitudAD;

        public int IdSolicitudAD
        {
            get { return idSolicitudAD; }
            set { idSolicitudAD = value; }
        }
        DateTime fechaReg;

        public DateTime FechaReg
        {
            get { return fechaReg; }
            set { fechaReg = value; }
        }
        DateTime fechaAtencion;

        public DateTime FechaAtencion
        {
            get { return fechaAtencion; }
            set { fechaAtencion = value; }
        }
       
        int idAlmacen;

        public int IdAlmacen
        {
            get { return idAlmacen; }
            set { idAlmacen = value; }
        }

        List<ProductoxSolicitudAd> lstProductos;

        public List<ProductoxSolicitudAd> LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value; }
        }

        string est;

        public string Est
        {
            get { return est; }
            set { est = value; }
        }

        string codigo;

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }


    }
}
