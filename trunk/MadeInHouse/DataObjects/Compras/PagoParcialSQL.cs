using MadeInHouse.Models.Compras;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.DataObjects.Compras
{
    class PagoParcialSQL: EntitySQL
    {
        public int Agregar(object entity)
        {
            DBConexion db = new DBConexion();
            int k = 0;
            PagoParcial p = entity as PagoParcial;

            db.cmd.CommandText = "INSERT INTO PagosParciales(idDocPago,monto,fechaPago) " +
                                 "VALUES (@idDocPago,@monto,@fechaPago)";

            db.cmd.Parameters.AddWithValue("@idDocPago", p.DocPago.IdDocPago);
            db.cmd.Parameters.AddWithValue("@monto", p.Monto);
            db.cmd.Parameters.AddWithValue("@fechaPago", p.FechaPago);

            try
            {
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                db.conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
        }

        public object Buscar(params object[] filters)
        {
            throw new NotImplementedException();
        }

        public int Actualizar(object entity)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(object entity)
        {
            throw new NotImplementedException();
        }

        public List<PagoParcial> BuscarPagos(DocPagoProveedor doc)
        {
            List<PagoParcial> lstPagos = new List<PagoParcial>();
            DBConexion db = new DBConexion();
            SqlDataReader reader;

            //MessageBox.Show("id = " + doc.IdDocPago + "\nprov = " + doc.Proveedor.RazonSocial);

            String where = "";
            where += " WHERE idDocPago = '" + doc.IdDocPago.ToString() + "' ";

            // MessageBox.Show("SELECT * FROM Proveedor WHERE  estado = 1 " + where);

            db.cmd.CommandText = "SELECT * FROM PagosParciales  " + where;
            db.cmd.CommandType = CommandType.Text;
            db.cmd.Connection = db.conn;

            try
            {
                db.conn.Open();
                reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {

                    PagoParcial p = new PagoParcial();

                    //ProveedorSQL eM = new ProveedorSQL();
                    //List<Proveedor> lstP = eM.Buscar(codProveedor) as List<Proveedor>;

                    p.IdPago = Convert.ToInt32(reader["idPago"].ToString());
                    p.DocPago = doc;
                    p.Monto = Convert.ToDouble(reader["monto"].ToString());
                    p.FechaPago = Convert.ToDateTime(reader["fechaPago"].ToString());

                    lstPagos.Add(p);
                }

                db.conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lstPagos;
        }
    }
}
