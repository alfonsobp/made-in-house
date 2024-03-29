﻿using MadeInHouse.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.Models.Almacen
{
    class MotivoISManager : EntityManager
    {

        public int Agregar(object entity)
        {
            DBConexion db = new DBConexion();
            Motivo m = entity as Motivo;
            int k = 0;

            db.cmd.CommandText = "INSERT INTO MotivoIS(nombre) VALUES (@nombre)";
            db.cmd.Parameters.AddWithValue("@nombre", m.Nombre);

            try
            {
                db.conn.Open();

                k = db.cmd.ExecuteNonQuery();

                db.conn.Close();

            }
            catch (SqlException e)
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
