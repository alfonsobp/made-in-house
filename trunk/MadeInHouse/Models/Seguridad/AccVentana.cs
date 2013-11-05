using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Seguridad
{
    public class AccVentana
    {
        int idAccVentana;

        public int IdAccVentana
        {
            get { return idAccVentana; }
            set { idAccVentana = value; }
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

        AccModulo accModulo;

        public AccModulo AccModulo
        {
            get { return accModulo; }
            set { accModulo = value; }
        }

        public AccVentana()
        {

        }
    }
}
