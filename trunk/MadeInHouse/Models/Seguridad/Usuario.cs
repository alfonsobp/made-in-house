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

        private string codUsuario;

        public string CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        private string contrasenha;

        public string Contrasenha
        {
            get { return contrasenha; }
            set { contrasenha = value; }
        }
        //private DateTime fechaReg { get; set; }
        //private DateTime fechaMod { get; set; }

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

        public Usuario(string codUsuario, string contrasenha, int estado, int idRol) {

            this.codUsuario = codUsuario;
            this.contrasenha = contrasenha;
            //this.fechaReg = fechaReg;
            //this.fechaMod = fechaMod;
            this.idRol = idRol;
            this.estado = estado;

        }
    }
}
