using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MadeInHouse.Models.Seguridad
{
    class EstadoHabilitado
    {
        private int estado;
        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public EstadoHabilitado()
        {
        }
    }
}