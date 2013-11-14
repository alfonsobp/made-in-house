using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Seguridad
{
    class Log
    {
        public Log()
        {

        }

        private DateTime fechaAccion;
        public DateTime FechaAccion
        {
            get { return fechaAccion; }
            set { fechaAccion = value; }
        }

        private string codEmpleado;
        public string CodEmpleado
        {
            get { return codEmpleado; }
            set { codEmpleado = value; }
        }

        private string nomEmpleado;
        public string NomEmpleado
        {
            get { return nomEmpleado; }
            set { nomEmpleado = value; }
        }

        private string nomVentana;
        public string NomVentana
        {
            get { return nomVentana; }
            set { nomVentana = value; }
        }

        private string idItem;
        public string IdItem
        {
            get { return idItem; }
            set { idItem = value; }
        }

        private int idAccion;
        public int IdAccion
        {
            get { return idAccion; }
            set { idAccion = value; }
        }

        private string descAccion;
        public string DescAccion
        {
            get { return descAccion; }
            set { descAccion = value; }
        }

        private int idUsuario;
        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }

        private string ip;
        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }


    }
}
