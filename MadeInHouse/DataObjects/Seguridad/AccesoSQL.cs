using System;
using MadeInHouse.Models.Seguridad;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;


namespace MadeInHouse.DataObjects.Seguridad
{
    class AccesoSQL
    {
        public static int NUM_MODULOS = 7;
        public static int NUM_VENTANAS = 100; //Máximo de ventas externas por módulo

        public static void cargarAccVentana(int idRol, out int[] ventana)
        {
            ventana = new int[NUM_VENTANAS];

            //inicializando en 0's
            for (int i = 0; i < NUM_VENTANAS; i++)
                ventana[i] = 0;

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = " SELECT idAccVentana FROM RolxAccVentana " +
                              " WHERE idRol = @idRol ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@idRol", idRol);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ventana[Convert.ToInt32(reader["idAccVentana"].ToString())] = 1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
        }


        public static void buscarModulos(int idRol, out int[] accModulo)
        {
            accModulo = new int[NUM_MODULOS];
            for (int i = 0; i < NUM_MODULOS; i++)
                accModulo[i] = 0;

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = " SELECT idAccModulo FROM RolxAccModulo " +
                              " WHERE idRol = @idRol ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@idRol", idRol);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //accModulo[k] = Convert.ToInt32(reader["idRol"].ToString());
                    accModulo[Convert.ToInt32(reader["idAccModulo"].ToString())] = 1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
        }


        public static void buscarVentanas(int[] idAccModulo, out int[,] accVentana)
        {
            accVentana = new int[NUM_MODULOS, NUM_VENTANAS];

            for (int i = 0; i < NUM_MODULOS; i++)
                for (int j = 0; j < NUM_VENTANAS; j++)
                    accVentana[i, j] = 0;

            for (int m = 0; m < NUM_MODULOS; m++)
                if (idAccModulo[m] == 1)
                {
                    SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;

                    cmd.CommandText = " SELECT idAccVentana FROM AccVentana " +
                                      " WHERE idAccModulo = @idAccModulo ";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@idAccModulo", m);

                    try
                    {
                        conn.Open();
                        reader = cmd.ExecuteReader();
                        int k = 0;

                        while (reader.Read())
                        {
                            accVentana[m, k] = Convert.ToInt32(reader["idAccVentana"].ToString());
                            k++;
                        }
                        conn.Close();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.StackTrace.ToString());
                    }
                }//Fin del If
        }
    }
}
