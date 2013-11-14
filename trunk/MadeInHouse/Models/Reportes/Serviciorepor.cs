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

        String cliente;

        public String Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        String tienda;

        public String Tienda
        {
            get { return tienda; }
            set { tienda = value; }
        }

        String nombre;

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        Double precio;

        public Double Precio
        {
            get { return precio; }
            set { precio = value; }
        }

        public Serviciorepor()
        {

        }

        public Serviciorepor(DateTime fecha, String cliente, String tienda,
                        String nombre, Double precio)
        {
            this.fecha = fecha;
            this.nombre = nombre;
            this.cliente = cliente;
            this.tienda = tienda;
            this.precio = precio;
        }

    }
}
