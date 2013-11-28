using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;
using System.Collections.ObjectModel;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Seguridad;
using System.Threading;
using System.Windows.Controls;
using System.ComponentModel.Composition;

namespace MadeInHouse.ViewModels.Almacen
{

    [Export(typeof(BuscarZonaViewModel))]
    class BuscarZonaViewModel : Screen
    {



        private List<TipoZona> cmbZonas;
        public List<TipoZona> CmbZonas
        {
            get { return cmbZonas; }
            set { cmbZonas = value; }
        }

        private int selectedZona;

        public int SelectedZona
        {
            get { return selectedZona; }
            set
            {
                List<ProductoxTienda> lstProductosAux= new List<ProductoxTienda>();
                selectedZona = value;
                ProductoxTienda find ;
                ProductoSQL pSQL = new ProductoSQL();
                LstProductos = pSQL.BuscarProductoxTienda(idTienda);
                 int ind=LstZonasAnq.FindIndex(x => x.IdTipoZona == SelectedZona);
                 
                for (int i = 0; i < LstZonasAnq[ind].LstSectores.Count; i++)
                 {
                     
                     if ((find=LstProductos.Find(x => x.IdProducto == LstZonasAnq[ind].LstSectores[i].IdProducto)) !=null)
                     {
                         lstProductosAux.Add(find);
                     }

                 }
                 if (LstZonasAnq[ind].LstSectores.Count == 0)
                     LstProductos = new List<ProductoxTienda>();
                 else
                     LstProductos = new List<ProductoxTienda>(lstProductosAux);

                NotifyOfPropertyChange(() => SelectedZona);

            }
        }

        private int numRows;

        public int NumRows
        {
            get { return numRows; }
            set
            {
                numRows = value;
                NotifyOfPropertyChange(() => NumRows);
            }
        }

        private int numColumns;

        public int NumColumns
        {
            get { return numColumns; }
            set
            {
                numColumns = value;
                NotifyOfPropertyChange(() => NumColumns);
            }
        }

        private int altura;

        public int Altura
        {
            get { return altura; }
            set
            {
                altura = value;
                NotifyOfPropertyChange(() => Altura);
            }
        }

        private List<TipoZona> lstZonasAnq;

        public List<TipoZona> LstZonasAnq
        {
            get { return lstZonasAnq; }
            set
            {
                lstZonasAnq = value;
                NotifyOfPropertyChange(() => LstZonasAnq);
            }
        }

        private List<ProductoxTienda> lstProductos;

        public List<ProductoxTienda> LstProductos
        {
            get { return lstProductos; }
            set
            {
                lstProductos = value;
                NotifyOfPropertyChange(() => LstProductos);
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

        private int idTienda;
        private int idResponsable;
        private int idAnaquel;



        private ProductoxTienda selectedProduct;

        public ProductoxTienda SelectedProduct
        {
            get { return selectedProduct; }
            set { selectedProduct = value; }
        }

        private String txtStockActual;

        public String TxtStockActual
        {
            get { return txtStockActual; }
            set { txtStockActual = value;
            NotifyOfPropertyChange(() => TxtStockActual);
            }
        }

        private String txtCapacidad;

        public String TxtCapacidad
        {
            get { return txtCapacidad; }
            set { txtCapacidad = value;
            NotifyOfPropertyChange(() => TxtCapacidad);
            }
        }


        private readonly IWindowManager _windowManager;

        [ImportingConstructor]
        public BuscarZonaViewModel(IWindowManager windowmanager)
        {

            _windowManager = windowmanager;
            Usuario u = new Usuario();
            u = DataObjects.Seguridad.UsuarioSQL.buscarUsuarioPorIdUsuario(Int32.Parse(Thread.CurrentPrincipal.Identity.Name));
            idTienda = u.IdTienda;
            idResponsable = u.IdUsuario;

            TiendaSQL tSQL = new TiendaSQL();
            CmbTiendas = tSQL.BuscarTienda();
            Index = this.CmbTiendas.FindIndex(x => x.IdTienda == idTienda);

            AlmacenSQL aSQL = new AlmacenSQL();
            Almacenes anaquel = aSQL.BuscarAlmacen(-1, idTienda, 2);

            idAnaquel = anaquel.IdAlmacen;

            NumColumns = anaquel.NroColumnas;
            NumRows = anaquel.NroFilas;
            Altura = anaquel.Altura;


            TipoZonaSQL tzSQL = new TipoZonaSQL();
            LstZonasAnq = tzSQL.ObtenerZonasxAlmacen(idAnaquel, 2);
            CmbZonas = lstZonasAnq;
            ProductoSQL pSQL = new ProductoSQL();
            LstProductos = pSQL.BuscarProductoxTienda(idTienda);
        }


        public void Recargar(MadeInHouse.Dictionary.DynamicGrid dg)
        {
            AlmacenSQL aSQL = new AlmacenSQL();
            Almacenes anaquel = aSQL.BuscarAlmacen(-1, idTienda, 2);

            idAnaquel = anaquel.IdAlmacen;

            NumColumns = anaquel.NroColumnas;
            NumRows = anaquel.NroFilas;
            Altura = anaquel.Altura;
            dg.RecreateGridCells();

            TipoZonaSQL tzSQL = new TipoZonaSQL();
            LstZonasAnq = tzSQL.ObtenerZonasxAlmacen(idAnaquel, 2);
            CmbZonas = lstZonasAnq;
            ProductoSQL pSQL = new ProductoSQL();
            LstProductos = pSQL.BuscarProductoxTienda(idTienda);
            TxtStockActual = "";
            TxtCapacidad = "";

        }

        public void SelectedItemChanged(object sender, MadeInHouse.Dictionary.DynamicGrid anaquel)
        {

            SelectedProduct = ((sender as DataGrid).SelectedItem as ProductoxTienda);


            if (SelectedProduct != null)
            {
                ProductoCant pc = new ProductoCant();
                pc.IdProducto = SelectedProduct.IdProducto;
                anaquel.SelectedProduct = pc;
                anaquel.UbicarSector(SelectedProduct.IdProducto, 4);
                TxtStockActual =  anaquel.StockActual.ToString();
                TxtCapacidad = anaquel.CapacidadActual.ToString();
                //(sender as DataGrid).SelectedItem = null;

            }


        }




    }
}
