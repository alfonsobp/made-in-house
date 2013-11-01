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

        private string telefono;

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        private string administrador;

        public string Administrador
        {
            get { return administrador; }
            set { administrador = value; }
        }

        private int  idUbigeo;

        public int  IdUbigeo
        {
            get { return idUbigeo; }
            set { idUbigeo = value; }
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

        private Almacenes anaqueles;

        public Almacenes Anaqueles
        {
            get { return anaqueles; }
            set { anaqueles = value; }
        }
        private Almacenes deposito;

        public Almacenes Deposito
        {
            get { return deposito; }
            set { deposito = value; }
        }

        




    }
}
