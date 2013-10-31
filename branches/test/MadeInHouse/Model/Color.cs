using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Color
    {
        int idColor;

        public int IdColor
        {
            get { return idColor; }
            set { idColor = value; }
        }
        string codHex;

        public string CodHex
        {
            get { return codHex; }
            set { codHex = value; }
        }
        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
    }
}
