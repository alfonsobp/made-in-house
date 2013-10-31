using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class ProductoxGuiaRemision
    {
        int idGuia;

        public int IdGuia
        {
            get { return idGuia; }
            set { idGuia = value; }
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
    }
}
