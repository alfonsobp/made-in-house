using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Servicio
    {
        int idServicio;

        public int IdServicio
        {
            get { return idServicio; }
            set { idServicio = value; }
        }

        string codServicio;

        public string CodServicio
        {
            get { return codServicio; }
            set { codServicio = value; }
        }

        string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        int idProveedor;

        public int IdProveedor
        {
            get { return idProveedor; }
            set { idProveedor = value; }
        }

        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        /*
        List<ServicioxProducto> lstProductos;

        public List<ServicioxProducto> LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value; }
        }
         */
    }
}
