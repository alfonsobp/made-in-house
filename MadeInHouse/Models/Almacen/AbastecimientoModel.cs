using MadeInHouse.DataObjects;
using MadeInHouse.DataObjects.Almacen;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.Models.Almacen
{
    class AbastecimientoModel
    {
        public bool registrarAbastecimiento(int idUsuario, List<AbastecimientoProducto> prod)
        {
            DBConexion db = new DBConexion();
            db.conn.Open();
            SqlTransaction trans = db.conn.BeginTransaction();
            db.trans = trans;
            AbastecimientoSQL solSQL = new AbastecimientoSQL(db);
            AlmacenSQL almSQL = new AlmacenSQL(db);
            int idDeposito;
            int idSolicitud;

            if ((idDeposito = almSQL.obtenerDeposito(idUsuario)) > 0)
            {
                if ((idSolicitud = solSQL.insertarAbastecimiento(idDeposito)) > 0)
                {
                    if (solSQL.insertarProductosAbastecimiento(idSolicitud, prod))
                    {
                        MessageBox.Show("La solicitud fue generada con exito!");
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al agregar los productos");
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo crear la solicitud");
                }
            }
            else
            {
                MessageBox.Show("No se encontro el deposito del usuario");
            }

            trans.Rollback();
            return false;
        }
    }
}
