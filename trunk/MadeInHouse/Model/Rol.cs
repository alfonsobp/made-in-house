using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Rol
    {
        int idRol;
        string descripcion;
        int estado;
        string nombre;
        List<AccModulo> lstAccesos;
    }
}
