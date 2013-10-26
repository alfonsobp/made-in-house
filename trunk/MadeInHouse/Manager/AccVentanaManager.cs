using MadeInHouse.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace MadeInHouse.Manager
{
    class AccVentanaManager : EntityManager
    {

        public int Agregar(object entity)
        {
            throw new NotImplementedException();
        }

        public object Buscar(params object[] filters)
        {

            List<AccVentana> lstAccVentanaManager = new List<AccVentana>();

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM AccVentana ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    AccVentana av = new AccVentana();
                    
                    av.IdAccVentana = Int32.Parse(reader["idAccVentana"].ToString());
                    av.Descripcion = reader["descripcion"].ToString();
                    av.Estado = Int32.Parse(reader["estado"].ToString());
                    //av.AccModulo = ACCMODULO de SQL

                    lstAccVentanaManager.Add(av);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lstAccVentanaManager;

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
