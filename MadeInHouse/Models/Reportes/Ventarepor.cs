using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Reportes
{
    class Ventarepor
    {
        private DateTime fecha;

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        String codigo;

        public String Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        String proveedor;

        public String Proveedor
        {
            get { return proveedor; }
            set { proveedor = value; }
        }

        String producto;

        public String Producto
        {
            get { return producto; }
            set { producto = value; }
        }

        String precioUnit;

        public String PrecioUnit
        {
            get { return precioUnit; }
            set { precioUnit = value; }
        }

        String cantidad;

        public String Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        String montoTotal;

        public String MontoTotal
        {
            get { return montoTotal; }
            set { montoTotal = value; }
        }

        public Ventarepor()
        {

        }

        public Ventarepor(String codigo,DateTime fecha, String proveedor, String producto,
                        String precioUnit, String cantidad, String montoTotal)
        {
            this.fecha = fecha;
            this.codigo = codigo;
            this.producto = producto;
            this.proveedor = proveedor;
            this.precioUnit = precioUnit;
            this.cantidad = cantidad;
            this.montoTotal = montoTotal;
        }
    }
}
