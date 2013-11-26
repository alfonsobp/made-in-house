using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Seguridad;
using MadeInHouse.ViewModels.Almacen;
using MadeInHouse.DataObjects.Ventas;
using MadeInHouse.DataObjects.Seguridad;
using MadeInHouse.DataObjects.Almacen;
using System.Data.SqlClient;
using System.Windows;


using System.Diagnostics;

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

        public int EditarOrdenDespacho(OrdenDespacho o)
        {
            int k = 0;

            db.cmd.CommandText = "UPDATE OrdenDespacho SET estado=@estado, idAlmacen=@idAlmacen " +
                                  "WHERE idOrdenDespacho=@idOrdenDespacho";
            db.cmd.Parameters.AddWithValue("@idOrdenDespacho", o.IdOrdenDespacho);
            db.cmd.Parameters.AddWithValue("@estado", o.Estado);
            if(o.AlmOrigen!=null )db.cmd.Parameters.AddWithValue("@idAlmacen", o.AlmOrigen.IdAlmacen);


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



        public List<OrdenDespacho> BuscarOrdenDespacho(int idOrdenDespacho, int idVenta, int estado)
        {
            List<OrdenDespacho> listaOrdenDespacho = new List<OrdenDespacho>();

            string select = "SELECT * ";
            string from = "FROM OrdenDespacho ";
            string where = "WHERE 1 = 1";

            if (estado > 0)
            {
                where += " AND estado = @estado ";
                db.cmd.Parameters.AddWithValue("@estado", estado);
            }
            if (idOrdenDespacho > 0)
            {
                where += " AND idOrdenDespacho = @idOrdenDespacho ";
                db.cmd.Parameters.AddWithValue("@idOrdenDespacho", idOrdenDespacho);
            }

            if (idVenta > 0)
            {
                where += " AND idVenta = @idVenta ";
                db.cmd.Parameters.AddWithValue("@idVenta", idVenta);
            }


            Usuario u = UsuarioSQL.buscarUsuarioPorIdUsuario(Int32.Parse(Thread.CurrentPrincipal.Identity.Name));
            
            // PARA USUARIO EN ALMACEN


            if (u.IdTienda != 0)
            {
                AlmacenSQL aSQL = new AlmacenSQL();
                int idTi = u.IdTienda;
                Almacenes alm = aSQL.BuscarAlmacen(-1, idTi, 1);

                if (alm != null)
                {
                    Trace.WriteLine("IDALMACEN: " + alm.IdAlmacen);
                    Trace.WriteLine("IDTIENDA: " + idTi);

                    where += " AND idAlmacen = @idAlmacen ";
                    db.cmd.Parameters.AddWithValue("@idAlmacen", alm.IdAlmacen);
                }
            }


            db.cmd.CommandText = select + from + where;

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {

                    OrdenDespacho p = new OrdenDespacho();

                    p.IdOrdenDespacho = Int32.Parse(reader["idOrdenDespacho"].ToString());
                    p.CodOrden = "OD-" + (1000000 + p.IdOrdenDespacho).ToString();

                    int id = Int32.Parse(reader["idVenta"].ToString());
                    p.Venta = new MantenerGuiaDeRemisionViewModel().getVentafromID(id);
                    p.Estado = Int32.Parse(reader["estado"].ToString());
                    p.FechaDespacho = DateTime.Parse(reader["fechaDespacho"].ToString());
                    p.Direccion = reader["direccion"].ToString();
                    p.Telefono = reader["telefono"].ToString();


                    int idTienda = u.IdTienda;

                    //Trace.WriteLine("IdTienda: "+idTienda);
                    p.AlmOrigen = BuscarTIENfromID(idTienda).Deposito;

                    if (p.Venta.IdUsuario == Int32.Parse(Thread.CurrentPrincipal.Identity.Name))
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



        public Tienda BuscarTIENfromID(int id)
        {

            DBConexion db = new DBConexion();
            db.cmd.CommandText = "SELECT * FROM Tienda WHERE idTienda = " + id;


            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                if (reader.Read())
                {
                    Tienda t = new Tienda();
                    t.IdTienda = int.Parse(reader["idTienda"].ToString());
                    t.IdUbigeo = reader.IsDBNull(reader.GetOrdinal("idUbigeo")) ? -1 : int.Parse(reader["idUbigeo"].ToString());
                    t.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();
                    t.Direccion = reader.IsDBNull(reader.GetOrdinal("direccion")) ? null : reader["direccion"].ToString();
                    t.Telefono = reader.IsDBNull(reader.GetOrdinal("telefono")) ? null : reader["telefono"].ToString();
                    t.Administrador = reader.IsDBNull(reader.GetOrdinal("administrador")) ? null : reader["administrador"].ToString();
                    AlmacenSQL aSQL = new AlmacenSQL();
                    t.Deposito = aSQL.BuscarAlmacen(-1, t.IdTienda, 1);

                    Trace.WriteLine("Deposito: " + t.Deposito.Nombre);
                    return t;
                }

                db.cmd.Parameters.Clear();
                db.conn.Close();
                reader.Close();


            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return null;
            }

            return null;
        }
    }
}
