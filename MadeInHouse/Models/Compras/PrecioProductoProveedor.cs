using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Compras
{
    class PrecioProductoProveedor
    {
        Proveedor prov;

        public Proveedor Prov
        {
            get { return prov; }
            set { prov = value; }
        }
        double costo;

        public double Costo
        {
            get { return costo; }
            set { costo = value; }
        }

    }
}
