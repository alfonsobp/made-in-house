using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Manager
{
     interface EntityManager
    {

          int  Agregar(Object entity);

        object Buscar(params Object[] filters);

          int  Actualizar(Object entity);

          int  Eliminar(Object entity);

    }
}
