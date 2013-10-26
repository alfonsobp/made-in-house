using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class OrdenCompra
    {

        int idOrden;
        string codOrdenCompra;
        DateTime fechaReg;
        DateTime fechaSinAtencion;
        Proveedor proveedor;
        double montoBruto;
        string observaciones;
        Almacen almacen;
    }
}
