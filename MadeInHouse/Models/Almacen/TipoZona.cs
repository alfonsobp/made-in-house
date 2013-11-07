using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class TipoZona
    {
        private int idTipoZona;

        public int IdTipoZona
        {
            get { return idTipoZona; }
            set { idTipoZona = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string color;

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        private int idColor;

        public int IdColor
        {
            get { return idColor; }
            set { idColor = value; }
        }

        private int nroBloquesDisp;

        public int NroBloquesDisp
        {
            get { return nroBloquesDisp; }
            set { nroBloquesDisp = value; }
        }

        private int nroBloquesTotal;

        public int NroBloquesTotal
        {
            get { return nroBloquesTotal; }
            set { nroBloquesTotal = value; }
        }


        private List<Ubicacion> lstUbicaciones;

        public List<Ubicacion> LstUbicaciones
        {
            get { return lstUbicaciones; }
            set { lstUbicaciones = value; }
        }
    
    }   

}
