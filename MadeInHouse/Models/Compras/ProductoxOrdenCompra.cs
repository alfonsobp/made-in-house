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


        string cantidad;

        public string Cantidad
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

        int cantAtendida;

        public int CantAtendida
        {
            get { return cantAtendida; }
            set { cantAtendida = value; }
        }

        


        public ProductoxOrdenCompra(Consolidado c,int idOrden) {

            this.cantidad = Convert.ToString(c.Cantidad);
            this.producto = c.Producto;
            this.idOrden = idOrden;
            this.PrecioUnitario = c.Costo;
        }

        public ProductoxOrdenCompra() { }

    }
}
