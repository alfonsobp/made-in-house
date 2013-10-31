using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class SubLineaProducto
    {

        private int idSubLinea;

        public int IdSubLinea
        {
            get { return idSubLinea; }
            set { idSubLinea = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private int idLinea;

        public int IdLinea
        {
            get { return idLinea; }
            set { idLinea = value; }
        }

        private string abreviatura;

        public string Abreviatura
        {
            get { return abreviatura; }
            set { abreviatura = value; }
        }

    

    }
}
