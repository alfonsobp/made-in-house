using MadeInHouse.Models.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.DataObjects.Compras
{
    class OrdenCompraSQL:EntitySQL
    {
        public int Agregar(object entity)
        {
            DBConexion db = new DBConexion();
            int k = 0;
            OrdenCompra c = entity as OrdenCompra;
            MessageBox.Show(" " + c.Proveedor.IdProveedor + " " + c.MedioPago + " " + c.Observaciones);
            db.cmd.CommandText = "INSERT INTO OrdenCompra(idProveedor,fechaReg,medioPago,estado,observaciones) " +
                                 "VALUES (@idProveedor,@fechaReg,@medioPago,@estado,@observaciones)";

            db.cmd.Parameters.AddWithValue("@idProveedor", c.Proveedor.IdProveedor);
            db.cmd.Parameters.AddWithValue("@fechaReg", DateTime.Now);
            db.cmd.Parameters.AddWithValue("@medioPago", c.MedioPago);
            db.cmd.Parameters.AddWithValue("@observaciones", c.Observaciones);
            db.cmd.Parameters.AddWithValue("@estado", 1);
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


    }
}
