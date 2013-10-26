using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Devolucion
    {
        int idDevolucion;
        Venta idVenta;
        int estado;
        DateTime fechaMod;
        DateTime fechaReg;
        double monto;
        string numDocCredito;
        List<DetalleDevolucion> lstDetalle;

    }
}
