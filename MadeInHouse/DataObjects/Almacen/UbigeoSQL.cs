using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.DataObjects.Almacen
{
    class UbigeoSQL
    {


        
        private DBConexion db;
        private bool tipo = true;

        public UbigeoSQL(DBConexion db = null){
            
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


        public List<Ubigeo> BuscarDpto()
        {
            List<Ubigeo> listaDpto = new List<Ubigeo>();
            
            string where = "WHERE codProv='00' AND codDist='00' ";

            
            db.cmd.CommandText = "SELECT * FROM Ubigeo " + where;
            

            try
            {
                 db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    Ubigeo p = new Ubigeo();
                    p.IdUbigeo = reader.IsDBNull(reader.GetOrdinal("idUbigeo")) ? -1 : int.Parse(reader["idUbigeo"].ToString());
                    p.CodDist = reader.IsDBNull(reader.GetOrdinal("codDist")) ? null : reader["codDist"].ToString();
                    p.CodDpto = reader.IsDBNull(reader.GetOrdinal("codDpto")) ? null : reader["codDpto"].ToString();
                    p.CodProv = reader.IsDBNull(reader.GetOrdinal("codProv")) ? null : reader["codProv"].ToString();
                    p.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();

                    listaDpto.Add(p);
                }

                reader.Close();
                db.conn.Close();
                
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return listaDpto;
        }
        
        public List<Ubigeo> BuscarProv(string codDpto)
        {
            List<Ubigeo> listaProv = new List<Ubigeo>();

            string where = "WHERE codDist='00' AND  codDpto=@codDpto AND codProv<>'00'";
            db.cmd.Parameters.AddWithValue("@codDpto", codDpto);

            db.cmd.CommandText = "SELECT * FROM Ubigeo " + where;
            Console.WriteLine(db.cmd.CommandText.ToString());

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    Ubigeo p = new Ubigeo();
                    p.IdUbigeo = reader.IsDBNull(reader.GetOrdinal("idUbigeo")) ? -1 : int.Parse(reader["idUbigeo"].ToString());
                    p.CodDist = reader.IsDBNull(reader.GetOrdinal("codDist")) ? null : reader["codDist"].ToString();
                    p.CodDpto = reader.IsDBNull(reader.GetOrdinal("codDpto")) ? null : reader["codDpto"].ToString();
                    p.CodProv = reader.IsDBNull(reader.GetOrdinal("codProv")) ? null : reader["codProv"].ToString();
                    p.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();

                    listaProv.Add(p);
                }
                db.cmd.Parameters.Clear();
                reader.Close();
                db.conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return listaProv;
        }

        public List<Ubigeo> BuscarDist(string codDpto,string codProv)
        {
            List<Ubigeo> listaDist = new List<Ubigeo>();

            string where = "WHERE codDpto=@codDpto AND codProv=@codProv  AND codDist<>'00' ";
            db.cmd.Parameters.AddWithValue("@codDpto", codDpto);
            db.cmd.Parameters.AddWithValue("@codProv", codProv);

            db.cmd.CommandText = "SELECT * FROM Ubigeo " + where;
            Console.WriteLine(db.cmd.CommandText.ToString());

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    Ubigeo p = new Ubigeo();
                    p.IdUbigeo = reader.IsDBNull(reader.GetOrdinal("idUbigeo")) ? -1 : int.Parse(reader["idUbigeo"].ToString());
                    p.CodDist = reader.IsDBNull(reader.GetOrdinal("codDist")) ? null : reader["codDist"].ToString();
                    p.CodDpto = reader.IsDBNull(reader.GetOrdinal("codDpto")) ? null : reader["codDpto"].ToString();
                    p.CodProv = reader.IsDBNull(reader.GetOrdinal("codProv")) ? null : reader["codProv"].ToString();
                    p.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();

                    listaDist.Add(p);
                }

                db.cmd.Parameters.Clear();
                reader.Close();
                db.conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return listaDist;
        }

        public List<Ubigeo> buscarUbigeo2(int idUbigeo=-1,string codDpto = null, string codProv = null, string codDist = null)
        {
            List<Ubigeo> lstUbigeo = new List<Ubigeo>();
            
            string where=" WHERE 1=1 ";
            if (idUbigeo > 0)
            {
                where +=" AND idUbigeo=@idUbigeo";
                db.cmd.Parameters.AddWithValue("@idUbigeo", idUbigeo);
            }

            if (!String.IsNullOrEmpty(codDpto)) {
                where += "AND codDpto=@codDpto ";
                db.cmd.Parameters.AddWithValue("@codDpto", codDpto);
            }

            if (!String.IsNullOrEmpty(codProv))
            {
                where += " AND codProv=@codProv ";
                db.cmd.Parameters.AddWithValue("@codProv", codProv);
            }

            if (!String.IsNullOrEmpty(codDist))
            {
                where += " AND codDist=@codDist";
                db.cmd.Parameters.AddWithValue("@codDist",codDist);
            }

            db.cmd.CommandText = "SELECT * FROM Ubigeo "+ where;

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                while (reader.Read())
                {
                    Ubigeo u = new Ubigeo();
                    u.IdUbigeo=int.Parse(reader["idUbigeo"].ToString());
                    u.CodDpto=reader["codDpto"].ToString();
                    u.CodProv=reader["codProv"].ToString();
                    u.CodDist=reader["codDist"].ToString();
                    u.Nombre = reader["nombre"].ToString();
                    lstUbigeo.Add(u);
                }
                db.cmd.Parameters.Clear();
                reader.Close();
                db.conn.Close();
            }catch (SqlException e) {
                Console.WriteLine(e);
                return null;
            }
            catch (Exception e) {
                Console.WriteLine(e.StackTrace.ToString());
                return null;
            }

            return lstUbigeo;

        }


        public Ubigeo buscarUbigeo(string codDpto, string codProv, string codDist)
        {

            Ubigeo ubigeo = new Ubigeo();

            string where = "WHERE codDpto=@codDpto AND codProv=@codProv AND codDist=@codDist";
            db.cmd.Parameters.AddWithValue("@codDpto", codDpto);
            db.cmd.Parameters.AddWithValue("@codProv", codProv);
            db.cmd.Parameters.AddWithValue("@codDist", codDist);

            db.cmd.CommandText = "SELECT * FROM Ubigeo " + where;
            Console.WriteLine(db.cmd.CommandText.ToString());

            try
            {
                if (tipo) db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    ubigeo.IdUbigeo = reader.IsDBNull(reader.GetOrdinal("idUbigeo")) ? -1 : int.Parse(reader["idUbigeo"].ToString());
                    ubigeo.CodDist = reader.IsDBNull(reader.GetOrdinal("codDist")) ? null : reader["codDist"].ToString();
                    ubigeo.CodDpto = reader.IsDBNull(reader.GetOrdinal("codDpto")) ? null : reader["codDpto"].ToString();
                    ubigeo.CodProv = reader.IsDBNull(reader.GetOrdinal("codProv")) ? null : reader["codProv"].ToString();
                    ubigeo.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();

                }

                reader.Close();
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

            return ubigeo;
        }
    }
}
