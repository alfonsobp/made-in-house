using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Producto
    {
        int idProducto;
        string abreviatura;
        string codProducto;
        string descripcion;
        int estado;
        LineaProducto lineaproducto;
        SubLineaProducto sublinea;
        UnidadMedida unidad;
        string unidadMedida;
        string nombre;
        string observaciones;
        int percepcion;
        double precio;
        string tipoUso;
    }
}
