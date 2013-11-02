﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Data;
using MadeInHouse.Views.Compras;
using System.Windows;
using System.Data.OleDb;
using System.Collections.ObjectModel;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.DataObjects;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Compras;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Collections;

namespace MadeInHouse.ViewModels.Compras
{
    class NuevaCotizacionViewModel:PropertyChangedBase
    {

        //Constructores de la clase

        public NuevaCotizacionViewModel()
        {
            indicador = 1;
        }

        public NuevaCotizacionViewModel(BuscarCotizacionViewModel m)
        {
            model = m;
            indicador = 1;
        }

        public NuevaCotizacionViewModel(Cotizacion c, BuscarCotizacionViewModel m)
        {
            prov.CodProveedor = c.Proveedor.CodProveedor;
            model = m;
            indicador = 2;
        }


        
        
        //Atributos de la clase

        // 1 = insertar, 2 = editar
        private int indicador;

        UtilesSQL usql = new UtilesSQL();

        int Id;

        BuscarCotizacionViewModel model;

        CotizacionxProductoSQL csql = new CotizacionxProductoSQL();



        private string path = "";

        public string Path
        {
            get { return path; }
            set { path = value; NotifyOfPropertyChange(() => Path); }
        }

        private Proveedor prov;

        public Proveedor Prov
        {
            get { return prov; }
            set { prov = value; NotifyOfPropertyChange(() => Prov); }
        }

        private DateTime txtFechaResp = DateTime.Now;

        public DateTime TxtFechaResp
        {
            get { return txtFechaResp; }
            set { txtFechaResp = value; NotifyOfPropertyChange(() => TxtFechaResp); }
        }

        private DateTime txtFechaIni = DateTime.Now;

        public DateTime TxtFechaIni
        {
            get { return txtFechaIni; }
            set { txtFechaIni = value; NotifyOfPropertyChange(() => TxtFechaIni); }
        }

        private DateTime txtFechaFin = DateTime.Now;

        public DateTime TxtFechaFin
        {
            get { return txtFechaFin; }
            set { txtFechaFin = value; NotifyOfPropertyChange(() => TxtFechaFin); }
        }

        private string txtObservacion;

        public string TxtObservacion
        {
            get { return txtObservacion; }
            set { txtObservacion = value; NotifyOfPropertyChange(() => TxtObservacion); }
        }

        List<CotizacionxProducto> lstProducto;

        public List<CotizacionxProducto> LstProducto
        {
            get { return lstProducto; }
            set { lstProducto = value; NotifyOfPropertyChange(() => LstProducto); }
        }





        //Funciones de la clase

        public void BuscarProveedor()
        {
            MadeInHouse.Models.MyWindowManager w = new MadeInHouse.Models.MyWindowManager();
            w.ShowWindow(new BuscadorProveedorViewModel(this));
        }

        

        public void GuardarCotizacion()
        {
            int k, y, i;

            Cotizacion c = new Cotizacion();
            c.Proveedor = new Proveedor();

            MessageBox.Show("idProv = " + prov.IdProveedor + " codProv = " + prov.CodProveedor + " rs = " + prov.RazonSocial);

            c.Proveedor.IdProveedor = prov.IdProveedor;
            c.Proveedor.RazonSocial = prov.RazonSocial;
            c.Proveedor.CodProveedor = prov.CodProveedor;

            c.FechaInicio = TxtFechaIni;
            c.FechaFin = TxtFechaFin;
            c.FechaRespuesta = TxtFechaResp;
            c.Observacion = TxtObservacion;

            if (c.Proveedor.CodProveedor != null)
            {
                Id = usql.ObtenerMaximoID("Cotizacion", "idCotizacion");
                int numCot = 1000000 + Id + 1;
                string codCotizacion = "COT-" + numCot.ToString();

                if (indicador == 1)
                {
                    k = new CotizacionSQL().Agregar(c);
                    Id = usql.ObtenerMaximoID("Cotizacion", "idCotizacion");

                    if (LstProducto != null)
                    {
                        for (i = 0; i < LstProducto.Count; i++)
                        {
                            LstProducto[i].IdCotizacion = Id;
                            y = csql.InsertarValidado(LstProducto[i]);
                        }
                    }

                    if (k == 0)
                        MessageBox.Show("Ocurrio un error");
                    else
                        MessageBox.Show("Cotizacion Registrada \n\nCodigo = " + codCotizacion + "\nProveedor = " + c.Proveedor.RazonSocial +
                                        " ("+ c.Proveedor.CodProveedor + ")" + "\nFecha registro = " + c.FechaRespuesta.ToString());
                }

                if (indicador == 2)
                {

                    k = new CotizacionSQL().Actualizar(c);

                    if (k == 0)
                        MessageBox.Show("Ocurrio un error");
                    else
                        MessageBox.Show("Cotizacion Registrada \n\nCodigo = " + codCotizacion + "\nProveedor = " + c.Proveedor.RazonSocial +
                                        " ("+ c.Proveedor.CodProveedor + ")" + "\nFecha respuesta = " + c.FechaRespuesta.ToString() + "\nFecha inicio = " + c.FechaInicio.ToString() +
                                        "\nFecha fin = " + c.FechaFin.ToString());

                }

                if (model != null)
                    model.ActualizarCotizacion();
            }

        }


