﻿﻿using System;
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
                "output INSERTED.idProducto " +
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

            int idProducto=-1;

            try
            {
                db.conn.Open();


                idProducto= (int) db.cmd.ExecuteScalar();
                db.cmd.Parameters.Clear();

                db.cmd.CommandText = "UPDATE Producto SET codProducto =@codProducto + RIGHT('000000' + cast(idProducto as varchar(2)),6) WHERE idProducto = @idProducto";
                db.cmd.Parameters.AddWithValue("@codProducto", p.CodigoProd);
                db.cmd.Parameters.AddWithValue("@idProducto", idProducto);
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
            string from = "SELECT p.* , L.nombre linea, S.nombre sublinea,U.nombre unidad FROM Producto p" +
                        " JOIN LineaProducto L " +
                        " ON (P.idLinea=L.idLinea) " +
                        " JOIN SubLineaProducto S " +
                        " ON (P.idSubLinea=S.idSubLinea) "+
                        "JOIN UnidadMedida U ON (p.idUnidad=U.idUnidad)";

            
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
                from = "SELECT p.*, pt.stockActual stockTienda , U.nombre unidad, pt.StockMin stockMinT, pt.stockMax stockMaxT , pt.precioVenta , L.nombre linea, S.nombre sublinea" +
                        " FROM Producto p " +
                        " JOIN LineaProducto L " +
                        " ON (P.idLinea=L.idLinea) " +
                        " JOIN SubLineaProducto S " +
                        " ON (P.idSubLinea=S.idSubLinea) " +
                        " JOIN UnidadMedida U ON (p.idUnidad=U.idUnidad) "+
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
                    p.StockActual = reader.IsDBNull(reader.GetOrdinal("stockActual")) ? 0 : Int32.Parse(reader["stockActual"].ToString());
                    LineaProducto lp = new LineaProducto();
                    lp.IdLinea = reader.IsDBNull(reader.GetOrdinal("idLinea")) ? 0 : (int)reader["idLinea"];
                    lp.Nombre = reader.IsDBNull(reader.GetOrdinal("linea")) ? null : reader["linea"].ToString();
                    p.Linea = lp;

                    SubLineaProducto slp = new SubLineaProducto();
                    slp.IdSubLinea = reader.IsDBNull(reader.GetOrdinal("idSubLinea")) ? 0 : (int)reader["idSubLinea"];
                    slp.Nombre = reader.IsDBNull(reader.GetOrdinal("sublinea")) ? null : reader["sublinea"].ToString();
                    p.Sublinea = slp;


                    p.Percepcion = Int32.Parse(reader["percepcion"].ToString());
                    p.UnidadMedida = reader.IsDBNull(reader.GetOrdinal("unidad")) ? null : reader["unidad"].ToString();
                    if (idTienda > 0)
                    {
                        p.Precio = reader.IsDBNull(reader.GetOrdinal("precioVenta")) ? 0 : float.Parse(reader["precioVenta"].ToString());
                        p.StockActual = reader.IsDBNull(reader.GetOrdinal("stockTienda")) ? 0 : int.Parse(reader["stockTienda"].ToString());
                        p.StockMin = reader.IsDBNull(reader.GetOrdinal("stockMinT")) ? 0 : int.Parse(reader["stockMinT"].ToString());
                        p.StockMax = reader.IsDBNull(reader.GetOrdinal("stockMaxT")) ? 0 : int.Parse(reader["stockMaxT"].ToString());
                    }
                    else
                    {
                        p.StockActual = reader.IsDBNull(reader.GetOrdinal("stockActual")) ? 0 : int.Parse(reader["stockActual"].ToString());
                        p.StockMin = reader.IsDBNull(reader.GetOrdinal("stockMin")) ? 0 : int.Parse(reader["stockMin"].ToString());
                        p.StockMax = reader.IsDBNull(reader.GetOrdinal("stockMax")) ? 0 : int.Parse(reader["stockMax"].ToString());

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
                db.cmd.Parameters.Clear();
                db.conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
                db.cmd.Parameters.Clear();
                db.conn.Close();
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

        public List<ProductoxTienda> BuscarProductoxTienda(int idTienda = -1, bool stockMin=false)
        {
            List<ProductoxTienda> lstPxa = null;
            ProductoxTienda pxa = null;
            string where = " WHERE 1=1";


            if (idTienda > 0)
            {
                where += " AND idTienda=@idTienda";
                db.cmd.Parameters.AddWithValue("@idTienda", idTienda);
            }
            if (stockMin)
            {
                where += " AND A.stockActual< A.stockMin ";
            }



            db.cmd.CommandText = "SELECT B.nombre,B.codProducto,A.* FROM ProductoxTienda A join Producto B ON (A.idProducto=B.idProducto) " + where;
            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                lstPxa=new List<ProductoxTienda>();
                while (reader.Read())
                {
                    pxa = new ProductoxTienda();
                    pxa.IdProducto = reader.IsDBNull(reader.GetOrdinal("idProducto")) ? -1: int.Parse(reader["idProducto"].ToString());
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
                db.conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
                db.conn.Close();
            }

            return lstPxa;
        }

        public List<ProductoxTienda> BuscarProductoxTienda(int idTienda, int idProducto)
        {
            List<ProductoxTienda> prodAlmacen = null;

            if (idTienda > 0)
            {
                string where = "";
                if (idProducto > 0)
                {
                    where = " AND idProducto = @idProducto ";
                    db.cmd.Parameters.AddWithValue("@idProducto", idProducto);
                }
                db.cmd.CommandText = " SELECT * FROM ProductoxTienda WHERE idTienda = @idTienda " + where;
                db.cmd.Parameters.AddWithValue("@idTienda", idTienda);

                try
                {
                    db.conn.Open();
                    SqlDataReader reader = db.cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (prodAlmacen == null) prodAlmacen = new List<ProductoxTienda>();
                        ProductoxTienda prod = new ProductoxTienda();
                        int posIdAlmacen = reader.GetOrdinal("idTienda");
                        int posIdProducto = reader.GetOrdinal("idProducto");
                        int posStockMin = reader.GetOrdinal("stockMin");
                        int posStockMax = reader.GetOrdinal("stockMax");
                        int posStock = reader.GetOrdinal("stockActual");
                        prod.IdAlmacen = reader.IsDBNull(posIdAlmacen) ? -1 : reader.GetInt32(posIdAlmacen);
                        prod.IdProducto = reader.IsDBNull(posIdProducto) ? -1 : reader.GetInt32(posIdProducto);
                        prod.StockMin = reader.IsDBNull(posStockMin) ? -1 : reader.GetInt32(posStockMin);
                        prod.StockMax = reader.IsDBNull(posStockMax) ? -1 : reader.GetInt32(posStockMax);
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

        public List<ProductoxTienda> BuscarProductoxCentral(int idAlmacen, int idProducto,bool stockMin=false)
        {
            List<ProductoxTienda> prodAlmacen = null;

            if (idAlmacen > 0)
            {
                string where = "";
                if (idProducto > 0)
                {
                    where = " WHERE idProducto = @idProducto ";
                    db.cmd.Parameters.AddWithValue("@idProducto", idProducto);
                }

                if (stockMin)
                {
                    where = " WHERE stockActual < stockMin ";
                }


                db.cmd.CommandText = " SELECT * FROM Producto " + where;
                db.cmd.Parameters.AddWithValue("@idAlmacen", idAlmacen);

                try
                {
                    db.conn.Open();
                    SqlDataReader reader = db.cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (prodAlmacen == null) prodAlmacen = new List<ProductoxTienda>();
                        ProductoxTienda prod = new ProductoxTienda();
                        int posIdProducto = reader.GetOrdinal("idProducto");
                        int posStockMin = reader.GetOrdinal("stockMin");
                        int posStockMax = reader.GetOrdinal("stockMax");
                        int posStock = reader.GetOrdinal("stockActual");
                        int posStockPendiente = reader.GetOrdinal("stockPendiente");
                        prod.CodProducto = reader.IsDBNull(reader.GetOrdinal("CodProducto")) ? "" : reader["CodProducto"].ToString();
                        prod.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? "" : reader["nombre"].ToString();
                        prod.IdProducto = reader.IsDBNull(posIdProducto) ? -1 : reader.GetInt32(posIdProducto);
                        prod.StockMin = reader.IsDBNull(posStockMin) ? -1 : reader.GetInt32(posStockMin);
                        prod.StockMax = reader.IsDBNull(posStockMax) ? -1 : reader.GetInt32(posStockMax);
                        prod.StockActual = reader.IsDBNull(posStock) ? -1 : reader.GetInt32(posStock);
                        prod.StockPendiente = reader.IsDBNull(posStockPendiente) ? -1 : reader.GetInt32(posStockPendiente);

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


        public int ActualizarProductoxAlmacen(DataTable data, SqlTransaction transaction)
        {

            try
            {

                if (tipo) db.conn.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(db.conn, SqlBulkCopyOptions.Default, transaction))
                {

                    s.DestinationTableName = "TemporalProductos";

                    foreach (var column in data.Columns)
                    {
                        s.ColumnMappings.Add(column.ToString(), column.ToString());
                    }

                    s.WriteToServer(data);
                    s.Close();
                }

               
                db.cmd.CommandText = "MERGE INTO  ProductoxTienda  USING " +
                                      "TemporalProductos on (TemporalProductos.idProducto=ProductoxTienda.idProducto and TemporalProductos.idTienda=ProductoxTienda.idTienda )" +
                                       "when matched then update " +
                                        "set ProductoxTienda.stockMin=TemporalProductos.stockMin ,  ProductoxTienda.stockMax=TemporalProductos.stockMax , ProductoxTienda.precioVenta=TemporalProductos.precioVenta , ProductoxTienda.vigente=TemporalProductos.vigente " +
                                        " when not matched then " +
                                        " insert (idProducto,idTienda,stockMin,stockMax,precioVenta,vigente) " +
                                        " values (TemporalProductos.idProducto,TemporalProductos.idTienda,TemporalProductos.stockMin,TemporalProductos.stockMax,TemporalProductos.precioVenta, TemporalProductos.vigente) ;";
                db.cmd.ExecuteNonQuery();

                db.cmd.CommandText = "TRUNCATE TABLE TemporalProductos ";
                db.cmd.ExecuteNonQuery();

                if (tipo) db.conn.Close();



            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return -1;
            }

            return 1;

        }


        public DataTable CrearProductoxAlmacenDT()
        {
            /*Agrego las zonas por almacen*/
            DataTable dataTable = new DataTable("TemporalProductos");

            // Create Column 1: idProducto

            DataColumn idProductoCol = new DataColumn();
            idProductoCol.DataType = Type.GetType("System.Int32");
            idProductoCol.ColumnName = "idProducto";


            // Create Column 2: idTienda
            DataColumn idTiendaCol = new DataColumn();
            idTiendaCol.DataType = Type.GetType("System.Int32");
            idTiendaCol.ColumnName = "idTienda";

            // Create Column 3: stockMin
            DataColumn stockMinCol = new DataColumn();
            stockMinCol.DataType = Type.GetType("System.Int32");
            stockMinCol.ColumnName = "stockMin";

            // Create Column 3: stockMax
            DataColumn stockMaxCol = new DataColumn();
            stockMaxCol.DataType = Type.GetType("System.Int32");
            stockMaxCol.ColumnName = "stockMax";

            // Create Column 3: stockMax
            DataColumn precioVentaCol = new DataColumn();
            precioVentaCol.DataType = Type.GetType("System.Int32");
            precioVentaCol.ColumnName = "precioVenta";

            // Create Column 3: stockMax
            DataColumn vigenteCol = new DataColumn();
            vigenteCol.DataType = Type.GetType("System.Int32");
            vigenteCol.ColumnName = "vigente";

            // Add the columns to the ProductSalesData DataTable
            dataTable.Columns.Add(idProductoCol);
            dataTable.Columns.Add(idTiendaCol);
            dataTable.Columns.Add(stockMinCol);
            dataTable.Columns.Add(stockMaxCol);
            dataTable.Columns.Add(precioVentaCol);
            dataTable.Columns.Add(vigenteCol);

            return dataTable;
        }

        public void AgregarFilasToDT(DataTable data, List<ProductoxTienda> filas)
        {
            for (int p = 0; p < filas.Count; p++)
            {
                DataRow pxaRow = data.NewRow();
                pxaRow["idProducto"] = filas[p].IdProducto;
                pxaRow["idTienda"] = filas[p].IdTienda;
                pxaRow["stockMin"] = filas[p].StockMin;
                pxaRow["stockMax"] = filas[p].StockMax;
                pxaRow["precioVenta"] = filas[p].PrecioVenta;
                pxaRow["vigente"] = filas[p].Vigente;
                data.Rows.Add(pxaRow);

            }
        }



        public int AgregarProductoxAlmacen(ProductoxTienda pxa) 
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

        public void ActualizarStockSalida(List<ProductoCant> list)
        {

            db.cmd.CommandText = "UPDATE Producto SET  stockActual=stockActual-@cantidad WHERE idproducto=@idProducto";
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

        public void ActualizarStockTemporalSalida(List<ProductoCant> list)
        {

            db.cmd.CommandText = "UPDATE Producto SET  stockPendiente=stockPendiente-@cantidad WHERE idproducto=@idProducto";
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

