using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class SubLineaProducto
    {
        int idLinea;

        public int IdLinea
        {
            get { return idLinea; }
            set { idLinea = value; }
        }
        int idSubLinea;

        public int IdSubLinea
        {
            get { return idSubLinea; }
            set { idSubLinea = value; }
        }
        string abreviatura;

        public string Abreviatura
        {
            get { return abreviatura; }
            set { abreviatura = value; }
        }
        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
    }
}
