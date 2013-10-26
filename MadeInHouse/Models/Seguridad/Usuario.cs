using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private DateTime fechaReg;

        public DateTime FechaReg
        {
            get { return fechaReg; }
            set { fechaReg = value; }
        }

        private DateTime fechaMod;

        public DateTime FechaMod
        {
            get { return fechaMod; }
            set { fechaMod = value; }
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

        public Usuario() { 
            
        }

        public Usuario(string codEmpleado, string contrasenha, int estado, int idRol)
        {

            this.codEmpleado = codEmpleado;
            this.contrasenha = contrasenha;
            //this.fechaReg = fechaReg;
            //this.fechaMod = fechaMod;
            this.idRol = idRol;
            this.estado = estado;

        }
    }
}
