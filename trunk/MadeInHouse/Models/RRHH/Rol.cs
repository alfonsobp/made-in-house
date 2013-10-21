using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.RRHH
{
    public class Rol
    {
        int idRol;

        public int IdRol
        {
            get { return idRol; }
            set { idRol = value; }
        }

        private string nombRol;

        public string NombRol
        {
            get { return nombRol; }
            set { nombRol = value; }
        }
        
        string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        string modulo;

        int estado;

        public string Modulo
        {
            get { return modulo; }
            set { modulo = value; }
        }

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }        

        public Rol()
        {

        }

        public Rol(int idRol, string nombRol, string descripcion, string modulo, int estado)
        {
            this.idRol = idRol;
            this.nombRol = nombRol;
            this.descripcion = descripcion;
            this.modulo = modulo;
            this.estado = estado;
        }
    }
}
