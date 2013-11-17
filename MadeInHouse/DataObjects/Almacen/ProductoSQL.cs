﻿using System;
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

        public ProductoSQL(DBConexion db = null)
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
            db.cmd.CommandText = "INSERT INTO Producto(codProducto, nombre, descripcion, percepcion,idSubLinea,idLinea,estado,idUnidad,abreviatura,stockMin,stockMax) " +
            "VALUES (@codProducto,@nombre,@descripcion,@percepcion,@idSubLinea,@idLinea,@estado,@idUnidad,@abreviatura,@stockMin,@stockMax)";
            db.cmd.Parameters.AddWithValue("@codProducto", p.CodigoProd);
            db.cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            db.cmd.Parameters.AddWithValue("@abreviatura", p.Abreviatura);
            db.cmd.Parameters.AddWithValue("@descripcion", p.Descripcion == null ? "" : p.Descripcion);
            db.cmd.Parameters.AddWithValue("@idUnidad", p.IdUnidad);
            db.cmd.Parameters.AddWithValue("@percepcion", p.Percepcion);
            db.cmd.Parameters.AddWithValue("@idSubLinea", p.IdSubLinea);
            db.cmd.Parameters.AddWithValue("@idLinea", p.IdLinea);
            db.cmd.Parameters.AddWithValue("@estado", 1);
            db.cmd.Parameters.AddWithValue("@stockMin", p.StockMin);
            db.cmd.Parameters.AddWithValue("@stockMax", p.StockMax);

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
            "percepcion= @percepcion,abreviatura= @abreviatura , idSubLinea=@idSubLinea, idLinea=@idLinea , stockMax=@stockMax , stockMin=@stockMin " +
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
            db.cmd.Parameters.AddWithValue("@stockMax", p.StockMax);
            db.cmd.Parameters.AddWithValue("@stockMin", p.StockMin);


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




        public List<Producto> BuscarProducto(String codigo = null, int idLinea = -1, int idSubLinea = -1, int idTienda = -1)
        {
            List<Producto> listaProductos = null;


            string where = "WHERE 1=1 ";
            string from = "SELECT p.* , L.nombre linea, S.nombre sublinea FROM Producto p" +
                        " JOIN LineaProducto L " +
                        " ON (P.idLinea=L.idLinea) " +
                        " JOIN SubLineaProducto S " +
                        " ON (P.idSubLinea=S.idSubLinea) ";



            if (!String.IsNullOrEmpty(codigo))
            {
                where = where + " AND codProducto = @codigo ";
                db.cmd.Parameters.AddWithValue("@codigo", codigo);
            }

            if (idLinea > 0)
            {
                where = where + " AND p.idLinea=@idLinea ";
                db.cmd.Parameters.AddWithValue("@idLinea", idLinea);
            }
            if (idSubLinea > 0)
            {
                where = where + " AND p.idSubLinea=@idSubLinea ";
                db.cmd.Parameters.AddWithValue("@idSubLinea", idSubLinea);
            }
            if (idTienda > 0)
            {
                from = "SELECT p.*, pt.stockActual stockTienda , pt.StockMin stockMinT, pt.stockMax stockMaxT , pt.precioVenta , L.nombre linea, S.nombre sublinea" +
                        " FROM Producto p " +
                        " JOIN LineaProducto L " +
                        " ON (P.idLinea=L.idLinea) " +
                        " JOIN SubLineaProducto S " +
                        " ON (P.idSubLinea=S.idSubLinea) " +
                        " JOIN ProductoxTienda pt ON ( p.idProducto = pt.idProducto) ";
                where += " AND  pt.idTienda = @idTienda AND vigente=1 ";
                db.cmd.Parameters.AddWithValue("@idTienda", idTienda);
            }



            db.cmd.CommandText = from + where;

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

                    LineaProducto lp = new LineaProducto();
                    lp.IdLinea = reader.IsDBNull(reader.GetOrdinal("idLinea")) ? -1 : (int)reader["idLinea"];
                    lp.Nombre = reader.IsDBNull(reader.GetOrdinal("linea")) ? null : reader["linea"].ToString();
                    p.Linea = lp;

                    SubLineaProducto slp = new SubLineaProducto();
                    slp.IdSubLinea = reader.IsDBNull(reader.GetOrdinal("idSubLinea")) ? -1 : (int)reader["idSubLinea"];
                    slp.Nombre = reader.IsDBNull(reader.GetOrdinal("sublinea")) ? null : reader["sublinea"].ToString();
                    p.Sublinea = slp;


                    p.Percepcion = Int32.Parse(reader["percepcion"].ToString());

                    if (idTienda > 0)
                    {
                        p.Precio = reader.IsDBNull(reader.GetOrdinal("precioVenta")) ? -1 : float.Parse(reader["precioVenta"].ToString());
                        p.StockActual = reader.IsDBNull(reader.GetOrdinal("stockTienda")) ? -1 : int.Parse(reader["stockTienda"].ToString());
                        p.StockMin = reader.IsDBNull(reader.GetOrdinal("stockMinT")) ? -1 : int.Parse(reader["stockMinT"].ToString());
                        p.StockMax = reader.IsDBNull(reader.GetOrdinal("stockMaxT")) ? -1 : int.Parse(reader["stockMaxT"].ToString());
                    }
                    else
                    {
                        p.StockActual = reader.IsDBNull(reader.GetOrdinal("stockActual")) ? -1 : int.Parse(reader["stockActual"].ToString());
                        p.StockMin = reader.IsDBNull(reader.GetOrdinal("stockMin")) ? -1 : int.Parse(reader["stockMin"].ToString());
                        p.StockMax = reader.IsDBNull(reader.GetOrdinal("stockMax")) ? -1 : int.Parse(reader["stockMax"].ToString());

                    }

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

        public List<ProductoxAlmacen> BuscarProductoxTienda(int idTienda = -1)
        {
            List<ProductoxAlmacen> lstPxa = null;
            ProductoxAlmacen pxa = null;
            string where = " WHERE 1=1";


            if (idTienda > 0)
            {
                where += " AND idTienda=@idTienda";
                db.cmd.Parameters.AddWithValue("@idTienda", idTienda);
            }



            db.cmd.CommandText = "SELECT B.nombre,B.codProducto,A.* FROM ProductoxTienda A join Producto B ON (A.idProducto=B.idProducto) " + where;
            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                lstPxa = new List<ProductoxAlmacen>();
                while (reader.Read())
                {
                    pxa = new ProductoxAlmacen();
                    pxa.CodProducto = reader["codProducto"].ToString();
                    pxa.IdTienda = reader.IsDBNull(reader.GetOrdinal("idTienda")) ? -1 : Int32.Parse(reader["idTienda"].ToString());
                    pxa.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();
                    pxa.StockActual = reader.IsDBNull(reader.GetOrdinal("stockActual")) ? -1 : Int32.Parse(reader["stockActual"].ToString());
                    pxa.StockMin = reader.IsDBNull(reader.GetOrdinal("stockMin")) ? -1 : Int32.Parse(reader["stockMin"].ToString());
                    pxa.StockMax = reader.IsDBNull(reader.GetOrdinal("stockMax")) ? -1 : Int32.Parse(reader["stockMax"].ToString());
                    pxa.PrecioVenta = reader.IsDBNull(reader.GetOrdinal("precioVenta")) ? -1 : float.Parse(reader["precioVenta"].ToString());
                    pxa.Vigente = reader.IsDBNull(reader.GetOrdinal("vigente")) ? 0 : int.Parse(reader["vigente"].ToString());
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
                        prod.IdAlmacen = reader.IsDBNull(posIdAlmacen) ? -1 : reader.GetInt32(posIdAlmacen);
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


            db.cmd.CommandText = "INSERT INTO ProductoxTienda (idProducto,idTienda,stockActual,stockMin,stockMax,precioVenta,vigente) " +
                            "VALUES (@idProducto,@idTienda,@stockActual,@stockMin,@stockMax,@precioVenta,@vigente) ";
            db.cmd.Parameters.AddWithValue("@idProducto", pxa.IdProducto);
            db.cmd.Parameters.AddWithValue("@idTienda", pxa.IdTienda);
            db.cmd.Parameters.AddWithValue("@stockActual", pxa.StockActual);
            db.cmd.Parameters.AddWithValue("@stockMin", pxa.StockMin);
            db.cmd.Parameters.AddWithValue("@stockMax", pxa.StockMax);
            db.cmd.Parameters.AddWithValue("@precioVenta", pxa.PrecioVenta);
            db.cmd.Parameters.AddWithValue("@vigente", pxa.Vigente);

            try
            {
                if (tipo) db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.cmd.Parameters.Clear();
                if (tipo) db.conn.Close();

            }
            catch (SqlException e)
            {
                System.Windows.MessageBox.Show("ERROR: Lo sentimos no se pudo realizar la operación");
                Console.WriteLine(e);
                return -1;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("ERROR: Lo sentimos no se pudo realizar la operación");
                Console.WriteLine(e.StackTrace.ToString());
                return -1;
            }

            return 1;
        }

        #endregion


        public void ActualizarStockEntrada(List<ProductoCant> list)
        {



            db.cmd.CommandText = "UPDATE Producto SET  stockActual=stockActual+@cantidad WHERE idproducto=@idProducto";
            try
            {
                db.conn.Open();
                for (int i = 0; i < list.Count; i++)
                {
                    db.cmd.Parameters.AddWithValue("@cantidad", list.ElementAt(i).CanAtender);
                    db.cmd.Parameters.AddWithValue("@idProducto", list.ElementAt(i).IdProducto);
                    db.cmd.ExecuteNonQuery();
                    db.cmd.Parameters.Clear();

                }
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

