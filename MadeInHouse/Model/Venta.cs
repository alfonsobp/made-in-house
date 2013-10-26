using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Venta
    {
        int idVenta;
        int codTarjeta;
        int descuento;
        int estado;
        DateTime fechaMod;
        DateTime fechaReg;
        int idUsuario;
        int igv;
        double monto;
        string numDocPago;
        int ptosGanados;
        string tipoDocPago;
        List<DetalleVenta> lstDetalle;

    }
}
