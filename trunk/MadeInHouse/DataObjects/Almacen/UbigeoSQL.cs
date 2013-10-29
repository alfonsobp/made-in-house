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

        public UbigeoSQL(){
            db = new DBConexion();
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
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    ubigeo.IdUbigeo = reader.IsDBNull(reader.GetOrdinal("idUbigeo")) ? -1 : int.Parse(reader["idUbigeo"].ToString());
                    ubigeo.CodDist = reader.IsDBNull(reader.GetOrdinal("codDist")) ? null : reader["codDist"].ToString();
                    ubigeo.CodDpto = reader.IsDBNull(reader.GetOrdinal("codDpto")) ? null : reader["codDpto"].ToString();
                    ubigeo.CodProv = reader.IsDBNull(reader.GetOrdinal("codProv")) ? null : reader["codProv"].ToString();
                    ubigeo.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();

                }

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

            return ubigeo;
        }
    }
}
