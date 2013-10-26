using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class ProductoxOrdenCompra
    {
        Producto producto;

        internal Producto Producto
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
        double monto;

        public double Monto
        {
            get { return monto; }
            set { monto = value; }
        }
    }
}
