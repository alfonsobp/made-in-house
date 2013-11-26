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


        string cantidad="0";

        public string Cantidad
        {
            get { return cantidad;}
            set { cantidad = value; }
        }

        string cantidadAtender = "0";
        double monto;
        public double Monto
        {
            get { return monto; }
            set { monto = value; }
        }
 public string CantidadAtender
        {
            get { return cantidadAtender; }
            set { cantidadAtender = value; }
        }        private double precioUnitario;

        public double PrecioUnitario
        {
            get { return precioUnitario; }
            set { precioUnitario = value; }
        }

        private double precioCot;
        public double PrecioCot
        {
            get { return precioCot; }
            set { precioCot = value; }
        }

        private double importe;
        public double Importe
        {
            get { return importe; }
            set { importe = value; }
        }

        private int cantAtendida=0;
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
