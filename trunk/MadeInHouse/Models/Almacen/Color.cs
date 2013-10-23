using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class Color
    {
        private int idColor;

        public int IdColor
        {
            get { return idColor; }
            set { idColor = value; }
        }
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private string codHex;

        public string CodHex
        {
            get { return codHex; }
            set { codHex = value; }
        }

    }
}
