using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Almacen
    {
        int idAlmacen;
        int altura;
        string codAlmacen;
        int estado;
        DateTime fechaReg;
        Tienda tienda;
        string nombre;
        int nroColumnas;
        int nroFilas;
        string tipo;
        List<AlmacenxProducto> lstProdAlm;
        List<Tienda> lstTienda;
        List<ZonaxAlmacen> lstZonas;
    }
}
