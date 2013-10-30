using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class ServicioxProducto
    {
        int idServicio;

        public int IdServicio
        {
            get { return idServicio; }
            set { idServicio = value; }
        }
        Producto producto;

        public Producto Producto
        {
            get { return producto; }
            set { producto = value; }
        }
        double precio;

        public double Precio
        {
            get { return precio; }
            set { precio = value; }
        }
    }
}
