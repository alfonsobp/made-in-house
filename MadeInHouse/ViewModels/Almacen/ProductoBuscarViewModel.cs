using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.DataObjects;
using System.Windows.Controls;
using MadeInHouse.ViewModels.Compras;

namespace MadeInHouse.ViewModels.Almacen
{

    class ExtendedProduct : Producto
    {
        public string Linea { get; set; }
        public string SubLinea { get; set; }

    }


    class ProductoBuscarViewModel : Screen
    {
        private MadeInHouse.Models.MyWindowManager win = new MadeInHouse.Models.MyWindowManager();

        #region constructores

        public ProductoBuscarViewModel()
        {
            LineaProductoSQL lpSQL = new LineaProductoSQL();
            LstLineasProducto = lpSQL.ObtenerLineasProducto();
            LineaProducto deftlinea = new LineaProducto();
            deftlinea.Nombre = "TODAS";
            deftlinea.IdLinea = -1;
            LstLineasProducto.Insert(0, deftlinea);
            SelectedIndex1 = 0;

            Tienda deft = new Tienda();
            deft.Nombre = "ALMACEN CENTRAL";
            deft.IdTienda = -1;
            TiendaSQL tSQL = new TiendaSQL();
            CmbTiendas = tSQL.BuscarTienda();
            CmbTiendas.Insert(0, deft);
            Index = 0;
            Estado = true;

        }

        private int ventanaAccion = 0;
        
        


        private Almacen.ProductoMovimientosViewModel ProductoMovimientosViewModel;
        public ProductoBuscarViewModel(Almacen.ProductoMovimientosViewModel ProductoMovimientosViewModel, int ventanaAccion)
            : this()
        {
            this.ProductoMovimientosViewModel = ProductoMovimientosViewModel;
            this.ventanaAccion = ventanaAccion;
        }

        private Almacen.MantenerTiendaViewModel mantenerTiendaViewModel;
        public ProductoBuscarViewModel(Almacen.MantenerTiendaViewModel mantenerTiendaViewModel, int ventanaAccion):this()
        {
            this.mantenerTiendaViewModel = mantenerTiendaViewModel;
            this.ventanaAccion = ventanaAccion;
        }        

        private Ventas.VentaRegistrarViewModel ventaRegistrarViewModel;
        public ProductoBuscarViewModel(Ventas.VentaRegistrarViewModel ventaRegistrarViewModel, int ventanaAccion):this()
        {
            this.ventaRegistrarViewModel = ventaRegistrarViewModel;
            this.ventanaAccion = ventanaAccion;
        }

        private Ventas.ProformaViewModel proformaViewModel;
        public ProductoBuscarViewModel(Ventas.ProformaViewModel proformaViewModel, int ventanaAccion)
        {
            this.proformaViewModel = proformaViewModel;
            LineaProductoSQL lpSQL = new LineaProductoSQL();
            LstLineasProducto = lpSQL.ObtenerLineasProducto();
            this.ventanaAccion = ventanaAccion;
        }

        private Reportes.reporteStockViewModel reporteStockViewModel;
        public ProductoBuscarViewModel(Reportes.reporteStockViewModel reporteStockViewModel, int ventanaAccion)
        {
            this.reporteStockViewModel = reporteStockViewModel;
            LineaProductoSQL lpSQL = new LineaProductoSQL();
            LstLineasProducto = lpSQL.ObtenerLineasProducto();
            this.ventanaAccion = ventanaAccion;
        }

        private SolicitudAbRegistrarViewModel solicitudView = null;
        public ProductoBuscarViewModel(SolicitudAbRegistrarViewModel solicitudView) : this()
        {
            this.solicitudView = solicitudView;
            AlmacenSQL almSQL = new AlmacenSQL();
            idAlmacen = almSQL.obtenerDeposito(solicitudView.idTienda);
        }

        public ProductoBuscarViewModel(MantenerNotaDeSalidaViewModel mantenerNotaDeSalidaViewModel, int ventanaAccion):this()
        {
            // TODO: Complete member initialization

            this.mantenerNotaDeSalidaViewModel = mantenerNotaDeSalidaViewModel;
            this.ventanaAccion = ventanaAccion;
            int i=0;
            Index = i;
            for (i = 0; i < CmbTiendas.Count; i++) {
                if (CmbTiendas.ElementAt(i).IdTienda == mantenerNotaDeSalidaViewModel.Almacen.ElementAt(0).IdTienda) {
                    Index = i;
                }
            }
            Estado = false;
        }

