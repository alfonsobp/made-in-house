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
    class OrdenCompraSQL:EntitySQL
    {
        public int Agregar(object entity)
        {
            DBConexion db = new DBConexion();
            int k = 0;
            OrdenCompra c = entity as OrdenCompra;
          //  MessageBox.Show(" " + c.Proveedor.IdProveedor + " " + c.MedioPago + " " + c.Observaciones);
            db.cmd.CommandText = "INSERT INTO OrdenCompra(idProveedor,fechaReg,medioPago,estado,observaciones,fechaSinAtencion) " +
                                 "VALUES (@idProveedor,@fechaReg,@medioPago,@estado,@observaciones,@fechaSinAtencion)";

            db.cmd.Parameters.AddWithValue("@idProveedor", c.Proveedor.IdProveedor);
            db.cmd.Parameters.AddWithValue("@fechaReg", DateTime.Now);
            db.cmd.Parameters.AddWithValue("@medioPago", c.MedioPago);
            db.cmd.Parameters.AddWithValue("@observaciones", c.Observaciones);
            db.cmd.Parameters.AddWithValue("@estado", c.Estado);
            db.cmd.Parameters.AddWithValue("@fechaSinAtencion", c.FechaSinAtencion);
            
            try
            {
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                db.conn.Close();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }

            return k;
        }

        public object Buscar(params object[] filters)
        {
            



               List<OrdenCompra> lst = new List<OrdenCompra>();
            DBConexion db = new DBConexion();
            SqlDataReader reader;

            String where = "";


           
                string codigoOrden = Convert.ToString(filters[0]);
                string codProv = Convert.ToString(filters[1]);
                int Estado = Convert.ToInt32(filters[2]);
                DateTime fechaIni = Convert.ToDateTime(filters[3]);
                DateTime fechaFin = Convert.ToDateTime(filters[4]);

                if (!String.IsNullOrEmpty(codigoOrden) )
                {
                    where += " and o.codOrdenCompra LIKE '%" + codigoOrden+ "%' ";
                }

                if (!String.IsNullOrEmpty(codProv))
                {
                    where += " and p.razonSocial like '%" + codProv + "%' ";
                }

                if (Estado != 4 )
                {
                    where += " and o.estado = "+ Estado;
                }

                if ((fechaIni != null) && (filters[3] != null))
                {


                    where += " and CONVERT(DATE,'" + fechaIni.ToString("yyyy-MM-dd") + "')   <=  CONVERT(DATE,o.fechaReg,103) ";

                }

                if ((fechaFin != null) && (filters[4] != null))
                {

                    where += " and CONVERT(DATE,'" + fechaFin.ToString("yyyy-MM-dd") + "')   >=  CONVERT(DATE,o.fechaReg,103) ";
                }



              //  MessageBox.Show(where);

              

            db.cmd.CommandText = "SELECT  o.idOrden , o.codOrdenCompra , o.idProveedor , o.fechaReg , o.fechaSinAtencion , o.observaciones , o.medioPago,o.estado "+
                                  " FROM OrdenCompra o INNER JOIN Proveedor p  "+
                                 "ON o.idProveedor = p.idProveedor "+
                                 "WHERE  1 = 1   "+ where;
            db.cmd.CommandType = CommandType.Text;
            db.cmd.Connection = db.conn;



            try
            {
                db.conn.Open();

                reader = db.cmd.ExecuteReader();


                while (reader.Read())
                {

                    OrdenCompra o = new OrdenCompra();
                    o.IdOrden = Convert.ToInt32(reader["idOrden"]);
                    o.CodOrdenCompra = Convert.ToString(reader["codOrdenCompra"]);
                    o.Proveedor = new ProveedorSQL().BuscarPorCodigo(Convert.ToInt32(reader["idProveedor"]));
                    o.FechaReg =  reader["fechaReg"].ToString();
                    o.FechaSinAtencion = reader.IsDBNull(reader.GetOrdinal("fechaSinAtencion"))?DateTime.Now:Convert.ToDateTime(reader["fechaSinAtencion"]);
                    o.Observaciones = reader["observaciones"].ToString();
                    o.MedioPago = reader["medioPago"].ToString();
                    o.Estado = Convert.ToInt32(reader["estado"]);
                    o.LstProducto = new OrdenCompraxProductoSQL().Buscar(o.IdOrden) as List<ProductoxOrdenCompra>;
                    lst.Add(o);

                    

                }

                db.conn.Close();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }


            return lst;



        }

        public int Actualizar(object entity)
        {
            int k = 0;

            OrdenCompra p = entity as OrdenCompra;




            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;


            cmd.CommandText = "UPDATE OrdenCompra  set medioPago = @medioPago , observaciones = @observaciones" +
                              " ,fechaSinAtencion = @fechaSinAtencion , estado = @estado  where idOrden = @idOrden ";


            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

                
                 cmd.Parameters.AddWithValue("@medioPago",p.MedioPago);
                 cmd.Parameters.AddWithValue("@observaciones",p.Observaciones);
                 cmd.Parameters.AddWithValue("@fechaSinAtencion", p.FechaSinAtencion  );
                 cmd.Parameters.AddWithValue("@estado",p.Estado);
                 cmd.Parameters.AddWithValue("@idOrden",p.IdOrden);

            try
            {
                conn.Open();


                k = cmd.ExecuteNonQuery();


             
                conn.Close();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }


            return k;
            
        }

        public int Eliminar(object entity)
        {
            int k = 0;

            OrdenCompra p = entity as OrdenCompra;




            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;
            OrdenCompraxProductoSQL oSQL = new OrdenCompraxProductoSQL();

            cmd.CommandText = "UPDATE OrdenCompra  set  estado = @estado  where idOrden = @idOrden ";


            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;


          
            cmd.Parameters.AddWithValue("@estado", 0);
            cmd.Parameters.AddWithValue("@idOrden", p.IdOrden);

            try
            {
                conn.Open();


                k = cmd.ExecuteNonQuery();

                foreach (ProductoxOrdenCompra op in p.LstProducto) {
                    op.Cantidad = op.CantAtendida.ToString();
                    oSQL.Actualizar(op);
                }


                conn.Close();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }


            return k;
        }

        public int relacionarOrden(int idOrden ,int  idSol){
        
                    DBConexion db = new DBConexion();
            int k = 0;
         
            
            db.cmd.CommandText = "INSERT INTO SolicitudAdquisicionxOrdenCompra(idSolicitudAD,idOrden) " +
                                 "VALUES (@idSol,@idOrden)";

            db.cmd.Parameters.AddWithValue("@idSol", idSol);
            db.cmd.Parameters.AddWithValue("@idOrden", idOrden);
          
            try
            {
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                db.conn.Close();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }

            return k;
        
        }

    }
}
