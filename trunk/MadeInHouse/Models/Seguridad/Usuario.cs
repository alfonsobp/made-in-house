using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MadeInHouse.Models.Seguridad;


namespace MadeInHouse.Models.Seguridad
{
    public class Usuario
    {
        public Usuario()
        {

        }
        
        private int idUsuario;
        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }

        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }


        private string codEmpleado;
        public string CodEmpleado
        {
            get { return codEmpleado; }
            set { codEmpleado = value; }
        }

        private string contrasenha;
        public string Contrasenha
        {
            get { return contrasenha; }
            set { contrasenha = value; }
        }

        private int numIntentos;
        public int NumIntentos
        {
            get { return numIntentos; }
            set { numIntentos = value; }
        }

        DateTime fechaMod;
        public DateTime FechaMod
        {
            get { return fechaMod; }
            set { fechaMod = value; }
        }

        DateTime fechaReg;
        public DateTime FechaReg
        {
            get { return fechaReg; }
            set { fechaReg = value; }
        }

        private Rol rol;
        public Rol Rol
        {
            get { return rol; }
            set { rol = value; }
        }

        //private int idRol;

        //public int IdRol
        //{
        //    get { return idRol; }
        //    set { idRol = value; }
        //}

        int estado;
        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        int idTienda;
        public int IdTienda
        {
            get { return idTienda; }
            set { idTienda = value; }
        }

        string nomTienda;
            public string NomTienda
        {
            get { return nomTienda; }
            set { nomTienda = value; }
        }

        int estadoHabilitado;
        public int EstadoHabilitado
        {
            get { return estadoHabilitado; }
            set { estadoHabilitado = value; }
        }
    }
}