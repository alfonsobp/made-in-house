using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.DataObjects;
using System.Data.SqlClient;
using System.Windows;

namespace MadeInHouse.Models.Ventas
{
    class ClienteGateway
    {
        private DBConexion db;

        public ClienteGateway()
        {
            db = new DBConexion();
        }

        public List<Tarjeta> BuscarClientes(string tarjeta = null, string dni = null, string telefono = null, string nombre = null, int sexo = -1, string ruc = null, string razonSocial = null, string registroDesde = null, string registroHasta = null)
        {
            List<Tarjeta> clientes = null;

            string where = null;

            if (!String.IsNullOrEmpty(tarjeta))
            {
                where = where + " AND codTarjeta = @tarjeta ";
            }

            if (!String.IsNullOrEmpty(dni))
            {
                where = where + " AND DNI = @dni ";
            }

            if (!String.IsNullOrEmpty(telefono))
            {
                where = where + " AND ( telefono = @telefono OR celular = @telefono ) ";
            }

            if (!String.IsNullOrEmpty(nombre))
            {
                where = where + " AND ( (nombre + ' ' + apePaterno + ' ' + apeMaterno) like @nombre OR (apePaterno + ' ' + apeMaterno + ', ' + nombre) like @nombre ) ";
            }

            if (sexo != -1)
            {
                where = where + " AND SEXO = @sexo ";
            }

            if (!String.IsNullOrEmpty(ruc))
            {
                where = where + " AND RUC = @ruc ";
            }

            if (!String.IsNullOrEmpty(razonSocial))
            {
                where = where + " AND razonSocial = @razonSocial ";
            }

            if (!String.IsNullOrEmpty(registroDesde))
            {
                where = where + " AND convert (char, fechaReg, 103) >= @registroDesde ";
            }
            
            if (!String.IsNullOrEmpty(registroHasta))
            {
                where = where + " AND convert (char, fechaReg, 103) <= @registroHasta ";
            }

            db.cmd.CommandText = "SELECT c.*, t.* FROM Cliente c, Tarjeta t WHERE c.idCliente = t.idCliente " + where;

            if (!String.IsNullOrEmpty(tarjeta))
            {
                db.cmd.Parameters.Add(new SqlParameter("tarjeta", tarjeta));
            }

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

            if (!String.IsNullOrEmpty(ruc))
            {
                db.cmd.Parameters.Add(new SqlParameter("ruc", ruc));
            }

            if (!String.IsNullOrEmpty(razonSocial))
            {
                db.cmd.Parameters.Add(new SqlParameter("razonSocial", razonSocial));
            }

            if (!String.IsNullOrEmpty(registroDesde))
            {
                db.cmd.Parameters.Add(new SqlParameter("registroDesde", registroDesde));
            }

            if (!String.IsNullOrEmpty(registroHasta))
            {
                db.cmd.Parameters.Add(new SqlParameter("registroHasta", registroHasta));
            }

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (clientes == null) clientes = new List<Tarjeta>();
                    clientes.Add(new Tarjeta(reader));
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
