using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class ProductoxNotaIS
    {
        int idProducto;

        public int IdProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }

        int idNota;

        public int IdNota
        {
            get { return idNota; }
            set { idNota = value; }
        }

        int idAlmacen;

        public int IdAlmacen
        {
            get { return idAlmacen; }
            set { idAlmacen = value; }
        }

        int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        int idUbicacion;

        public int IdUbicacion
        {
            get { return idUbicacion; }
            set { idUbicacion = value; }
        }
    }
}
