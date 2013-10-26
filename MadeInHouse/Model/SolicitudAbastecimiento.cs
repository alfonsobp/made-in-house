using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class SolicitudAbastecimiento
    {
        int idSolicitudAB;
        int estado;
        DateTime fechaAtencion;
        DateTime fechaReg;
        Almacen almacen;
        int idOrden;
        List<ProductoxSolicitudAb> lstProductos;
    }
}
