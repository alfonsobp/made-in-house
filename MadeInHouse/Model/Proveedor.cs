using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Proveedor
    {
        int idProveedor;
        string codProveedor;
        string contacto;
        string direccion;
        string email;
        int estado;
        string fax;
        string observaciones;
        string razonSocial;
        string ruc;
        string telefono;
        string telefonoContacto;
        List<ProveedorxProducto> lstProducto;
    }
}
