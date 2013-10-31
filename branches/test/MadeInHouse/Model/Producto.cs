using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Producto
    {
        int idProducto;

        public int IdProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }
        string abreviatura;

        public string Abreviatura
        {
            get { return abreviatura; }
            set { abreviatura = value; }
        }
        string codProducto;

        public string CodProducto
        {
            get { return codProducto; }
            set { codProducto = value; }
        }
        string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        LineaProducto lineaprod;

        public LineaProducto Lineaprod
        {
            get { return lineaprod; }
            set { lineaprod = value; }
        }
        SubLineaProducto sublinea;

        internal SubLineaProducto Sublinea
        {
            get { return sublinea; }
            set { sublinea = value; }
        }
        UnidadMedida unidad;

        internal UnidadMedida Unidad
        {
            get { return unidad; }
            set { unidad = value; }
        }
        string unidadMedida;

        public string UnidadMedida
        {
            get { return unidadMedida; }
            set { unidadMedida = value; }
        }
        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        string observaciones;

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }
        int percepcion;

        public int Percepcion
        {
            get { return percepcion; }
            set { percepcion = value; }
        }
        double precio;

        public double Precio
        {
            get { return precio; }
            set { precio = value; }
        }
        string tipoUso;

        public string TipoUso
        {
            get { return tipoUso; }
            set { tipoUso = value; }
        }
    }
}
