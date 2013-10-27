using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class UnidadMedida
    {
        int idUnidad;

        public int IdUnidad
        {
            get { return idUnidad; }
            set { idUnidad = value; }
        }
        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
    }
}
