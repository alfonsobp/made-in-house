using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class ProductoCant
    {
        private string proNombre;

        public string ProNombre
        {
            get { return proNombre; }
            set { proNombre = value; }
        }

        private int proId;

        public int ProId
        {
            get { return proId; }
            set { proId = value; }
        }

        private int can;

        public int Can
        {
            get { return can; }
            set { can = value; }
        }

    }
}
