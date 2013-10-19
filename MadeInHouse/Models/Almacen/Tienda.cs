using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class Tienda
    {

        private int idTienda;

        public int IdTienda
        {
            get { return idTienda; }
            set { idTienda = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string direccion;

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        private string ubigeo;

        public string Ubigeo
        {
            get { return ubigeo; }
            set { ubigeo = value; }
        }

        private DateTime fechaReg;

        public DateTime FechaReg
        {
            get { return fechaReg; }
            set { fechaReg = value; }
        }

        private int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        




    }
}
