using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class Producto
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
        String descripcion;

        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        String unidadMedida;

        public String UnidadMedida
        {
            get { return unidadMedida; }
            set { unidadMedida = value; }
        }
        int percepcion;

        public int Percepcion
        {
            get { return percepcion; }
            set { percepcion = value; }
        }
        String tipoUso;

        public String TipoUso
        {
            get { return tipoUso; }
            set { tipoUso = value; }
        }
        String abreviatura;

        public String Abreviatura
        {
            get { return abreviatura; }
            set { abreviatura = value; }
        }
        String observaciones;

        public String Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }
        int idLinea;

        public int IdLinea
        {
            get { return idLinea; }
            set { idLinea = value; }
        }

        int idSubLinea;

        public int IdSubLinea
        {
            get { return idSubLinea; }
            set { idSubLinea = value; }
        }

        int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        int idUnidad;

        public int IdUnidad
        {
            get { return idUnidad; }
            set { idUnidad = value; }
        }

        private double precio;

        public double Precio
        {
            get { return precio; }
            set { precio = value; }
        }
        

    }

}
