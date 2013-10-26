using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class SolicitudAdquisicion
    {
        int idSolicitudAD;
        int estado;
        DateTime fechaAtencion;
        DateTime fechaReg;
        Almacen almacen;
        List<ProductoxSolicitudAd> lstProductos;
    }
}
