using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Ventas;

namespace MadeInHouse.Models.Almacen
{
    class OrdenDespacho
    {

        int idOrdenDespacho;
        public int IdOrdenDespacho
        {
            get { return idOrdenDespacho; }
            set { idOrdenDespacho = value; }
        }

        string codOrden;
        public string CodOrden
        {
            get { return codOrden; }
            set { codOrden = value; }
        }

        DateTime fechaDespacho;

        public DateTime FechaDespacho
        {
            get { return fechaDespacho; }
            set { fechaDespacho = value; }
        }

        Venta venta;

        public Venta Venta
        {
            get { return venta; }
            set { venta = value; }
        }

        int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        private Almacenes almOrigen;
        public Almacenes AlmOrigen
        {
            get { return almOrigen; }
            set { almOrigen = value; }
        }


        private string direccion;
        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        private string telefono;
        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }


        public void OrdenDeDespacho()
        {

        }
    }
}
