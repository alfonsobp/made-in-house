using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Almacen
    {
        int idAlmacen;

        public int IdAlmacen
        {
            get { return idAlmacen; }
            set { idAlmacen = value; }
        }
        int altura;

        public int Altura
        {
            get { return altura; }
            set { altura = value; }
        }
        string codAlmacen;

        public string CodAlmacen
        {
            get { return codAlmacen; }
            set { codAlmacen = value; }
        }
        int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        DateTime fechaReg;

        public DateTime FechaReg
        {
            get { return fechaReg; }
            set { fechaReg = value; }
        }
        Tienda tienda;

        internal Tienda Tienda
        {
            get { return tienda; }
            set { tienda = value; }
        }
        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        int nroColumnas;

        public int NroColumnas
        {
            get { return nroColumnas; }
            set { nroColumnas = value; }
        }
        int nroFilas;

        public int NroFilas
        {
            get { return nroFilas; }
            set { nroFilas = value; }
        }
        string tipo;

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        List<AlmacenxProducto> lstProdAlm;

        public List<AlmacenxProducto> LstProdAlm
        {
            get { return lstProdAlm; }
            set { lstProdAlm = value; }
        }
        List<Tienda> lstTienda;

        public List<Tienda> LstTienda
        {
            get { return lstTienda; }
            set { lstTienda = value; }
        }
        List<ZonaxAlmacen> lstZonas;

        public List<ZonaxAlmacen> LstZonas
        {
            get { return lstZonas; }
            set { lstZonas = value; }
        }
    }
}
