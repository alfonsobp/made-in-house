using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Compras
{
    class CatalogoProveedor
    {

        string estado="ACTIVO";

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        DateTime fechaReg = DateTime.Now;

        public DateTime FechaReg
        {
            get { return fechaReg; }
            set { fechaReg = value; }
        }

        DateTime fechaAct = DateTime.Now;

        public DateTime FechaAct
        {
            get { return fechaAct; }
            set { fechaAct = value; }
        }

        string codigoProducto;

        public string CodigoProducto
        {
            get { return codigoProducto; }
            set { codigoProducto = value; }
        }
         string codigoComercial;

         public string CodigoComercial
         {
             get { return codigoComercial; }
             set { codigoComercial = value; }
         }
         double precio;

         public double Precio
         {
             get { return precio; }
             set { precio = value; }
         }
         string descripcion;

         public string Descripcion
         {
             get { return descripcion; }
             set { descripcion = value; }
         }
         Proveedor proveedor ;

        public  Proveedor Proveedor
        {
          get { return proveedor; }
          set { proveedor = value; }
        }

       

    }
}
