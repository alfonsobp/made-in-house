using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Reportes
{
    class StockRepor
    {
        private string producto;

        public string Producto
        {
            get { return producto; }
            set { producto = value; }
        }

        private string almacen;

        public string Almacen
        {
            get { return almacen; }
            set { almacen = value; }
        }

        private string tienda;

        public string Tienda
        {
            get { return tienda; }
            set { tienda = value; }
        }

        private int stock;

        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }
    }
}
