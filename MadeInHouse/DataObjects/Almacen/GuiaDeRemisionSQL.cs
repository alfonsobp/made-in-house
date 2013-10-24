using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;
using Caliburn.Micro;

namespace MadeInHouse.DataObjects.Almacen
{
    class GuiaDeRemisionSQL
    {
        public static List<GuiaRemision> BuscarGuiaDeRemision(string codigo, DateTime fechaReg, string tipo)
        {

            List<GuiaRemision> lstGuiaDeRemision = new List<GuiaRemision>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM GuiaRemision ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    GuiaRemision g = new GuiaRemision();
                    g.CodGuiaRem = reader["codGuiaRem"].ToString();
                    g.DirPartida = reader["dirPartida"].ToString();
                    g.DirLlegada = reader["dirLlegada"].ToString();
                    g.Camion = reader["camion"].ToString();
                    g.FechaReg = (DateTime)(reader["fechaReg"]);
                    g.Tipo = reader["tipo"].ToString();
                    g.Observaciones = reader["observaciones"].ToString();
                   
                    lstGuiaDeRemision.Add(g);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return lstGuiaDeRemision;

        }


        public static int agregarGuiaDeRemision(GuiaRemision g)
        {

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "INSERT INTO GuiaRemision(codGuiaRem,dirPartida,dirLlegada,camion,conductor,fechaReg,tipo,observaciones) " +
            "VALUES (@codGuiaRem,@dirPartida,@dirLlegada,@camion,@conductor,@fechaReg,@tipo,@observaciones)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@codGuiaRem", g.CodGuiaRem);
            cmd.Parameters.AddWithValue("@dirPartida", g.DirPartida);
            cmd.Parameters.AddWithValue("@dirLlegada", g.DirLlegada);
            cmd.Parameters.AddWithValue("@camion", g.Camion);
            cmd.Parameters.AddWithValue("@conductor", g.Conductor);
            cmd.Parameters.AddWithValue("@fechaReg", g.FechaReg);
            cmd.Parameters.AddWithValue("@tipo", g.Tipo);
            cmd.Parameters.AddWithValue("@observaciones", g.Observaciones);

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

        public static int editarGuiaDeRemision(GuiaRemision g)
        {

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "UPDATE GuiaRemision  " +
            "SET dirPartida= @dirPartida,dirLlegada= @dirLlegada,camion= @camion,conductor= @conductor,fechaReg= @fechaReg,tipo= @tipo,observaciones= @observaciones " +
            " WHERE codGuiaRem= @codGuiaRem ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@codGuiaRem", g.CodGuiaRem);
            cmd.Parameters.AddWithValue("@dirPartida", g.DirPartida);
            cmd.Parameters.AddWithValue("@dirLlegada", g.DirLlegada);
            cmd.Parameters.AddWithValue("@camion", g.Camion);
            cmd.Parameters.AddWithValue("@conductor", g.Conductor);
            cmd.Parameters.AddWithValue("@fechaReg", g.FechaReg);
            cmd.Parameters.AddWithValue("@tipo", g.Tipo);
            cmd.Parameters.AddWithValue("@observaciones", g.Observaciones);

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
