﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;
using Caliburn.Micro;
using MadeInHouse.Models.Ventas;
using MadeInHouse.ViewModels.Almacen;
using MadeInHouse.Models.Seguridad;

namespace MadeInHouse.DataObjects.Almacen
{
    class GuiaDeRemisionSQL
    {
        public List<GuiaRemision> BuscarGuiaDeRemision(string codigo, int estado, string tipo)
        {

            DBConexion db = new DBConexion();
            List<GuiaRemision> lstGuiaDeRemision = new List<GuiaRemision>();
            SqlDataReader reader;
            String where = "";

            if (!String.IsNullOrEmpty(codigo))
            {
                    where += " and codGuiaRem = '" + codigo.ToString() + "' ";
            }

            if (estado != 0)
            {

                where += " and estado = " + estado;

            }


            if (!String.IsNullOrEmpty(tipo))
            {

                where += " and tipo = '" + tipo.ToString() + "' ";
            }



            db.cmd.CommandText = "SELECT * FROM GuiaRemision WHERE estado >= 0 " + where;
            db.cmd.CommandType = CommandType.Text;

            try
            {
                db.conn.Open();

                reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {

                    GuiaRemision g = new GuiaRemision();
                    g.Almacen = new Almacenes();

                    g.IdGuia = Convert.ToInt32(reader["idGuia"].ToString());
                    g.CodGuiaRem = reader["codGuiaRem"].ToString();
                    g.Conductor = reader["conductor"].ToString();
                    g.FechaTraslado = Convert.ToDateTime(reader["fechaTraslado"].ToString());
                    g.FechaReg = Convert.ToDateTime(reader["fechaReg"].ToString());
                    g.Camion = reader["camion"].ToString();
                    g.Tipo = reader["tipo"].ToString();
                    g.Observaciones = reader["observaciones"].ToString();
                    g.Estado = Convert.ToInt32(reader["estado"].ToString());

                    if (!reader.IsDBNull(reader.GetOrdinal("idAlmacen")))                    
                    {
                        int idAlmacen = Convert.ToInt32(reader["idAlmacen"].ToString());
                        int idNota = Convert.ToInt32(reader["idNota"].ToString());
                        g.Almacen = getALMfromIDAlm(idAlmacen);
                        g.Nota = getNOTAfromIDnota(idNota);
                        g.AlmOrigen = getALMfromIDAlm(g.Nota.IdAlmacen);
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("idOrdenDespacho")))
                    {
                        int idOrd = Convert.ToInt32(reader["idOrdenDespacho"].ToString());
                        g.Orden = getORDENfromIDorden(idOrd);

                        Venta v = new MantenerGuiaDeRemisionViewModel().getVentafromID(g.Orden.Venta.IdVenta);
                        Usuario u = new MantenerGuiaDeRemisionViewModel().getUsuariofromID(v.IdUsuario);
                        Cliente c = new MantenerGuiaDeRemisionViewModel().getClientefromID(v.IdCliente);
                        g.AlmOrigen = new MantenerGuiaDeRemisionViewModel().getAlmacenfromIDTienda(u.IdTienda);
                    }

                    if (idNota != 0) {
                        g.Nota = getNOTAfromIDnota(idNota);
                    }
                    if (idAlmacen != 0) {
                        g.Almacen = getALMfromIDAlm(idAlmacen);
                    }
                    lstGuiaDeRemision.Add(g);
                }

                db.conn.Close();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return lstGuiaDeRemision;

        }

