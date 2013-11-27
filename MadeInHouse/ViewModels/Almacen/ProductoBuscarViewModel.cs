using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;
using MadeInHouse.ViewModels.Compras;
using MadeInHouse.ViewModels.Layouts;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Controls;

namespace MadeInHouse.ViewModels.Almacen
{
    class ExtendedProduct : Producto
    {
        public string Linea { get; set; }
        public string SubLinea { get; set; }
    }

    [Export(typeof(ProductoBuscarViewModel))]
    class ProductoBuscarViewModel : Screen
    {
        #region constructores

        [ImportingConstructor]
        public ProductoBuscarViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
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

        private Almacen.ProductoMovimientosViewModel ProductoMovimientosViewModel;
        public ProductoBuscarViewModel(IWindowManager windowmanager, Almacen.ProductoMovimientosViewModel ProductoMovimientosViewModel, int ventanaAccion, int idTienda = -1)
            : this(windowmanager)
        {
            this.ProductoMovimientosViewModel = ProductoMovimientosViewModel;
            this.ventanaAccion = ventanaAccion;
            Index = this.CmbTiendas.FindIndex(x => x.IdTienda == idTienda);
            Estado = false;
        }

        private Almacen.MantenerTiendaViewModel mantenerTiendaViewModel;
        public ProductoBuscarViewModel(IWindowManager windowmanager, Almacen.MantenerTiendaViewModel mantenerTiendaViewModel, int ventanaAccion)
            : this(windowmanager)
        {
            this.mantenerTiendaViewModel = mantenerTiendaViewModel;
            this.ventanaAccion = ventanaAccion;
        }

        private Ventas.VentaRegistrarViewModel ventaRegistrarViewModel;
        public ProductoBuscarViewModel(IWindowManager windowmanager, Ventas.VentaRegistrarViewModel ventaRegistrarViewModel, int ventanaAccion)
            : this(windowmanager)
        {
            this.ventaRegistrarViewModel = ventaRegistrarViewModel;
            this.ventanaAccion = ventanaAccion;
            AlmacenSQL almSQL = new AlmacenSQL();
            idAlmacen = almSQL.obtenerDeposito(ventaRegistrarViewModel.idTienda);
            SelectedTienda = ventaRegistrarViewModel.idTienda;
            Index = CmbTiendas.FindIndex(x => x.IdTienda == SelectedTienda);
        }

        private Ventas.ProformaViewModel proformaViewModel;
        public ProductoBuscarViewModel(IWindowManager windowmanager, Ventas.ProformaViewModel proformaViewModel, int ventanaAccion)
            : this(windowmanager)
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
        public ProductoBuscarViewModel(IWindowManager windowmanager, SolicitudAbRegistrarViewModel solicitudView)
            : this(windowmanager)
        {
            this.solicitudView = solicitudView;
            AlmacenSQL almSQL = new AlmacenSQL();
            idAlmacen = solicitudView.idTienda;
            SelectedTienda = solicitudView.idTienda;
            Index = CmbTiendas.FindIndex(x => x.IdTienda == SelectedTienda);
        }

        public ProductoBuscarViewModel(IWindowManager windowmanager, MantenerNotaDeSalidaViewModel mantenerNotaDeSalidaViewModel, int ventanaAccion)
            : this(windowmanager)
        {
            // TODO: Complete member initialization

            this.mantenerNotaDeSalidaViewModel = mantenerNotaDeSalidaViewModel;
            this.ventanaAccion = ventanaAccion;
            int i = 0;
            Index = i;
            for (i = 0; i < CmbTiendas.Count; i++)
            {
                if (CmbTiendas.ElementAt(i).IdTienda == mantenerNotaDeSalidaViewModel.Almacen.ElementAt(0).IdTienda)
                {
                    Index = i;
                }
            }
            Estado = false;
        }

        public ProductoBuscarViewModel(IWindowManager windowmanager, MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel, int p)
            : this(windowmanager)
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

        public ProductoBuscarViewModel(IWindowManager windowmanager, ProductoViewModel pvm)
            : this(windowmanager)
        {
            this.pvm = pvm;

        }

        #endregion

        #region atributos

        private readonly IWindowManager _windowManager;

        private int ventanaAccion = 0;
        public ProductoViewModel pvm = null;
        private bool estado;

        public bool Estado
        {
            get { return estado; }
            set
            {
                estado = value;
                NotifyOfPropertyChange("Estado");
            }
        }
        private List<Tienda> cmbTiendas;

        public List<Tienda> CmbTiendas
        {
            get { return cmbTiendas; }
            set
            {
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
            set
            {
                index = value;
                NotifyOfPropertyChange(() => Index);
            }
        }
        private int selectedIndex1;

        public int SelectedIndex1
        {
            get { return selectedIndex1; }
            set
            {
                selectedIndex1 = value;
                NotifyOfPropertyChange(() => SelectedIndex1);
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


        public void AbrirMantenerProducto()
        {
            _windowManager.ShowWindow(new Almacen.ProductoMantenerViewModel(_windowManager));
        }



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
            if (LstProductos == null)
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No se encontro ningún producto"));
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

            if (pvm != null)
            {

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
                List<ProductoxTienda> prod = pSQL.BuscarProductoxTienda(idAlmacen, productoSel.IdProducto);
                prodPedido.stock = prod.ElementAt(0).StockActual;
                prodPedido.sugerido = prod.ElementAt(0).StockMin - prod.ElementAt(0).StockActual > 0 ? prod.ElementAt(0).StockMin - prod.ElementAt(0).StockActual : 0;
                prodPedido.pedido = prodPedido.sugerido;
                prodPedido.atendido = 0;
                solicitudView.addProducto(prodPedido);
            }
            else
            {
                _windowManager.ShowWindow(new ProductoMantenerViewModel(_windowManager, ProductoSel));
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