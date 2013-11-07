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
                    t.IdUbigeo= int.Parse(reader["idUbigeo"].ToString());
                    t.Nombre = reader["nombre"].ToString();
                    t.Direccion = reader["direccion"].ToString();
                    t.Telefono= reader["telefono"].ToString();
                    t.Administrador = reader["administrador"].ToString();
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
            db.cmd.CommandText = "INSERT INTO Tienda (nombre,direccion,administrador,telefono,idUbigeo,estado,fechaReg) " +
                                "output INSERTED.idTienda "+
                                " VALUES (@nombre,@direccion,@admin,@telefono,@idUbigeo,@estado,@fechaReg)";
            db.cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            db.cmd.Parameters.AddWithValue("@direccion", p.Direccion);
            db.cmd.Parameters.AddWithValue("@admin", p.Administrador);
            db.cmd.Parameters.AddWithValue("@telefono", p.Telefono);
            db.cmd.Parameters.AddWithValue("@idUbigeo", p.IdUbigeo);
            db.cmd.Parameters.AddWithValue("@estado", 1);
            db.cmd.Parameters.AddWithValue("@fechaReg", DateTime.Today);
            //db.cmd.Parameters.Add("@idTienda", SqlDbType.Int).Direction = ParameterDirection.Output;

            int idtienda=-1;

            try
            {
                if (tipo) db.conn.Open();


                idtienda= (int) db.cmd.ExecuteScalar();
               // idtienda = Convert.ToInt32(db.cmd.Parameters["@idTienda"].Value);
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

    }
}
