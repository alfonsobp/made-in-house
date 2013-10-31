using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.DataObjects.Almacen
{
    class TipoZonaSQL
    {

        private DBConexion db;

        public TipoZonaSQL(){
            db = new DBConexion();
        }

        public ObservableCollection<TipoZona> BuscarZona(int codigo = -1, string descripcion = null)
        {
            ObservableCollection<TipoZona> listaTipoZona = new ObservableCollection<TipoZona>();
            
            string where = "WHERE 1=1 ";

            if (codigo != -1)
            {
                where = where + " AND idTipoZona = @idTipoZona ";
                db.cmd.Parameters.Add(new SqlParameter("idTipoZona", codigo));
            }

            if (!string.IsNullOrEmpty(descripcion))
            {
                where = where + " AND nombre like @descripcion ";
                db.cmd.Parameters.Add(new SqlParameter("descripcion", '%'+descripcion+'%'));
            }

            db.cmd.CommandText = "SELECT * FROM TipoZona " + where;
            Console.WriteLine(db.cmd.CommandText.ToString());

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    TipoZona p = new TipoZona();
                    p.Color = reader.IsDBNull(reader.GetOrdinal("color")) ? null : reader["color"].ToString();
                    p.IdTipoZona = reader.IsDBNull(reader.GetOrdinal("idTipoZona")) ? -1 : int.Parse(reader["idTipoZona"].ToString());  
                    p.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();
                    p.IdColor = reader.IsDBNull(reader.GetOrdinal("idColor")) ? -1 : int.Parse(reader["idColor"].ToString());
                    listaTipoZona.Add(p);
                }

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

            return listaTipoZona;
        }

        public void agregarTipoZona(TipoZona p)
        {
            
            db.cmd.CommandText = "INSERT INTO TipoZona(nombre,color,idColor)" +
            "VALUES (@nombre,@color,@idColor)";
            
            db.cmd.Parameters.Add(new SqlParameter("nombre", p.Nombre));
            db.cmd.Parameters.Add(new SqlParameter("color", p.Color));
            db.cmd.Parameters.Add(new SqlParameter("idColor", p.IdColor));

            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.cmd.Parameters.Clear();
                db.conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }
        }

        public int modificarTipoZona(TipoZona p)
        {

            db.cmd.CommandText = "UPDATE TipoZona " +
            " SET nombre=@nombre, idColor=@idColor" +
            " WHERE idTipoZona=@idTipoZona";
            int k = 0;

            db.cmd.Parameters.Add(new SqlParameter("idTipoZona", p.IdTipoZona));
            db.cmd.Parameters.Add(new SqlParameter("nombre", p.Nombre));
            db.cmd.Parameters.Add(new SqlParameter("idColor", p.Color));
            try
            {
                db.conn.Open();

                k = db.cmd.ExecuteNonQuery();

                db.conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return k;
        }

        public int eliminarProveedor(TipoZona p)
        {
            
            int k = 0;
            db.cmd.CommandText = "UPDATE TipoZona SET estado=0 WHERE idTipoZona = @idTipoZona";
            db.cmd.Parameters.Add(new SqlParameter("idTipoZona", p.IdTipoZona));
            try
            {
                db.conn.Open();

                k = db.cmd.ExecuteNonQuery();

                db.conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return k;

        }

    }
}
