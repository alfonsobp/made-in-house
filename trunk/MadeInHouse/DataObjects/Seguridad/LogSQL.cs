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
        public static int RegistrarActividad(string nomVentana, string idItem, int idAccion)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;
            string dirIp;
            cmd.CommandText = "INSERT INTO Log(fechaAccion,nomVentana,idItem,idAccion,idUsuario,ip) " +
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

        //BUSCAR:
        public static List<Log> BuscarAcciones()
        {
            List<Log> lstLog = new List<Log>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT L.fechaAccion fecha, U.codEmpleado codEmp, E.nombre + ' ' + E.apePaterno + ' ' + E.apeMaterno nombEmpleado, L.nomVentana ventana, A.Descripcion accion, L.idItem codItem, L.ip ipUser FROM LOG L, ACCION A, USUARIO U, EMPLEADO E WHERE L.idAccion = A.idAccion AND U.idUsuario=L.idUsuario AND E.codEmpleado = U.codEmpleado";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Log lg = new Log();
                    lg.FechaAccion = DateTime.Parse(reader["fecha"].ToString());
                    lg.CodEmpleado = reader["codEmp"].ToString();
                    lg.NomEmpleado = reader["nombEmpleado"].ToString();
                    lg.NomVentana = reader["ventana"].ToString();
                    lg.DescAccion = reader["accion"].ToString();
                    lg.IdItem = reader["codItem"].ToString();
                    lg.Ip = reader["ipUser"].ToString();

                    lstLog.Add(lg);
                }

                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lstLog;
        }

    }



}