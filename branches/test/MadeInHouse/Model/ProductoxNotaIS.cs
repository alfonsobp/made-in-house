using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class ProductoxNotaIS
    {
        int idNota;

        public int IdNota
        {
            get { return idNota; }
            set { idNota = value; }
        }
        Almacen alm;

        internal Almacen Alm
        {
            get { return alm; }
            set { alm = value; }
        }
        Producto prod;

        internal Producto Prod
        {
            get { return prod; }
            set { prod = value; }
        }
        TipoZona tipozon;

        internal TipoZona Tipozon
        {
            get { return tipozon; }
            set { tipozon = value; }
        }
        Ubicacion ubi;

        internal Ubicacion Ubi
        {
            get { return ubi; }
            set { ubi = value; }
        }
        int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

    }
}
