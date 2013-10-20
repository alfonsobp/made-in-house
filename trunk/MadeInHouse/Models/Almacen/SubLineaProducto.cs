using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class SubLineaProducto
    {

        private int idSubLinea;

        public int IdSubLinea
        {
            get { return idSubLinea; }
            set { idSubLinea = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private LineaProducto idLinea;

        public LineaProducto IdLinea
        {
            get { return idLinea; }
            set { idLinea = value; }
        }
    

    }
}
