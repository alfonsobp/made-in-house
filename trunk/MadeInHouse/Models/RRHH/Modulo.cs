using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.RRHH
{
    class Modulo
    {
        int idModulo;

        public int IdModulo
        {
            get { return idModulo; }
            set { idModulo = value; }
        }

        string nombreModulo;

        public string NombreModulo
        {
            get { return nombreModulo; }
            set { nombreModulo = value; }
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
        
        public Modulo()
        {

        }

        public Modulo(int idModulo, string nombreModulo, string descripcion)
        {
            this.idModulo = idModulo;
            this.nombreModulo = nombreModulo;
            this.descripcion = descripcion;
        }
    }
}
