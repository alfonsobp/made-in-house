using MadeInHouse.Models.Almacen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Compras
{
    class CotizacionxProducto
    {

        int idCotizacion;

        public int IdCotizacion
        {
            get { return idCotizacion; }
            set { idCotizacion = value; }
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
        double precio;

        public double Precio
        {
            get { return precio; }
            set { precio = value; }
        }
    }
}
