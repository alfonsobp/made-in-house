using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Model.Ventas;

namespace MadeInHouse.Model
{
    class DetalleDevolucion
    {
        int idDetalleDev;

        public int IdDetalleDev
        {
            get { return idDetalleDev; }
            set { idDetalleDev = value; }
        }
        Devolucion devolucion;

        internal Devolucion Devolucion
        {
            get { return devolucion; }
            set { devolucion = value; }
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
        Producto producto;

        internal Producto Producto
        {
            get { return producto; }
            set { producto = value; }
        }
        string motivo;

        public string Motivo
        {
            get { return motivo; }
            set { motivo = value; }
        }

    }
}
