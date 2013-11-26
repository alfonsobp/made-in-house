using MadeInHouse.DataObjects;
using MadeInHouse.DataObjects.Almacen;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.Models.Almacen
{
    class AbastecimientoModel
    {
        public string registrarAbastecimiento(int idUsuario, List<AbastecimientoProducto> prod, out int idSolicitud)
        {
            DBConexion db = new DBConexion();
            db.conn.Open();
            SqlTransaction trans = db.conn.BeginTransaction();
            db.cmd.Transaction = trans;
            AbastecimientoSQL solSQL = new AbastecimientoSQL(db);
            TiendaSQL tiendaSQL = new TiendaSQL(db);
            int idTienda;
            idSolicitud = 0;
            string message;

            if ((idTienda = tiendaSQL.obtenerTienda(idUsuario)) > 0)
            {
                if ((idSolicitud = solSQL.insertarAbastecimiento(idTienda)) > 0)
                {
                    if (solSQL.insertarProductosAbastecimiento(idSolicitud, prod))
                    {
                        trans.Commit();
                        return "La operacion fue exitosa";
                    }
                    else
                        message = "Hubo un error al agregar los productos";
                }
                else
                    message = "No se pudo crear la solicitud";
            }
            else
                message = "No se encontro el deposito del usuario";

            trans.Rollback();
            return message;
        }

        public string editarAbastecimiento(int idUsuario, int idSolicitud, List<AbastecimientoProducto> prod)
        {
            DBConexion db = new DBConexion();
            db.conn.Open();
            SqlTransaction trans = db.conn.BeginTransaction();
            db.cmd.Transaction = trans;
            AbastecimientoSQL solSQL = new AbastecimientoSQL(db);
            string message;

            if (solSQL.actualizarAbastecimiento(idSolicitud, idUsuario) > 0)
            {
                if (solSQL.eliminarProductosAbastecimiento(idSolicitud) >= 0)
                {
                    if (solSQL.insertarProductosAbastecimiento(idSolicitud, prod))
                    {
                        trans.Commit();
                        db.conn.Close();
                        return "La operacion fue exitosa";
                    }
                    else
                        message = "Hubo un error al agregar los productos";
                }
                else
                    message = "No se pudo eliminar los productos";
            }
            else
                message = "No se pudo actualizar la solicitud";

            trans.Rollback();
            db.conn.Close();
            return message;
        }

        public string atenderAbastecimiento(int idUsuario, int idSolicitud, List<AbastecimientoProducto> prod)
        {
            DBConexion db = new DBConexion();
            db.conn.Open();
            SqlTransaction trans = db.conn.BeginTransaction();
            db.cmd.Transaction = trans;
            AbastecimientoSQL solSQL = new AbastecimientoSQL(db);
            string message;

            if (solSQL.atenderAbastecimiento(idSolicitud, idUsuario) > 0)
            {
                if (solSQL.eliminarProductosAbastecimiento(idSolicitud) >= 0)
                {
                    if (solSQL.insertarProductosAbastecimiento(idSolicitud, prod))
                    {
                        trans.Commit();
                        db.conn.Close();
                        return "La operacion fue exitosa";
                    }
                    else
                        message = "Hubo un error al agregar los productos";
                }
                else
                    message = "No se pudo eliminar los productos";
            }
            else
                message = "No se pudo crear la solicitud";

            trans.Rollback();
            db.conn.Close();
            return message;
        }

        public string consolidarSolicitudes(List<Abastecimiento> solicitudes)
        {
            DBConexion db = new DBConexion();
            db.conn.Open();
            SqlTransaction trans = db.conn.BeginTransaction();
            db.cmd.Transaction = trans;
            AdquisicionSQL adSQL = new AdquisicionSQL(db);
            AbastecimientoSQL abSQL = new AbastecimientoSQL(db);
            List<AbastecimientoProducto> prod = new List<AbastecimientoProducto>();
            List<AbastecimientoProducto> temp = new List<AbastecimientoProducto>();
            int idSolicitud;
            int index;
            string message ="";
            try
            {
                if ((idSolicitud = adSQL.insertarAdquisicion(solicitudes.First().idTienda)) > 0)
                {
                    foreach (Abastecimiento sol in solicitudes)
                    {
                        temp = abSQL.buscarProductosAbastecimiento(sol.idSolicitudAB);

                        foreach (AbastecimientoProducto item in temp)
                        {
                            if ((index = prod.FindIndex(x => x.idProducto == item.idProducto)) >= 0)
                            {
                                prod.ElementAt(index).atendido += item.atendido;
                                prod.ElementAt(index).pedido += item.pedido;
                            }
                            else
                            {
                                prod.Add(item);
                            }
                        }

                        abSQL.actualizarAbastecimiento(sol.idSolicitudAB, Int32.Parse(Thread.CurrentPrincipal.Identity.Name), 4);
                    }

                    if (adSQL.insertarProductosAdquisicion(idSolicitud, prod) > 0)
                    {
                        trans.Commit();
                        db.conn.Close();
                        return "La operacion fue exitosa";
                    }
                    else
                        message = "Hubo un error al agregar los productos";
                }
                else
                    message = "No se pudo crear la solicitud";
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace.ToString());
            }
            trans.Rollback();
            db.conn.Close();
            return message;
        }
    }
}