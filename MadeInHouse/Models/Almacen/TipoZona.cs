using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class TipoZona
    {
        private int idTipoZona;

        public int IdTipoZona
        {
            get { return idTipoZona; }
            set { idTipoZona = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string color;

        public string Color
        {
            get { return color; }
            set { color = value; }
        }
    
    }   

}
