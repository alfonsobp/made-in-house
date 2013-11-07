using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class ProductoxAlmacen
    {
        private int idAlmacen;

        public int IdAlmacen
        {
            get { return idAlmacen; }
            set { idAlmacen = value; }
        }
        private int idProducto;

        public int IdProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }

        private string codProducto;

        public string CodProducto
        {
            get { return codProducto; }
            set { codProducto = value; }
        }


        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        
        private int stockActual;

        public int StockActual
        {
            get { return stockActual; }
            set { stockActual = value; }
        }
        private int stockMin;

        public int StockMin
        {
            get { return stockMin; }
            set { stockMin = value; }
        }
        private int stockMax;

        public int StockMax
        {
            get { return stockMax; }
            set { stockMax = value; }
        }

        private float precioVenta;

        public float PrecioVenta
        {
            get { return precioVenta; }
            set { precioVenta = value; }
        }
        private int vigente;

        public int Vigente
        {
            get { return vigente; }
            set { vigente = value; }
        }

        private int idTienda;

        public int IdTienda
        {
            get { return idTienda; }
            set { idTienda = value; }
        }




        
    }
}
