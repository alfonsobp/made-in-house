using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.Models.Compras
{
    class OrdenCompra
    {


        int idOrden;

        public int IdOrden
        {
            get { return idOrden; }
            set { idOrden = value; }
        }
        string codOrdenCompra;

        public string CodOrdenCompra
        {
            get { return codOrdenCompra; }
            set { codOrdenCompra = value; }
        }
        DateTime fechaReg;

        public DateTime FechaReg
        {
            get { return fechaReg; }
            set { fechaReg = value; }
        }
        DateTime fechaSinAtencion;

        public DateTime FechaSinAtencion
        {
            get { return fechaSinAtencion; }
            set { fechaSinAtencion = value; }
        }
        Proveedor proveedor;

        public Proveedor Proveedor
        {
            get { return proveedor; }
            set { proveedor = value; }
        }
        double montoBruto;

        public double MontoBruto
        {
            get { return montoBruto; }
            set { montoBruto = value; }
        }
        string observaciones;

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }
      

        

        List<ProductoxOrdenCompra> lstProducto;

        internal List<ProductoxOrdenCompra> LstProducto
        {
            get { return lstProducto; }
            set { lstProducto = value; }
        }


        int idAlmacen;

        public int IdAlmacen
        {
            get { return idAlmacen; }
            set { idAlmacen = value; }
        }

        string medioPago;

        public string MedioPago
        {
            get { return medioPago; }
            set { medioPago = value; }
        }


        public OrdenCompra(int idAlmacen, Proveedor p,String observaciones) {

            this.idAlmacen = idAlmacen;
            this.proveedor = p;
            this.observaciones = observaciones;
            this.MedioPago = (p.EsBoleta) ? "BOLETA" : "FACTURA";

        
        }

        public OrdenCompra(){}


    }
}
