using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Compras
{
    class PagoParcial
    {
        private int idPago;

        public int IdPago
        {
            get { return idPago; }
            set { idPago = value; }
        }

        DocPagoProveedor docPago;

        public DocPagoProveedor DocPago
        {
            get { return docPago; }
            set { docPago = value; }
        }

        

        private double monto;

        public double Monto
        {
            get { return monto; }
            set { monto = value; }
        }

        private DateTime fechaPago;

        public DateTime FechaPago
        {
            get { return fechaPago; }
            set { fechaPago = value; }
        }
    }
}
