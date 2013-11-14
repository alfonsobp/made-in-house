using MadeInHouse.Models.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Views.Compras;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.Models;
using System.Data.OleDb;
using System.Data;
using MadeInHouse.Dictionary;
namespace MadeInHouse.ViewModels.Compras
{
    class BuscarOrdenCompraViewModel:Screen
    {
        bool estado = true;

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        
        string idOrdenCompra;

        public string IdOrdenCompra
        {
            get { return idOrdenCompra; }
            set { idOrdenCompra = value; NotifyOfPropertyChange("IdOrdenCompra"); }
        }
        string rSProveedor;

        public string RSProveedor
        {
            get { return rSProveedor; }
            set { rSProveedor = value; NotifyOfPropertyChange("RSProveedor"); }
        }

        List<String> lstEstados = new List<String>() { "TODOS", "CANCELADA", "PRE EMITIDA", "EN EJECUCION", "FINALIZADA" };

        public List<String> LstEstados
        {
            get { return lstEstados; }
            set { lstEstados = value; NotifyOfPropertyChange("LstEstados"); }
        }
        string selectedEstado = "TODOS";

        public string SelectedEstado
        {
            get { return selectedEstado; }
            set { selectedEstado = value; NotifyOfPropertyChange("SelectedEstado"); }
        }

        DateTime fechaIni = new DateTime(DateTime.Now.Year, 1, 1);

