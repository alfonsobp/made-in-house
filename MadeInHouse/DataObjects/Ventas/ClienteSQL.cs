using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Ventas;
using System.Data.SqlClient;

namespace MadeInHouse.DataObjects.Ventas
{
    class ClienteSQL
    {
        private DBConexion db;

        public ClienteSQL()
        {
            db = new DBConexion();
        }

        public List<Tarjeta> BuscarClientes(string dni = null, string nombre = null, int tipoCliente = -1, string registroDesde = null, string registroHasta = null)
        {
            List<Tarjeta> clientes = null;

            string where = "";

            if (!String.IsNullOrEmpty(dni))
            {
                if (where.Equals("")) where += " WHERE ";
                else where += " AND ";
                if (dni.Length == 8)
                {
                    where = where + " DNI = @dni ";
                    db.cmd.Parameters.Add(new SqlParameter("dni", dni));
                }
                else
                {
                    where = where + " RUC = @ruc ";
                    db.cmd.Parameters.Add(new SqlParameter("ruc", dni));
                }
            }

            if (!String.IsNullOrEmpty(nombre) && tipoCliente != -1)
            {
                if (where.Equals("")) where += " WHERE ";
                else where += " AND ";
                if (tipoCliente == 0)
                {
                    where = where + " ( (nombre + ' ' + apePaterno + ' ' + apeMaterno) like @nombre OR (apePaterno + ' ' + apeMaterno + ', ' + nombre) like @nombre ) ";
                    db.cmd.Parameters.Add(new SqlParameter("nombre", '%' + nombre + '%'));
                }
                else
                {
                    where = where + " ( razonSocial like @nombre ) ";
                    db.cmd.Parameters.Add(new SqlParameter("nombre", '%' + nombre + '%'));
                }
            }

            if (!String.IsNullOrEmpty(registroDesde))
            {
                if (where.Equals("")) where += " WHERE ";
                else where += " AND ";
                where = where + " convert (char, fechaReg, 103) >= @registroDesde ";
                db.cmd.Parameters.Add(new SqlParameter("registroDesde", registroDesde));
            }
            
            if (!String.IsNullOrEmpty(registroHasta))
            {
                if (where.Equals("")) where += " WHERE ";
                else where += " AND ";
                where = where + " convert (char, fechaReg, 103) <= @registroHasta ";
                db.cmd.Parameters.Add(new SqlParameter("registroHasta", registroHasta));
            }

            db.cmd.CommandText = "SELECT c.*, t.* FROM Cliente c LEFT JOIN Tarjeta t ON c.idCliente = t.idCliente AND t.estado = 1 " + where;
            
            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (clientes == null) clientes = new List<Tarjeta>();
                    Tarjeta tarj = new Tarjeta(reader);
                    Cliente cli = tarj.cliente;
                    if (String.IsNullOrEmpty(cli.razonSocial))
                    {
                        cli.nombreCompleto = cli.nombre + ' ' + cli.apePaterno + ' ' + cli.apeMaterno;
                        cli.documento = cli.dni;
                    }
                    else
                    {
                        cli.nombreCompleto = cli.razonSocial;
                        cli.documento = cli.ruc;
                    }
                    clientes.Add(tarj);
                }

                db.conn.Close();
            }
            catch(SqlException e)
            {
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
