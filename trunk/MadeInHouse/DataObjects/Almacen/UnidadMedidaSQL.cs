using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MadeInHouse.Models.Almacen;
using System.Data;
using System.Windows;

namespace MadeInHouse.DataObjects.Almacen
{
    class UnidadMedidaSQL
    {
        private DBConexion db;
        private bool tipo = true;

        public UnidadMedidaSQL(DBConexion db = null)
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

        public int AgregarUnidadMedida(UnidadMedida u)
        {
            int k = 0;
            
            db.cmd.CommandText = "INSERT INTO UnidadMedida(nombre,estado) VALUES (@nombre,1)";
            db.cmd.Parameters.AddWithValue("@nombre", u.Nombre);

            try
            {
                if(tipo)db.conn.Open();

                k = db.cmd.ExecuteNonQuery();
                db.cmd.Parameters.Clear();
                if (tipo) db.conn.Close();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
        }

        public List<UnidadMedida> BuscarUnidadMedida()
        {

            List<UnidadMedida> listaUnidadMedidas = new List<UnidadMedida>();

            db.cmd.CommandText = "SELECT * FROM UnidadMedida WHERE estado=1";
           
            try
            {
                 if(tipo) db.conn.Open();
                 SqlDataReader reader = db.cmd.ExecuteReader();
                 while (reader.Read())
                {
                    UnidadMedida u = new UnidadMedida();
                    u.IdUnidad = reader.IsDBNull(reader.GetOrdinal("idUnidad")) ? -1 : (int)reader["idUnidad"];
                    u.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();
                    listaUnidadMedidas.Add(u);
                }
                 reader.Close();
                 db.cmd.Parameters.Clear();
                 if (tipo) db.conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return listaUnidadMedidas;
        }

        public int Eliminar(UnidadMedida tz)
        {
            int k = 0;

            db.cmd.CommandText = "UPDATE UnidadMedida SET estado=0 WHERE idUnidad = @idUnidad";
            db.cmd.Parameters.AddWithValue("@idUnidad", tz.IdUnidad);

            try
            {
                if(tipo) db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                db.cmd.Parameters.Clear();
                if (tipo) db.conn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }

            return k;

        }

        public string BuscarUnidadById(int idUnidad)
        {
            UnidadMedida u = null;
            db.cmd.CommandText = "select * from UnidadMedida where idUnidad=@idUnidad";
            db.cmd.Parameters.AddWithValue("@idUnidad", idUnidad);
            try
            {
                if (tipo) db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                while (reader.Read())
                {
                    u = new UnidadMedida();
                    u.IdUnidad = Convert.ToInt32(reader["idUnidad"].ToString());
                    u.Nombre = reader["nombre"].ToString();
                }
                reader.Close();
                db.cmd.Parameters.Clear();
                if (tipo) db.conn.Close();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
            return u.Nombre;
        }
    }
}
