using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.DataObjects.Reportes
{
    public class notas
    {
        int idnota;
        string tiponota;
        int idalmacen;
        string nombrealmacen;
        string motivo;
        string codproducto;
        string nombreproducto;
        string fechareg;
        int cantidad;

        public int IdNota { get { return idnota; } set { idnota = value; } }
        public string TipoNota { get { return tiponota; } set { tiponota = value; } }
        public int IdAlmacen { get { return idalmacen; } set { idalmacen = value; } }
        public string NombreAlmacen { get { return nombrealmacen; } set { nombrealmacen = value; } }
        public string Motivo { get { return motivo; } set { motivo = value; } }
        public string CodProducto { get { return codproducto; } set { codproducto = value; } }
        public string NombreProducto { get { return nombreproducto; } set { nombreproducto = value; } }
        public string FechaReg { get { return fechareg; } set { fechareg = value; } }
        public int Cantidad { get { return cantidad; } set { cantidad = value} }
    }


    class reporteKardexSQL
    {
        public static List<notas> BuscarEntradaSalida(int almacen, int producto)
        {
            List<notas> lstVenta = new List<notas>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT nis.idNota   ,CASE nis.tipo WHEN '1' THEN 'ENTRADA' ELSE 'SALIDA' END AS tipoNota , nis.idAlmacen,a.nombre as nombreAlmacen,m.nombre as Motivo ,p.codProducto,p.nombre as nombreProducto,nis.fechaReg,pnis.cantidad FROMProductoxNotaIS pnis  INNER JOIN  NotaIS nis  ON pnis.idNota = nis.idNota INNER JOIN MotivoIS m ON nis.idMotivo =  m.idMotivo INNER JOIN Producto p ON  pnis.idProducto = p.idProducto INNER JOIN Almacen a ON   nis.idAlmacen = a.idAlmacen where nis.idalmacen = "+almacen+" and p.idproducto = " + producto;

            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    notas e = new notas();
                    e.IdNota = Convert.ToInt32(reader["idNota"]);
                    e.TipoNota = reader["tipoNota"].ToString();
                    e.IdAlmacen = Convert.ToInt32(reader["idAlmacen"]);
                    e.NombreAlmacen = reader["nombreAlmacen"].ToString();
                    e.Motivo = reader["Motivo"].ToString();
                    e.CodProducto = reader["codProducto"].ToString();
                    e.NombreProducto = reader["nombreProducto"].ToString();
                    e.FechaReg = reader["fechaReg"].ToString();
                    e.Cantidad = Convert.ToInt32(reader["cantidad"]);

                    lstVenta.Add(e);

                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return lstVenta;
        }
    }
}
