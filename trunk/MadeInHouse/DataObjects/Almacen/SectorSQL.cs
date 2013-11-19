using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;
using System.Data;

namespace MadeInHouse.DataObjects.Almacen
{
    class SectorSQL
    {
        private DBConexion db;
        private bool tipo = true;

        public SectorSQL(DBConexion db = null)
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


        public List<Sector> ObtenerSectores (int idAlmacen = -1, int idTipoZona = -1)
        {
            List<Sector> lstSectores = null;
                db.cmd.CommandText = "SELECT S.* FROM Sector S" +
                                     " WHERE S.idAlmacen=@idAlmacen and S.idTipoZona=@idTipoZona";
            db.cmd.Parameters.AddWithValue("@idAlmacen", idAlmacen);
            db.cmd.Parameters.AddWithValue("@idTipoZona", idTipoZona);

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                lstSectores = new List<Sector>();
                while (reader.Read())
                {
                    Sector s = new Sector();
                    s.IdSector= int.Parse(reader["idSector"].ToString());
                    s.IdAlmacen = idAlmacen;
                    s.IdTipoZona = idTipoZona;
                    s.IdProducto = reader.IsDBNull(reader.GetOrdinal("idProducto")) ? 0 : int.Parse(reader["idProducto"].ToString());
                    s.Cantidad = reader.IsDBNull(reader.GetOrdinal("cantidad")) ? 0 : int.Parse(reader["cantidad"].ToString());
                    s.VolOcupado = reader.IsDBNull(reader.GetOrdinal("volOcupado")) ? 0: int.Parse(reader["volOcupado"].ToString());
                    s.NroColor = reader.IsDBNull(reader.GetOrdinal("nroColor")) ? 0 : int.Parse(reader["nroColor"].ToString());
                    s.Capacidad = reader.IsDBNull(reader.GetOrdinal("capacidad")) ? 0 : int.Parse(reader["capacidad"].ToString());
                    s.NroUbicaciones = reader.IsDBNull(reader.GetOrdinal("nroUbicaciones")) ? 0 : int.Parse(reader["nroUbicaciones"].ToString());
                    lstSectores.Add(s);

                }
                reader.Close();
                db.conn.Close();
                db.cmd.Parameters.Clear();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
                return null;
            }

            UbicacionSQL uSQL = new UbicacionSQL();
            for (int i = 0; i < lstSectores.Count; i++)
            {
                lstSectores[i].LstUbicaciones = uSQL.ObtenerUbicaciones(idAlmacen, lstSectores[i].IdTipoZona,lstSectores[i].IdSector);
            }



            return lstSectores;
        }


        public DataTable CrearSectoresDT()
        {
            /*Agrego las zonas por almacen*/
            DataTable sectoresDT = new DataTable();

            // Create Column 1: idTipoZona

            DataColumn idSectorCol = new DataColumn();
            idSectorCol.DataType = Type.GetType("System.Int32");
            idSectorCol.ColumnName = "idSector";


            DataColumn idTipoZonaCol = new DataColumn();
            idTipoZonaCol.DataType = Type.GetType("System.Int32");
            idTipoZonaCol.ColumnName = "idTipoZona";

            // Create Column 2: idAlmacen
            DataColumn idAlmacenCol = new DataColumn();
            idAlmacenCol.DataType = Type.GetType("System.Int32");
            idAlmacenCol.ColumnName = "idAlmacen";

            DataColumn idProductoCol = new DataColumn();
            idProductoCol.DataType = Type.GetType("System.Int32");
            idProductoCol.ColumnName = "idProducto";

            DataColumn cantidadCol = new DataColumn();
            cantidadCol.DataType = Type.GetType("System.Int32");
            cantidadCol.ColumnName = "cantidad";

            DataColumn volOcupadoCol = new DataColumn();
            volOcupadoCol.DataType = Type.GetType("System.Int32");
            volOcupadoCol.ColumnName = "volOcupado";

            DataColumn nroUbicacionesCol = new DataColumn();
            nroUbicacionesCol.DataType = Type.GetType("System.Int32");
            nroUbicacionesCol.ColumnName = "nroUbicaciones";

            DataColumn capacidadCol = new DataColumn();
            capacidadCol.DataType = Type.GetType("System.Int32");
            capacidadCol.ColumnName = "capacidad";

            DataColumn idNotaCol = new DataColumn();
            idNotaCol.DataType = Type.GetType("System.Int32");
            idNotaCol.ColumnName = "idNota";

            DataColumn cantidadIngresadaCol = new DataColumn();
            cantidadIngresadaCol.DataType = Type.GetType("System.Int32");
            cantidadIngresadaCol.ColumnName = "cantidadIngresada";


            // Add the columns to the ProductSalesData DataTable
            sectoresDT.Columns.Add(idSectorCol);
            sectoresDT.Columns.Add(idTipoZonaCol);
            sectoresDT.Columns.Add(idAlmacenCol);
            sectoresDT.Columns.Add(idProductoCol);
            sectoresDT.Columns.Add(cantidadCol);
            sectoresDT.Columns.Add(volOcupadoCol);
            sectoresDT.Columns.Add(nroUbicacionesCol);
            sectoresDT.Columns.Add(capacidadCol);
            sectoresDT.Columns.Add(idNotaCol);
            sectoresDT.Columns.Add(cantidadIngresadaCol);


            sectoresDT.TableName = "TemporalSector";

            return sectoresDT;
        }

