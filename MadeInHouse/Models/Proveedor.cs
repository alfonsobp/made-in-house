using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models
{
    class Proveedor
    {

        string codigo;

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        string razonSocial;

        public string RazonSocial
        {
            get { return razonSocial; }
            set { razonSocial = value; }
        }
        string ruc;

        public string Ruc
        {
            get { return ruc; }
            set { ruc = value; }
        }

        string telefono;

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        string fax;

        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }

        string contacto;

        public string Contacto
        {
            get { return contacto; }
            set { contacto = value; }
        }

        string telefonoContacto;

        public string TelefonoContacto
        {
            get { return telefonoContacto; }
            set { telefonoContacto = value; }
        }

        string direccion;

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public Proveedor(string codigo, string razonsocial, string ruc) {
            this.codigo = codigo;
            this.razonSocial = razonsocial;
            this.ruc = ruc;     
        }
    }
}
