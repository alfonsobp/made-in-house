using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Ventas;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Diagnostics;

namespace MadeInHouse.DataObjects.Ventas
{
    class ClienteSQL
    {
        public static List<Tarjeta> BuscarClientes(string dni = null, string nombre = null, int tipoCliente = -1, string registroDesde = null, string registroHasta = null)
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

            cmd.CommandText = "SELECT c.*, t.* FROM Cliente c RIGHT JOIN Tarjeta t ON c.idCLiente = t.idCLiente AND t.estado = 1 " + where + " ORDER by c.idCLiente";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                clientes = new List<Tarjeta>();
                while (reader.Read())
                {
                    Cliente cli = new Cliente();
                    cli.Id = Int32.Parse(reader["idCLiente"].ToString());
                    cli.Dni = reader["DNI"].ToString();
                    cli.Nombre = reader["nombre"].ToString();
                    cli.ApePaterno = reader["apePaterno"].ToString();
                    cli.ApeMaterno = reader["apeMaterno"].ToString();
                    cli.NombreCompleto = cli.Nombre + " " + cli.ApePaterno + " " + cli.ApeMaterno;
                    if (reader["sexo"].ToString().Equals("M"))
                    {
                        cli.Sexo = 1;
                    }
                    else
                    {
                        cli.Sexo = 0;
                    }
                    cli.FecNacimiento = reader["fechaNac"].ToString();
                    cli.Direccion = reader["direccion"].ToString();
                    cli.Telefono = reader["telefono"].ToString();
                    cli.Ruc = reader["RUC"].ToString();
                    cli.RazonSocial = reader["razonSocial"].ToString();
                    cli.Estado = Int32.Parse(reader["estado"].ToString());
                    cli.FecRegistro = reader["fechaReg"].ToString();
                    cli.Distrito = reader["distrito"].ToString();
                    cli.Documento = cli.Dni;
                    Tarjeta tarj = new Tarjeta();
                    tarj.CodTarjeta = reader["codTarjeta"].ToString();
                    MessageBox.Show(tarj.CodTarjeta + "");
                    tarj.FecEmision = reader["fechaEmi"].ToString();
                    tarj.FecAnulado = reader["fechaAnu"].ToString();
                    tarj.Estado = Int32.Parse(reader["estado"].ToString());
                    tarj.Cliente = cli;
                    /*if (String.IsNullOrEmpty(cli.RazonSocial))
                    {
                        cli.NombreCompleto = cli.Nombre + ' ' + cli.ApePaterno + ' ' + cli.ApeMaterno;
                        cli.Documento = cli.Dni;
                    }
                    else
                    {
                        cli.NombreCompleto = cli.RazonSocial;
                        cli.Documento = cli.Ruc;
                    }*/
                    clientes.Add(tarj);
                }

                conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return clientes;
        }

