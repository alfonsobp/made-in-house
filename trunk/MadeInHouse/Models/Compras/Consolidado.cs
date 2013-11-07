using MadeInHouse.Models.Almacen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Compras
{
    class Consolidado
    {

        int idAlmacen;

        public int IdAlmacen
        {
            get { return idAlmacen; }
            set { idAlmacen = value; }
        }

        Producto producto;

        public Producto Producto
        {
            get { return producto; }
            set { producto = value;}
        }

        int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        private double costo;

        public double Costo
        {
            get { return costo; }
            set { costo = value; }
        }


        Proveedor prov;

        public Proveedor Prov
        {
            get { return prov; }
            set { prov = value; }
        }

    }
}
