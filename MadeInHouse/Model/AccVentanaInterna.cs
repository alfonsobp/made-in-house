using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class AccVentanaInterna
    {
        int idAccesoVentanaInterna;

        public int IdAccesoVentanaInterna
        {
            get { return idAccesoVentanaInterna; }
            set { idAccesoVentanaInterna = value; }
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
        AccVentana accVentana;

        internal AccVentana AccVentana
        {
            get { return accVentana; }
            set { accVentana = value; }
        }
    }
}
