using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class AccModulo
    {
        int idAccModulo;

        public int IdAccModulo
        {
            get { return idAccModulo; }
            set { idAccModulo = value; }
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

        public AccModulo()
        {

        }
    }
}
