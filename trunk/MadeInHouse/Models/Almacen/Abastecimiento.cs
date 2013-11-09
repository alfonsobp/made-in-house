using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class Abastecimiento
    {
        public int idSolicitudAB { get; set; }
        public string fechaReg { get; set; }
        public string fechaAtencion { get; set; }
        public int estado { get; set; }
        public string txtEstado { get; set; }
        public int idTienda { get; set; }
        public string nomTienda { get; set; }
    }
}
