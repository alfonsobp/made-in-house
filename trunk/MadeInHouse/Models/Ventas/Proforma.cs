using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Ventas
{
    class Proforma
    {
        private string producto;

        public string Producto
        {
            get { return producto; }
            set { producto = value; }
        }

        private int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        private double subtotal;

        public double Subtotal
        {
            get { return subtotal; }
            set { subtotal = value; }
        }


    }
}
