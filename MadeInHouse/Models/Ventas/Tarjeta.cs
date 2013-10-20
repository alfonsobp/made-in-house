using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Ventas
{
    class Tarjeta
    {
        public string codTarjeta { get; set; }
        public string fecEmision { get; set; }
        public string fecAnulado { get; set; }
        public int estado { get; set; }
        public Cliente cliente { get; set; }

        private Tarjeta() { }

        public Tarjeta(SqlDataReader reader)
        {
            this.codTarjeta = reader.IsDBNull(reader.GetOrdinal("codTarjeta"))? null : (string)reader["codTarjeta"];
            this.fecEmision = reader.IsDBNull(reader.GetOrdinal("fechaEmi"))? null : (string)reader["fechaEmi"].ToString();
            this.fecAnulado = reader.IsDBNull(reader.GetOrdinal("fechaAnu"))? null : (string)reader["fechaAnu"].ToString();
            this.estado = reader.IsDBNull(reader.GetOrdinal("estado"))? -1: (int)reader["estado"];
            this.cliente = new Cliente(reader);
        }
    }
}
