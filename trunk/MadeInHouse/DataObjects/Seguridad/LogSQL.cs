using System;
using MadeInHouse.Models.Seguridad;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Threading;
using System.Net;

namespace MadeInHouse.DataObjects.Seguridad
{
    class LogSQL
    {
        //INSERTAR:
        /*
                public static int RegistrarActividad(Log lg)
                {
                    SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
                    SqlCommand cmd = new SqlCommand();
                    int k = 0;

                    cmd.CommandText = "INSERT INTO Log(fechaAccion,nomVentana,idItem,idAccion,idUsuario) " +
                    "VALUES (getdate(),@nomVentana,@idItem,@idAccion,@idUsuario)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@nomVentana", lg.NomVentana);
                    cmd.Parameters.AddWithValue("@idItem", lg.IdItem);
                    cmd.Parameters.AddWithValue("@idAccion", lg.IdAccion);
                    //cmd.Parameters.AddWithValue("@idUsuario", lg.Usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@idUsuario", lg.IdUsuario);

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

        */
        //INSERTAR:
        public static int RegistrarActividad(string nomVentana, string idItem, int idAccion)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;
            string dirIp;

            cmd.CommandText = "INSERT INTO Log(fechaAccion,nomVentana,idItem,idAccion,idUsuario,dirIp) " +
            "VALUES (getdate(),@nomVentana,@idItem,@idAccion,@idUsuario,@dirIp)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@nomVentana", nomVentana);
            cmd.Parameters.AddWithValue("@idItem", idItem);
            cmd.Parameters.AddWithValue("@idAccion", idAccion);
            cmd.Parameters.AddWithValue("@idUsuario", Int32.Parse(Thread.CurrentPrincipal.Identity.Name));

            IPAddress[] a = Dns.GetHostByName(Dns.GetHostName()).AddressList;
            dirIp = a[0].ToString();

            cmd.Parameters.AddWithValue("@dirIp", dirIp);

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