using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class Almacen
    {
        private int idAlmacen;

        public int IdAlmacen
        {
            get { return idAlmacen; }
            set { idAlmacen = value; }
        }

        private string tipo;

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private string codAlmacen;

        public string CodAlmacen
        {
            get { return codAlmacen; }
            set { codAlmacen = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private int nroFilas;

        public int NroFilas
        {
            get { return nroFilas; }
            set { nroFilas = value; }
        }

        private int nroColumnas;

        public int NroColumnas
        {
            get { return nroColumnas; }
            set { nroColumnas = value; }
        }

        private int altura;

        public int Altura
        {
            get { return altura; }
            set { altura = value; }
        }

        private int idTienda;

        public int IdTienda
        {
            get { return idTienda; }
            set { idTienda = value; }
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
