using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Ventas
{
    class DevolucionProducto
    {
        public string DocPago { get; set; }
        public int IdProducto { get; set; }
        public string CodProducto { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public int Devuelve { get; set; }
        public int Devuelto { get; set; }
        public double Precio { get; set; }
        public double Descuento { get; set; }
        public string Observaciones { get; set; }
    }
}
