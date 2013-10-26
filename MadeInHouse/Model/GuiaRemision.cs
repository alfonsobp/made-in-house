using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class GuiaRemision
    {
        int idGuia;
        string camion;
        string codGuiaRem;
        string conductor;
        string dirLlegada;
        string dirPartida;
        int estado;
        DateTime fechaReg;
        DateTime fechaTraslado;
        string observaciones;
        string tipo;
        List<ProductoxGuiaRemision> lstProductos ;
    }
}
