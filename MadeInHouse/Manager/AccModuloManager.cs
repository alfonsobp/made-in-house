using MadeInHouse.Model.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.Manager.Seguridad
{
    class AccModuloManager:EntityManager
    {


        public int Agregar(object entity)
        {
            throw new NotImplementedException();
        }

        public object Buscar(params object[] filters)
        {
            List<AccModulo> lstAccModulo = new List<AccModulo>();
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            //SqlCommand cmd = new SqlCommand();
            DBConexion db = new DBConexion();
            SqlDataReader reader;
            
            db.cmd.CommandText = "SELECT * FROM AccModulo ";
            db.cmd.CommandType = CommandType.Text;
            //db.cmd.Connection = conn;

            try
            {
                db.conn.Open();

                reader = db.cmd.ExecuteReader();


                while (reader.Read())
                {

                    AccModulo am = new AccModulo();

                    am.IdAccModulo = Convert.ToInt32(reader["idAccModulo"].ToString());
                    am.Descripcion = reader["descripcion"].ToString();
                    am.Estado = Int32.Parse(reader["estado"].ToString());

                    lstAccModulo.Add(am);
                }

                db.conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lstAccModulo;

        }

        public int Actualizar(object entity)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
