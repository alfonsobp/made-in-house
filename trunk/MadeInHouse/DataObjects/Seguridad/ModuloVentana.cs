using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.DataObjects.Seguridad
{
    class ModuloVentana
    {
        private int idAccModulo;

        public int IdAccModulo
        {
            get { return idAccModulo; }
            set { idAccModulo = value; }
        }
        private int idAccVentana;

        public int IdAccVentana
        {
            get { return idAccVentana; }
            set { idAccVentana = value; }
        }

        public ModuloVentana()
        {

        }
    }
}
