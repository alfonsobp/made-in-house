using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Cotizacion
    {
        int idCotizacion;
        int estado;
        int fechaFin;
        int fechaInicio;
        Proveedor proveedor;
        string observacion;
        List<CotizacionxProducto> lstProdCotizacion;
    }
}
