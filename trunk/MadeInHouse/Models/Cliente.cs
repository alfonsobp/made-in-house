using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models
{
    class Cliente
    {
        public int id { get; set; }
        public string dni { get; set; }
        public string nombre { get; set; }
        public string apePaterno { get; set; }
        public string apeMaterno { get; set; }
        public int sexo { get; set; }
        public string fecNacimiento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string ruc { get; set; }
        public string razonSocial { get; set; }
        public int estado { get; set; }
        public string fecRegistro { get; set; }

        private Cliente() { }
        
        public Cliente(int id, string dni, string nombre, string apePaterno, string apeMaterno, int sexo, string fecNacimiento, string direccion, string telefono, string ruc, string razonSocial, int estado, string fecRegistro)
        {
            this.id = id;
            this.dni = dni;
            this.nombre = nombre;
            this.apePaterno = apePaterno;
            this.apeMaterno = apeMaterno;
            this.sexo = sexo;
            this.fecNacimiento = fecNacimiento;
            this.direccion = direccion;
            this.telefono = telefono;
            this.ruc = ruc;
            this.razonSocial = razonSocial;
            this.estado = estado;
            this.fecRegistro = fecRegistro;
        }

        public Cliente(SqlDataReader reader)
        {
            this.id = reader["idCliente"] == null ? -1 : (int)reader["idCliente"];
            this.dni = reader["DNI"] == null ? null : (string)reader["DNI"];
            this.nombre = reader["nombre"] == null ? null : (string)reader["nombre"];
            this.apePaterno = reader["apePaterno"] == null ? null : (string)reader["apePaterno"];
            this.apeMaterno = reader["apeMaterno"] == null ? null : (string)reader["apeMaterno"];
            this.sexo = reader["sexo"] == null ? -1 : (int)reader["sexo"];
            this.fecNacimiento = reader["fechaNac"] == null ? null : (string)reader["fechaNac"].ToString();
            this.direccion = reader["direccion"] == null ? null : (string)reader["direccion"];
            Console.WriteLine("Tel: " + reader["telefono"]);
            this.telefono = reader["telefono"] == null ? null : reader["telefono"].Equals("") ? null : (string)reader["telefono"];
            this.ruc = reader["RUC"] == null ? null : (string)reader["RUC"];
            this.razonSocial = reader["razonSocial"] == null ? null : (string)reader["razonSocial"];
            this.estado = reader["estado"] == null ? -1 : (int)reader["estado"];
            this.fecRegistro = reader["fechaReg"] == null ? null : (string)reader["fechaReg"].ToString();
        }
    }
}