        public Boolean BuscarIDNota(int id)
        {

            DBConexion db = new DBConexion();
            List<GuiaRemision> lstGuiaDeRemision = new List<GuiaRemision>();
            SqlDataReader reader;
            
            db.cmd.CommandText = "SELECT * FROM GuiaRemision WHERE idNota = " + id.ToString();
            db.cmd.CommandType = CommandType.Text;

            try
            {
                db.conn.Open();

                reader = db.cmd.ExecuteReader();

                if (reader.Read())
                {
                        int idNota = Convert.ToInt32(reader["idNota"].ToString());
                        return true;
                    
                }

                db.conn.Close();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return false;

        }


        public Boolean BuscarIDOrden(int id)
        {

            DBConexion db = new DBConexion();
            List<GuiaRemision> lstGuiaDeRemision = new List<GuiaRemision>();
            SqlDataReader reader;

            db.cmd.CommandText = "SELECT * FROM GuiaRemision WHERE idOrden = " + id;
            db.cmd.CommandType = CommandType.Text;

            try
            {
                db.conn.Open();

                reader = db.cmd.ExecuteReader();

                if (reader.Read())
                {
                        int idOrd = Convert.ToInt32(reader["idOrdenDespacho"].ToString());
                        return true; 
                }


                db.conn.Close();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return false;

        }



        public int agregarGuiaDeRemision(GuiaRemision g)
        {
            DBConexion db = new DBConexion();
            int k = 0;

            /*MessageBox.Show("\ncod = " + g.CodGuiaRem + "\ncamion = " + g.Camion + "\nconductor = " + g.Conductor + "\nfechaR = " + g.FechaReg +
                            "\nfechaT = " + g.FechaTraslado + "\ntipo = " + g.Tipo + "\nobs = " + g.Observaciones + "\nidAlm = " + g.Almacen.IdAlmacen +
                            "\nidNota = " + g.Nota.IdNota);*/

            if (g.Nota != null)
            {
                db.cmd.CommandText =
                "INSERT INTO GuiaRemision(codGuiaRem,camion,conductor,fechaReg,fechaTraslado,tipo,observaciones,estado,idAlmacen,idNota) " +
                "VALUES (@codGuiaRem,@camion,@conductor,@fechaReg,@fechaTraslado,@tipo,@observaciones,@estado,@idAlmacen,@idNota)";


                db.cmd.Parameters.AddWithValue("@codGuiaRem", g.CodGuiaRem);
                db.cmd.Parameters.AddWithValue("@camion", g.Camion);
                db.cmd.Parameters.AddWithValue("@conductor", g.Conductor);
                db.cmd.Parameters.AddWithValue("@fechaReg", g.FechaReg);
                db.cmd.Parameters.AddWithValue("@fechaTraslado", g.FechaTraslado);
                db.cmd.Parameters.AddWithValue("@tipo", g.Tipo);
                db.cmd.Parameters.AddWithValue("@observaciones", g.Observaciones);
                db.cmd.Parameters.AddWithValue("@estado", 1);
                db.cmd.Parameters.AddWithValue("@idAlmacen", g.Almacen.IdAlmacen);
                db.cmd.Parameters.AddWithValue("@idNota", g.Nota.IdNota);
            }


            if (g.Orden != null)
            {
                db.cmd.CommandText =
                "INSERT INTO GuiaRemision(codGuiaRem,camion,conductor,fechaReg,fechaTraslado,tipo,observaciones,estado,idOrdenDespacho) " +
                "VALUES (@codGuiaRem,@camion,@conductor,@fechaReg,@fechaTraslado,@tipo,@observaciones,@estado,@idOrdenDespacho)";


                db.cmd.Parameters.AddWithValue("@codGuiaRem", g.CodGuiaRem);
                db.cmd.Parameters.AddWithValue("@camion", g.Camion);
                db.cmd.Parameters.AddWithValue("@conductor", g.Conductor);
                db.cmd.Parameters.AddWithValue("@fechaReg", g.FechaReg);
                db.cmd.Parameters.AddWithValue("@fechaTraslado", g.FechaTraslado);
                db.cmd.Parameters.AddWithValue("@tipo", g.Tipo);
                db.cmd.Parameters.AddWithValue("@observaciones", g.Observaciones);
                db.cmd.Parameters.AddWithValue("@estado", 1);
                db.cmd.Parameters.AddWithValue("@idOrdenDespacho", g.Orden.IdOrdenDespacho);
            }

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

        public int editarGuiaDeRemision(GuiaRemision g)
        {
            DBConexion db = new DBConexion();
            int k = 0;

            db.cmd.CommandText = "UPDATE GuiaRemision  " +
            "SET camion= @camion, conductor= @conductor, fechaTraslado= @fechaTraslado, observaciones= @observaciones, estado= @estado " +
            " WHERE idGuia= @idGuia ";

            db.cmd.Parameters.AddWithValue("@idGuia", g.IdGuia);
            db.cmd.Parameters.AddWithValue("@camion", g.Camion);
            db.cmd.Parameters.AddWithValue("@conductor", g.Conductor);
            db.cmd.Parameters.AddWithValue("@fechaTraslado", g.FechaTraslado);
            db.cmd.Parameters.AddWithValue("@observaciones", g.Observaciones);
            db.cmd.Parameters.AddWithValue("@estado", g.Estado);

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

        public Almacenes getALMfromIDAlm(int id)
        {
            List<Almacenes> list = new AlmacenSQL().BuscarAlmacen();
            for (int i = 0; i < list.Count; i++)
                if (list[i].IdAlmacen == id)
                {
                    return list[i];
                }

            return null;
        }

        public NotaIS getNOTAfromIDnota(int id)
        {
            List<NotaIS> list = new NotaISSQL().BuscarNotas();
            for (int i = 0; i < list.Count; i++)
                if (list[i].IdNota == id)
                {
                    return list[i];
                }

            return null;
        }

        public OrdenDespacho getORDENfromIDorden(int id)
        {
            List<OrdenDespacho> list = new OrdenDespachoSQL().BuscarOrdenDespacho(-1, -1);
            for (int i = 0; i < list.Count; i++)
                if (list[i].IdOrdenDespacho == id)
                {
                    return list[i];
                }

            return null;
        }

    }
}
