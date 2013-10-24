using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;
using System.Data.SqlClient;
using System.Data;
using Caliburn.Micro;

namespace MadeInHouse.DataObjects.Almacen
{
    class SubLineaProductoSQL
    {
        private DBConexion db = null;

        public SubLineaProductoSQL()
        {
            db = new DBConexion();
        }

        public BindableCollection<SubLineaProducto> ObtenerSubLineas(int id)
        {
            db.cmd.CommandText = "SELECT * FROM SubLineaProducto WHERE idLinea=@idLinea";
            db.cmd.Parameters.AddWithValue("idLinea", id);
            SqlDataReader reader;
            BindableCollection<SubLineaProducto> lstSubLinea = new BindableCollection<SubLineaProducto>();
            try
            {
                db.conn.Open();
                reader=db.cmd.ExecuteReader();
                while (reader.Read())
                {
                    SubLineaProducto slp = new SubLineaProducto();
                    slp.IdLinea = id;
                    slp.Nombre=reader["Nombre"].ToString();
                    slp.IdSubLinea = Int32.Parse(reader["IdSubLinea"].ToString());
                    slp.Abreviatura = reader["Abreviatura"].ToString();
                    lstSubLinea.Add(slp);
                }
                db.cmd.Parameters.Clear();
                db.conn.Close();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return lstSubLinea;

        }

        public void AgregarSubLineaProducto(SubLineaProducto slp,int idLinea)
        {
            
            db.cmd.CommandText = "INSERT INTO SubLineaProducto (nombre,idLinea,abreviatura) values (@nombre,@idLinea,@abreviatura)";
            db.cmd.Parameters.AddWithValue("@nombre", slp.Nombre);
            db.cmd.Parameters.AddWithValue("@idLinea", idLinea);
            db.cmd.Parameters.AddWithValue("@Abreviatura", slp.Abreviatura);

            try
            {   
                
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.conn.Close();
                db.cmd.Parameters.Clear();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }



        }




    }
}
