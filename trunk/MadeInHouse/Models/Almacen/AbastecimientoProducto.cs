using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class AbastecimientoProducto
    {
        public int idProducto { get; set; }
        public string nombre { get; set; }
        public int stock { get; set; }
        public int stockPendiente { get; set; }
        public int sugerido { get; set; }
        public int pedido { get; set; }
        public int atendido { get; set; }
        public int atendidoReal { get; set; }
    }
}
