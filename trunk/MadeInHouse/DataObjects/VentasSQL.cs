using MadeInHouse.Models;
using MadeInHouse.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace MadeInHouse.DataObjects
{
    class VentasSQL
    {
        public static int agregarCliente(Cliente p)
        {

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "INSERT INTO Cliente(DNI,nombre,apePaterno,apeMaterno,RUC,sexo,razonSocial,direccion,telefono,estado,distrito)" +
            "VALUES (@DNI,@nombre,@apePaterno,@apeMaterno,@RUC,@sexo,@razonSocial,@direccion,@telefono,@estado,@distrito)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@DNI", p.dni);
            cmd.Parameters.AddWithValue("@nombre", p.nombre);
            cmd.Parameters.AddWithValue("@apePaterno", p.apePaterno);
            cmd.Parameters.AddWithValue("@apeMaterno", p.apeMaterno);
            cmd.Parameters.AddWithValue("@RUC", p.ruc);
            cmd.Parameters.AddWithValue("@sexo", 'M');
            cmd.Parameters.AddWithValue("@razonSocial", p.razonSocial);
            cmd.Parameters.AddWithValue("@direccion", p.direccion);
            cmd.Parameters.AddWithValue("@telefono", p.telefono);
            cmd.Parameters.AddWithValue("@estado", p.estado);
            cmd.Parameters.AddWithValue("@distrito", p.distrito);

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
