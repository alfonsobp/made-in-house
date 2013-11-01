using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class ProductoxAlmacen
    {
        public int idAlmacen { get; set; }
        public int idProducto { get; set; }
        public string producto { get; set; }
        public int stock { get; set; }
        public int stockMin { get; set; }
        public int sugerido { get; set; }
        public int pedido { get; set; }
    }
}
