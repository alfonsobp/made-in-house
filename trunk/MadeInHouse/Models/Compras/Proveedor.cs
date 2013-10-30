using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Compras
{
    class Proveedor
    {

        int idProveedor;

        public int IdProveedor
        {
            get { return idProveedor; }
            set { idProveedor = value; }
        }
        string codProveedor;

        public string CodProveedor
        {
            get { return codProveedor; }
            set { codProveedor = value; }
        }
        string contacto;

        public string Contacto
        {
            get { return contacto; }
            set { contacto = value; }
        }
        string direccion;

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }
        string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        string fax;

        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }
        string observaciones;

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
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
        string telefonoContacto;

        public string TelefonoContacto
        {
            get { return telefonoContacto; }
            set { telefonoContacto = value; }
        }
        List<ProveedorxProducto> lstProducto;

        public List<ProveedorxProducto> LstProducto
        {
            get { return lstProducto; }
            set { lstProducto = value; }
        }
    }
}
