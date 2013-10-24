using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using Caliburn.Micro;

using MadeInHouse.Models.RRHH;

namespace MadeInHouse.DataObjects.Seguridad
{
    class RolSQL
    {
        //private DBConexion db = null;
        //public RolSQL()
        //{
        //    //db = new DBConexion();
        //}

        //public BindableCollection<Rol> ListaRol()
        //{
        //    SqlDataReader reader;
        //    BindableCollection<Rol> lstRol = new BindableCollection<Rol>();
        //    db.cmd.CommandText = "SELECT * FROM Rol";

        //    try
        //    {
        //        db.conn.Open();
        //        reader = db.cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Rol r = new Rol();
        //            r.IdRol = Int32.Parse("" + reader["idRol"]);
        //            r.NombRol = reader["nombre"].ToString();
        //            r.Descripcion = reader["descripcion"].ToString();
        //            r.Estado = Int32.Parse("" + reader["estado"]);

        //            lstRol.Add(r);
        //        }
        //        db.conn.Close();

        //    }
        //    catch (SqlException e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.StackTrace.ToString());
        //    }

        //    return lstRol;
        //}


        public  BindableCollection<Rol> ListarRol()
        {

            BindableCollection<Rol> lstRol = new BindableCollection<Rol>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM Rol ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Rol r = new Rol();
                    r.IdRol = Int32.Parse("" + reader["idRol"]);
                    r.NombRol = reader["nombre"].ToString();
                    r.Descripcion = reader["descripcion"].ToString();
                    r.Estado = Int32.Parse("" + reader["estado"]);

                    Trace.Write("<<" + r.IdRol + ">>");
                    Trace.Write("<<" + r.NombRol + ">>");
                    Trace.Write("<<" + r.Descripcion + ">>");
                    Trace.Write("<<" + r.Estado + ">>");

                    lstRol.Add(r);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lstRol;

        }

    }
}