using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class SolicitudAbastecimiento
    {
        int idSolicitudAB;

        public int IdSolicitudAB
        {
            get { return idSolicitudAB; }
            set { idSolicitudAB = value; }
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
        Almacen almacen;

        public Almacen Almacen
        {
            get { return almacen; }
            set { almacen = value; }
        }
        int idOrden;

        public int IdOrden
        {
            get { return idOrden; }
            set { idOrden = value; }
        }
        List<ProductoxSolicitudAb> lstProductos;

        public List<ProductoxSolicitudAb> LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value; }
        }
    }
}
