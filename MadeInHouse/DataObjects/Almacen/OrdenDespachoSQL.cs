using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;
using MadeInHouse.ViewModels.Almacen;
using MadeInHouse.DataObjects.Ventas;
using System.Data.SqlClient;
using System.Windows;

namespace MadeInHouse.DataObjects.Almacen
{
    class OrdenDespachoSQL
    {

        private DBConexion db;
        private bool tipo = true;

        public OrdenDespachoSQL(DBConexion db = null)
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

        //Cambiar de estado a la orden de despacho

        public int ActualizarOrdenDespacho(OrdenDespacho o)
        {
            int k = 0;

            db.cmd.CommandText = "UPDATE OrdenDespacho(idVenta,fechaDespacho,estado)" +
                                  " VALUES (@idVenta,@fechaDespacho,@estado)" +
                                  "WHERE idOrdenDespacho=@idOrdenDespacho";
            db.cmd.Parameters.AddWithValue("@idVenta", o.Venta.IdVenta);
            db.cmd.Parameters.AddWithValue("@fechaDespacho", o.FechaDespacho);
            db.cmd.Parameters.AddWithValue("@estado", o.Estado);
            db.cmd.Parameters.AddWithValue("@idOrdenDespacho", o.IdOrdenDespacho);

            try
            {
                if (tipo) db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                if (tipo) db.conn.Close();
                db.cmd.Parameters.Clear();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }

            return k;
        }

        public List<OrdenDespacho> BuscarOrdenDespacho(int idOrdenDespacho = -1, int idVenta = -1)
        {
            List<OrdenDespacho> listaOrdenDespacho = null;


            string where = "WHERE estado >= 0 ";
            string from = "SELECT * FROM OrdenDespacho ";

            if (idOrdenDespacho > 0)
            {
                where = where + " AND idOrdenDespacho = @idOrdenDespacho ";
                db.cmd.Parameters.AddWithValue("@idOrdenDespacho", idOrdenDespacho);
            }

            if (idVenta > 0)
            {
                where = where + " AND idVenta = @idVenta ";
                db.cmd.Parameters.AddWithValue("@idVenta", idVenta);
            }

            db.cmd.CommandText = from + where;

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                MessageBox.Show(from + where);

                while (reader.Read())
                {
                    if (listaOrdenDespacho == null) listaOrdenDespacho = new List<OrdenDespacho>();
                    OrdenDespacho p = new OrdenDespacho();

                    p.IdOrdenDespacho = Int32.Parse(reader["idOrdenDespacho"].ToString());

                    int id = Int32.Parse(reader["idVenta"].ToString());
                    p.Venta = new MantenerGuiaDeRemisionViewModel().getVentafromID(id);

                    p.Estado = Int32.Parse(reader["estado"].ToString());
                    p.FechaDespacho = DateTime.Parse(reader["fechaDespacho"].ToString());

                    listaOrdenDespacho.Add(p);
                }
                db.cmd.Parameters.Clear();
                reader.Close();
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

            return listaOrdenDespacho;
        }
    }
}
