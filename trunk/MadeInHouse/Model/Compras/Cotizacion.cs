using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Cotizacion
    {
        int idCotizacion;

        public int IdCotizacion
        {
            get { return idCotizacion; }
            set { idCotizacion = value; }
        }
        int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        int fechaFin;

        public int FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; }
        }
        int fechaInicio;

        public int FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; }
        }
        Proveedor proveedor;

        public Proveedor Proveedor
        {
            get { return proveedor; }
            set { proveedor = value; }
        }
        string observacion;

        public string Observacion
        {
            get { return observacion; }
            set { observacion = value; }
        }
        List<CotizacionxProducto> lstProdCotizacion;

        public List<CotizacionxProducto> LstProdCotizacion
        {
            get { return lstProdCotizacion; }
            set { lstProdCotizacion = value; }
        }
    }
}
