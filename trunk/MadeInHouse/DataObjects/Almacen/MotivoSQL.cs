using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MadeInHouse.Models.Almacen;
using System.Data;
using System.Windows;

namespace MadeInHouse.DataObjects.Almacen
{
    class MotivoSQL
    {

        public static int AgregarMotivo(Motivo m)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "INSERT INTO MotivoIS(nombre,tipo,estado) VALUES (@motivo,3,1)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@motivo", m.NombreMotivo);

            try
            {
                conn.Open();

                k = cmd.ExecuteNonQuery();

                conn.Close();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
        }

        public static List<Motivo> BuscarMotivos()
        {
            List<Motivo> listaMotivos = new List<Motivo>(); ;
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM MotivoIS where estado=1";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Motivo m = new Motivo();
                    m.Id = reader.IsDBNull(reader.GetOrdinal("idMotivo")) ? -1 : (int)reader["idMotivo"];
                    m.NombreMotivo = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();
                    listaMotivos.Add(m);
                }

                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return listaMotivos;
        }

        internal static List<Motivo> BuscarMotivos(int p)
        {
            List<Motivo> listaMotivos = new List<Motivo>(); ;
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM MotivoIS WHERE (tipo=@tipo OR tipo=3) AND estado=1";
            cmd.Parameters.AddWithValue("@tipo", p);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Motivo m = new Motivo();
                    m.Id = reader.IsDBNull(reader.GetOrdinal("idMotivo")) ? -1 : (int)reader["idMotivo"];
                    m.NombreMotivo = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();
                    listaMotivos.Add(m);
                }

                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return listaMotivos;
        }

        internal static int Eliminar(Motivo tz)
        {

            
            
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "UPDATE MotivoIS SET estado=0 WHERE idMotivo = @idMotivo";
            
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@idMotivo",tz.Id);

            try
            {
                conn.Open();

                k = cmd.ExecuteNonQuery();

                conn.Close();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;

        }

        internal static Motivo BuscarMotivo(string SelectedMotivo)
        {
            Motivo m = new Motivo(); 
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM MotivoIS where nombre=@nombre";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@nombre",SelectedMotivo);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    m.Id = reader.IsDBNull(reader.GetOrdinal("idMotivo")) ? -1 : (int)reader["idMotivo"];
                    m.NombreMotivo = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();
                   
                }

                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
                conn.Close();
            }

            return m;
        }
        
    }
}
