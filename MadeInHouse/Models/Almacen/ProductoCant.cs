using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class ProductoCant : Producto 
    {

        public ProductoCant()
        {
            ubicaciones = new List<Ubicacion>();
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

        public List<Ubicacion> Ubicaciones
        {
            
            get { return ubicaciones; }
            set { ubicaciones = value; }
        }

        private bool atendido = true;

        public bool Atendido
        {
            get { return atendido; }
            set { atendido = value; }
        }

    }
}
