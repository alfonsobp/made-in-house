using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Seguridad
{
    public class AccModulo
    {
        int idAccModulo;

        public int IdAccModulo
        {
            get { return idAccModulo; }
            set { idAccModulo = value; }
        }
        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
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
