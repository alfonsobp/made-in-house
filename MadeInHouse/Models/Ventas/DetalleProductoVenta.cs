using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.Models.Ventas
{
    class DetalleProductoVenta
    {
        int idProducto;

        public int IdProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }
        String codigoProd;

        public String CodigoProd
        {
            get { return codigoProd; }
            set { codigoProd = value; }
        }
        String nombre;

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        //String descripcion;

        //public String Descripcion
        //{
        //    get { return descripcion; }
        //    set { descripcion = value; }
        //}

        //String abreviatura;

        //public String Abreviatura
        //{
        //    get { return abreviatura; }
        //    set { abreviatura = value; }
        //}

        int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        //private double precio;

        //public double Precio
        //{
        //    get { return precio; }
        //    set { precio = value; }
        //}

        private LineaProducto linea;

        public LineaProducto Linea
        {
            get { return linea; }
            set { linea = value; }
        }

        private SubLineaProducto sublinea;

        public SubLineaProducto Sublinea
        {
            get { return sublinea; }
            set { sublinea = value; }
        }

        private Tienda tienda;

        public Tienda Tienda
        {
            get { return tienda; }
            set { tienda = value; }
        }

        private float precioVenta;

        public float PrecioVenta
        {
            get { return precioVenta; }
            set { precioVenta = value; }
        }

        public DetalleProductoVenta()
        {

        }

    }
}
