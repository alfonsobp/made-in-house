using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class NotaIS
    {
        int idNota;
        string codNota;
        DateTime fechaMod;
        DateTime fechaReg;
        Almacen almacen;
        GuiaRemision guia;
        MotivoIS motivo;
        List<ProductoxNotaIS> lstProducto;
    }
}
