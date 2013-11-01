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
        public int id { get; set; }
        public string dni { get; set; }
        public string documento { get; set; }
        public string nombre { get; set; }
        public string apePaterno { get; set; }
        public string apeMaterno { get; set; }
        public string nombreCompleto { get; set; }
        public int sexo { get; set; }
        public string fecNacimiento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string ruc { get; set; }
        public string razonSocial { get; set; }
        public int estado { get; set; }
        public string fecRegistro { get; set; }
        public string distrito { get; set; }
        public DateTime fechaNacimiento { get; set; }

       // private Cliente() { }

        public Cliente(SqlDataReader reader)
        {
            this.id = reader.IsDBNull(reader.GetOrdinal("idCliente"))? -1 : (int)reader["idCliente"];
            this.dni = reader.IsDBNull(reader.GetOrdinal("DNI"))? null : (string)reader["DNI"];
            this.nombre = reader.IsDBNull(reader.GetOrdinal("nombre"))? null : (string)reader["nombre"];
            this.apePaterno = reader.IsDBNull(reader.GetOrdinal("apePaterno"))? null : (string)reader["apePaterno"];
            this.apeMaterno = reader.IsDBNull(reader.GetOrdinal("apeMaterno"))? null : (string)reader["apeMaterno"];
            this.sexo = reader.IsDBNull(reader.GetOrdinal("sexo"))? -1 : (int)reader["sexo"];
            this.fecNacimiento = reader.IsDBNull(reader.GetOrdinal("fechaNac"))? null : (string)reader["fechaNac"].ToString();
            this.direccion = reader.IsDBNull(reader.GetOrdinal("direccion"))? null : (string)reader["direccion"];
            this.telefono = reader.IsDBNull(reader.GetOrdinal("telefono"))? null : (string)reader["telefono"];
            this.ruc = reader.IsDBNull(reader.GetOrdinal("RUC"))? null : (string)reader["RUC"];
            this.razonSocial = reader.IsDBNull(reader.GetOrdinal("razonSocial"))? null : (string)reader["razonSocial"];
            this.estado = reader.IsDBNull(reader.GetOrdinal("estado"))? -1 : (int)reader["estado"];
            this.fecRegistro = reader.IsDBNull(reader.GetOrdinal("fechaReg"))? null : (string)reader["fechaReg"].ToString();
            this.distrito = reader.IsDBNull(reader.GetOrdinal("distrito")) ? null : (string)reader["distrito"].ToString();
        }

        public Cliente()
        {
            // TODO: Complete member initialization
        }
    }
}
