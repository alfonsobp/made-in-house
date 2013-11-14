using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;
using System.Windows.Controls;

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


        private AlmacenSQL aSQL;
        private TipoZonaSQL tzSQL;
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
            idTienda = 18;
            aSQL = new AlmacenSQL();
            Almacenes deposito = aSQL.BuscarAlmacen(-1, idTienda, 1);

            NumColumnsU=1;
            NumRowsU = 1;
            
            NumColumns =deposito.NroColumnas ;
            NumRows = deposito.NroFilas;
            Altura = deposito.Altura;

            //Accion1 = 1;
            Accion2 = 2;

            tzSQL = new TipoZonaSQL();
            LstZonas = tzSQL.ObtenerZonasxAlmacen(deposito.IdAlmacen);
            



            /*this.NumRowsU = deposito.NroFilas;
            this.NumColumns = 1;
            this.AlturaU = 1;*/
        }

        public PosicionProductoViewModel()
        {
            
            //mantenerNotaDeIngresoViewModel.Almacen.First();
            // TODO: Complete member initialization
        }

        public void Agregar()
        {
            


        }


        public void SelectedItemChanged(object sender, MadeInHouse.Dictionary.DynamicGrid sender2)
        {

            SelectedProduct = ((sender as DataGrid).SelectedItem as ProductoCant);
            if (SelectedProduct != null)
            {

                sender2.UbicarProducto(13);
                System.Windows.MessageBox.Show("SE SELECIONO:" + selectedProduct.CodPro);

            }
        }




    }
}
