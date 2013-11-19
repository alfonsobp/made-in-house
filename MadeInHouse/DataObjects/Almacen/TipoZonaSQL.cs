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
            
            string where = "WHERE 1=1 AND estado=1";

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

                reader.Close();
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
            
            db.cmd.CommandText = "INSERT INTO TipoZona(nombre,color,idColor,estado)" +
            "VALUES (@nombre,@color,@idColor,1)";
            
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
            " SET nombre=@nombre, idColor=@idColor, color=@color" +
            " WHERE idTipoZona=@idTipoZona";
            int k = 0;

            db.cmd.Parameters.Add(new SqlParameter("idTipoZona", p.IdTipoZona));
            db.cmd.Parameters.Add(new SqlParameter("nombre", p.Nombre));
            db.cmd.Parameters.Add(new SqlParameter("idColor", p.IdColor));
            db.cmd.Parameters.Add(new SqlParameter("color", p.Color));

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

        public int eliminarTipoZona(TipoZona p)
        {
            
            int k = 0;
            db.cmd.CommandText = "UPDATE TipoZona SET estado=0 WHERE idTipoZona = @idTipoZona";
            db.cmd.Parameters.Add(new SqlParameter("idTipoZona", p.IdTipoZona));
            db.conn.Open();
            try
            {
                
                k = db.cmd.ExecuteNonQuery();

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }
            finally {
                db.conn.Close();
            }

            return k;

        }


        public List<TipoZona> ObtenerZonasxAlmacen(int idAlmacen,int tipo=-1)
        {
            List<TipoZona> lstZonas=new List<TipoZona>();;
            db.cmd.CommandText = "SELECT A.*,B.nombre nombreZona,C.codHex color,C.codHex FROM ZonaxAlmacen A " +
                                "JOIN TipoZona B ON (A.idTipoZona=B.idTipoZona) " +
                                " JOIN Color C ON (B.idColor=C.idColor) " +
                                " WHERE A.idAlmacen=@idAlmacen";
            db.cmd.Parameters.AddWithValue("@idAlmacen", idAlmacen);



            try
            {

                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    TipoZona tz = new TipoZona();                    
                    tz.IdTipoZona = int.Parse(reader["idTipoZona"].ToString());
                    tz.Nombre = reader["nombreZona"].ToString();
                    tz.Color = reader["color"].ToString();
                    tz.NroBloquesDisp = reader.IsDBNull(reader.GetOrdinal("nroBloquesDisp"))? -1:int.Parse(reader["nroBloquesDisp"].ToString());
                    tz.NroBloquesTotal= reader.IsDBNull(reader.GetOrdinal("nroBloquesTotal"))? -1:int.Parse(reader["nroBloquesTotal"].ToString());
                    lstZonas.Add(tz);

                }

                db.conn.Close();
                reader.Close();
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


            UbicacionSQL uSQL =  new UbicacionSQL();
            for (int i = 0; i < lstZonas.Count; i++)
            {
                lstZonas[i].LstUbicaciones = uSQL.ObtenerUbicaciones(idAlmacen, lstZonas[i].IdTipoZona);
            }

            SectorSQL sSQL = new SectorSQL();
            if (tipo == 2)
            {
                for (int i = 0; i < lstZonas.Count; i++)
                {
                    lstZonas[i].LstSectores = sSQL.ObtenerSectores(idAlmacen, lstZonas[i].IdTipoZona);
                    if (lstZonas[i].LstSectores == null)
                    {
                        lstZonas[i].LstSectores = new List<Sector>();
                    }
                }
                
            }

            return lstZonas;

        }

    }
}
