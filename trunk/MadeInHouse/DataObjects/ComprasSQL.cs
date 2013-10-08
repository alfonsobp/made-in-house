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

           //'
           //CODPROVEEDOR , RUC , RAZONSOCIAL , FAX  , TELEFONO , DIRECCION , ESTADO ( ENTERO )  , OBSERVACIONES 


             List<Proveedor> lstProveedor = new List<Proveedor>();
             SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
             SqlCommand cmd = new SqlCommand();
             SqlDataReader reader;

             cmd.CommandText = "SELECT * FROM Proveedor  ";
             cmd.CommandType = CommandType.Text;
             cmd.Connection = conn;

             try
             {
                 conn.Open();

                 reader = cmd.ExecuteReader();


                 while (reader.NextResult())
                 {

                     Proveedor p = new Proveedor();
                     p.Codigo =reader["CODPROVEEDOR"].ToString() ;
                     p.Contacto = reader["CONTACTO"].ToString();
                     p.Direccion = reader["DIRECCION"].ToString();
                     p.Fax = reader["FAX"].ToString();
                     p.Telefono = reader["TELEFONO"].ToString();
                     p.TelefonoContacto = reader["TELEFCONTACTO"].ToString();
                     p.Email = reader["EMAIL"].ToString();


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


     public static int  agregarProveedor(Proveedor p ){


         return 1;
     }

    public static   int editarProveedor(Proveedor p){
    
    return 1;
    }

    public static int eliminarProveedor(Proveedor p) {

        return 1;
    
    }



    }

}
