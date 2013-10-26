using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class TipoZona
    {
        int idTipoZona;

        public int IdTipoZona
        {
            get { return idTipoZona; }
            set { idTipoZona = value; }
        }
        string color;

        public string Color
        {
            get { return color; }
            set { color = value; }
        }
        int idColor;

        public int IdColor
        {
            get { return idColor; }
            set { idColor = value; }
        }
        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

    }
}
