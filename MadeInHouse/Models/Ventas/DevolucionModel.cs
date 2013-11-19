using MadeInHouse.DataObjects;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.DataObjects.Ventas;
using MadeInHouse.Dictionary;
using MadeInHouse.Models.Almacen;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Ventas
{
    class DevolucionModel
    {
        public string GenerarNotaCredito(Devolucion d, int num, List<DevolucionProducto> lstProductos)
        {
            GenerarPDF pdf = new GenerarPDF();
            string body = formatoBP(d, num, lstProductos).ToString();
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            pdf.createPDF(body, "\\Archivos\\NotasCredito\\Nota"+date+".pdf", true);
            return "\\Archivos\\NotasCredito\\Nota" + date + ".pdf";
        }

        public string formatoBP(Devolucion d, int num, List<DevolucionProducto> lstProductos)
        {
            double valorVenta = 0;
            double precioReal;
            num++;
            string content =
                @"<html>
                    <body>
                        <table>
                            <tr>
                                <td>
                                    <span style='text-align: center; text-decoration: underline; font-size: 1em; font-weight: bold'>MadeInHouse S.A.</span><br>
                                    <span style='text-align: center; font-size: 0.5em'>
                                        Av. Priority N° xxx - San Miguel - Lima<br>
                                        Telf: 999-9999<br>
                                        www.MadeInHouse.com Email: info@mih.com
                                    </span>
                                </td>
                                <td></td>
                                <td border=1>
                                    <span style='text-align: center; font-size: 1em'>
                                        R.U.C. N° XXXXXXXXXXX<br>
                                        NOTA DE CREDITO<br>
                                        001 - N° " + (d.NumDocCredito = num.ToString().PadLeft(6, '0'));
            content +=              "</span>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br>" +
                        "<table width=100%>" +
                            "<tr>" +
                                "<td width=200 border=1>";
            if (d.venta.IdCliente > 0)
            {
                ClienteSQL cSQL = new ClienteSQL();
                Cliente cli = cSQL.BuscarClienteByIdCliente(d.venta.IdCliente);
                content += "<span style='font-size: 0.5em'>" +
                                        "Señor (es): " + cli.ApePaterno + " " + cli.ApeMaterno + ", " + cli.Nombre + "<br>" +
                                        "Dirección : " + cli.Direccion + "<br>" +
                                        "R.U.C. N° : " + cli.Ruc +
                                    "</span>";
            }
            else
            {
                content +=          "<span style='font-size: 0.5em'>" +
                                        "DNI: " + d.venta.dni + "<br>" +
                                    "</span>";
            }
            content += "</td>" +
                                "<td width=100>" +
                                    "<table border=1 width=150 align=center>" +
                                        "<tr><td><span style='text-align: center; font-size: 0.5em'>FECHA DE EMISIÓN</span></td></tr>" +
                                        "<tr><td><span style='text-align: center; font-size: 0.5em'>" + DateTime.Now.ToString("dd/MM/yyyy") + "</span></td></tr>" +
                                    "</table>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br>" +
                        "<table border=1 height=700>" +
                            "<tr>" +
                                "<th><span style='font-size: 0.5em'>CODIGO</span></th>" +
                                "<th><span style='font-size: 0.5em'>CANTIDAD</span></th>" +
                                "<th><span style='font-size: 0.5em'>DESCRIPCION</span></th>" +
                                "<th><span style='font-size: 0.5em'>PRECIO</span></th>" +
                                "<th><span style='font-size: 0.5em'>SUB TOTAL</span></th>" +
                            "</tr>";
            if (lstProductos != null)
            {
                foreach (DevolucionProducto prod in lstProductos)
                {
                    if (prod.Devuelve > 0)
                    {
                        content += "<tr>" +
                                    "<td><span style='font-size: 0.5em'>" + prod.CodProducto + "</span></th>" +
                                    "<td><span style='font-size: 0.5em'>" + prod.Devuelve + "</span></th>" +
                                    "<td><span style='font-size: 0.5em'>" + prod.Producto + "</span></th>" +
                                    "<td><span style='font-size: 0.5em'>" + Math.Round(precioReal = (prod.Precio / 1.18), 2) + "</span></th>" +
                                    "<td><span style='font-size: 0.5em'>" + Math.Round(valorVenta += prod.Devuelve * precioReal, 2) + "</span></th>" +
                                    "</tr>";

                    }
                }
            }
            content += "</table>" +
                        "<br>" +
                        "<table border=1>" +
                            "<tr>" +
                                "<td><span style='font-size: 0.5em'>VALOR VENTA</span></td>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(valorVenta, 2) + "</span></td>" +
                                "<td><span style='font-size: 0.5em'>IGV</span></td>" +
                                "<td><span style='font-size: 0.5em'>" + Math.Round(0.18 * valorVenta, 2) + "</span></td>" +
                                "<td><span style='font-size: 0.5em'>TOTAL A PAGAR</span></td>" +
                                "<td><span style='font-size: 0.5em'>" + (d.Monto = Math.Round(1.18 * valorVenta, 2)) + "</span></td>" +
                            "</tr>" +
                        "</table>" +
                    "</body>" +
                "</html>";

            return content;
        }

        public string registrarDevolucion(Devolucion dev, List<DevolucionProducto> prod)
        {
            DBConexion db = new DBConexion();
            db.conn.Open();
            SqlTransaction trans = db.conn.BeginTransaction();
            db.cmd.Transaction = trans;
            DevolucionSQL devSQL = new DevolucionSQL(db);
            int idDevolucion;
            string message;

            if ((idDevolucion = devSQL.insertarDevolucion(dev)) > 0)
            {
                if (devSQL.insertarProductosDevolucion(idDevolucion, prod))
                {
                    trans.Commit();
                    return "La operacion fue exitosa";
                }
                else
                    message = "Hubo un error al agregar los productos";
            }
            else
                message = "No se pudo crear la solicitud";

            trans.Rollback();
            return message;
        }
    }
}
