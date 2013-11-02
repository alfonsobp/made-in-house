using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Compras
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
        DateTime fechaFin;

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; }
        }
        DateTime fechaInicio;

        public DateTime FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; }
        }
        DateTime fechaRespuesta;

        public DateTime FechaRespuesta
        {
            get { return fechaRespuesta; }
            set { fechaRespuesta = value; }
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
