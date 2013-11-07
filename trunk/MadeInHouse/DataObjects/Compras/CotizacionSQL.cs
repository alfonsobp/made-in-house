using MadeInHouse.Models.Compras;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;
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
    class CotizacionSQL:EntitySQL
    {
        public int Agregar(object entity)
        {
            DBConexion db = new DBConexion();
            int k = 0;
            Cotizacion c = entity as Cotizacion;

            db.cmd.CommandText = "INSERT INTO Cotizacion(fechaInicio,fechaFin,estado,idProveedor,fechaResp,observacion) " +
                                 "VALUES (@fechaInicio,@fechaFin,@estado,@idProveedor,@fechaResp,@observacion)";

            db.cmd.Parameters.AddWithValue("@fechaInicio", DateTime.Now);
            db.cmd.Parameters.AddWithValue("@fechaFin", DateTime.Now);
            db.cmd.Parameters.AddWithValue("@estado", 1);
            db.cmd.Parameters.AddWithValue("@idProveedor", c.Proveedor.IdProveedor);
            db.cmd.Parameters.AddWithValue("@fechaResp", DateTime.Now);
            db.cmd.Parameters.AddWithValue("@observacion", c.Observacion);

            try
            {
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                db.conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
        }

        public object Buscar(params object[] filters)
        {
            List<Cotizacion> lstCotizacion = new List<Cotizacion>();
            DBConexion db = new DBConexion();
            SqlDataReader reader;

            String where = "";
            int est = 2;
            string codCotizacion = "", codProveedor = "";

            if (filters.Length > 1 && filters.Length <= 5)
            {

                codCotizacion = Convert.ToString(filters[0]);
                codProveedor = Convert.ToString(filters[1]);
                string estado = Convert.ToString(filters[2]);
                DateTime fechaIni = Convert.ToDateTime(filters[3]);
                DateTime fechaFin = Convert.ToDateTime(filters[4]);

                if (codCotizacion != "")
                {
                    int idCot = getIDfromCOD(codCotizacion);

                    MessageBox.Show("ID cot = " + idCot);
                    where += " and idCotizacion = '" + idCot.ToString() + "' ";
                }

                if (codProveedor != "")
                {
                    int idProveedor = getIDfromCOD(codProveedor);

                    MessageBox.Show("ID prov = " + idProveedor);
                    where += " and idProveedor = '" + idProveedor.ToString() + "' ";
                }

                if (estado != "")
                {
                    if (estado.Equals("ATENDIDO"))
                        est = 2;
                    if (estado.Equals("PENDIENTE"))
                        est = 1;
                    if (estado.Equals("CANCELADO"))
                        est = 0;

                    where += " and estado = '" + est + "' ";
                }

                if (fechaIni != null)
                {

                    where += " and CONVERT(DATE,'" + fechaIni.ToString("yyyy-MM-dd") + "')   <=  CONVERT(DATE,fechaFin,103) ";

                }

                if (fechaFin != null)
                {

                    where += " and CONVERT(DATE,'" + fechaFin.ToString("yyyy-MM-dd") + "')   >=  CONVERT(DATE,fechaFin,103) ";
                }

            }

            // MessageBox.Show("SELECT * FROM Proveedor WHERE  estado = 1 " + where);


            db.cmd.CommandText = "SELECT * FROM Cotizacion  WHERE   estado >= 0   " + where;
            db.cmd.CommandType = CommandType.Text;
            db.cmd.Connection = db.conn;



            try
            {
                db.conn.Open();

                reader = db.cmd.ExecuteReader();


                while (reader.Read())
                {

                    Cotizacion p = new Cotizacion();
                    p.Proveedor = new Proveedor();

                    //ProveedorSQL eM = new ProveedorSQL();
                    //List<Proveedor> lstP = eM.Buscar(codProveedor) as List<Proveedor>;

                    p.IdCotizacion = Convert.ToInt32(reader["idCotizacion"].ToString());
                    p.CodCotizacion = "COT-" + (1000000 + p.IdCotizacion).ToString();
                    int idProv = Convert.ToInt32(reader["idProveedor"].ToString());
                    p.Proveedor = getPROVfromID(idProv);
                    p.FechaRespuesta = Convert.ToDateTime(reader["fechaResp"].ToString());
                    p.FechaInicio = Convert.ToDateTime(reader["fechaInicio"].ToString());
                    p.FechaFin = Convert.ToDateTime(reader["fechaFin"].ToString());
                    p.Observacion = reader["observacion"].ToString();
                    p.Estado = Convert.ToInt32(reader["estado"].ToString());

                    lstCotizacion.Add(p);
                }

                db.conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return lstCotizacion;
        }

        public int Actualizar(object entity)
        {
            DBConexion db = new DBConexion();
            Cotizacion c = entity as Cotizacion;
            int k = 0;

            db.cmd.CommandText = "UPDATE Cotizacion " +
                                 "SET estado= @estado,fechaInicio= @fechaInicio,fechaFin= @fechaFin,fechaResp= @fechaResp,observacion= @observacion " +
                                 "WHERE idCotizacion= @idCotizacion ";

            db.cmd.Parameters.AddWithValue("@idCotizacion", c.IdCotizacion);
            db.cmd.Parameters.AddWithValue("@estado", 2);
            db.cmd.Parameters.AddWithValue("@fechaInicio", c.FechaInicio);
            db.cmd.Parameters.AddWithValue("@fechaFin", c.FechaFin);
            db.cmd.Parameters.AddWithValue("@fechaResp", c.FechaRespuesta);
            db.cmd.Parameters.AddWithValue("@observacion", c.Observacion);

            try
            {
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                MessageBox.Show("Se realizo la actualizacion");
                db.conn.Close();

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

        public int getIDfromCOD(string cod)
        {
            int finalVal = 0, factor = 1;
            int last = cod.Length - 1;

            
            for (int i = 0; i < 7; i++)
            {
                int val = Convert.ToInt32(cod[last].ToString());
                finalVal = factor * val + finalVal;
                factor = factor * 10; last = last - 1;
            }

            return (finalVal-1000000);
        }

        public Proveedor getPROVfromID(int idProv)
        {
            ProveedorSQL eM = new ProveedorSQL(); 
            List<Proveedor> lstP = eM.Buscar() as List<Proveedor>;

            for (int i = 0; i<lstP.Count; i++)
                if (lstP[i].IdProveedor == idProv)
                    return lstP[i]; 

            return null;
        }


    }
}
