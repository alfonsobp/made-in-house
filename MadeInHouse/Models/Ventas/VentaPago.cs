using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Ventas
{
    class VentaPago
    {
        public int IdVenta { get; set; }
        public int IdModoPago { get; set; }
        public string Nombre { get; set; }
        public double Monto { get; set; }
    }
}