        public ProductoBuscarViewModel(MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel, int p):this()
        {
            // TODO: Complete member initialization
            this.mantenerNotaDeIngresoViewModel = mantenerNotaDeIngresoViewModel;
            this.ventanaAccion = p;

            int i = 0;
            Index = i;
            for (i = 0; i < CmbTiendas.Count; i++)
            {
                if (CmbTiendas.ElementAt(i).IdTienda == mantenerNotaDeIngresoViewModel.Almacen.ElementAt(0).IdTienda)
                {
                    Index = i;
                }
            }
            Estado = false;
        }

        public ProductoBuscarViewModel(ProductoViewModel pvm):this() {
            this.pvm = pvm;
            
        }

        #endregion

        #region atributos

	public ProductoViewModel pvm = null;
    private bool estado;

    public bool Estado
    {
        get { return estado; }
        set { estado = value;
        NotifyOfPropertyChange("Estado");
        }
    }
	private List<Tienda> cmbTiendas;

        public List<Tienda> CmbTiendas
        {
            get { return cmbTiendas; }
            set {
                if (this.cmbTiendas == value)
                {
                    return;
                }

            cmbTiendas = value;
            NotifyOfPropertyChange(() => CmbTiendas);
            }
        }

        private int selectedTienda;

        public int SelectedTienda
        {
            get { return selectedTienda; }
            set { selectedTienda = value; }
        }

        private int index;

        public int Index
        {
            get { return index; }
            set { index = value;
            NotifyOfPropertyChange(() => Index);
            }
        }
        private int selectedIndex1;

        public int SelectedIndex1
        {
            get { return selectedIndex1; }
            set { selectedIndex1 = value; 
            NotifyOfPropertyChange(()=>SelectedIndex1);
            }
        }        public int idAlmacen { get; set; }

        private string txtCodigo;

        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }

        private List<Producto> lstProductos;
  
        public List<Producto> LstProductos
        {
            get { return lstProductos; }
            set
            {
                lstProductos = value;
                NotifyOfPropertyChange(() => LstProductos);
            }
        }



        private BindableCollection<LineaProducto> lstLineasProducto;

        public BindableCollection<LineaProducto> LstLineasProducto
        {
            get { return lstLineasProducto; }
            set
            {
                if (this.lstLineasProducto == value)
                {
                    return;
                }
                this.lstLineasProducto = value;
                this.NotifyOfPropertyChange(() => this.lstLineasProducto);
            }
        }

        private BindableCollection<SubLineaProducto> lstSubLineasProducto;

        public BindableCollection<SubLineaProducto> LstSubLineasProducto
        {
            get { return lstSubLineasProducto; }
            set
            {
                if (this.lstSubLineasProducto == value)
                {
                    return;
                }
                this.lstSubLineasProducto = value;
                this.NotifyOfPropertyChange(() => this.lstSubLineasProducto);
            }
        }



