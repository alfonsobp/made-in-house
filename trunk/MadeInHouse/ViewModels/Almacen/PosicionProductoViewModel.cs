using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;
using System.Windows.Controls;
using MadeInHouse.Dictionary;

namespace MadeInHouse.ViewModels.Almacen
{
    class PosicionProductoViewModel : PropertyChangedBase
    {
        List<ProductoCant> lstProductos;

        public List<ProductoCant> LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value;
            NotifyOfPropertyChange(() => LstProductos);
            
            }
        }

        private int numRows;

        public int NumRows
        {
            get { return numRows; }
            set { numRows = value;
                NotifyOfPropertyChange(()=>NumRows);
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
            set { altura = value;
            NotifyOfPropertyChange(() => Altura);
            }
        }



        private int numRowsU;

        public int NumRowsU
        {
            get { return numRowsU; }
            set
            {
                numRowsU = value;
            }
        }

        private int numColumnsU;

        public int NumColumnsU
        {
            get { return numColumnsU; }
            set
            {
                numColumnsU = value;
            }
        }

        private int alturaU;

        public int AlturaU
        {
            get { return alturaU; }
            set
            {
                alturaU = value;
                NotifyOfPropertyChange(() => AlturaU);
            }
        }

        private int idTienda;
        private MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel;
      
        private int accion1;

        public int Accion1
        {
            get { return accion1; }
            set { accion1 = value;
            NotifyOfPropertyChange(() => Accion1);
            }
        }

        private int accion2;

        public int Accion2
        {
            get { return accion2; }
            set
            {
                accion2 = value;
                NotifyOfPropertyChange(() => Accion2);
            }
        }


        private ProductoCant selectedProduct;

        public ProductoCant SelectedProduct
        {
            get { return selectedProduct; }
            set { selectedProduct = value; }
        }


        private string txtCantActual ;

        public string TxtCantActual
        {
            get { return txtCantActual; }
            set { txtCantActual = value;
            NotifyOfPropertyChange(() => TxtCantActual);
            }
        }

        private string txtVolActual;

        public string TxtVolActual
        {
            get { return txtVolActual; }
            set { txtVolActual = value;
            NotifyOfPropertyChange(() => TxtVolActual);
            }
        }

        private string cantIngresar;

        public string CantIngresar
        {
          get { return cantIngresar; }
          set { cantIngresar = value; }
        }

        private string volIngresar;

        public string VolIngresar
        {
            get { return volIngresar; }
            set { volIngresar = value; }
        }

        private AlmacenSQL aSQL;
        private TipoZonaSQL tzSQL;
        private UbicacionSQL uSQL;

        private List<TipoZona> lstZonas;

        public List<TipoZona> LstZonas
        {
            get { return lstZonas; }
            set { lstZonas = value;
            NotifyOfPropertyChange(() => LstZonas);
            }
        }

        public PosicionProductoViewModel(MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel, int accion):this()
        {


            // TODO: Complete member initialization
            this.mantenerNotaDeIngresoViewModel = mantenerNotaDeIngresoViewModel;
            
            this.LstProductos = mantenerNotaDeIngresoViewModel.LstProductos;
            idTienda = 19;
            aSQL = new AlmacenSQL();
            Almacenes deposito = aSQL.BuscarAlmacen(-1, idTienda, 1);

            NumColumnsU=1;
            NumRowsU = 1;
            
            NumColumns =deposito.NroColumnas ;
            NumRows = deposito.NroFilas;
            Altura = deposito.Altura;

            
            
            tzSQL = new TipoZonaSQL();
            LstZonas = tzSQL.ObtenerZonasxAlmacen(deposito.IdAlmacen);

            Accion2 = 2;
            Accion1 = 2;
            Enable = true;

        }

        public PosicionProductoViewModel()
        {
            
            //mantenerNotaDeIngresoViewModel.Almacen.First();
            // TODO: Complete member initialization
        }

        private bool enable;

        public bool Enable
        {
            get { return enable; }
            set { enable = value;
            NotifyOfPropertyChange(() => Enable);
            
            }
        }

        public void Agregar(DynamicGrid colUbicacion , DynamicGrid zonas)
        {
            colUbicacion.AgregarProductos(int.Parse(CantIngresar),int.Parse(VolIngresar),SelectedProduct.IdProducto ,zonas.lstZonas);
            
        }

        public void MostrarColumna(MadeInHouse.Dictionary.DynamicGrid almacen, MadeInHouse.Dictionary.DynamicGrid ubicacionCol)
        {

            if (SelectedProduct!=null)
                ubicacionCol.Mostar(almacen.columna,SelectedProduct.IdProducto);
            else
                ubicacionCol.Mostar(almacen.columna);
            //System.Windows.MessageBox.Show("" +sender.columna[0].CordX + sender.columna[0].CordY);
        }

        int limpia = 0;

        public void SelectedItemChanged(object sender, MadeInHouse.Dictionary.DynamicGrid sender2)
        {

            SelectedProduct = ((sender as DataGrid).SelectedItem as ProductoCant);
            if (SelectedProduct != null)
            {

                sender2.UbicarProducto(SelectedProduct.IdProducto,limpia);
                limpia += 1;
               

            }
        }

        public void Guardar(DynamicGrid almacenDG )
        {
            uSQL = new UbicacionSQL();
            


        }




    }
}
