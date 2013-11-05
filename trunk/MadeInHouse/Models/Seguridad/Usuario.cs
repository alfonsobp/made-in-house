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
        private int idUsuario;

        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
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

        private int idRol;

        public int IdRol
        {
            get { return idRol; }
            set { idRol = value; }
        }

        int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public Usuario()
        {

        }

    }
}
