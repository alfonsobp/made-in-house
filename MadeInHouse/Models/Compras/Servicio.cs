using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Compras
{
    class Servicio
    {

        string codigo;

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        string proveedor;

        public string Proveedor
        {
            get { return proveedor; }
            set { proveedor = value; }
        }

        public Servicio()
        {

        }

        public Servicio(string codigo, string nombre, string descripcion,
                        string proveedor)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.proveedor = proveedor;
        }

    }
}