        public void AgregarFilasToSectoresDT(DataTable data, List<Sector> sectores)
        {

            for (int i = 0; i < sectores.Count; i++)
            {
                
                DataRow sectorRow = data.NewRow();
                sectorRow["idSector"] = sectores[i].IdSector;
                sectorRow["idTipoZona"] = sectores[i].IdTipoZona;
                sectorRow["idAlmacen"] = sectores[i].IdAlmacen;
                sectorRow["idProducto"] = sectores[i].IdProducto;
                sectorRow["cantidad"] = sectores[i].Cantidad;
                sectorRow["volOcupado"] = sectores[i].VolOcupado;
                sectorRow["nroUbicaciones"] = sectores[i].NroUbicaciones;
                sectorRow["capacidad"] = sectores[i].Capacidad;
                sectorRow["cantidadIngresada"] = sectores[i].CantidadIngresada;
                data.Rows.Add(sectorRow);
            }
        }


        public int AgregarMasivo(DataTable data, SqlTransaction trans=null)
        {
            try
            {
                if (tipo) db.conn.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(db.conn))
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

        public int ActualizarIdSector()
        {
            try
            {

                if (tipo) db.conn.Open();
                db.cmd.CommandText = "MERGE INTO  TemporalSector  USING " +
                                      "Sector on (TemporalUbicacion.idProducto=Sector.idProducto and  TemporalUbicacion.idTipoZona=Sector.idTipoZona and TemporalUbicacion.idAlmacen=Sector.idAlmacen )" +
                                       "when matched then update " +
                                        "set TemporalSector.idSector=Sector.idSector ;";
                db.cmd.ExecuteNonQuery();
                //db.cmd.ExecuteNonQuery();
                if (tipo) db.conn.Close();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return -1;
            }



            return 1;
        }



        public int ActualizarTemporalSector(int idNota)
        {
            db.cmd.CommandText = "UPDATE TemporalSector SET idNota=@idNota";
            db.cmd.Parameters.AddWithValue("@idNota", idNota);
            try
            {
                if (tipo) db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.cmd.Parameters.Clear();
                db.conn.Close();
            }catch (SqlException e) 
            {
                Console.WriteLine(e);
                return -1;
            }

            return 1;
        }


        public int ActualizarSectorMasivo()
        {
            try
            {
                if (tipo) db.conn.Open();

                db.cmd.CommandText = "MERGE INTO  Sector  USING " +
                                      "TemporalSector on Sector.idSector=TemporalSector.idSector " +
                                       "when matched then update " +
                                        "set Sector.idProducto=TemporalSector.idProducto , " +
                                        "Sector.cantidad = TemporalSector.cantidad , Sector.capacidad=TemporalSector.capacidad, " +
                                        "Sector.nroUbicaciones=TemporalSector.nroUbicaciones, " +
                                        "Sector.volOcupado=TemporalSector.volOcupado " +
                                        " when not matched then " +
                                        " insert (idTipoZona,idAlmacen,idProducto,cantidad,volOcupado,nroUbicaciones,capacidad) "+
                                        "  values (TemporalSector.idTipoZona,TemporalSector.idAlmacen,TemporalSector.idProducto,TemporalSector.cantidad,TemporalSector.volOcupado,TemporalSector.nroUbicaciones,TemporalSector.capacidad ) ;";
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



    }
}
