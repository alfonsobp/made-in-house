using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models;


namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerNotaDeSalidaViewModel:PropertyChangedBase
    {
        MyWindowManager win = new MyWindowManager();
        DataObjects.Almacen.ProductoxSolicitudAbSQL gateWay = new ProductoxSolicitudAbSQL();
        ProductoSQL pxaSQL;
        public MantenerNotaDeSalidaViewModel() {
            pxaSQL = new ProductoSQL();
            this.cmbMotivo = DataObjects.Almacen.MotivoSQL.BuscarMotivos();
            /*
             <ComboBoxItem Content="Orden de despacho" HorizontalAlignment="Left" Width="118"/>
                            <ComboBoxItem Content="Traslado" HorizontalAlignment="Left" Width="118"/>
                            <ComboBoxItem Content="Robo" HorizontalAlignment="Left" Width="118"/>
                            <ComboBoxItem Content="Desperfectos" HorizontalAlignment="Left" Width="118"/>
                            <ComboBoxItem Content="Otros" HorizontalAlignment="Left" Width="118"/>
             */
            AlmacenSQL aGW = new AlmacenSQL();
            Models.Almacen.Almacenes a = aGW.BuscarAlmacen(4);
            List <Models.Almacen.Almacenes> al = new List<Models.Almacen.Almacenes>();
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
        
        List<Models.Almacen.Almacenes> almacen;

        public List<Models.Almacen.Almacenes> Almacen
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
            set { lstProductos = value;
                  NotifyOfPropertyChange(() => LstProductos);
            }
        }

        string txtCodPro;

        public string TxtCodPro
        {
            get { return txtCodPro; }
            set
            {
                txtCodPro = value;
                NotifyOfPropertyChange(() => TxtCodPro);
            }
        }

        string txtCantPro;

        public string TxtCantPro
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

        public void BuscarProducto()
        {
            MadeInHouse.Models.MyWindowManager wm = new Models.MyWindowManager();
            wm.ShowWindow(new ProductoBuscarViewModel(this, 3));
        }

        public void AgregarProducto() {
            if (TxtCodPro == null || TxtCantPro == null)
            {
                System.Windows.MessageBox.Show("Debe completar todos los campos");
            }
            else
            {
                ProductoCant pxa;
                List<Producto> lstAux = null;
                lstAux = pxaSQL.BuscarProducto(TxtCodPro);
                
                if ( (lstAux != null))
                {
                    if (LstProductos != null)
                    {
                        if ((pxa = LstProductos.Find(x => x.ProId == lstAux[0].IdProducto.ToString())) == null)
                        {

                            pxa = new ProductoCant();
                            pxa.Can = TxtCantPro;
                            pxa.ProId = lstAux[0].IdProducto.ToString();
                            pxa.ProNombre = lstAux[0].Nombre;
                            LstProductos.Add(pxa);
                            LstProductos = new List<ProductoCant>(LstProductos);
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("El producto que se quiere registrar ya existe en la tienda");
                        }
                    }
                    else {
                        pxa = new ProductoCant();
                        pxa.Can = TxtCantPro;
                        pxa.ProId = lstAux[0].IdProducto.ToString();
                        pxa.ProNombre = lstAux[0].Nombre;
                        LstProductos = new List<ProductoCant>();
                        LstProductos.Add(pxa);
                        LstProductos = new List<ProductoCant>(LstProductos);
                    }

                }
                else
                {
                    System.Windows.MessageBox.Show("El código proporcionado no existe");
                }
            }
        }
        public void AbrirPosicionProducto()
        {
            Almacen.PosicionProductoViewModel abrirPosView = new Almacen.PosicionProductoViewModel();
            win.ShowWindow(abrirPosView);
        }
        void QuitarProducto() { }

    }



}
