using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class Sector
    {
        private int idSector;

        public int IdSector
        {
            get { return idSector; }
            set { idSector = value; }
        }

        private int idAlmacen;

        public int IdAlmacen
        {
            get { return idAlmacen; }
            set { idAlmacen = value; }
        }

        private int idTipoZona;

        public int IdTipoZona
        {
            get { return idTipoZona; }
            set { idTipoZona = value; }
        }

        private int idProducto;

        public int IdProducto
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

        private int nroColor;

        public int NroColor
        {
            get { return nroColor; }
            set { nroColor = value; }
        }

        private int volOcupado;

        public int VolOcupado
        {
            get { return volOcupado; }
            set { volOcupado = value; }
        }




    }
}