        public static int agregarCliente(Cliente a, DateTime hoy)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "INSERT INTO Cliente(DNI,nombre,apePaterno,apeMaterno,RUC,sexo,razonSocial,direccion,telefono,estado,distrito) " +
            "VALUES (@DNI,@nombre,@apePaterno,@apeMaterno,@RUC,@sexo,@razonSocial,@direccion,@telefono,@estado,@distrito)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@DNI", a.Dni);
            cmd.Parameters.AddWithValue("@nombre", a.Nombre);
            cmd.Parameters.AddWithValue("@apePaterno", a.ApePaterno);
            cmd.Parameters.AddWithValue("@apeMaterno", a.ApeMaterno);
            cmd.Parameters.AddWithValue("@RUC", a.Ruc);
            cmd.Parameters.AddWithValue("@sexo", 'M');//a.sexo
            cmd.Parameters.AddWithValue("@razonSocial", a.RazonSocial);
            cmd.Parameters.AddWithValue("@direccion", a.Direccion);
            cmd.Parameters.AddWithValue("@telefono", a.Telefono);
            cmd.Parameters.AddWithValue("@estado", a.Estado);
            cmd.Parameters.AddWithValue("@distrito", a.Distrito);
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

        public static int BuscarIDCliente(Cliente a)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            int k = 0;

            cmd.CommandText = "SELECT idCLiente FROM Cliente WHERE DNI=@DNI";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@DNI", a.Dni);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    k = Int32.Parse(reader["idCLiente"].ToString());
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

        public static Tarjeta BuscarClientePorId(int id)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            Tarjeta tarje = null;

            cmd.CommandText = "SELECT c.*,t.estado as estadotarjeta FROM Cliente c, Tarjeta t WHERE c.idCLiente=t.idCLiente and c.idCLiente=@idCLiente";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@idCLiente", id);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Cliente cli = new Cliente();
                    cli.Id = Int32.Parse(reader["idCLiente"].ToString());
                    cli.Dni = reader["DNI"].ToString();
                    cli.Nombre = reader["nombre"].ToString();
                    cli.ApePaterno = reader["apePaterno"].ToString();
                    cli.ApeMaterno = reader["apeMaterno"].ToString();
                    if (reader["sexo"].ToString().Equals("M"))
                    {
                        cli.Sexo = 1;
                    }
                    else
                    {
                        cli.Sexo = 0;
                    }
                    cli.FecNacimiento = reader["fechaNac"].ToString();
                    if (!cli.FecNacimiento.Equals("") && cli.FecNacimiento != null)
                    {
                        cli.FechaNacimiento = DateTime.Parse(reader["fechaNac"].ToString());
                    }
                    cli.Direccion = reader["direccion"].ToString();
                    cli.Telefono = reader["telefono"].ToString();
                    cli.Ruc = reader["RUC"].ToString();
                    cli.RazonSocial = reader["razonSocial"].ToString();
                    cli.Estado = Int32.Parse(reader["estado"].ToString());
                    cli.FecRegistro = reader["fechaReg"].ToString();
                    cli.Distrito = reader["distrito"].ToString();
                    cli.Documento = cli.Dni;
                    tarje = new Tarjeta();
                    tarje.Estado = Int32.Parse(reader["estadotarjeta"].ToString());
                    tarje.Cliente = cli;
                }
                else
                    conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
            return tarje;
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

        public static int editarCliente(Cliente a, int id)
        {
            //DBConexion db = new DBConexion();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;
            Trace.WriteLine("Editar Usuario 1");

            cmd.CommandText = "UPDATE Cliente SET DNI = @DNI, nombre = @nombre, apePaterno = @apePaterno, apeMaterno = @apeMaterno, RUC = @RUC, sexo = @sexo, razonSocial = @razonSocial, direccion = @direccion, telefono = @telefono, distrito=@distrito WHERE idCLiente = @idCLiente ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            //db.cmd.Parameters.AddWithValue("@codEmpleado", u.IdUsuario);
            cmd.Parameters.AddWithValue("@DNI", a.Dni);
            cmd.Parameters.AddWithValue("@nombre", a.Nombre);
            cmd.Parameters.AddWithValue("@apePaterno", a.ApePaterno);
            cmd.Parameters.AddWithValue("@apeMaterno", a.ApeMaterno);
            cmd.Parameters.AddWithValue("@RUC", a.Ruc);
            cmd.Parameters.AddWithValue("@sexo", 'M');//a.sexo
            cmd.Parameters.AddWithValue("@razonSocial", a.RazonSocial);
            cmd.Parameters.AddWithValue("@direccion", a.Direccion);
            cmd.Parameters.AddWithValue("@telefono", a.Telefono);
            cmd.Parameters.AddWithValue("@distrito", a.Distrito);
            cmd.Parameters.AddWithValue("@idCLiente", id);

            Trace.WriteLine("<" + a.Dni + ">");
            Trace.WriteLine("<" + a.Nombre + ">");
            Trace.WriteLine("<" + a.ApePaterno + ">");
            Trace.WriteLine("<" + a.ApeMaterno + ">");
            Trace.WriteLine("<" + a.Estado + ">");


            Trace.WriteLine("Editar Usuario 2");
            try
            {
                conn.Open();
                k = cmd.ExecuteNonQuery();
                conn.Close();
                Trace.WriteLine("Editar Usuario 3");

            }

            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
        }
    }
}