        //Funcion para mandar correo al proveedor
        public void EnviarCorreo()
        {

            if ((path != "") && (prov.Email != null))
            {

                string time;

                if (DateTime.Now.Hour >= 12)
                    time = "tardes";
                else
                    time = "dias";

                string bodyMessage = "Buenos/as " + time + " empresa " + prov.RazonSocial + "\nsolicito una cotizacion de los" +
                                     " siguientes productos adjuntos, \nSaludos";


                // Create a message and set up the recipients.

                MailMessage message = new MailMessage("alonsodp2692@gmail.com", prov.Email,
                                                      "Cotizacion de productos", bodyMessage);

                message.IsBodyHtml = true;

                MessageBoxResult r = MessageBox.Show("Desea enviar el correo al proveedor " + prov.RazonSocial + " ?", "EnviarCorreo", 
                                                     MessageBoxButton.YesNo);

                if (r == MessageBoxResult.Yes)
                {

                    // Create  the file attachment for this e-mail message.
                    Attachment data = new Attachment(path, MediaTypeNames.Application.Octet);

                    // Add time stamp information for the file.
                    ContentDisposition disposition = data.ContentDisposition;
                    disposition.CreationDate = System.IO.File.GetCreationTime(path);
                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(path);
                    disposition.ReadDate = System.IO.File.GetLastAccessTime(path);

                    // Add the file attachment to this e-mail message.
                    message.Attachments.Add(data);


                    //Send the message.

                    var client = new SmtpClient("smtp.gmail.com", 587)
                    {
                        // Add credentials if the SMTP server requires them.
                        Credentials = new NetworkCredential("alonsodp2692@gmail.com", "adp980407912"),
                        EnableSsl = true
                    };


                    try
                    {
                        client.Send(message);
                        MessageBox.Show("Mensaje enviado satisfactoriamente");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.StackTrace.ToString());
                    }
                }

            }

            else
                MessageBox.Show("Ingrese los productos a cotizar");

        }


        //Funciones para importar desde un Excel

        public void Cargar()
        {

            if (path != "")
            {

                List<CotizacionxProducto> lista = new List<CotizacionxProducto>();

                String name = "Cotizaciones";
                String constr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;Persist Security Info=False";
                OleDbConnection con = new OleDbConnection(constr);
                OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
                con.Open();

                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable data = new DataTable();
                sda.Fill(data);
                DataTableReader ds = data.CreateDataReader();

                while (ds.Read())
                {

                    CotizacionxProducto cp = new CotizacionxProducto();

                    cp.Producto = new Producto();
                    cp.Producto.CodigoProd = ds["Codigo"].ToString();
                    cp.Producto.Nombre = ds["Descripcion"].ToString();
                    cp.Cantidad = Convert.ToInt32(ds["Cantidad"].ToString());

                    string valPrecio = ds["Precio"].ToString();

                    if (valPrecio != "")
                        cp.Precio = Convert.ToDouble(ds["Precio"].ToString());


                    lista.Add(cp);

                }

                LstProducto = lista;
            }


        }

        public void BuscarPath()
        {

            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.xlsx)|*.xlsx";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                Path = filename;

            }

        }

        public void Importar()
        {

            BuscarPath();

            MessageBoxResult r = MessageBox.Show("Desea Importar el Archivo ? ", "Importar", MessageBoxButton.YesNo);

            if (r == MessageBoxResult.Yes)
            {

                Cargar();
            }


        }



       
    }
}
