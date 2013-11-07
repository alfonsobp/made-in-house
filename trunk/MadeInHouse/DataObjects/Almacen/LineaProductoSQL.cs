using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;
using System.Data.SqlClient;
using Caliburn.Micro;

namespace MadeInHouse.DataObjects.Almacen
{
    class LineaProductoSQL
    {

        private DBConexion db = null;
        public LineaProductoSQL()
        {
            db = new DBConexion();
        }


        public BindableCollection<LineaProducto> ObtenerLineasProducto()
        {
            SqlDataReader reader;
            BindableCollection<LineaProducto> lstLineasProducto = new BindableCollection<LineaProducto>();
            db.cmd.CommandText = "SELECT * FROM LineaProducto";

            try
            {
                db.conn.Open();
                reader=db.cmd.ExecuteReader();
                while(reader.Read()) 
                {
                    LineaProducto lp= new LineaProducto();
                    lp.IdLinea=Int32.Parse(reader["idLinea"].ToString());
                    lp.Nombre=reader["Nombre"].ToString();
                    lp.Abreviatura=reader["Abreviatura"].ToString();
                    lstLineasProducto.Add(lp);   
                }
                reader.Close();
                db.conn.Close();
                

            }
            catch (SqlException e )
            {
                Console.WriteLine(e);
            }
            catch (Exception e) {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return lstLineasProducto;
        }


        public void AgregarLineaProducto(LineaProducto lp)
        {

            db.cmd.CommandText = "INSERT INTO LineaProducto (nombre,abreviatura) values (@nombre,@abreviatura)";
            db.cmd.Parameters.AddWithValue("@nombre", lp.Nombre);
            db.cmd.Parameters.AddWithValue("@abreviatura", lp.Abreviatura);

            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
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

            int idLinea= new UtilesSQL().ObtenerMaximoID("LineaProducto", "idLinea");
            SubLineaProductoSQL slpSQL = new SubLineaProductoSQL();
            foreach (SubLineaProducto slp in lp.Sublineas)
            {
                
                slpSQL.AgregarSubLineaProducto(slp,idLinea);
            }

        }


        public void ActualizarLineaProducto(LineaProducto lp)
        {
            db.cmd.CommandText = "UPDATE LineaProducto SET nombre= @nombre , abreviatura=@abreviatura WHERE idLinea=@idLinea";
            db.cmd.Parameters.AddWithValue("@idLinea", lp.IdLinea);
            db.cmd.Parameters.AddWithValue("@nombre", lp.Nombre);
            db.cmd.Parameters.AddWithValue("@abreviatura", lp.Abreviatura);
            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
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

        }



    }
}
