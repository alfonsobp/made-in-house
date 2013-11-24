using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;
using System.Data;
using System.Windows;

namespace MadeInHouse.DataObjects.Almacen
{
    class TiendaSQL
    {

        private DBConexion db;
        private bool tipo=true;

        public TiendaSQL(DBConexion db=null)
        {
               if (db == null)
            {
                this.db = new DBConexion();
            }
            else {
                this.db=db;
                tipo=false;
            }
        }


        public List<Tienda> BuscarTienda(string lstUbigeos=null,int idTienda=-1,int idUbigeo=-1, string admin=null )
        {
            List<Tienda> lstTienda = new List<Tienda>();
            string where = "WHERE 1=1";
            if (idTienda > 0)
            {
                where += " AND idTienda=@idTienda ";
                db.cmd.Parameters.AddWithValue("@idTienda", idTienda);
            }
            if (idUbigeo > 0)
            {
                where += " AND idUbigeo=@idUbigeo ";
                db.cmd.Parameters.AddWithValue("@idUbigeo", idUbigeo);
            }
            if (!String.IsNullOrEmpty(admin))
            {
                where += " AND administrador=@admin ";
                db.cmd.Parameters.AddWithValue("@admin", admin);
            }

            if (!String.IsNullOrEmpty(lstUbigeos))
            {
                where += " AND idUbigeo in ( " + lstUbigeos + " )";
            }

            db.cmd.CommandText = "SELECT * FROM Tienda " + where;


            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                while (reader.Read())
                {
                    Tienda t = new Tienda();
                    t.IdTienda = int.Parse(reader["idTienda"].ToString());
                    t.IdUbigeo= reader.IsDBNull(reader.GetOrdinal("idUbigeo")) ? -1: int.Parse(reader["idUbigeo"].ToString());
                    t.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();
                    t.Direccion = reader.IsDBNull(reader.GetOrdinal("direccion")) ? null :reader["direccion"].ToString();
                    t.Telefono= reader.IsDBNull(reader.GetOrdinal("telefono"))? null: reader["telefono"].ToString();
                    t.Administrador = reader.IsDBNull(reader.GetOrdinal("administrador")) ? null:reader["administrador"].ToString();
                    lstTienda.Add(t);
                }
                db.cmd.Parameters.Clear();
                db.conn.Close();
                reader.Close();


            }catch (SqlException e) 
            {
                Console.WriteLine(e);
                    return null;
            }
            catch (Exception e) {
                Console.WriteLine(e.StackTrace.ToString());
                return null;
            }




            return lstTienda;


        }



        public int AgregarTienda(Tienda p)
        {
            //db.cmd.CommandText = "sp_AgregarTienda";
            //db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.CommandText = "INSERT INTO Tienda (nombre,direccion,telefono,idUbigeo,estado,fechaReg) " +
                                "output INSERTED.idTienda "+
                                " VALUES (@nombre,@direccion,@telefono,@idUbigeo,@estado,@fechaReg)";
            db.cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            db.cmd.Parameters.AddWithValue("@direccion", p.Direccion);
            db.cmd.Parameters.AddWithValue("@telefono", p.Telefono);
            db.cmd.Parameters.AddWithValue("@idUbigeo", p.IdUbigeo);
            db.cmd.Parameters.AddWithValue("@estado", 1);
            db.cmd.Parameters.AddWithValue("@fechaReg", DateTime.Today);
            

            int idtienda=-1;

            try
            {
                if (tipo) db.conn.Open();


                idtienda= (int) db.cmd.ExecuteScalar();
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

            return idtienda;

        }

        public int obtenerTienda(int idUsuario)
        {
            int idTienda = -1;
            if (db.cmd.Transaction == null) db.conn.Open();
            db.cmd.CommandText = " SELECT idTienda FROM Usuario u WHERE idUsuario = @idUsuario ";
            db.cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
            object obj = db.cmd.ExecuteScalar();
            idTienda = String.IsNullOrEmpty(obj.ToString())? -1 : (int) obj;
            if (db.cmd.Transaction == null)
            {
                db.conn.Close();
                db.cmd.Parameters.Clear();
            }
            db.cmd.Parameters.Clear();
            return idTienda;
        }

        public Tienda obtenerTiendaPorId(int idTienda)
        {
            Tienda t = null;

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

           

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    t = new Tienda();

                    t.IdTienda = Int32.Parse(reader["idTienda"].ToString());
                    t.Nombre = reader["nombre"].ToString();
                    t.Direccion = reader["direccion"].ToString();
                    t.Administrador = reader["administrador"].ToString();
                    t.Telefono = reader["telefono"].ToString();
                    t.IdUbigeo = Int32.Parse(reader["idUbigeo"].ToString());
                    t.Estado = Int32.Parse(reader["estado"].ToString());
                    t.FechaReg = DateTime.Parse(reader["fechaReg"].ToString());
                }
                else
                    conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return t;
        }

        public int ActualizarTienda(Tienda t)
        {
            db.cmd.CommandText = "UPDATE Tienda SET nombre=@nombre , direccion=@direccion , idUbigeo=@idUbigeo , telefono=@telefono " +
                                 "WHERE idTienda=@idTienda ";
            db.cmd.Parameters.AddWithValue("@nombre", t.Nombre);
            db.cmd.Parameters.AddWithValue("@direccion", t.Direccion);
            db.cmd.Parameters.AddWithValue("@idUbigeo", t.IdUbigeo);
            db.cmd.Parameters.AddWithValue("@telefono", t.Telefono);
            db.cmd.Parameters.AddWithValue("@idTienda", t.IdTienda);

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
            return 1;
        }



        public double obtenerPrecioPorIdProd(int idProd, int idTienda)
        {
            SqlDataReader reader;

            db.cmd.CommandText = "SELECT * FROM ProductoxTienda WHERE idProducto = " + idProd.ToString() + " and idTienda = " + idTienda.ToString();


            try
            {
                db.conn.Open();
                reader = db.cmd.ExecuteReader();

                if (reader.Read())
                {
                    double p = Convert.ToDouble(reader["precioVenta"].ToString());
                    return p;
                }
                else
                {
                    db.conn.Close();
                    return 0;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return 0;
        }


    }
}
