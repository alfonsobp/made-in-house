using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Ventas;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

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
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            List<Tarjeta> clientes = null;

            string where = "";

            if (!String.IsNullOrEmpty(dni))
            {
                if (where.Equals("")) where += " WHERE ";
                else where += " AND ";
                if (dni.Length == 8)
                {
                    where = where + " DNI = @dni ";
                    cmd.Parameters.Add(new SqlParameter("dni", dni));
                }
                else
                {
                    where = where + " RUC = @ruc ";
                    cmd.Parameters.Add(new SqlParameter("ruc", dni));
                }
            }

            if (!String.IsNullOrEmpty(nombre) && tipoCliente != -1)
            {
                if (where.Equals("")) where += " WHERE ";
                else where += " AND ";
                if (tipoCliente == 0)
                {
                    where = where + " ( (nombre + ' ' + apePaterno + ' ' + apeMaterno) like @nombre OR (apePaterno + ' ' + apeMaterno + ', ' + nombre) like @nombre ) ";
                    cmd.Parameters.Add(new SqlParameter("nombre", '%' + nombre + '%'));
                }
                else
                {
                    where = where + " ( razonSocial like @nombre ) ";
                    cmd.Parameters.Add(new SqlParameter("nombre", '%' + nombre + '%'));
                }
            }

            if (!String.IsNullOrEmpty(registroDesde))
            {
                if (where.Equals("")) where += " WHERE ";
                else where += " AND ";
                where = where + " convert (char, fechaReg, 103) >= @registroDesde ";
                cmd.Parameters.Add(new SqlParameter("registroDesde", registroDesde));
            }
            
            if (!String.IsNullOrEmpty(registroHasta))
            {
                if (where.Equals("")) where += " WHERE ";
                else where += " AND ";
                where = where + " convert (char, fechaReg, 103) <= @registroHasta ";
                cmd.Parameters.Add(new SqlParameter("registroHasta", registroHasta));
            }

            cmd.CommandText = "SELECT c.*, t.* FROM Cliente c LEFT JOIN Tarjeta t ON c.idCliente = t.idCliente AND t.estado = 1 " + where;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

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

                conn.Close();
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

        public static int agregarCliente(Cliente a,DateTime hoy)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "INSERT INTO Cliente(DNI,nombre,apePaterno,apeMaterno,RUC,sexo,razonSocial,direccion,telefono,estado,distrito) " +
            "VALUES (@DNI,@nombre,@apePaterno,@apeMaterno,@RUC,@sexo,@razonSocial,@direccion,@telefono,@estado,@distrito)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@DNI", a.dni);
            cmd.Parameters.AddWithValue("@nombre", a.nombre);
            cmd.Parameters.AddWithValue("@apePaterno", a.apePaterno);
            cmd.Parameters.AddWithValue("@apeMaterno", a.apeMaterno);
            cmd.Parameters.AddWithValue("@RUC", a.ruc);
            cmd.Parameters.AddWithValue("@sexo", 'M');//a.sexo
            cmd.Parameters.AddWithValue("@razonSocial", a.razonSocial);
            cmd.Parameters.AddWithValue("@direccion", a.direccion);
            cmd.Parameters.AddWithValue("@telefono", a.telefono);
            cmd.Parameters.AddWithValue("@estado", a.estado);
            cmd.Parameters.AddWithValue("@distrito", a.distrito);
            //cmd.Parameters.AddWithValue("@fechaReg", hoy);
            //cmd.Parameters.AddWithValue("@fechaNac", a.fechaNacimiento);

            try
            {
                conn.Open();
                k = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
            return k;
        }

        public static int buscarIDCliente(Cliente a)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            int k = 0;

            cmd.CommandText = "SELECT idCliente FROM Cliente WHERE DNI=@DNI";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@DNI", a.dni);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    k = Int32.Parse(reader["idCliente"].ToString());
                }
                else
                    conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
            return k;
        }

        public static int RegistrarTarjeta(Int32 id, DateTime date, String codt)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "INSERT INTO Tarjeta(fechaEmi,codTarjeta,estado,idCliente) " +
            "VALUES (@fechaEmi,@codTarjeta,@estado,@idCliente)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@fechaEmi", date);
            cmd.Parameters.AddWithValue("@codTarjeta", codt);
            cmd.Parameters.AddWithValue("@estado", 1);
            cmd.Parameters.AddWithValue("@idCliente", id);

            try
            {
                conn.Open();
                k = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
            return k;
        }
    }
}
