using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Ubicacion
    {
        int idUbicacion;

        public int IdUbicacion
        {
            get { return idUbicacion; }
            set { idUbicacion = value; }
        }
        Almacen alm;

        public Almacen Alm
        {
            get { return alm; }
            set { alm = value; }
        }
        TipoZona tipoZon;

        public TipoZona TipoZon
        {
            get { return tipoZon; }
            set { tipoZon = value; }
        }
        int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        int cordX;

        public int CordX
        {
            get { return cordX; }
            set { cordX = value; }
        }
        int cordY;

        public int CordY
        {
            get { return cordY; }
            set { cordY = value; }
        }
        int cordZ;

        public int CordZ
        {
            get { return cordZ; }
            set { cordZ = value; }
        }
        Producto prod;

        public Producto Prod
        {
            get { return prod; }
            set { prod = value; }
        }
        int volOcupado;

        public int VolOcupado
        {
            get { return volOcupado; }
            set { volOcupado = value; }
        }
    }
}
