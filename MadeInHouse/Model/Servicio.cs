using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Servicio
    {
        int idServicio;
        string codServicio;
        string descripcion;
        int idProveedor;
        string nombre;
        List<ServicioxProducto> lstProductos;
    }
}
