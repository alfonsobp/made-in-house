using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class Ubicacion
    {
        private string idUbicacion;

        public string IdUbicacion
        {
            get { return idUbicacion; }
            set { idUbicacion = value; }
        }

        private TipoZona idTipoZona;

        public TipoZona IdTipoZona
        {
            get { return idTipoZona; }
            set { idTipoZona = value; }
        }

        private Almacen idAlmacen;

        public Almacen IdAlmacen
        {
            get { return idAlmacen; }
            set { idAlmacen = value; }
        }

        private int cordX;

        public int CordX
        {
            get { return cordX; }
            set { cordX = value; }
        }

        private int cordY;

        public int CordY
        {
            get { return cordY; }
            set { cordY = value; }
        }

        private int cordZ;

        public int CordZ
        {
            get { return cordZ; }
            set { cordZ = value; }
        }

        private Producto idProducto;

        public Producto IdProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }

        private int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        private int volOcupado;

        public int VolOcupado
        {
            get { return volOcupado; }
            set { volOcupado = value; }
        }

    }
}
