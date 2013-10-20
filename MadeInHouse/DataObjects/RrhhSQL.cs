using MadeInHouse.Models.RRHH;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.DataObjects
{
    class RrhhSQL
    {
        public static List<Empleado> BuscarEmpleado(string codigo, string dni, string nombre, string apePaterno, string tienda, string area, string puesto)
        {
            List<Empleado> lstEmpleado = new List<Empleado>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM Empleado ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    Empleado e = new Empleado();
                    e.

                    p.Email = reader["email"].ToString();
                    p.Ruc = reader["Ruc"].ToString(); lstProveedor.Add(p);
                }

                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }


            return lstProveedor;

        } 
        

    }
}
