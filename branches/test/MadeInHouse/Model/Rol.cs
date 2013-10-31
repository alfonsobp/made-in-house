using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Rol
    {
        int idRol;

        public int IdRol
        {
            get { return idRol; }
            set { idRol = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        private List<AccVentana> lstAccVentana;

        private List<AccVentana> LstAccVentana
        {
            get { return lstAccVentana; }
            set { lstAccVentana = value; }
        }

        public Rol()
        {

        }
    }
}