        public DateTime FechaIni
        {
            get { return fechaIni; }
            set { fechaIni = value; NotifyOfPropertyChange("FechaIni"); }
        }
        DateTime fechaFin = new DateTime(DateTime.Now.Year, 12, 31);

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; NotifyOfPropertyChange("FechaFin"); }
        }

        public void Limpiar() {
            FechaIni = new DateTime(DateTime.Now.Year, 1, 1);
            FechaFin = new DateTime(DateTime.Now.Year, 12, 31);
            SelectedEstado = "TODOS";
            IdOrdenCompra = "";
            RSProveedor = "";
        }

        public void Actualizar() {


            LstOrdenes = new OrdenCompraSQL().Buscar(IdOrdenCompra, RSProveedor, getEstado(SelectedEstado), FechaIni, FechaFin) as List<OrdenCompra>;
        
        }

        public void Eliminar() {

            if (OrdenSelected != null)
            {
                if (OrdenSelected.Estado == 1)
                {
                    new OrdenCompraSQL().Eliminar(OrdenSelected);
                    MessageBox.Show("Se ha CANCELADO la orden de compra..", "AVISO", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Actualizar();

                }
                else
                {
                    MessageBox.Show("Solo puede CANCELAR Ordenes de compra NO EMITIDAS..", "AVISO", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                  
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado ninguna Orden de compra ..", "AVISO", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            
            }
        }

        public int getEstado(string path) {

            if (path == "TODOS") return 4;
            if (path == "CANCELADA") return 0;
            if(path == "PRE EMITIDA") return 1;
            if(path == "EN EJECUCION") return 2;
            if (path == "FINALIZADA") return 3;

            return 4;
        }

        OrdenCompra ordenSelected = null;

        public  OrdenCompra OrdenSelected
        {
            get { return ordenSelected; }
            set { ordenSelected = value; NotifyOfPropertyChange("OrdenSelected"); }
        }

        List<OrdenCompra> lstOrdenes;

        public List<OrdenCompra> LstOrdenes
        {
            get { return lstOrdenes; }
            set { lstOrdenes = value; NotifyOfPropertyChange("LstOrdenes"); }
        }


        public void Buscar() { 
        
        LstOrdenes =   new OrdenCompraSQL().Buscar(IdOrdenCompra, RSProveedor, getEstado(SelectedEstado), FechaIni, FechaFin) as List<OrdenCompra>;
        
        }

       
        
           
        private MyWindowManager win = new MyWindowManager();

        public BuscarOrdenCompraViewModel()
        {
           lstOrdenes= new OrdenCompraSQL().Buscar(IdOrdenCompra, RSProveedor, getEstado(SelectedEstado), FechaIni, FechaFin) as List<OrdenCompra>;
        
        }

        registrarDocumentosViewModel r;
        public BuscarOrdenCompraViewModel(registrarDocumentosViewModel r)
        {
            this.r = r;
           LstOrdenes= new OrdenCompraSQL().Buscar(IdOrdenCompra, RSProveedor, getEstado(SelectedEstado), FechaIni, FechaFin) as List<OrdenCompra>;
        
        }

        private OrdenCompra ordenSeleccionada;

        public void SelectedItemChanged()
        {
            ordenSeleccionada = OrdenSelected; 
            if (r != null)
            {
                r.Ord = ordenSeleccionada;
                TryClose();
            }
        }


        public void NuevaOrden()
        {
            generarOrdenCompraViewModel obj = new  generarOrdenCompraViewModel (this);
            win.ShowWindow(obj);
        }
        public void EditarOrden()
        {
            if (OrdenSelected != null)
            {
                generarOrdenCompraViewModel obj = new generarOrdenCompraViewModel(OrdenSelected, this);
                win.ShowWindow(obj);
            }
            else
            {
             
                MessageBox.Show("No ha seleccionado ninguna Orden de compra ..", "AVISO", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        public void GenerarPDF()
        {
           GenerarPDF pdf = new GenerarPDF();
           Correo c = new Correo();
            //m.coloma@pucp.pe
           string body = formato().ToString();
           string msg = "Estimados :\n  Se adjunta la orden de compra , porfavor atenderla lo antes posible.";
           pdf.createPDF(body, "\\OC.pdf");
           c.EnviarCorreo("ORDEN DE COMPRA AL " + DateTime.Now.ToString(), OrdenSelected.Proveedor.Email, msg,Environment.CurrentDirectory+"\\OC.pdf");

        }

        public string formato() {

            string content = @"<HTML><BODY>";
            content += "<center> MadeInHouse  S.A. </center><br><br> ";
            content += "<center> Ruc. 99999999999 </center><br><br> ";
            content += "ORDEN DE COMPRA  Nro "+ OrdenSelected.IdOrden.ToString()+"<br><br>";
            content += "<br><br>";
            content += "Proveedor : " + OrdenSelected.Proveedor.RazonSocial+"<br><br>";
            content += "Fecha de pedido : " + OrdenSelected.FechaSinAtencion.ToString()+ "<br><br>";
            content += "Terminos de entrega : Entrega en Almacen central de la Empresa <br><br>";
            content += "Sirvase por este medio suministrar los siguientes articulos <br><br>";
            content += "<table border = 1 ><tr><th>NRO</th><th>ARTICULO</th><th>PRECIO UNITARIO</th>"+
                        "<th>CANTIDAD</th><th>PRECIO TOTAL</th><tr>";
            double sumaAporte = 0; 
            int i = 1;
            foreach (ProductoxOrdenCompra o in OrdenSelected.LstProducto) {

                int cantidad = Convert.ToInt32(o.Cantidad);
                double parcial = o.PrecioUnitario*cantidad;
                content += "<tr><td>"+i.ToString()+"</td>"+
                               "<td>"+o.Producto.Nombre+"</td>"+
                               "<td>"+o.PrecioUnitario.ToString()+"</td>"+
                               "<td>"+o.Cantidad.ToString()+"</td>"+
                "<td>"+ parcial.ToString()+"</td></tr>";
                i++;
                sumaAporte += parcial;
            } 
            
            content += "<tr><td colspan = 4 > TOTAL</td><td>"+sumaAporte.ToString()+"</td> </tr></table>";
            content += "</BODY></HTML>";

            return content;
        }

        #region Busqueda desde Almacen
        private int ventanaAccion = 0;
        private Almacen.MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel;

        public BuscarOrdenCompraViewModel(Almacen.MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel, int accionVentana)
        {
            // TODO: Complete member initialization
            this.mantenerNotaDeIngresoViewModel = mantenerNotaDeIngresoViewModel;
            this.ventanaAccion = accionVentana;
            this.estado = false;
            this.selectedEstado = "EN EJECUCION";
           LstOrdenes= new OrdenCompraSQL().Buscar(IdOrdenCompra, RSProveedor, getEstado(SelectedEstado), FechaIni, FechaFin) as List<OrdenCompra>;
        
        }

        
        
        #endregion

        #region Acciones Doble Click


        public void Acciones()
        {
            if (ventanaAccion == 0)
            {
                //Actualizar();
            }
            else if (ventanaAccion == 1)
            {
                
                if (this.mantenerNotaDeIngresoViewModel != null)
                {
                    mantenerNotaDeIngresoViewModel.SelectedOrden = OrdenSelected;
                    this.TryClose();
                }
            }
            else if (ventanaAccion == 2)
            {
                
            }
            else if (ventanaAccion == 3)
            {
                
            }
        }
        #endregion
    }
}
