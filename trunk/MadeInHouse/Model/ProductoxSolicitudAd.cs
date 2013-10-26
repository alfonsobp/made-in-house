using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class ProductoxSolicitudAd
    {
        int idSolicitudAD;

        public int IdSolicitudAD
        {
            get { return idSolicitudAD; }
            set { idSolicitudAD = value; }
        }
        Producto producto;

        public Producto Producto
        {
            get { return producto; }
            set { producto = value; }
        }
        int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        int cantidadAtendida;

        public int CantidadAtendida
        {
            get { return cantidadAtendida; }
            set { cantidadAtendida = value; }
        }
    }
}
