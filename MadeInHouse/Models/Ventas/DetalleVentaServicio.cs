using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Compras;

namespace MadeInHouse.Models.Ventas
{
    class DetalleVentaServicio
    {
        public int IdServicio { get; set; }
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public Servicio Servicio { get; set; }
    }
}
