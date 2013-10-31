using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class AlmacenxProducto
    {
        int idAlmacen;

        public int IdAlmacen
        {
            get { return idAlmacen; }
            set { idAlmacen = value; }
        }
        Producto produto;

       public  Producto Produto
        {
            get { return produto; }
            set { produto = value; }
        }
        int stockActual;

        public int StockActual
        {
            get { return stockActual; }
            set { stockActual = value; }
        }
        int stockMinimo;

        public int StockMinimo
        {
            get { return stockMinimo; }
            set { stockMinimo = value; }
        }
    }
}
