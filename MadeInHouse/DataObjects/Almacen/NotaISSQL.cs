using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.DataObjects.Almacen
{
    class NotaISSQL
    {

        private DBConexion db;
        private bool tipo = true;

        public NotaISSQL(DBConexion db = null)
        {
            if (db == null)
            {
                this.db = new DBConexion();
            }
            else
            {
                this.db = db;
                tipo = false;
            }
        }

        public int AgregarNota(NotaIS p)
        {
            int retorno=-1;
            db.cmd.CommandText = "INSERT INTO NotaIS(tipo,fechaReg,observaciones,responsable,idDoc,idMotivo,idAlmacen) " +
            "VALUES (@tipo,GETDATE(),@observaciones,@responsable,@idDoc,@idMotivo,@idAlmacen); SELECT CAST(scope_identity() AS int)";
            db.cmd.Parameters.AddWithValue("@tipo", p.Tipo);
            db.cmd.Parameters.AddWithValue("@observaciones", p.Observaciones);
            db.cmd.Parameters.AddWithValue("@responsable", p.IdResponsable);
            db.cmd.Parameters.AddWithValue("@idDoc", p.IdDoc);
            db.cmd.Parameters.AddWithValue("@idMotivo", p.IdMotivo);
            db.cmd.Parameters.AddWithValue("@idAlmacen", p.IdAlmacen);
           
            try
            {
                db.conn.Open();
                retorno = (Int32)db.cmd.ExecuteScalar();
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

            //Agregamos en ProductoxNotaIS
            db.cmd.CommandText = "INSERT INTO ProductoxNotaIS(idProducto,idNota,idAlmacen,cantidad,idUbicacion)"+
            "VALUES (@idProducto,@idNota,@idAlmacen,@cantidad,@idUbicacion)";
            try
            {
                db.conn.Open();
                for (int i = 0; i < p.LstProducto.Count; i++)
                {

                    for (int j = 0; j < p.LstProducto.ElementAt(i).Ubicaciones.Count; j++)
                    {
                        db.cmd.Parameters.AddWithValue("@idProducto", p.LstProducto.ElementAt(i).IdProducto);
                        db.cmd.Parameters.AddWithValue("@idNota", retorno);
                        db.cmd.Parameters.AddWithValue("@idAlmacen", p.IdAlmacen);
                        db.cmd.Parameters.AddWithValue("@cantidad", p.LstProducto.ElementAt(i).Ubicaciones.ElementAt(j).Cantidad);
                        db.cmd.Parameters.AddWithValue("@idUbicacion", p.LstProducto.ElementAt(i).Ubicaciones.ElementAt(j).IdUbicacion);
                        db.cmd.ExecuteNonQuery();
                        db.cmd.Parameters.Clear();

                    }

                }
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
            

            return retorno;
        }

        public List<NotaIS> BuscarNotas()
        {
            
        List<NotaIS> lstNotaIs = new List<NotaIS>();


            db.cmd.CommandText = "SELECT * FROM NotaIS";

            try
            {
                if (tipo)  db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    NotaIS nota = new NotaIS();
                    nota.FechaReg = reader.IsDBNull(reader.GetOrdinal("fechaReg")) ? DateTime.MinValue : DateTime.Parse(reader["fechaReg"].ToString());
                    nota.IdAlmacen = reader.IsDBNull(reader.GetOrdinal("idAlmacen")) ? -1 : int.Parse(reader["idAlmacen"].ToString());
                    nota.IdDoc = reader.IsDBNull(reader.GetOrdinal("idDoc")) ? -1 : int.Parse(reader["idDoc"].ToString());
                    nota.IdMotivo = reader.IsDBNull(reader.GetOrdinal("idMotivo")) ? -1 : int.Parse(reader["idMotivo"].ToString());
                    nota.IdNota = reader.IsDBNull(reader.GetOrdinal("idNota")) ? -1 : int.Parse(reader["idNota"].ToString());
                    nota.IdResponsable = reader.IsDBNull(reader.GetOrdinal("responsable")) ? -1 : int.Parse(reader["responsable"].ToString());
                    nota.Observaciones = reader.IsDBNull(reader.GetOrdinal("observaciones")) ? "" : reader["observaciones"].ToString();
                    nota.Tipo = reader.IsDBNull(reader.GetOrdinal("tipo")) ? -1 : int.Parse(reader["tipo"].ToString());
                    nota.LstProducto = BuscarNotas(nota.IdNota);
                    lstNotaIs.Add(nota);

                }
                db.cmd.Parameters.Clear();
                if (tipo)  db.conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

        return lstNotaIs;

        }

        public List<ProductoCant> BuscarNotas(int p)
        {
            DBConexion db1 = new DBConexion();
            List<ProductoCant> lstProCant = new List<ProductoCant>();

            db1.cmd.CommandText = "SELECT idProducto,idNota,sum(cantidad) AS cantidad ,idAlmacen FROM ProductoxNotaIS WHERE idNota=@idNota GROUP BY idProducto, idNota, idAlmacen";
            db1.cmd.Parameters.AddWithValue("@idNota", p);
                        
            try
            {
                db1.conn.Open();
                SqlDataReader reader = db1.cmd.ExecuteReader();

                while (reader.Read())
                {
                    ProductoSQL psql = new ProductoSQL();
                    ProductoCant procan = new ProductoCant();
                    procan.CanAtender = reader.IsDBNull(reader.GetOrdinal("cantidad")) ? null : reader["cantidad"].ToString();
                    procan.IdProducto = reader.IsDBNull(reader.GetOrdinal("idProducto")) ? -1 : int.Parse(reader["idProducto"].ToString());
                    Producto pr = psql.Buscar_por_CodigoProducto(procan.IdProducto);
                    procan.Nombre = pr.Nombre;
                    procan.CodigoProd = pr.CodigoProd;
                    lstProCant.Add(procan);

                }
                db1.cmd.Parameters.Clear();
                db1.conn.Close();
                
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return lstProCant;

        
        }

        public List<ProductoxNotaIS> BuscarNotasXProductos()
        {
            List<ProductoxNotaIS> lista = new List<ProductoxNotaIS>();

            db.cmd.CommandText = "SELECT * FROM ProductoxNotaIS ";

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    ProductoxNotaIS p = new ProductoxNotaIS();
                    p.IdProducto = Convert.ToInt32(reader["idProducto"].ToString());
                    p.IdNota = Convert.ToInt32(reader["idNota"].ToString());
                    p.IdAlmacen = Convert.ToInt32(reader["idAlmacen"].ToString());
                    p.IdUbicacion = Convert.ToInt32(reader["idUbicacion"].ToString());
                    p.Cantidad = Convert.ToInt32(reader["cantidad"].ToString());

                    lista.Add(p);
                }

                db.cmd.Parameters.Clear();
                if (tipo) db.conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return lista;
        }
    }
}
