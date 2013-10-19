using MadeInHouse.Models;
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
    class ComprasSQL
    {
       public static List<Proveedor>  BuscarProveedor(string codigo , string razonSocial , string Ruc, string fechaIni , string fechaFin){

          

             List<Proveedor> lstProveedor = new List<Proveedor>();
             SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
             SqlCommand cmd = new SqlCommand();
             SqlDataReader reader;

             cmd.CommandText = "SELECT * FROM Proveedor ";
             cmd.CommandType = CommandType.Text;
             cmd.Connection = conn;

             try
             {
                 conn.Open();

                 reader = cmd.ExecuteReader();


                 while (reader.Read())
                 {

                     Proveedor p = new Proveedor();
                     p.Codigo =reader["codProveedor"].ToString() ;
                     p.RazonSocial = reader["razonSocial"].ToString();
                     p.Contacto = reader["contacto"].ToString();
                     p.Direccion = reader["direccion"].ToString();
                     p.Fax = reader["fax"].ToString();
                     p.Telefono = reader["telefono"].ToString();
                     p.TelefonoContacto = reader["telefContacto"].ToString();
                     p.Email = reader["email"].ToString();
                     p.Ruc = reader["Ruc"].ToString();                     lstProveedor.Add(p);
                 }

                 conn.Close();

             }
             catch (Exception e)
             {
                 MessageBox.Show(e.StackTrace.ToString());
             }


             return lstProveedor;
       
       } 


       public static int  agregarProveedor(Proveedor p )
       {

         SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
         SqlCommand cmd = new SqlCommand();
         int k = 0;

         cmd.CommandText = "INSERT INTO Proveedor(codProveedor,razonSocial,contacto,direccion,fax,telefono ,telefContacto,email,ruc)" +
         "VALUES (@codProveedor,@razonSocial,@contacto,@direccion,@fax,@telefono ,@telefContacto,@email,@ruc)";
         cmd.CommandType = CommandType.Text;
         cmd.Connection = conn;
          
         cmd.Parameters.AddWithValue("@codProveedor",p.Codigo);
         cmd.Parameters.AddWithValue("@razonSocial",p.RazonSocial);
         cmd.Parameters.AddWithValue("@contacto",p.Contacto);
         cmd.Parameters.AddWithValue("@direccion",p.Direccion);
         cmd.Parameters.AddWithValue("@fax",p.Fax);
         cmd.Parameters.AddWithValue("@telefono",p.Telefono);
         cmd.Parameters.AddWithValue("@telefContacto",p.TelefonoContacto);
         cmd.Parameters.AddWithValue("@email",p.Email);
         cmd.Parameters.AddWithValue("@ruc", p.Ruc);

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

       public static   int editarProveedor(Proveedor p)
       {

        SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
        SqlCommand cmd = new SqlCommand();
        int k = 0;

        cmd.CommandText = "UPDATE Proveedor  "+
        "SET razonSocial= @razonSocial,contacto= @contacto,direccion= @direccion,fax= @fax,telefono= @telefono ,telefContacto= @telefContacto,email= @email ,ruc = @ruc " +
        " WHERE codProveedor= @codProveedor ";
        cmd.CommandType = CommandType.Text;
        cmd.Connection = conn;

        cmd.Parameters.AddWithValue("@codProveedor", p.Codigo);
        cmd.Parameters.AddWithValue("@razonSocial", p.RazonSocial);
        cmd.Parameters.AddWithValue("@contacto", p.Contacto);
        cmd.Parameters.AddWithValue("@direccion", p.Direccion);
        cmd.Parameters.AddWithValue("@fax", p.Fax);
        cmd.Parameters.AddWithValue("@telefono", p.Telefono);
        cmd.Parameters.AddWithValue("@telefContacto", p.TelefonoContacto);
        cmd.Parameters.AddWithValue("@email", p.Email);
        cmd.Parameters.AddWithValue("@ruc", p.Ruc);

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

       public static int eliminarProveedor(Proveedor p) 
       {
           SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
           SqlCommand cmd = new SqlCommand();
           int k = 0;

           cmd.CommandText = "DELETE FROM Proveedor WHERE codProveedor = @codProveedor";
           cmd.CommandType = CommandType.Text;
           cmd.Connection = conn;

           cmd.Parameters.AddWithValue("@codProveedor", p.Codigo);

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
