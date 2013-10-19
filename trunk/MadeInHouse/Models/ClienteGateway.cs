using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.DataObjects;
using System.Data.SqlClient;
using System.Windows;

namespace MadeInHouse.Models
{
    class ClienteGateway
    {
        private DBConexion db;

        public ClienteGateway()
        {
            db = new DBConexion();
        }

        public List<Cliente> BuscarClientes(string dni = null, string telefono = null, string nombre = null, int sexo = -1)
        {
            List<Cliente> clientes = null;

            string where = null;

            if (!String.IsNullOrEmpty(dni))
            {
                if (where == null) where = " WHERE ";
                else where = where + " AND ";
                where = where + " DNI = @dni";
            }

            if (!String.IsNullOrEmpty(telefono))
            {
                if (where == null) where = " WHERE ";
                else where = where + " AND ";
                where = where + " telefono = @telefono";
            }

            if (!String.IsNullOrEmpty(nombre))
            {
                if (where == null) where = " WHERE ";
                else where = where + " AND ";
                where = where + " ( (nombre + ' ' + apePaterno + ' ' + apeMaterno) like @nombre OR (apePaterno + ' ' + apeMaterno + ', ' + nombre) like @nombre ) ";
            }

            if (sexo != -1)
            {
                if (where == null) where = " WHERE ";
                else where = where + " AND ";
                where = where + " SEXO = @sexo";
            }

            db.cmd.CommandText = "SELECT * FROM Cliente " + where;

            if (!String.IsNullOrEmpty(dni))
            {
                db.cmd.Parameters.Add(new SqlParameter("dni", dni));
            }

            if (!String.IsNullOrEmpty(telefono))
            {
                db.cmd.Parameters.Add(new SqlParameter("telefono", telefono));
            }

            if (!String.IsNullOrEmpty(nombre))
            {
                db.cmd.Parameters.Add(new SqlParameter("nombre", '%' + nombre + '%'));
            }

            if (sexo != -1)
            {
                db.cmd.Parameters.Add(new SqlParameter("sexo", sexo));
            }

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (clientes == null) clientes = new List<Cliente>();
                    clientes.Add(new Cliente(reader));
                }

                db.conn.Close();
            }
            catch(SqlException e) {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return clientes;
        }
    }
}
