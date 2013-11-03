using MadeInHouse.Models.Almacen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Compras
{
    class ProveedorxProducto
    {
       

        int idProveedor;

        public int IdProveedor
        {
            get { return idProveedor; }
            set { idProveedor = value; }
        }
        Producto producto;

        public Producto Producto
        {
            get { return producto; }
            set { producto = value; }
        }
        string codComercial;

        public string CodComercial
        {
            get { return codComercial; }
            set { codComercial = value; }
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
        double precio;

        public double Precio
        {
            get { return precio; }
            set { precio = value; }
        }

        DateTime fechaReg;

        public DateTime FechaReg
        {
            get { return fechaReg; }
            set { fechaReg = value; }
        }
        DateTime fechaAct;

        public DateTime FechaAct
        {
            get { return fechaAct; }
            set { fechaAct = value; }
        }

        private string estadoVisible;

        public string EstadoVisible
        {
            get { return estadoVisible; }
            set { estadoVisible = value; }
        }
    }
}
