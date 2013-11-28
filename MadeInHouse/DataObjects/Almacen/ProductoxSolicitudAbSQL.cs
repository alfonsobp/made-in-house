using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.DataObjects.Almacen
{
    class ProductoxSolicitudAbSQL
    {
        
        
        private DBConexion db;

        public ProductoxSolicitudAbSQL(){
            db = new DBConexion();
        }


        internal List<ProductoCant> ListaProductos(string idSolicitudAB)
        {
            List<ProductoCant> productos = new List<ProductoCant>();
            ProductoSQL pGw = new ProductoSQL();
            
            string where = "WHERE idSolicitudAB=@idSolicitudAB";
            
            db.cmd.Parameters.AddWithValue("@idSolicitudAB", idSolicitudAB);

            db.cmd.CommandText = "SELECT * FROM ProductoxSolicitudAb " + where;
            
            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                    
                Producto p = new Producto();
                while (reader.Read())
                {
                    ProductoCant pa = new ProductoCant();
                    pa.IdProducto= reader.IsDBNull(reader.GetOrdinal("idProducto")) ? -1 : int.Parse(reader["idProducto"].ToString());
                    p = pGw.Buscar_por_CodigoProducto(pa.IdProducto);                    
                    pa.CodigoProd = p.CodigoProd.ToString();
                    pa.Nombre = p.Nombre;
                    pa.Can = reader.IsDBNull(reader.GetOrdinal("cantidad")) ? null : reader["cantidad"].ToString();
                    pa.CanAtend = reader.IsDBNull(reader.GetOrdinal("cantidadAtendida")) ? null : reader["cantidadAtendida"].ToString();

                    productos.Add(pa);
                
                }
                db.cmd.Parameters.Clear();
                db.conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return productos;

        }

        public void Atendida(Abastecimiento selectedSolicitud)
        {

            
            db.cmd.CommandText = "UPDATE SolicitudAbastecimiento SET estado=@estado WHERE idSolicitudAB=@idSolicitudAB ";

            db.cmd.Parameters.AddWithValue("@idSolicitudAB",selectedSolicitud.idSolicitudAB);
            db.cmd.Parameters.AddWithValue("@estado", selectedSolicitud.estado);


            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.cmd.Parameters.Clear();
                db.conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }
        }
    }

    }

