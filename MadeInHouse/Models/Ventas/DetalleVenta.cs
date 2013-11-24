using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Ventas
{
    class DetalleVenta
    {
        int idDetalleV;
        public int IdDetalleV
        {
            get { return idDetalleV; }
            set { idDetalleV = value; }
        }

        Venta venta;
        internal Venta Venta
        {
            get { return venta; }
            set { venta = value; }
        }

        int cantidad;
        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        double descuento;
        public double Descuento
        {
            get { return descuento; }
            set { descuento = value; }
        }

        private int idProducto;
        public int IdProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }

        private double precio;
        public double Precio
        {
            get { return precio; }
            set { precio = value; }
        }

        private double subTotal;
        public double SubTotal
        {
            get { return subTotal; }
            set { subTotal = value; }
        }

        private string descripcion;
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private string codProducto;
        public string CodProducto
        {
            get { return codProducto; }
            set { codProducto = value; }
        }

        string unidad;

        public string Unidad
        {
            get { return unidad; }
            set { unidad = value; }
        }

    }
}
