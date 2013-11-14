using MadeInHouse.Models.Compras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.DataObjects.Compras
{
    class SolicitudAdquisicionSQL:EntitySQL
    {


        public int Agregar(object entity)
        {
            throw new NotImplementedException();
        }

        public object Buscar(params object[] filters)
        {

            List<SolicitudAdquisicion> lstSolicitud = new List<SolicitudAdquisicion>();
            DBConexion db = new DBConexion();
            SqlDataReader reader;

            String where = "";


            if (filters.Length > 1 && filters.Length <= 5)
            {

                string tienda = Convert.ToString(filters[0]);
                int  estado = Convert.ToInt32(filters[1]);
                DateTime fechaIni = Convert.ToDateTime(filters[2]);
                DateTime fechaFin = Convert.ToDateTime(filters[3]);

                if (!String.IsNullOrEmpty(tienda))
                {
                    where += " and t.nombre like '%" + tienda+"%' " ;
                }

               

                    if (estado != 4)
                    {

                        where += " and s.estado = " + estado;
                    }
                    else { 
                     where += " and s.estado <> 0 ";
                    }
                

              

                if (fechaIni != null)
                {


                    where += " and CONVERT(DATE,'" + fechaIni.ToString("yyyy-MM-dd") + "')   <=  CONVERT(DATETIME,s.fechaReg,103) ";

                }

                if (fechaFin != null)
                {

                    where += " and CONVERT(DATE,'" + fechaFin.ToString("yyyy-MM-dd") + "')   >=  CONVERT(DATETIME,s.fechaReg,103) ";
                }

            }

          

            db.cmd.CommandText = "SELECT s.estado , s.idSolicitudAD ,s.fechaCierre, s.fechaReg , s.fechaAtencion , t.nombre "
            + "FROM SolicitudAdquisicion s INNER JOIN Tienda t ON s.idTienda = t.idTienda " +
            "WHERE  1=1   " + where;
            db.cmd.CommandType = CommandType.Text;
            db.cmd.Connection = db.conn;

            //MessageBox.Show(where);

            try
            {
                db.conn.Open();

                reader = db.cmd.ExecuteReader();


                while (reader.Read())
                {

                    SolicitudAdquisicion p = new SolicitudAdquisicion();
                   p.Estado =  Convert.ToInt32(reader["estado"].ToString());                   
                   p.IdSolicitudAD = Convert.ToInt32 (reader["idSolicitudAD"].ToString());
                   p.NombreTienda = reader["nombre"].ToString();
                   p.FechaReg  =reader["fechaReg"].ToString();               
                    p.FechaAtencion = reader.IsDBNull(reader.GetOrdinal("fechaAtencion")) ? "" :reader["fechaAtencion"].ToString();
                    p.FechaCierre = reader.IsDBNull(reader.GetOrdinal("fechaCierre")) ? "" : reader["fechaCierre"].ToString();
                   p.Codigo = "SOL-"+Convert.ToString(10000000 + p.IdSolicitudAD);
                   p.LstProductos = new ProductoxSolicitudAdSQL().Buscar(p.IdSolicitudAD) as List<ProductoxSolicitudAd>;
                   p.Est = getEstado(p.Estado);
                   lstSolicitud.Add(p);
                  // MessageBox.Show("id = " + p.Codigo);
                }

                db.conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return lstSolicitud;

        }

        public String getEstado(int i) {

            if (i == 1) return "NO ATENDIDA";

            if (i == 2) return "ATENDIENDO";

            if (i == 3) return "ATENDIDA";

            return "";
        }

        public int Actualizar(object entity)
        {
            int k = 0;

            SolicitudAdquisicion pp = entity as SolicitudAdquisicion;




            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;


            cmd.CommandText = "UPDATE SolicitudAdquisicion set estado = 2 , fechaAtencion = GETDATE()  " +
                                " where idSolicitudAD =  "+ pp.IdSolicitudAD;

           
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;




            try
            {
                conn.Open();


                k = cmd.ExecuteNonQuery();


                cmd.ExecuteNonQuery();

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            
            }


            return k;


        }

        public int Eliminar(object entity)
        {
            throw new NotImplementedException();
        }


        public void TerminarSolicitudes(int idAlmacen,int idSol) {

            DBConexion db = new DBConexion();



            db.cmd.CommandText = "UPDATE SolicitudAdquisicion " +
                                 "SET fechaCierre = GETDATE(),estado = 3 "+
                                 "where estado = 2  and idSolicitudAD = @idSol ";
                                
           
            db.cmd.Parameters.AddWithValue("@idSol", idSol);
            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.conn.Close();

            }

            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

           

        }
    }
}
