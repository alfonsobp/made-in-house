using MadeInHouse.Models.Almacen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Compras
{
    class ProductoxOrdenCompra
    {

        private int idOrden;

        public int IdOrden
        {
            get { return idOrden; }
            set { idOrden = value; }
        }

        Producto producto;

        public Producto Producto
        {
            get { return producto; }
            set { producto = value; }
        }
        int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        double monto;

        private double precioUnitario;

        public double PrecioUnitario
        {
            get { return precioUnitario; }
            set { precioUnitario = value; }
        }


        public ProductoxOrdenCompra(Consolidado c,int idOrden) {

            this.cantidad = c.Cantidad;
            this.producto = c.Producto;
            this.idOrden = idOrden;
        
        }

        public ProductoxOrdenCompra() { }

    }
}
