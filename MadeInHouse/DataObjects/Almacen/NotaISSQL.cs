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
    }
}
