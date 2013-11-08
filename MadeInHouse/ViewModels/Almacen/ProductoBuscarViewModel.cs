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


        }

        private int ventanaAccion = 0;
        
        private Almacen.MantenerTiendaViewModel mantenerTiendaViewModel;
        public ProductoBuscarViewModel(Almacen.MantenerTiendaViewModel mantenerTiendaViewModel, int ventanaAccion):this()
        {
            this.mantenerTiendaViewModel = mantenerTiendaViewModel;
            this.ventanaAccion = ventanaAccion;
        }        

        private Ventas.VentaRegistrarViewModel ventaRegistrarViewModel;
        public ProductoBuscarViewModel(Ventas.VentaRegistrarViewModel ventaRegistrarViewModel, int ventanaAccion)
        {
            this.ventaRegistrarViewModel = ventaRegistrarViewModel;
            LineaProductoSQL lpSQL = new LineaProductoSQL();
            LstLineasProducto = lpSQL.ObtenerLineasProducto();
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
            idAlmacen = solicitudView.idAlmacen;
        }

        public ProductoBuscarViewModel(MantenerNotaDeSalidaViewModel mantenerNotaDeSalidaViewModel, int ventanaAccion):this()
        {
            // TODO: Complete member initialization
            this.mantenerNotaDeSalidaViewModel = mantenerNotaDeSalidaViewModel;
            this.ventanaAccion = ventanaAccion;
        }

        public ProductoBuscarViewModel(MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel, int p):this()
        {
            // TODO: Complete member initialization
            this.mantenerNotaDeIngresoViewModel = mantenerNotaDeIngresoViewModel;
            this.ventanaAccion = p;
        }

        #endregion

        #region atributos

        public int idAlmacen { get; set; }

        private string txtCodigo;

        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }

        private List<ExtendedProduct> lstProductos;

        public List<ExtendedProduct> LstProductos
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
             List<Producto> lp;
             lp=pSQL.BuscarProducto(TxtCodigo, SelectedValue, SelectedValueSub, idAlmacen);
             List<ExtendedProduct> LstProductosAux = new List<ExtendedProduct>();

             if (lp != null)
             {
                 foreach (Producto p in lp)
                 {
                     ExtendedProduct exp = new ExtendedProduct();
                     exp.IdProducto = p.IdProducto;
                     exp.CodigoProd = p.CodigoProd;
                     exp.Nombre = p.Nombre;
                     exp.Descripcion = p.Descripcion;
                     exp.Abreviatura = p.Abreviatura;
                     exp.Percepcion = p.Percepcion;
                     exp.Precio = p.Precio;
                     if (SelectedValue != 0) 
                         exp.Linea = GetLinea(SelectedValue).Nombre;

                     else
                         exp.Linea = GetLinea(p.IdLinea).Nombre;

                     if (SelectedValueSub != 0) exp.SubLinea = GetSubLinea(SelectedValueSub).Nombre;
                     else
                     {
                         SelectedIndex = -1;
                         SubLineaProductoSQL slpSQL = new SubLineaProductoSQL();
                         LstSubLineasProducto = slpSQL.ObtenerSubLineas(p.IdLinea);
                         exp.SubLinea = GetSubLinea(p.IdSubLinea).Nombre;
                     }
                     LstProductosAux.Add(exp);
                 }
                 LstProductos = LstProductosAux;
             }
             else
             {
                 LstProductos = null;
                 System.Windows.Forms.MessageBox.Show("No se encontro ningún producto");
             }
             
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
                    mantenerNotaDeSalidaViewModel.TxtCodPro = productoSel.CodigoProd;
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
        
        #endregion
    }
}
