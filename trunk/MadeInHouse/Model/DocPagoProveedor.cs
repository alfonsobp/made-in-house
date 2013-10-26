using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class DocPagoProveedor
    {
        int idDocPago;
        int cantProductos;
        double descuentos;
        DateTime fechaRecepcion;
        DateTime fechaVencimiento;
        OrdenCompra ordenCompra;
        Proveedor proveedor;
        int igv;
        double montoTotal;
        string observaciones;
        double totalBruto;

    }
}
