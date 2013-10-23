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

        String proveedor;

        public String Proveedor
        {
            get { return proveedor; }
            set { proveedor = value; }
        }

        String categoria;

        public String Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        String nombre;

        public String Nombre
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

        public Serviciorepor(DateTime fecha, String proveedor, String categoria,
                        String nombre, String precio)
        {
            this.fecha = fecha;
            this.nombre = nombre;
            this.categoria = categoria;
            this.proveedor = proveedor;
            this.precio = precio;
        }

    }
}
