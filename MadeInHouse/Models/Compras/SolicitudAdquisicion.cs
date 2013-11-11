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

        string fechaCierre;

        public string FechaCierre
        {
            get { return fechaCierre; }
            set { fechaCierre = value; }
        }

        string nombreTienda;

        public string NombreTienda
        {
            get { return nombreTienda; }
            set { nombreTienda = value; }
        }


        string fechaReg;

        public string FechaReg
        {
            get { return fechaReg; }
            set { fechaReg = value; }
        }

       

        string fechaAtencion;

        public string FechaAtencion
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
