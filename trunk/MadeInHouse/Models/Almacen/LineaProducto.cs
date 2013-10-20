using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class LineaProducto
    {
        int idLinea;

        public int IdLinea
        {
            get { return idLinea; }
            set { idLinea = value; }
        }
        string codigoLinea;

        public string CodigoLinea
        {
            get { return codigoLinea; }
            set { codigoLinea = value; }
        }
        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

    }
}
