using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class ProductoCant : Producto 
    {
        private string proNombre;

        public string ProNombre
        {
            get { return proNombre; }
            set { proNombre = value; }
        }

        private string codPro;

        public string CodPro
        {
            get { return codPro; }
            set { codPro = value; }
        }

        private string can;

        public string Can
        {
            get { return can; }
            set { can = value; }
        }
        private string canAtend;
        public string CanAtend
        {
            get { return canAtend; }
            set { canAtend = value; }
        }
        private string canAtender;
        public string CanAtender
        {
            get { return canAtender; }
            set { canAtender = value;}
        }
        private List<Ubicacion> ubicaciones;

        internal List<Ubicacion> Ubicaciones
        {
            
            get { return ubicaciones; }
            set { ubicaciones = value; }
        }

    }
}
