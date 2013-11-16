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

        public void AgregarNota(NotaIS p)
        {
            db.cmd.CommandText = "INSERT INTO NotaIS(tipo,fechaReg,observaciones,responsable,idDoc,idMotivo,idAlmacen) " +
            "VALUES (@tipo,GETDATE(),@observaciones,@responsable,@idDoc,@idMotivo,@idAlmacen)";
            db.cmd.Parameters.AddWithValue("@tipo", p.Tipo);
            db.cmd.Parameters.AddWithValue("@observaciones", p.Observaciones);
            db.cmd.Parameters.AddWithValue("@responsable", p.IdResponsable);
            db.cmd.Parameters.AddWithValue("@idDoc", p.IdDoc);
            db.cmd.Parameters.AddWithValue("@idMotivo", p.IdMotivo);
            db.cmd.Parameters.AddWithValue("@idAlmacen", p.IdAlmacen);
            
            try
            {
                db.conn.Open();


                db.cmd.ExecuteNonQuery();
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
        }
    }
}
