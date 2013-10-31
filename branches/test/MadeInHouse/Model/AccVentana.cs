using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class AccVentana
    {
        int idAccVentana;

        public int IdAccVentana
        {
            get { return idAccVentana; }
            set { idAccVentana = value; }
        }

        string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        AccModulo accModulo;

        internal AccModulo AccModulo
        {
            get { return accModulo; }
            set { accModulo = value; }
        }

        public AccVentana()
        {

        }
    }
}
