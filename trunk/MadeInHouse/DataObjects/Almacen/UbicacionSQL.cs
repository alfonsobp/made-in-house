using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;
using System.Data.SqlClient;
using System.Data;

namespace MadeInHouse.DataObjects.Almacen
{
    class UbicacionSQL
    {
        private DBConexion db;
        private bool tipo = true;

        public UbicacionSQL(DBConexion db = null)
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
        public int Agregar(Ubicacion u)
        {
            db.cmd.CommandText = "INSERT INTO Ubicacion (idTipoZona,idAlmacen,cordX,cordY,cordZ) VALUES (@idTipoZona,@idAlmacen,@cordX,@cordY,@cordZ)";
            db.cmd.Parameters.AddWithValue("@idTipoZona", u.IdTipoZona);
            db.cmd.Parameters.AddWithValue("@idAlmacen", u.IdAlmacen);
            db.cmd.Parameters.AddWithValue("@cordX", u.CordX);
            db.cmd.Parameters.AddWithValue("@cordY", u.CordY);
            db.cmd.Parameters.AddWithValue("@cordZ", u.CordZ);

            try
            {
                if (tipo) db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.cmd.Parameters.Clear();
                if (tipo) db.conn.Close();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return -1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
                return -1;
            }

            return 1;
        }

        public int AgregarMasivo(DataTable data, SqlTransaction transaction)
        {
            try
            {
                if (tipo) db.conn.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(db.conn, SqlBulkCopyOptions.Default, transaction))
                {

                    s.DestinationTableName = data.TableName;

                    foreach (var column in data.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString());

                    s.WriteToServer(data);
                    s.Close();
                }
                if (tipo) db.conn.Close();


            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return -1;
            }


            return 1;
        }

        public int Eliminar(int idAlmacen)
        {
            db.cmd.CommandText = "DELETE FROM Ubicaciones WHERE idAlmacen=@idAlmacen";
            db.cmd.Parameters.AddWithValue("@idAlmacen", idAlmacen);
            try
            {
                if (tipo) db.conn.Open();
                db.cmd.ExecuteNonQuery();
                if (tipo) db.conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }



            return 1;
        }


        public List<Ubicacion> ObtenerUbicaciones(int idAlmacen = -1, int idTipoZona = -1)
        {
            List<Ubicacion> lstUbicaciones = null;
            db.cmd.CommandText = "SELECT A.* FROM Ubicacion A" +
                " WHERE A.idAlmacen=@idAlmacen and A.idTipoZona=@idTipoZona";
            db.cmd.Parameters.AddWithValue("@idAlmacen", idAlmacen);
            db.cmd.Parameters.AddWithValue("@idTipoZona", idTipoZona);

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                lstUbicaciones = new List<Ubicacion>();
                while (reader.Read())
                {
                    Ubicacion u = new Ubicacion();
                    u.IdUbicacion = int.Parse(reader["idUbicacion"].ToString());
                    u.IdAlmacen = idAlmacen;
                    u.IdTipoZona = idTipoZona;
                    u.IdProducto = reader.IsDBNull(reader.GetOrdinal("idProducto")) ? 0 : int.Parse(reader["idProducto"].ToString());
                    u.CordX = int.Parse(reader["cordX"].ToString());
                    u.CordY = int.Parse(reader["cordY"].ToString());
                    u.CordZ = int.Parse(reader["cordZ"].ToString());
                    u.Cantidad = reader.IsDBNull(reader.GetOrdinal("cantidad")) ? -1 : int.Parse(reader["cantidad"].ToString());
                    u.VolOcupado = reader.IsDBNull(reader.GetOrdinal("volOcupado")) ? -1 : int.Parse(reader["volOcupado"].ToString());
                    lstUbicaciones.Add(u);

                }

                reader.Close();
                db.conn.Close();
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


            return lstUbicaciones;
        }

