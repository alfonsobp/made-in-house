using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class Ubigeo
    {
        private int idUbigeo;

        public int IdUbigeo
        {
            get { return idUbigeo; }
            set { idUbigeo = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private string codDpto;

        public string CodDpto
        {
            get { return codDpto; }
            set { codDpto = value; }
        }
        private string codProv;

        public string CodProv
        {
            get { return codProv; }
            set { codProv = value; }
        }
        private string codDist;

        public string CodDist
        {
            get { return codDist; }
            set { codDist = value; }
        }


    }
}
