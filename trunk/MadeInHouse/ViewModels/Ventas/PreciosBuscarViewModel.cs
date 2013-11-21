using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Ventas;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.DataObjects.Ventas;
using MadeInHouse.DataObjects;
using System.Windows.Controls;
using System.Windows;
using MadeInHouse.ViewModels.Compras;
using MadeInHouse.DataObjects.Seguridad;

namespace MadeInHouse.ViewModels.Ventas
{
    class PreciosBuscarViewModel : Screen
    {
        public PreciosBuscarViewModel()
        {
            LineaProductoSQL lpSQL = new LineaProductoSQL();
            LstLineasProducto = lpSQL.ObtenerLineasProducto();
            LineaProducto deftlinea = new LineaProducto();
            deftlinea.Nombre = "TODAS";
            deftlinea.IdLinea = -1;
            LstLineasProducto.Insert(0, deftlinea);
            SelectedLinea = 0;

            Tienda deft = new Tienda();
            deft.Nombre = "ALMACEN CENTRAL";
            deft.IdTienda = -1;

            //deft.IdTienda = UsuarioSQL.buscarUsuarioPorIdUsuario(Int32.Parse(Thread.CurrentPrincipal.Identity.Name)).IdTienda;//Jalar del usuario
            TiendaSQL tSQL = new TiendaSQL();
            CmbTiendas = tSQL.BuscarTienda();
            CmbTiendas.Insert(0, deft);
            IndexTienda = 0;
            SelectedTienda = UsuarioSQL.buscarUsuarioPorIdUsuario(Int32.Parse(Thread.CurrentPrincipal.Identity.Name)).IdTienda;
        }

        #region atributos
        public ProductoViewModel pvm = null;

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

        private int indexTienda;

        public int IndexTienda
        {
            get { return indexTienda; }
            set
            {
                indexTienda = value;
                NotifyOfPropertyChange(() => IndexTienda);
            }
        }
        private int selectedLinea;

        public int SelectedLinea
        {
            get { return selectedLinea; }
            set
            {
                selectedLinea = value;
                NotifyOfPropertyChange(() => SelectedLinea);
            }
        }
        public int idAlmacen { get; set; }

        private string txtCodigo;

        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }

        private bool isFocusDataGrid;
        public bool IsFocusDataGrid
        {
            get { return isFocusDataGrid; }
            set
            {
                if (isFocusDataGrid == value)
                    return;
                isFocusDataGrid = value;
                NotifyOfPropertyChange("IsFocusDataGrid");
            }
        }

        private List<DetalleProductoVenta> lstProductos;

        public List<DetalleProductoVenta> LstProductos
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



        private int selectedSubLinea;

        public int SelectedSubLinea
        {
            get { return selectedSubLinea; }
            set { selectedSubLinea = value; NotifyOfPropertyChange(() => SelectedSubLinea); }
        }


        private int selectedValueLinea;

        public int SelectedValueLinea
        {
            get { return selectedValueLinea; }
            set
            {
                selectedValueLinea = value;
                SubLineaProductoSQL slpSQL = new SubLineaProductoSQL();
                LstSubLineasProducto = slpSQL.ObtenerSubLineas(selectedValueLinea);
                SubLineaProducto deft = new SubLineaProducto();
                deft.Nombre = "TODAS";
                deft.IdSubLinea = -1;
                LstSubLineasProducto.Insert(0, deft);
                SelectedSubLinea = 0;

            }
        }

        private int selectedValueSubLinea;

        public int SelectedValueSubLinea
        {
            get { return selectedValueSubLinea; }
            set
            { selectedValueSubLinea = value; }
        }

        private DetalleProductoVenta productoSel;

        public void SelectedItemChanged(object sender)
        {
            productoSel = ((sender as DataGrid). SelectedItem as DetalleProductoVenta);
            
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
        private DetalleProductoVentaSQL dpvSQL = new DetalleProductoVentaSQL();

        public void BuscarProductos()
        {
            //LstProductos = pSQL.BuscarProducto(TxtCodigo, SelectedValueLinea, SelectedValueSubLinea, SelectedTienda);
            LstProductos = dpvSQL.BuscarProducto(TxtCodigo, SelectedValueLinea, SelectedValueSubLinea, SelectedTienda);
            if (LstProductos == null)
                System.Windows.Forms.MessageBox.Show("No se encontro ningún producto");
        }

        public void GuardarPrecioProducto()
        {
            //isFocusDataGrid = false;
            //productoSel = null;
            //SelectedItemChanged();

            if (LstProductos!=null){
                for(int i=0;i<LstProductos.Count;i++){
                    dpvSQL.ActualizarProducto(LstProductos[i].IdProducto,17,LstProductos[i].PrecioVenta);
                }
            }
            //if (productoSel != null)
            //{
            //    MessageBox.Show(""+productoSel.PrecioVenta+" --");
            //    dpvSQL.ActualizarProducto(productoSel.IdProducto, 17,productoSel.PrecioVenta);
            //}
        }

        public void Actualizar()
        {
            BuscarProductos();
                //ProductoSQL pSQL = new ProductoSQL();
                //AbastecimientoProducto prodPedido = new AbastecimientoProducto();
                //prodPedido.idProducto = productoSel.IdProducto;
                //prodPedido.nombre = productoSel.Nombre;

                //List<ProductoxAlmacen> prod = pSQL.BuscarProductoxAlmacen(idAlmacen, productoSel.IdProducto);

                //prodPedido.stock = prod.ElementAt(0).StockActual;
                //prodPedido.sugerido = prod.ElementAt(0).StockMin - prod.ElementAt(0).StockActual;
                //prodPedido.pedido = prodPedido.sugerido;


        }

        public void Limpiar()
        {
            TxtCodigo = null;
            IndexTienda = 0;
            SelectedSubLinea= 0;
            selectedLinea = 0;
            LstProductos = null;

        }

        #endregion
    }
}
