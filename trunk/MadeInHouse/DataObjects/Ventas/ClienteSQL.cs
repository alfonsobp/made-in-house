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
        public static List<Tarjeta> BuscarClientes(string dni , string nombre , int tipoCliente , string registroDesde , string registroHasta )
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

            if (!String.IsNullOrEmpty(nombre))
            {
                if (where.Equals("")) where += " WHERE ";
                else where += " AND ";
                if (tipoCliente == 0)
                {
                    where = where + " ( (nombre + ' ' + apePaterno + ' ' + apeMaterno) like @nombre OR (apePaterno + ' ' + apeMaterno + ', ' + nombre) like @nombre ) ";
                    cmd.Parameters.Add(new SqlParameter("nombre", '%' + nombre + '%'));
                }
                else if (tipoCliente == 1)
                {
                    where = where + " ( razonSocial like @nombre ) ";
                    cmd.Parameters.Add(new SqlParameter("nombre", '%' + nombre + '%'));
                }
                else
                {
                    where = where + " ( (nombre + ' ' + apePaterno + ' ' + apeMaterno) like @nombre OR (apePaterno + ' ' + apeMaterno + ', ' + nombre) like @nombre OR  razonSocial like @nombre  ) ";
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

            cmd.CommandText = "SELECT c.*, t.* FROM Tarjeta t LEFT JOIN Cliente c ON c.idCLiente = t.idCLiente AND c.estado = 1 " + where + " ORDER by c.idCLiente";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                clientes = new List<Tarjeta>();
                while (reader.Read())
                {
                    if (!String.IsNullOrEmpty(reader["idCLiente"].ToString()))
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
                        cli.Direccion = reader["direccion"].ToString();
                        cli.Telefono = reader["telefono"].ToString();
                        cli.Ruc = reader["RUC"].ToString();
                        cli.RazonSocial = reader["razonSocial"].ToString();
                        cli.Estado = Int32.Parse(reader["estado"].ToString());
                        cli.FecRegistro = reader["fechaReg"].ToString();
                        cli.Distrito = reader["distrito"].ToString();
                        if (!String.IsNullOrEmpty(reader["tipoCliente"].ToString()))
                        {
                            cli.TipoCliente = Int32.Parse(reader["tipoCliente"].ToString());
                        }
                        cli.Email = reader["email"].ToString();
                        cli.Celular = reader["celular"].ToString();
                        if (String.IsNullOrEmpty(cli.RazonSocial))
                        {
                            cli.NombreCompleto = cli.Nombre + ' ' + cli.ApePaterno + ' ' + cli.ApeMaterno;
                            cli.Documento = cli.Dni;
                        }
                        else
                        {
                            cli.NombreCompleto = cli.RazonSocial;
                            cli.Documento = cli.Ruc;
                        }
                        Tarjeta tarj = new Tarjeta();
                        tarj.CodTarjeta = reader["codTarjeta"].ToString();
                        //MessageBox.Show(tarj.CodTarjeta + "");
                        tarj.FecEmision = reader["fechaEmi"].ToString();
                        tarj.FecAnulado = reader["fechaAnu"].ToString();
                        tarj.Estado = Int32.Parse(reader["estado"].ToString());
                        tarj.Cliente = cli;
                        if ((tipoCliente == cli.TipoCliente) && (tipoCliente == 0))
                        {
                            clientes.Add(tarj);
                        }
                        else if ((tipoCliente == 1) && (tipoCliente == cli.TipoCliente))
                        {
                            clientes.Add(tarj);
                        }
                        else if (tipoCliente == -1)
                        {
                            clientes.Add(tarj);
                        }
                    }
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

        public static int agregarCliente(Cliente a, DateTime date)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "INSERT INTO Cliente(DNI,nombre,apePaterno,apeMaterno,RUC,sexo,razonSocial,direccion,telefono,estado,distrito,tipoCliente,email,celular) " +
            "VALUES (@DNI,@nombre,@apePaterno,@apeMaterno,@RUC,@sexo,@razonSocial,@direccion,@telefono,@estado,@distrito,@tipoCliente,@email,@celular)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@DNI", a.Dni);
            cmd.Parameters.AddWithValue("@nombre", a.Nombre);
            cmd.Parameters.AddWithValue("@apePaterno", a.ApePaterno);
            cmd.Parameters.AddWithValue("@apeMaterno", a.ApeMaterno);
            cmd.Parameters.AddWithValue("@RUC", a.Ruc);
            if (a.Sexo == 0)
            {
                cmd.Parameters.AddWithValue("@sexo", 'M');
            }
            else
            {
                cmd.Parameters.AddWithValue("@sexo", 'F');
            }
            cmd.Parameters.AddWithValue("@razonSocial", a.RazonSocial);
            cmd.Parameters.AddWithValue("@direccion", a.Direccion);
            cmd.Parameters.AddWithValue("@telefono", a.Telefono);
            cmd.Parameters.AddWithValue("@estado", a.Estado);
            //cmd.Parameters.AddWithValue("@fechaReg", date);
            cmd.Parameters.AddWithValue("@distrito", a.Distrito);
            //cmd.Parameters.AddWithValue("@fechaNac", a.FechaNacimiento);
            cmd.Parameters.AddWithValue("@tipoCliente", a.TipoCliente);
            cmd.Parameters.AddWithValue("@email", a.Email);
            cmd.Parameters.AddWithValue("@celular", a.Celular);

            try
            {
                conn.Open();
                k = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
            return k;
        }

        public static int BuscarIDCliente(Cliente a)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            int k = 0;

            string where = " WHERE ";
            if (!String.IsNullOrEmpty(a.Dni))
            {
                where = where + "DNI=@DNI";
            }
            else if (!String.IsNullOrEmpty(a.Ruc))
            {
                where = where + "RUC=@RUC";
            }
            cmd.CommandText = "SELECT idCLiente FROM Cliente "+ where;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            if (!String.IsNullOrEmpty(a.Dni))
            {
                cmd.Parameters.AddWithValue("@DNI", a.Dni);
            }
            else if (!String.IsNullOrEmpty(a.Ruc))
            {
                cmd.Parameters.AddWithValue("@RUC", a.Ruc);
            }

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
                    if (!String.IsNullOrEmpty(reader["tipoCliente"].ToString()))
                    {
                        cli.TipoCliente = Int32.Parse(reader["tipoCliente"].ToString());
                    }
                    cli.Email = reader["email"].ToString();
                    cli.Celular = reader["celular"].ToString();
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

            cmd.CommandText = "UPDATE Cliente SET DNI = @DNI, nombre = @nombre, apePaterno = @apePaterno, apeMaterno = @apeMaterno, RUC = @RUC, sexo = @sexo, razonSocial = @razonSocial, direccion = @direccion, telefono = @telefono, distrito=@distrito, email=@email, celular=@celular, tipoCliente=@tipoCliente WHERE idCLiente = @idCLiente ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            //db.cmd.Parameters.AddWithValue("@codEmpleado", u.IdUsuario);
            cmd.Parameters.AddWithValue("@DNI", a.Dni);
            cmd.Parameters.AddWithValue("@nombre", a.Nombre);
            cmd.Parameters.AddWithValue("@apePaterno", a.ApePaterno);
            cmd.Parameters.AddWithValue("@apeMaterno", a.ApeMaterno);
            cmd.Parameters.AddWithValue("@RUC", a.Ruc);
            if (a.Sexo == 0)
            {
                cmd.Parameters.AddWithValue("@sexo", 'M');
            }
            else if (a.Sexo == 1)
            {
                cmd.Parameters.AddWithValue("@sexo", 'F');
            }
            cmd.Parameters.AddWithValue("@razonSocial", a.RazonSocial);
            cmd.Parameters.AddWithValue("@direccion", a.Direccion);
            cmd.Parameters.AddWithValue("@telefono", a.Telefono);
            cmd.Parameters.AddWithValue("@distrito", a.Distrito);
            cmd.Parameters.AddWithValue("@email", a.Email);
            cmd.Parameters.AddWithValue("@celular", a.Celular);
            if (String.IsNullOrEmpty(a.RazonSocial))
            {
                a.TipoCliente = 0;
            }
            else
            {
                a.TipoCliente = 1;
            }
            cmd.Parameters.AddWithValue("@tipoCliente", a.TipoCliente);

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

        public Cliente BuscarClienteByTarjeta(string cod)
        {
            DBConexion db = new DBConexion();
            Cliente c = new Cliente();

            db.cmd.CommandText = "SELECT idCLiente FROM Tarjeta WHERE codTarjeta=" + cod;

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                while (reader.Read())
                {
                    c = BuscarClienteByIdCliente(Convert.ToInt32(reader["idCliente"].ToString()));
                }
                db.cmd.Parameters.Clear();
                db.conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
            return c;
        }

        public Cliente BuscarClienteByIdCliente(int id)
        {
            DBConexion db = new DBConexion();
            Cliente cli = new Cliente();

            db.cmd.CommandText = "SELECT * FROM Cliente WHERE idCliente=" + id;

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                while (reader.Read())
                {
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
                }
                db.cmd.Parameters.Clear();
                db.conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
            return cli;
        }

        public static int EliminarCliente(int id)
        {
            //DBConexion db = new DBConexion();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "UPDATE Cliente SET estado = @estado WHERE idCLiente = @idCLiente ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            //db.cmd.Parameters.AddWithValue("@codEmpleado", u.IdUsuario);
            cmd.Parameters.AddWithValue("@estado", 0);
            
            cmd.Parameters.AddWithValue("@idCLiente", id);

            try
            {
                conn.Open();
                k = cmd.ExecuteNonQuery();
                conn.Close();
                int j=AnularTarjeta(id);

            }

            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
        }

        public static int AnularTarjeta(int id)
        {
            //DBConexion db = new DBConexion();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "UPDATE Tarjeta SET estado = @estado WHERE idCLiente = @idCLiente ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            //db.cmd.Parameters.AddWithValue("@codEmpleado", u.IdUsuario);
            cmd.Parameters.AddWithValue("@estado", 0);

            cmd.Parameters.AddWithValue("@idCLiente", id);

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
        

