using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Ventas
{
    class Cliente
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string dni;

        public string Dni
        {
            get { return dni; }
            set { dni = value; }
        }

        private string documento;

        public string Documento
        {
            get { return documento; }
            set { documento = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string apePaterno;

        public string ApePaterno
        {
            get { return apePaterno; }
            set { apePaterno = value; }
        }

        private string apeMaterno;

        public string ApeMaterno
        {
            get { return apeMaterno; }
            set { apeMaterno = value; }
        }

        private string nombreCompleto;

        public string NombreCompleto
        {
            get { return nombreCompleto; }
            set { nombreCompleto = value; }
        }

        private int sexo;

        public int Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }

        private string fecNacimiento;

        public string FecNacimiento
        {
            get { return fecNacimiento; }
            set { fecNacimiento = value; }
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

        private string ruc;

        public string Ruc
        {
            get { return ruc; }
            set { ruc = value; }
        }

        private string razonSocial;

        public string RazonSocial
        {
            get { return razonSocial; }
            set { razonSocial = value; }
        }

        private int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        private string fecRegistro;

        public string FecRegistro
        {
            get { return fecRegistro; }
            set { fecRegistro = value; }
        }

        private string distrito;

        public string Distrito
        {
            get { return distrito; }
            set { distrito = value; }
        }

        private DateTime fechaNacimiento;

        public DateTime FechaNacimiento
        {
            get { return fechaNacimiento; }
            set { fechaNacimiento = value; }
        }

        // private Cliente() { }

        public Cliente(SqlDataReader reader)
        {
            this.id = reader.IsDBNull(reader.GetOrdinal("idCliente")) ? -1 : (int)reader["idCliente"];
            this.dni = reader.IsDBNull(reader.GetOrdinal("DNI")) ? null : (string)reader["DNI"];
            this.nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : (string)reader["nombre"];
            this.apePaterno = reader.IsDBNull(reader.GetOrdinal("apePaterno")) ? null : (string)reader["apePaterno"];
            this.apeMaterno = reader.IsDBNull(reader.GetOrdinal("apeMaterno")) ? null : (string)reader["apeMaterno"];
            this.sexo = reader.IsDBNull(reader.GetOrdinal("sexo")) ? -1 : (int)reader["sexo"];
            this.fecNacimiento = reader.IsDBNull(reader.GetOrdinal("fechaNac")) ? null : (string)reader["fechaNac"].ToString();
            this.direccion = reader.IsDBNull(reader.GetOrdinal("direccion")) ? null : (string)reader["direccion"];
            this.telefono = reader.IsDBNull(reader.GetOrdinal("telefono")) ? null : (string)reader["telefono"];
            this.ruc = reader.IsDBNull(reader.GetOrdinal("RUC")) ? null : (string)reader["RUC"];
            this.razonSocial = reader.IsDBNull(reader.GetOrdinal("razonSocial")) ? null : (string)reader["razonSocial"];
            this.estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? -1 : (int)reader["estado"];
            this.fecRegistro = reader.IsDBNull(reader.GetOrdinal("fechaReg")) ? null : (string)reader["fechaReg"].ToString();
            this.distrito = reader.IsDBNull(reader.GetOrdinal("distrito")) ? null : (string)reader["distrito"].ToString();
        }

        public Cliente()
        {
            // TODO: Complete member initialization
        }
    }
}
