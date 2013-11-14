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
    class UnidadMedidaSQL
    {

        public int AgregarUnidadMedida(UnidadMedida u)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;
            
            cmd.CommandText = "INSERT INTO UnidadMedida(nombre,estado) VALUES (@nombre,1)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@nombre", u.Nombre);

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

        public List<UnidadMedida> BuscarUnidadMedida()
        {

            List<UnidadMedida> listaUnidadMedidas = new List<UnidadMedida>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM UnidadMedida WHERE estado=1";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                 conn.Open();
                 reader = cmd.ExecuteReader();

                 while (reader.Read())
                {
                    UnidadMedida u = new UnidadMedida();
                    u.IdUnidad = reader.IsDBNull(reader.GetOrdinal("idUnidad")) ? -1 : (int)reader["idUnidad"];
                    u.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();
                    listaUnidadMedidas.Add(u);
                }

                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return listaUnidadMedidas;
        }

        internal static int Eliminar(UnidadMedida tz)
        {

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "UPDATE UnidadMedida SET estado=0 WHERE idUnidad = @idUnidad";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@idUnidad", tz.IdUnidad);

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
    }
}
