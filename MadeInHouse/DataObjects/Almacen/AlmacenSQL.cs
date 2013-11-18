using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;
using System.Data;
using System.Data.SqlClient;
using MadeInHouse.DataObjects.Seguridad;
using MadeInHouse.Models.Seguridad;


namespace MadeInHouse.DataObjects.Almacen
{
    class AlmacenSQL
    {
        private DBConexion db;
        private bool tipo = true;

  
        public AlmacenSQL(DBConexion db=null)
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


        public int AgregarZonas(int nroBloques,int idTipoZona,int idAlmacen) 
        {
            db.cmd.CommandType = CommandType.Text;
            db.cmd.CommandText = "INSERT INTO ZonaxAlmacen (idTipoZona , idAlmacen,nroBloquesDisp,nroBloquesTotal) VALUES (@idTipoZona,@idAlmacen,@nroBloquesDisp,@nroBloquesTotal)";
            db.cmd.Parameters.AddWithValue("@idTipoZona", idTipoZona);
            db.cmd.Parameters.AddWithValue("@idAlmacen", idAlmacen);
            db.cmd.Parameters.AddWithValue("@nroBloquesDisp", nroBloques);
            db.cmd.Parameters.AddWithValue("@nroBloquesTotal", nroBloques);

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

        public int ActualizarZonasMasivo(DataTable data, SqlTransaction transaction )
        {
            int n = 0;
            string select="";
            try
            {
                
                if (tipo) db.conn.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(db.conn, SqlBulkCopyOptions.Default, transaction))
                {

                    s.DestinationTableName = "Temporal";

                    foreach (var column in data.Columns)
                    {
                        s.ColumnMappings.Add(column.ToString(), "column" + n.ToString());
                        n++;
                    }

                    s.WriteToServer(data);
                    s.Close();
                }

                for(int i=0;i<n-1;i++) {
                    select+="column"+i.ToString()+",";
                }
                select +="column"+(n-1).ToString();


                db.cmd.CommandText = "INSERT INTO ZonaxAlmacen (idTipoZona,idAlmacen,nroBloquesDisp,nroBloquesTotal) " +
                                    "SELECT " + select + " FROM Temporal T LEFT JOIN ZonaxAlmacen ZA " +
                                    " ON (T.column1=ZA.idAlmacen and T.column2=ZA.idTipoZona " +
                                    " WHERE ZA.idAlmacen is null and ZA.idTipoZona is null ";
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


        public int AgregarZonasMasivo(DataTable data,SqlTransaction transaction)
        {

            try {

            if (tipo) db.conn.Open();
            using (SqlBulkCopy s = new SqlBulkCopy(db.conn, SqlBulkCopyOptions.Default,transaction))
            {
                
                s.DestinationTableName = data.TableName;

                foreach (var column in data.Columns)
                    
                    s.ColumnMappings.Add(column.ToString(), column.ToString());

                s.WriteToServer(data);
            }
           if (tipo) db.conn.Close();

            }catch(SqlException e) {
                    Console.WriteLine(e);
                    return -1;
                }

            return 1;
        }


        public int Agregar(Almacenes alm)
        {
            //db.cmd.CommandText = "sp_AgregarAlmacen";
            //db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.CommandText = "INSERT INTO Almacen (nombre,codAlmacen,direccion,tipo,idTienda,telefono,nroFilas,nroColumnas,altura,fechaReg,estado) "+
                                    "output INSERTED.idAlmacen "+
                                    "VALUES (@nombre,@codAlmacen,@direccion,@tipo,@idTienda,@telefono,@nroFilas,@nroColumnas,@altura,@fechaReg,@estado)";
            db.cmd.Parameters.AddWithValue("@nombre", alm.Nombre);
            db.cmd.Parameters.AddWithValue("@codAlmacen",alm.CodAlmacen);
            db.cmd.Parameters.AddWithValue("@direccion", alm.Direccion);
            db.cmd.Parameters.AddWithValue("@tipo", alm.Tipo);
            db.cmd.Parameters.AddWithValue("@idTienda", alm.IdTienda);
            db.cmd.Parameters.AddWithValue("@telefono", alm.Telefono);
            db.cmd.Parameters.AddWithValue("@nroFilas", alm.NroFilas);
            db.cmd.Parameters.AddWithValue("@nroColumnas", alm.NroColumnas);
            db.cmd.Parameters.AddWithValue("@altura", alm.Altura);
            db.cmd.Parameters.AddWithValue("@fechaReg", DateTime.Today);
            db.cmd.Parameters.AddWithValue("@estado", 1);


            //db.cmd.Parameters.Add("@idAlmacen", SqlDbType.Int).Direction = ParameterDirection.Output;

            int idalmacen = -1;

            try
            {
                if (tipo)  db.conn.Open();
                idalmacen = (int) db.cmd.ExecuteScalar();
                //idalmacen = Convert.ToInt32(db.cmd.Parameters["@idAlmacen"].Value);
                db.cmd.Parameters.Clear();
                if (tipo)  db.conn.Close();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                System.Windows.MessageBox.Show(e.ToString());
                return -1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
                System.Windows.MessageBox.Show(e.ToString());
                return -1;
            }

            return idalmacen;

        }

        public Models.Almacen.Almacenes BuscarAlmacen(int idAlmacen=-1,int idTienda=-1,int tipoAlmacen=-1)
        {


            Models.Almacen.Almacenes almacen = new Models.Almacen.Almacenes();
            string where = "WHERE 1=1 ";

            if (idAlmacen > 0)
            {
                where += " AND idAlmacen=@idAlmacen";
                db.cmd.Parameters.AddWithValue("@idAlmacen", idAlmacen);
            }
            if (idTienda >= 0)
            {
                where += " AND idTienda=@idTienda";
                db.cmd.Parameters.AddWithValue("@idTienda", idTienda);
            }
            if (tipoAlmacen > 0)
            {
                where += " AND tipo=@tipo";
                db.cmd.Parameters.AddWithValue("@tipo", tipoAlmacen);
            }

            db.cmd.CommandText = "SELECT * FROM Almacen " + where;

            
            try
            {
                if (tipo)  db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {

                    almacen.Altura = reader.IsDBNull(reader.GetOrdinal("altura")) ? -1 : int.Parse(reader["altura"].ToString());
                    almacen.CodAlmacen = reader.IsDBNull(reader.GetOrdinal("codAlmacen")) ? null : reader["codAlmacen"].ToString();
                    almacen.Estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? -1 : int.Parse(reader["estado"].ToString());

                    almacen.FechaReg = reader.IsDBNull(reader.GetOrdinal("fechaReg")) ? DateTime.MinValue : DateTime.Parse(reader["fechaReg"].ToString());
                    almacen.IdAlmacen = reader.IsDBNull(reader.GetOrdinal("idAlmacen")) ? -1 : int.Parse(reader["idAlmacen"].ToString());
                    almacen.IdTienda = reader.IsDBNull(reader.GetOrdinal("idTienda")) ? -1 : int.Parse(reader["idTienda"].ToString());
                    almacen.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();
                    almacen.NroColumnas = reader.IsDBNull(reader.GetOrdinal("nroColumnas")) ? -1 : int.Parse(reader["nroColumnas"].ToString());
                    almacen.NroFilas = reader.IsDBNull(reader.GetOrdinal("nroFilas")) ? -1 : int.Parse(reader["nroFilas"].ToString());
                    almacen.Tipo = reader.IsDBNull(reader.GetOrdinal("tipo")) ? -1: int.Parse(reader["tipo"].ToString());

                }
                db.cmd.Parameters.Clear();
                if (tipo)  db.conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return almacen;

        }

        public List<Almacenes> BuscarAlmacen()
        {
            List<Almacenes> listaal = new List<Almacenes>();

            db.cmd.CommandText = "SELECT * FROM Almacen ";

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    Models.Almacen.Almacenes almacen = new Models.Almacen.Almacenes();
                    almacen.Altura = reader.IsDBNull(reader.GetOrdinal("altura")) ? -1 : int.Parse(reader["altura"].ToString());
                    almacen.CodAlmacen = reader.IsDBNull(reader.GetOrdinal("codAlmacen")) ? null : reader["codAlmacen"].ToString();
                    almacen.Estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? -1 : int.Parse(reader["estado"].ToString());
                    almacen.Direccion = reader["direccion"].ToString();
                    almacen.FechaReg = reader.IsDBNull(reader.GetOrdinal("fechaReg")) ? DateTime.MinValue : DateTime.Parse(reader["fechaReg"].ToString());
                    almacen.IdAlmacen = reader.IsDBNull(reader.GetOrdinal("idAlmacen")) ? -1 : int.Parse(reader["idAlmacen"].ToString());
                    almacen.IdTienda = reader.IsDBNull(reader.GetOrdinal("idTienda")) ? -1 : int.Parse(reader["idTienda"].ToString());
                    almacen.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();
                    almacen.NroColumnas = reader.IsDBNull(reader.GetOrdinal("nroColumnas")) ? -1 : int.Parse(reader["nroColumnas"].ToString());
                    almacen.NroFilas = reader.IsDBNull(reader.GetOrdinal("nroFilas")) ? -1 : int.Parse(reader["nroFilas"].ToString());
                    almacen.Tipo = reader.IsDBNull(reader.GetOrdinal("tipo")) ? -1 : int.Parse(reader["tipo"].ToString());
                    listaal.Add(almacen);
                }
                db.cmd.Parameters.Clear();
                if (tipo) db.conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return listaal;

        }

        /*public int obtenerDeposito(int idUsuario)
        {
            int idDeposito = -1;
            db.cmd.CommandText = " SELECT * FROM Usuario u, AlmacenxTienda at WHERE idUsuario = @idUsuario AND u.idTienda = at.idTienda ";
            db.cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

            SqlDataReader reader = db.cmd.ExecuteReader();

            if (reader.Read())
            {
                int posIdAlmacen = reader.GetOrdinal("idAlmacen");
                idDeposito = reader.IsDBNull(posIdAlmacen) ? -1 : reader.GetInt32(posIdAlmacen);
            }
            reader.Close();

            return idDeposito;
        }*/

        public int obtenerDeposito(int idTienda)
        {
            if (db.cmd.Transaction == null) db.conn.Open();
            int idDeposito = -1;
            db.cmd.CommandText = " SELECT * FROM Almacen WHERE idTienda = @idTienda AND tipo = 1 ";
            db.cmd.Parameters.AddWithValue("@idTienda", idTienda);

            SqlDataReader reader = db.cmd.ExecuteReader();

            if (reader.Read())
            {
                int posIdAlmacen = reader.GetOrdinal("idAlmacen");
                idDeposito = reader.IsDBNull(posIdAlmacen) ? -1 : reader.GetInt32(posIdAlmacen);
            }
            reader.Close();
            if (db.cmd.Transaction == null) db.conn.Close();
            return idDeposito;
        }


        public int Actualizar(Almacenes alm)
        {
            db.cmd.CommandText = "UPDATE Almacen SET codAlmacen=@codAlmacen WHERE idAlmacen=@idAlmacen";
            db.cmd.Parameters.AddWithValue("@codAlmacen", alm.CodAlmacen);
            db.cmd.Parameters.AddWithValue("@idAlmacen", alm.IdAlmacen);

            try
            {
                if (tipo) db.conn.Open();
                db.cmd.ExecuteNonQuery();
                if(tipo) db.conn.Close();
                db.cmd.Parameters.Clear();


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

       

    }
}