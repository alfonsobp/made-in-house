using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
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

        internal Proveedor Proveedor
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
        Almacen alm;


        internal Almacen Alm
        {
            get { return alm; }
            set { alm = value; }
        }
        List<ProductoxOrdenCompra> lstProducto;

        internal List<ProductoxOrdenCompra> LstProducto
        {
            get { return lstProducto; }
            set { lstProducto = value; }
        }
    }
}
