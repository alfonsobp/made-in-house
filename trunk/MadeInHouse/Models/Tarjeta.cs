using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models
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
            this.codTarjeta = reader["codTarjeta"] == null ? null : (string)reader["codTarjeta"];
            this.fecEmision = reader["fechaEmi"] == null ? null : (string)reader["fechaEmi"];
            this.fecAnulado = reader["fechaAnu"] == null ? null : (string)reader["fechaAnu"];
            this.estado = reader["estado"] == null ? -1: (int)reader["estado"];
            this.cliente = new Cliente(reader);
        }
    }
}
