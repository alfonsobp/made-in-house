using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Ventas
{
    class Devolucion
    {
        public int IdDevolucion { get; set; }
        public string NumDocCredito { get; set; }
        public double Monto { get; set; }
        public string fechaReg { get; set; }
        public string fechaMod { get; set; }
        public int estado { get; set; }
        public string nomEstado { get; set; }
        public int idUsuario { get; set; }
        public string archivo { get; set; }
        public Venta venta { get; set; }
    }
}
