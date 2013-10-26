using MadeInHouse.Models;
using MadeInHouse.Models.Compras;
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
       public static List<Proveedor> BuscarProveedor(string codigo , string razonSocial , string Ruc, string fechaIni , string fechaFin){


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
                     p.TelefonoContacto = reader["telefonoContacto"].ToString();
                     p.Email = reader["email"].ToString();
                     p.Ruc = reader["Ruc"].ToString();                     
                     lstProveedor.Add(p);
                 }

                 conn.Close();

             }
             catch (Exception e)
             {
                 MessageBox.Show(e.StackTrace.ToString());
             }


             return lstProveedor;
       
       } 


       public static int  agregarProveedor(Proveedor p)
       {

         SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
         SqlCommand cmd = new SqlCommand();
         int k = 0;

         cmd.CommandText = "INSERT INTO Proveedor(codProveedor,razonSocial,contacto,direccion,fax,telefono ,telefonoContacto,email,RUC)" +
         "VALUES (@codProveedor,@razonSocial,@contacto,@direccion,@fax,@telefono ,@telefonoContacto,@email,@ruc)";
         cmd.CommandType = CommandType.Text;
         cmd.Connection = conn;
          
         cmd.Parameters.AddWithValue("@codProveedor",p.Codigo);
         cmd.Parameters.AddWithValue("@razonSocial",p.RazonSocial);
         cmd.Parameters.AddWithValue("@contacto",p.Contacto);
         cmd.Parameters.AddWithValue("@direccion",p.Direccion);
         cmd.Parameters.AddWithValue("@fax",p.Fax);
         cmd.Parameters.AddWithValue("@telefono",p.Telefono);
         cmd.Parameters.AddWithValue("@telefonoContacto",p.TelefonoContacto);
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
        "SET razonSocial= @razonSocial,contacto= @contacto,direccion= @direccion,fax= @fax,telefono= @telefono ,telefonoContacto= @telefonoContacto,email= @email ,ruc = @ruc " +
        " WHERE codProveedor= @codProveedor ";
        cmd.CommandType = CommandType.Text;
        cmd.Connection = conn;

        cmd.Parameters.AddWithValue("@codProveedor", p.Codigo);
        cmd.Parameters.AddWithValue("@razonSocial", p.RazonSocial);
        cmd.Parameters.AddWithValue("@contacto", p.Contacto);
        cmd.Parameters.AddWithValue("@direccion", p.Direccion);
        cmd.Parameters.AddWithValue("@fax", p.Fax);
        cmd.Parameters.AddWithValue("@telefono", p.Telefono);
        cmd.Parameters.AddWithValue("@telefonoContacto", p.TelefonoContacto);
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

       public static int getIDfromProv(string proveedor)
       {
           SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
           SqlCommand cmd = new SqlCommand();
           SqlDataReader reader;
           int idProveedor = 0;

           cmd.CommandText = "SELECT idProveedor FROM Proveedor WHERE codProveedor=@codProveedor ";
           cmd.CommandType = CommandType.Text;
           cmd.Connection = conn;
           cmd.Parameters.AddWithValue("@codProveedor", proveedor);

           try
           {
               conn.Open();
               reader = cmd.ExecuteReader();

               if (reader.Read())
                   idProveedor = (int)(reader["idProveedor"]);
               else
                   MessageBox.Show("Proveedor no válido, revisar datos");

               conn.Close();

           }
           catch (Exception e)
           {
               MessageBox.Show(e.StackTrace.ToString());
           }

           return idProveedor;
       }

       public static string getCODfromProv(int proveedor)
       {
           SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
           SqlCommand cmd = new SqlCommand();
           SqlDataReader reader;
           string codProveedor = null;

           cmd.CommandText = "SELECT codProveedor FROM Proveedor WHERE idProveedor=@idProveedor ";
           cmd.CommandType = CommandType.Text;
           cmd.Connection = conn;
           cmd.Parameters.AddWithValue("@idProveedor", proveedor);

           try
           {
               conn.Open();
               reader = cmd.ExecuteReader();

               if (reader.Read())
                   codProveedor = reader["codProveedor"].ToString();
               else
                   MessageBox.Show("Proveedor no válido, revisar datos");

               conn.Close();

           }
           catch (Exception e)
           {
               MessageBox.Show(e.StackTrace.ToString());
           }

           return codProveedor;
       }

       public static List<Servicio> BuscarServicio(string proveedor, string producto)
       {

           List<Servicio> lstServicio = new List<Servicio>();
           SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
           SqlCommand cmd = new SqlCommand();
           SqlDataReader reader;

           cmd.CommandText = "SELECT * FROM Servicio ";
           cmd.CommandType = CommandType.Text;
           cmd.Connection = conn;

           try
           {
               conn.Open();
               reader = cmd.ExecuteReader();

               while (reader.Read())
               {
                   Servicio s = new Servicio();
                   s.Codigo = reader["codServicio"].ToString();
                   s.Nombre = reader["nombre"].ToString();
                   s.Proveedor = getCODfromProv((int)(reader["idProveedor"]));
                   s.Descripcion = reader["descripcion"].ToString();
        
                   lstServicio.Add(s);
               }

               conn.Close();

           }
           catch (Exception e)
           {
               MessageBox.Show(e.StackTrace.ToString());
           }

           return lstServicio;

       }

       public static int agregarServicio(Servicio s)
       {
           SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
           SqlCommand cmd = new SqlCommand();
           int k = 0, idProveedor = 0;

           cmd.CommandText = "INSERT INTO Servicio(codServicio,idProveedor,nombre,descripcion)" +
           "VALUES (@codServicio,@idProveedor,@nombre,@descripcion)";
           cmd.CommandType = CommandType.Text;
           cmd.Connection = conn;

           idProveedor = getIDfromProv(s.Proveedor);
           cmd.Parameters.AddWithValue("@codServicio", s.Codigo);
           cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
           cmd.Parameters.AddWithValue("@nombre", s.Nombre);
           cmd.Parameters.AddWithValue("@descripcion", s.Descripcion);

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

       public static int editarServicio(Servicio s)
       {
           SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
           SqlCommand cmd = new SqlCommand();
           int k = 0;

           cmd.CommandText = "UPDATE Servicio " +
                             "SET nombre= @nombre,descripcion= @descripcion " +
                             "WHERE codServicio= @codServicio ";

           cmd.CommandType = CommandType.Text;
           cmd.Connection = conn;

           cmd.Parameters.AddWithValue("@codServicio", s.Codigo);
           cmd.Parameters.AddWithValue("@nombre", s.Nombre);
           cmd.Parameters.AddWithValue("@descripcion", s.Descripcion);

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

       public static int eliminarServicio(Servicio s)
       {
           SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
           SqlCommand cmd = new SqlCommand();
           int k = 0;

           cmd.CommandText = "DELETE FROM Servicio WHERE codServicio = @codServicio";
           cmd.CommandType = CommandType.Text;
           cmd.Connection = conn;

           cmd.Parameters.AddWithValue("@codServicio", s.Codigo);

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
