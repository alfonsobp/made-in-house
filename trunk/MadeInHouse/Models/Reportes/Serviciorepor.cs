using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Reportes
{
    class Serviciorepor
    {
        private DateTime fecha;

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        string proveedor;

        public string Proveedor
        {
            get { return proveedor; }
            set { proveedor = value; }
        }

        string categoria;

        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        String precio;

        public String Precio
        {
            get { return precio; }
            set { precio = value; }
        }

        public Serviciorepor()
        {

        }

        public Serviciorepor(DateTime fecha, string proveedor, string categoria,
                        string nombre, String precio)
        {
            this.fecha = fecha;
            this.nombre = nombre;
            this.categoria = categoria;
            this.proveedor = proveedor;
            this.precio = precio;
        }

    }
}
