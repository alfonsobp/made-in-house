using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class ZonaxAlmacen
    {
        int idAlmacen;

        public int IdAlmacen
        {
            get { return idAlmacen; }
            set { idAlmacen = value; }
        }
        int idTipoZona;

        public int IdTipoZona
        {
            get { return idTipoZona; }
            set { idTipoZona = value; }
        }
        int nroBloquesDisp;

        public int NroBloquesDisp
        {
            get { return nroBloquesDisp; }
            set { nroBloquesDisp = value; }
        }
    }
}
