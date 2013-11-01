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

                string idAlmacen = Convert.ToString(filters[0]);
                string estado = Convert.ToString(filters[1]);
                DateTime fechaIni = Convert.ToDateTime(filters[2]);
                DateTime fechaFin = Convert.ToDateTime(filters[3]);

                if (idAlmacen != "")
                {
                    where += " and idAlmacen = " + idAlmacen;
                }

                if (!string.IsNullOrEmpty (estado )){
                  string i="";

                  if(estado == "NO ATENDIDA") i="1"; 
                  if(estado == "ATENDIDA") i ="2";
                  if(estado == "TODOS") i ="estado";

                  where += " and estado = " + i;
                }

              

                if (fechaIni != null)
                {


                    where += " and CONVERT(DATE,'" + fechaIni.ToString("yyyy-MM-dd") + "')   <=  CONVERT(DATE,fechaReg,103) ";

                }

                if (fechaFin != null)
                {

                    where += " and CONVERT(DATE,'" + fechaFin.ToString("yyyy-MM-dd") + "')   >=  CONVERT(DATE,fechaReg,103) ";
                }

            }

          //   MessageBox.Show("SELECT * FROM SolicitudAdquisicion WHERE  estado <> 0 " + where);

            db.cmd.CommandText = "SELECT * FROM SolicitudAdquisicion WHERE  1=1   " + where;
            db.cmd.CommandType = CommandType.Text;
            db.cmd.Connection = db.conn;



            try
            {
                db.conn.Open();

                reader = db.cmd.ExecuteReader();


                while (reader.Read())
                {

                    SolicitudAdquisicion p = new SolicitudAdquisicion();
                   p.Estado =  Convert.ToInt32(reader["estado"].ToString());
                   p.IdAlmacen = Convert.ToInt32(reader["idAlmacen"].ToString());
                   p.IdSolicitudAD = Convert.ToInt32 (reader["idSolicitudAD"].ToString());
                   p.FechaReg  =Convert.ToDateTime (reader["fechaReg"]);
                   p.FechaAtencion = Convert.ToDateTime(reader["fechaAtencion"]);
                   p.Codigo = "SOL-"+Convert.ToString(10000000 + p.IdSolicitudAD);
                   p.LstProductos = new ProductoxSolicitudAdSQL().Buscar(p.IdSolicitudAD) as List<ProductoxSolicitudAd>;
                   p.Est = (p.Estado == 1)?"NO ATENDIDA":"ATENDIDA";
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

        public int Actualizar(object entity)
        {
            int k = 0;

            SolicitudAdquisicion pp = entity as SolicitudAdquisicion;




            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;


            cmd.CommandText = "UPDATE SolicitudAdquisicion set estado = 2  " +
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
    }
}