        private int selectedIndex;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; NotifyOfPropertyChange(() => SelectedIndex); }
        }


        private int selectedValue;

        public int SelectedValue
        {
            get { return selectedValue; }
            set
            {
                selectedValue = value;
                SubLineaProductoSQL slpSQL = new SubLineaProductoSQL();
                LstSubLineasProducto = slpSQL.ObtenerSubLineas(selectedValue);
                SubLineaProducto deft = new SubLineaProducto();
                deft.Nombre = "TODAS";
                deft.IdSubLinea = -1;
                LstSubLineasProducto.Insert(0, deft);
                SelectedIndex = 0;

            }
        }

        private int selectedValueSub;

        public int SelectedValueSub
        {
            get { return selectedValueSub; }
            set
            { selectedValueSub = value; }
        }

        private Producto productoSel;

        public Producto ProductoSel
        {
            get { return productoSel; }
            set { productoSel = value; }
        }

        #endregion

        #region metodos

        private LineaProducto GetLinea(int idLinea)
        {
            return (from lp in LstLineasProducto
                    where lp.IdLinea == idLinea
                    select lp).FirstOrDefault();
        }
        private SubLineaProducto GetSubLinea(int idSubLinea)
        {
            return (from slp in LstSubLineasProducto
                    where slp.IdSubLinea == idSubLinea
                    select slp).FirstOrDefault();
        }

        private ProductoSQL pSQL = new ProductoSQL();
        private MantenerNotaDeSalidaViewModel mantenerNotaDeSalidaViewModel;
        private int p;
        private MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel;
        //private LineaProductoSQL lsql;
        //private SubLineaProductoSQL ssql;
        

        public void BuscarProductos()
        {
        LstProductos = pSQL.BuscarProducto(TxtCodigo, SelectedValue, SelectedValueSub, SelectedTienda);   
		   if (LstProductos ==null)
                 System.Windows.Forms.MessageBox.Show("No se encontro ningún producto");
             }
             
   


        public void Acciones(object sender)
        {
            if (ventanaAccion == 0)
            {
                Actualizar();
            }
            else if (ventanaAccion == 1)
            {
                productoSel = ((sender as DataGrid).SelectedItem as Producto);
                if (ventaRegistrarViewModel != null)
                {
                    ventaRegistrarViewModel.Prod = productoSel;
                    this.TryClose();
                }
            }
            else if (ventanaAccion == 2)
            {
                productoSel = ((sender as DataGrid).SelectedItem as Producto);
                if (mantenerTiendaViewModel != null)
                {
                    mantenerTiendaViewModel.TxtCodProducto = productoSel.CodigoProd;
                    this.TryClose();
                    
                }
            }
            else if (ventanaAccion == 3)
            {
                productoSel = ((sender as DataGrid).SelectedItem as Producto);
                if (mantenerNotaDeSalidaViewModel != null)
                {
                    mantenerNotaDeSalidaViewModel.SelectedProducto = productoSel;
                    this.TryClose();
                }
            }
            else if (ventanaAccion == 4)
            {
                productoSel = ((sender as DataGrid).SelectedItem as Producto);
                if (mantenerNotaDeIngresoViewModel != null)
                {
                    mantenerNotaDeIngresoViewModel.SelectedProducto = productoSel;
                    this.TryClose();
                }
            }
            else if (ventanaAccion == 5)
            {
                productoSel = ((sender as DataGrid).SelectedItem as Producto);
                if (proformaViewModel != null)
                {
                    proformaViewModel.Prod = productoSel;
                    this.TryClose();
                }
            }
            else if (ventanaAccion == 6)
            {
                productoSel = ((sender as DataGrid).SelectedItem as Producto);
                if (proformaViewModel != null)
                {
                    proformaViewModel.Prod = productoSel;
                    this.TryClose();
                }
                
            }
            else if (ventanaAccion == 7)
            {
                productoSel = ((sender as DataGrid).SelectedItem as Producto);
                if (ProductoMovimientosViewModel != null)
                {
                    ProductoMovimientosViewModel.ProductoSeleccionado = productoSel;
                    ProductoMovimientosViewModel.TxtProducto = productoSel.CodigoProd;
                    this.TryClose();
                }

            }

            if (pvm != null) {

                productoSel = ((sender as DataGrid).SelectedItem as Producto);
                
                    pvm.P = productoSel;
                    this.TryClose();
                

            }

        }


        public void Actualizar()
        {
            if (solicitudView != null)
            {
                ProductoSQL pSQL = new ProductoSQL();
                AbastecimientoProducto prodPedido = new AbastecimientoProducto();
                prodPedido.idProducto = productoSel.IdProducto;
                prodPedido.nombre = productoSel.Nombre;
                List<ProductoxAlmacen> prod = pSQL.BuscarProductoxAlmacen(idAlmacen, productoSel.IdProducto);
                prodPedido.stock = prod.ElementAt(0).StockActual;
                prodPedido.sugerido = prod.ElementAt(0).StockMin - prod.ElementAt(0).StockActual;
                prodPedido.pedido = prodPedido.sugerido;
                solicitudView.addProducto(prodPedido);
            }
            else
            {
                ProductoMantenerViewModel pmVM = new ProductoMantenerViewModel(ProductoSel);
                win.ShowWindow(pmVM);
            }
        }

        public void Limpiar()
        {
            TxtCodigo = null;
            Index = 0;
            SelectedIndex = 0;
            selectedIndex1 = 0;
            LstProductos = null;

        }
        
        
        #endregion
    }
}