        public int ActualizarUbicacionMasivo()
        {
            try
            {

                if (tipo) db.conn.Open();

                db.cmd.CommandText = "MERGE INTO  Ubicacion  USING "+
                                      "Temporal on Ubicacion.idUbicacion=Temporal.idUbicacion " +
                                       "when matched then update " + 
                                        "set Ubicacion.idProducto=Temporal.idProducto , Ubicacion.cantidad = Temporal.cantidad , Ubicacion.volOcupado=Temporal.volOcupado;";
                db.cmd.ExecuteNonQuery();

               
                db.cmd.CommandText = "TRUNCATE TABLE Temporal";
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

        public DataTable CrearUbicacionesDT()
        {
            /*Agrego las zonas por almacen*/
            DataTable ubicacionesData = new DataTable("Ubicacion");

            // Create Column 1: idTipoZona

            DataColumn idZonaCol = new DataColumn();
            idZonaCol.DataType = Type.GetType("System.Int32");
            idZonaCol.ColumnName = "idTipoZona";


            // Create Column 2: idAlmacen
            DataColumn idAlmacenCol = new DataColumn();
            idAlmacenCol.DataType = Type.GetType("System.Int32");
            idAlmacenCol.ColumnName = "idAlmacen";

            // Create Column 3: cordX
            DataColumn cordXCol = new DataColumn();
            cordXCol.DataType = Type.GetType("System.Int32");
            cordXCol.ColumnName = "cordX";

            // Create Column 3: cordY
            DataColumn cordYCol = new DataColumn();
            cordYCol.DataType = Type.GetType("System.Int32");
            cordYCol.ColumnName = "cordY";

            // Create Column 3: cordY
            DataColumn cordZCol = new DataColumn();
            cordZCol.DataType = Type.GetType("System.Int32");
            cordZCol.ColumnName = "cordZ";

            DataColumn idProductoCol = new DataColumn();
            idProductoCol.DataType = Type.GetType("System.Int32");
            idProductoCol.ColumnName = "idProducto";

            DataColumn cantidadCol = new DataColumn();
            cantidadCol.DataType = Type.GetType("System.Int32");
            cantidadCol.ColumnName = "cantidad";

            DataColumn volOcupadoCol = new DataColumn();
            volOcupadoCol.DataType = Type.GetType("System.Int32");
            volOcupadoCol.ColumnName = "volOcupado";

            DataColumn idUbicacionCol = new DataColumn();
            idUbicacionCol.DataType = Type.GetType("System.Int32");
            idUbicacionCol.ColumnName = "idUbicacion";


            // Add the columns to the ProductSalesData DataTable
            ubicacionesData.Columns.Add(idZonaCol);
            ubicacionesData.Columns.Add(idAlmacenCol);
            ubicacionesData.Columns.Add(cordXCol);
            ubicacionesData.Columns.Add(cordYCol);
            ubicacionesData.Columns.Add(cordZCol);
            ubicacionesData.Columns.Add(idProductoCol);
            ubicacionesData.Columns.Add(cantidadCol);
            ubicacionesData.Columns.Add(volOcupadoCol);
            ubicacionesData.Columns.Add(idUbicacionCol);

            ubicacionesData.TableName = "Temporal";

            return ubicacionesData;
        }

        public void AgregarFilasToUbicacionesDT(DataTable data, List<List<List<Ubicacion>>> filas, int id)
        {
            for (int m = 0; m < filas.Count; m++)
            {
                for (int n = 0; n < filas[m].Count; n++)
                {
                    for (int p = 0; p < filas[m][n].Count; p++)
                    {
                        filas[m][n][p].IdAlmacen = id;
                        DataRow ubicacionxAlmacenRow = data.NewRow();
                        ubicacionxAlmacenRow["idTipoZona"] = filas[m][n][p].IdTipoZona;
                        ubicacionxAlmacenRow["idAlmacen"] = filas[m][n][p].IdAlmacen;
                        ubicacionxAlmacenRow["CordX"] = filas[m][n][p].CordX;
                        ubicacionxAlmacenRow["CordY"] = filas[m][n][p].CordY;
                        ubicacionxAlmacenRow["CordZ"] = filas[m][n][p].CordZ;
                        ubicacionxAlmacenRow["cantidad"] = filas[m][n][p].Cantidad;
                        ubicacionxAlmacenRow["volOcupado"] = filas[m][n][p].VolOcupado;
                        ubicacionxAlmacenRow["idProducto"] = filas[m][n][p].IdProducto;
                        ubicacionxAlmacenRow["idUbicacion"] = filas[m][n][p].IdUbicacion;
                        data.Rows.Add(ubicacionxAlmacenRow);

                    }
                }

            }

        }

    }
}
