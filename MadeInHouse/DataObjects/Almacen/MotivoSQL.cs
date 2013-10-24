﻿using System;
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

            cmd.CommandText = "INSERT INTO Motivo(motivo) VALUES (@motivo)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@motivo", m.motivo);

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

            cmd.CommandText = "SELECT * FROM Motivo";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Motivo m = new Motivo();
                    m.id = reader.IsDBNull(reader.GetOrdinal("idMotivo")) ? -1 : (int)reader["idMotivo"];
                    m.motivo = reader.IsDBNull(reader.GetOrdinal("motivo")) ? null : reader["motivo"].ToString();
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
    }
}