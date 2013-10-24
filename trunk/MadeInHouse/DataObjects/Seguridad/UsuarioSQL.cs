using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using MadeInHouse.ViewModels.Seguridad;
using MadeInHouse.Models.Seguridad;

namespace MadeInHouse.DataObjects.Seguridad
{
    class UsuarioSQL
    {

        //AGREGAR
        public static int agregarUsuario(Usuario u)
        {

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "INSERT INTO Usuario(codEmpleado,contrasenha,estado,idRol,fechaReg,fechaMod) VALUES (@codEmpleado,@contrasenha,@estado,@rol,getdate(),getdate())";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            Trace.WriteLine("Flag1");
            Trace.WriteLine("<" + u.CodUsuario + ">");
            Trace.WriteLine("<" + u.Contrasenha + ">");
            Trace.WriteLine("<" + u.IdRol + ">");
            Trace.WriteLine("<" + u.Estado + ">");

            cmd.Parameters.AddWithValue("@codEmpleado", u.CodUsuario);
            cmd.Parameters.AddWithValue("@contrasenha", u.Contrasenha);
            cmd.Parameters.AddWithValue("@rol", u.IdRol);
            cmd.Parameters.AddWithValue("@estado", u.Estado);
            

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
        
        public static string buscarPass(string codEmpleado)
        {
            string passEnc="";

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT contrasenha FROM Usuario WHERE codEmpleado=@codEmpleado ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@codEmpleado", codEmpleado);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                    passEnc = (reader["contrasenha"]).ToString();
                else
                    //MessageBox.Show("No se encontro contrasenha");

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return passEnc;
        }



        public static int autenticarUsuario(string codEmpleado, string password)
        {
            CifrarAES cifradoAES = new CifrarAES();

            string ContrasenhaDescifrada = cifradoAES.descifrarTextoAES(buscarPass(codEmpleado), "MadeInHouse",
                     "MadeInHouse", "MD5", 22, "1234567891234567", 128);

            Trace.WriteLine("<<<<Descifrada" + ContrasenhaDescifrada + ">>>>");
            if (String.Compare(password, ContrasenhaDescifrada)==0)

                return 1;
            else return 0;

        }


        //public static List<Usuario> BuscarUsuario(string codUsuario, string producto)
        //{

        //    List<Servicio> lstServicio = new List<Servicio>();
        //    SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
        //    SqlCommand cmd = new SqlCommand();
        //    SqlDataReader reader;

        //    cmd.CommandText = "SELECT * FROM Servicio ";
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Connection = conn;

        //    try
        //    {
        //        conn.Open();
        //        reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            Servicio s = new Servicio();
        //            s.Codigo = reader["codServicio"].ToString();
        //            s.Nombre = reader["nombre"].ToString();
        //            s.Proveedor = getCODfromProv((int)(reader["idProveedor"]));
        //            s.Descripcion = reader["descripcion"].ToString();

        //            lstServicio.Add(s);
        //        }

        //        conn.Close();

        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.StackTrace.ToString());
        //    }

        //    return lstServicio;

        //}

       

        //public static int editarServicio(Servicio s)
        //{
        //    SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
        //    SqlCommand cmd = new SqlCommand();
        //    int k = 0;

        //    cmd.CommandText = "UPDATE Servicio " +
        //                      "SET nombre= @nombre,descripcion= @descripcion " +
        //                      "WHERE codServicio= @codServicio ";

        //    cmd.CommandType = CommandType.Text;
        //    cmd.Connection = conn;

        //    cmd.Parameters.AddWithValue("@codServicio", s.Codigo);
        //    cmd.Parameters.AddWithValue("@nombre", s.Nombre);
        //    cmd.Parameters.AddWithValue("@descripcion", s.Descripcion);

        //    try
        //    {
        //        conn.Open();
        //        k = cmd.ExecuteNonQuery();
        //        conn.Close();

        //    }

        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.StackTrace.ToString());
        //    }

        //    return k;
        //}

        //public static int eliminarServicio(Servicio s)
        //{
        //    SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
        //    SqlCommand cmd = new SqlCommand();
        //    int k = 0;

        //    cmd.CommandText = "DELETE FROM Servicio WHERE codServicio = @codServicio";
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Connection = conn;

        //    cmd.Parameters.AddWithValue("@codServicio", s.Codigo);

        //    try
        //    {
        //        conn.Open();
        //        k = cmd.ExecuteNonQuery();
        //        conn.Close();

        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.StackTrace.ToString());
        //    }

        //    return k;
        //}

    }
}
