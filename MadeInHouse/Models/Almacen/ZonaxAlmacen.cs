using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class ZonaxAlmacen
    {
        private TipoZona idTipoZona;

        internal TipoZona IdTipoZona
        {
            get { return idTipoZona; }
            set { idTipoZona = value; }
        }

        private Almacen idAlmacen;

        internal Almacen IdAlmacen
        {
            get { return idAlmacen; }
            set { idAlmacen = value; }
        }

        private int nroBloquesDisp;

        public int NroBloquesDisp
        {
            get { return nroBloquesDisp; }
            set { nroBloquesDisp = value; }
        }

        private int nroBloquesTotal;

        public int NroBloquesTotal
        {
            get { return nroBloquesTotal; }
            set { nroBloquesTotal = value; }
        }

    }
}