using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Ventas
{
    class Tarjeta
    {//C:\Users\cliente712\Documents\Visual Studio 2012\Projects\made-in-house\MadeInHouse\Models\

        private string codTarjeta;

        public string CodTarjeta
        {
            get { return codTarjeta; }
            set { codTarjeta = value; }
        }

        private string fecEmision;

        public string FecEmision
        {
            get { return fecEmision; }
            set { fecEmision = value; }
        }

        private string fecAnulado;

        public string FecAnulado
        {
            get { return fecAnulado; }
            set { fecAnulado = value; }
        }

        private int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        private Cliente cliente;

        public Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        public Tarjeta()
        {

        }

        public Tarjeta(SqlDataReader reader)
        {
            this.codTarjeta = reader.IsDBNull(reader.GetOrdinal("codTarjeta")) ? null : (string)reader["codTarjeta"];
            this.fecEmision = reader.IsDBNull(reader.GetOrdinal("fechaEmi")) ? null : (string)reader["fechaEmi"].ToString();
            this.fecAnulado = reader.IsDBNull(reader.GetOrdinal("fechaAnu")) ? null : (string)reader["fechaAnu"].ToString();
            this.estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? -1 : (int)reader["estado"];
            this.cliente = new Cliente(reader);

        }
    }
}

