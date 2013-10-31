using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;


namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerNotaDeSalidaViewModel:PropertyChangedBase
    {

        DataObjects.Almacen.ProductoxSolicitudAbSQL gateWay = new ProductoxSolicitudAbSQL();

        public MantenerNotaDeSalidaViewModel() {
            this.cmbMotivo = DataObjects.Almacen.MotivoSQL.BuscarMotivos();
            /*
             <ComboBoxItem Content="Orden de despacho" HorizontalAlignment="Left" Width="118"/>
                            <ComboBoxItem Content="Traslado" HorizontalAlignment="Left" Width="118"/>
                            <ComboBoxItem Content="Robo" HorizontalAlignment="Left" Width="118"/>
                            <ComboBoxItem Content="Desperfectos" HorizontalAlignment="Left" Width="118"/>
                            <ComboBoxItem Content="Otros" HorizontalAlignment="Left" Width="118"/>
             */
            AlmacenSQL aGW = new AlmacenSQL();
            Models.Almacen.Almacen a = aGW.BuscarAlmacen(4);
            List <Models.Almacen.Almacen> al = new List<Models.Almacen.Almacen>();
            al.Add(a);
            this.almacen = al;
           //lstProductos = gateWay.ListaProductos("1");
            
        }

        string txtDoc;

        public string TxtDoc
        {
            get { return txtDoc; }
            set { txtDoc = value; }
        }

        int txtCod;

        public int TxtCod
        {
            get { return txtCod; }
            set { txtCod = value; }
        }
        List<Motivo> cmbMotivo = null;

        public List<Motivo> CmbMotivo
        {
            get { return cmbMotivo; }
            set { cmbMotivo = value; }
        }

        string selectedMotivo;

        public string SelectedMotivo
        {
            get { return selectedMotivo; }
            set { selectedMotivo = value; }
        }
        Producto selectedProducto;

        public Producto SelectedProducto
        {
            get { return selectedProducto; }
            set { selectedProducto = value; }
        }
        
        List<Models.Almacen.Almacen> almacen;

        public List<Models.Almacen.Almacen> Almacen
        {
            get { return almacen; }
            set { almacen = value; }
        }
        string responsable;

        public string Responsable
        {
            get { return responsable; }
            set { responsable = value; }
        }
        string observaciones;

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        //Estado (Documento Referencia);

        List<ProductoCant> lstProductos;

        public List<ProductoCant> LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value; }
        }

        int txtCodPro;

        public int TxtCodPro
        {
            get { return txtCodPro; }
            set { txtCodPro = value; }
        }

        int txtCantPro;

        public int TxtCantPro
        {
            get { return txtCantPro; }
            set { txtCantPro = value; }
        }

        //Botones:

        public void CargarProductos() {
        
            string referencia = TxtDoc;
            string mot = this.selectedMotivo;
            //Si fuera Por Traslado, Saco informacion de Solicitud de Abastecimiento con la referencia
           lstProductos = gateWay.ListaProductos(referencia);
           NotifyOfPropertyChange(() => LstProductos);
        }
        void BuscarProducto() { }
        void AgregarProducto() { }
        void QuitarProducto() { }

    }



}
