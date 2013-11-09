using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;
using System.Data.SqlClient;
using System.Windows;
using System.Data;

namespace MadeInHouse.DataObjects.Almacen
{
    class ProductoSQL
    {

        private DBConexion db;
        private bool tipo = true;

        public ProductoSQL(DBConexion db=null)
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


        public void AgregarProducto(Producto p)
        {
            db.cmd.CommandText = "INSERT INTO Producto(codProducto, nombre, descripcion, percepcion,idSubLinea,idLinea,estado,idUnidad,abreviatura) " +
            "VALUES (@codProducto,@nombre,@descripcion,@percepcion,@idSubLinea,@idLinea,@estado,@idUnidad,@abreviatura)";
            db.cmd.Parameters.AddWithValue("@codProducto", p.CodigoProd);
            db.cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            db.cmd.Parameters.AddWithValue("@abreviatura", p.Abreviatura);
            db.cmd.Parameters.AddWithValue("@descripcion", p.Descripcion==null ? "":p.Descripcion );
            db.cmd.Parameters.AddWithValue("@idUnidad", p.IdUnidad);
            db.cmd.Parameters.AddWithValue("@percepcion", p.Percepcion);
            db.cmd.Parameters.AddWithValue("@idSubLinea", p.IdSubLinea);
            db.cmd.Parameters.AddWithValue("@idLinea", p.IdLinea);
            db.cmd.Parameters.AddWithValue("@estado", 1);

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


        public void ActualizarProducto(Producto p)
        {

            db.cmd.CommandText = "UPDATE Producto  " +
            "SET codProducto= @codProducto,nombre= @nombre,descripcion= @descripcion,idUnidad=@idUnidad, " +
            "percepcion= @percepcion,abreviatura= @abreviatura , idSubLinea=@idSubLinea, idLinea=@idLinea " +
            " WHERE idProducto= @idProducto ";


            db.cmd.Parameters.AddWithValue("@idProducto", p.IdProducto);
            db.cmd.Parameters.AddWithValue("@codProducto", p.CodigoProd);
            db.cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            db.cmd.Parameters.AddWithValue("@descripcion", p.Descripcion);
            db.cmd.Parameters.AddWithValue("@idUnidad", p.IdUnidad);
            db.cmd.Parameters.AddWithValue("@percepcion", p.Percepcion);
            db.cmd.Parameters.AddWithValue("@abreviatura", p.Abreviatura);
            db.cmd.Parameters.AddWithValue("@idSubLinea", p.IdSubLinea);
            db.cmd.Parameters.AddWithValue("@idLinea", p.IdLinea);
            

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


        public void EliminarProducto(int id)
        {
            db.cmd.CommandText = "UPDATE Producto SET estado=0 WHERE idProducto=@idProducto";
            db.cmd.Parameters.AddWithValue("@idProducto", id);

            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
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




        public List<Producto> BuscarProducto(String codigo=null, int idLinea=0, int idSubLinea=0, int idAlmacen=0)
        {
            List<Producto> listaProductos = null;
            
            
            string where = "WHERE 1=1 ";
            string from = "";

            if (!String.IsNullOrEmpty(codigo))
            {
                where = where + " AND codProducto = @codigo ";
                db.cmd.Parameters.AddWithValue("@codigo", codigo);
            }
            
            if (idLinea > 0)
            {
                where = where + " AND idLinea=@idLinea ";
                db.cmd.Parameters.AddWithValue("@idLinea", idLinea);
            }
            if (idSubLinea > 0)
            {
                where = where + " AND idSubLinea=@idSubLinea ";
                db.cmd.Parameters.AddWithValue("@idSubLinea", idSubLinea);
            }
            if (idAlmacen > 0)
            {
                from = " p, AlmacenxProducto ap ";
                where = where + " AND p.idProducto = ap.idProducto AND idAlmacen = @idAlmacen ";
                db.cmd.Parameters.AddWithValue("@idAlmacen", idAlmacen);
            }

            db.cmd.CommandText = "SELECT * FROM Producto " + from + where;

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (listaProductos == null) listaProductos = new List<Producto>();
                    Producto p = new Producto();
                    p.IdProducto = Int32.Parse(reader["idProducto"].ToString());
                    p.CodigoProd = reader.IsDBNull(reader.GetOrdinal("codProducto")) ? null : reader["codProducto"].ToString();
                    p.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();
                    p.Abreviatura = reader.IsDBNull(reader.GetOrdinal("abreviatura")) ? null : reader["abreviatura"].ToString();
                    p.Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader["descripcion"].ToString();
                    p.IdLinea = reader.IsDBNull(reader.GetOrdinal("idLinea")) ? -1 : (int)reader["idLinea"];
                    p.IdSubLinea = reader.IsDBNull(reader.GetOrdinal("idSubLinea")) ? -1 : (int)reader["idSubLinea"];
                    p.Percepcion = Int32.Parse(reader["percepcion"].ToString());
                    p.Precio = Double.Parse(reader["precio"].ToString());
                    //Console.WriteLine(p.Precio);
                    listaProductos.Add(p);
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

            return listaProductos;
        }


        public Producto Buscar_por_CodigoProducto(string codProducto)
        {

            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;
            SqlDataReader reader;

            Producto p = null;

            cmd.CommandText = "SELECT * from Producto where codProducto = @codProducto and estado = 1 ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@codProducto", codProducto);


            try
            {
                conn.Open();





                reader = cmd.ExecuteReader();


                if (reader.Read())
                {

                    p = new Producto();
                    p.IdProducto = Convert.ToInt32(reader["idProducto"].ToString());
                    p.Nombre = reader["nombre"].ToString();
                    p.CodigoProd = reader["codProducto"].ToString();
                }

                reader.Close();
                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return p;

        }

        public Producto Buscar_por_CodigoProducto(int IdProducto)
        {

            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;
            SqlDataReader reader;

            Producto p = null;

            cmd.CommandText = "SELECT * from Producto where idProducto = @idProducto and estado = 1 ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@idProducto", IdProducto);


            try
            {
                conn.Open();





                reader = cmd.ExecuteReader();


                if (reader.Read())
                {

                    p = new Producto();
                    p.IdProducto = Convert.ToInt32(reader["idProducto"].ToString());
                    p.Nombre = reader["nombre"].ToString();
                    p.CodigoProd = reader["codProducto"].ToString();
                }

                reader.Close();
                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return p;

        }

        #region Almacen

        public List<ProductoxAlmacen> BuscarProductoxTienda(int idTienda=-1,int idAlmacen=-1)
        {
            List<ProductoxAlmacen> lstPxa = null;
            ProductoxAlmacen pxa = null;
            string where=" WHERE vigente=1";

            if (idTienda > 0)
            {
                where += " AND idTienda=@idTienda";
                db.cmd.Parameters.AddWithValue("@idTienda", idTienda);
            }
            if (idAlmacen > 0)
            {
                where += " AND idAlmacen=@idAlmacen";
                db.cmd.Parameters.AddWithValue("@idAlmacen", idAlmacen);
            }


            db.cmd.CommandText = "SELECT B.nombre,B.codProducto,A.* FROM AlmacenxProducto A join Producto B ON (A.idProducto=B.idProducto) " + where;
            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                lstPxa=new List<ProductoxAlmacen>();
                while (reader.Read())
                {
                    pxa = new ProductoxAlmacen();
                    pxa.CodProducto = reader["codProducto"].ToString();
                    pxa.IdTienda = reader.IsDBNull(reader.GetOrdinal("idTienda")) ? -1 : Int32.Parse(reader["idTienda"].ToString());
                    pxa.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();
                    pxa.IdAlmacen = reader.IsDBNull(reader.GetOrdinal("idProducto")) ? -1 : Int32.Parse(reader["idProducto"].ToString());
                    pxa.StockActual = reader.IsDBNull(reader.GetOrdinal("stockActual")) ? -1 : Int32.Parse(reader["stockActual"].ToString());
                    pxa.StockMin=reader.IsDBNull(reader.GetOrdinal("stockMin")) ? -1:Int32.Parse(reader["stockMin"].ToString());
                    pxa.StockMax = reader.IsDBNull(reader.GetOrdinal("stockMax")) ? -1 : Int32.Parse(reader["stockMax"].ToString());
                    pxa.PrecioVenta = reader.IsDBNull(reader.GetOrdinal("precioVenta")) ? -1 : float.Parse(reader["precioVenta"].ToString());
                    lstPxa.Add(pxa);
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

            return lstPxa;
        }

        public List<ProductoxAlmacen> BuscarProductoxAlmacen(int idAlmacen, int idProducto)
        {
            List<ProductoxAlmacen> prodAlmacen = null;

            if (idAlmacen > 0)
            {
                string where = "";
                if (idProducto > 0)
                {
                    where = " AND idProducto = @idProducto ";
                    db.cmd.Parameters.AddWithValue("@idProducto", idProducto);
                }
                db.cmd.CommandText = " SELECT * FROM AlmacenxProducto WHERE idAlmacen = @idAlmacen " + where;
                db.cmd.Parameters.AddWithValue("@idAlmacen", idAlmacen);

                try
                {
                    db.conn.Open();
                    SqlDataReader reader = db.cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (prodAlmacen == null) prodAlmacen = new List<ProductoxAlmacen>();
                        ProductoxAlmacen prod = new ProductoxAlmacen();
                        int posIdAlmacen = reader.GetOrdinal("idAlmacen");
                        int posIdProducto = reader.GetOrdinal("idProducto");
                        int posStockMin = reader.GetOrdinal("stockMin");
                        int posStock = reader.GetOrdinal("stockActual");
                        prod.IdAlmacen = reader.IsDBNull(posIdAlmacen)? -1 : reader.GetInt32(posIdAlmacen);
                        prod.IdProducto = reader.IsDBNull(posIdProducto) ? -1 : reader.GetInt32(posIdProducto);
                        prod.StockMin = reader.IsDBNull(posStockMin) ? -1 : reader.GetInt32(posStockMin);
                        prod.StockActual = reader.IsDBNull(posStock) ? -1 : reader.GetInt32(posStock);

                        prodAlmacen.Add(prod);
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
            }

            return prodAlmacen;
        }

        public int AgregarProductoxAlmacen(ProductoxAlmacen pxa) 
        {

           
            db.cmd.CommandText = "INSERT INTO AlmacenxProducto (idProducto,idAlmacen,stockActual,stockMin,stockMax,precioVenta,vigente,idTienda) " +
                            "VALUES (@idProducto,@idAlmacen,@stockActual,@stockMin,@stockMax,@precioVenta,@vigente,@idTienda) ";
            db.cmd.Parameters.AddWithValue("@idProducto", pxa.IdProducto);
            db.cmd.Parameters.AddWithValue("@idAlmacen", pxa.IdAlmacen);
            db.cmd.Parameters.AddWithValue("@stockActual", pxa.StockActual);
            db.cmd.Parameters.AddWithValue("@stockMin", pxa.StockMin);
            db.cmd.Parameters.AddWithValue("@stockMax", pxa.StockMax);
            db.cmd.Parameters.AddWithValue("@precioVenta", pxa.PrecioVenta);
            db.cmd.Parameters.AddWithValue("@vigente", pxa.Vigente);
            db.cmd.Parameters.AddWithValue("@idTienda", pxa.IdTienda);

            try
            {
                if(tipo) db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.cmd.Parameters.Clear();
                if(tipo) db.conn.Close();
                    
            }
            catch (SqlException e)
            {
                System.Windows.MessageBox.Show("ERROR: Lo sentimos no se pudo realizar la operación");
                Console.WriteLine(e);
                return -1;
            }
            catch (Exception e){
                System.Windows.MessageBox.Show("ERROR: Lo sentimos no se pudo realizar la operación");
                Console.WriteLine(e.StackTrace.ToString());
                return -1;
            }

            return 1;
        }

        #endregion

    }



}

