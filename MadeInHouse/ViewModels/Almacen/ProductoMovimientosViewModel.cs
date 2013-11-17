using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using MadeInHouse.DataObjects;
using System.Data;
using System.Data.SqlClient;
using MadeInHouse.Models.Seguridad;
using System.Threading;
using MadeInHouse.Models;
using System.Windows;

namespace MadeInHouse.ViewModels.Almacen
{
    class ProductoMovimientosViewModel:PropertyChangedBase
    {

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

        private int numRowsU;

        public int NumRowsU
        {
            get { return numRowsU; }
            set
            {
                numRowsU = value;
                NotifyOfPropertyChange(() => NumRowsU);
            }
        }

        private int numColumnsU;

        public int NumColumnsU
        {
            get { return numColumnsU; }
            set
            {
                numColumnsU = value;
                NotifyOfPropertyChange(() => NumColumnsU);

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

        private List<TipoZona> lstZonas;

        public List<TipoZona> LstZonas
        {
            get { return lstZonas; }
            set
            {
                lstZonas = value;
                NotifyOfPropertyChange(() => LstZonas);
            }
        }

        private int accion1;

        public int Accion1
        {
            get { return accion1; }
            set
            {
                accion1 = value;
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


        private ObservableCollection<TipoZona> cmbZonas;
        public ObservableCollection<TipoZona> CmbZonas
        {
            get { return cmbZonas; }
            set { cmbZonas = value; }
        }

        private List<Ubicacion> columnaU;

        public List<Ubicacion> ColumnaU
        {
            get { return columnaU; }
            set
            {
                columnaU = value;
                NotifyOfPropertyChange(() => ColumnaU);
            }
        }

        private string txtProducto;

        public string TxtProducto
        {
            get { return txtProducto; }
            set { txtProducto = value; 
            
            NotifyOfPropertyChange(()=>TxtProducto);
            }
        }

        private Producto productoSeleccionado;

        internal Producto ProductoSeleccionado
        {
            get { return productoSeleccionado; }
            set { productoSeleccionado = value; }
        }

        


        private AlmacenSQL aSQL;
        private TipoZonaSQL tzSQL;
        private int idTienda;
        private int id;

        public ProductoMovimientosViewModel()
        {
            CmbZonas = (new TipoZonaSQL()).BuscarZona();
            Usuario u = new Usuario();
            u = DataObjects.Seguridad.UsuarioSQL.buscarUsuarioPorIdUsuario(Int32.Parse(Thread.CurrentPrincipal.Identity.Name));

            idTienda = u.IdTienda;
            aSQL = new AlmacenSQL();
            Almacenes deposito = aSQL.BuscarAlmacen(-1, idTienda, 1);

            id = deposito.IdAlmacen;

            /*NumColumnsU=1;
            NumRowsU = 1;*/

            NumColumns = deposito.NroColumnas;
            NumRows = deposito.NroFilas;
            Altura = deposito.Altura;

            tzSQL = new TipoZonaSQL();
            LstZonas = tzSQL.ObtenerZonasxAlmacen(deposito.IdAlmacen);

            Accion2 = 2;
            Accion1 = 2;



        }

        public void BuscarProductos( MadeInHouse.Dictionary.DynamicGrid almacen, MadeInHouse.Dictionary.DynamicGrid ubicacionCol)
        {

            if (TxtProducto == null || TxtProducto=="")
            {
                MyWindowManager w = new MyWindowManager();
                w.ShowWindow(new ProductoBuscarViewModel(this, 7));

            }
            
            else
            {

                ProductoCant pc = new ProductoCant();
                //Buscar producto del textBox Inicial:
                if (productoSeleccionado == null)
                {
                    ProductoSQL prodSQL= new ProductoSQL();
                    List<Producto> lstProd;
                    lstProd= prodSQL.BuscarProducto(TxtProducto);
                    if (lstProd == null)
                        MessageBox.Show("Producto no existente con ese código");
                    else
                    {
                        pc.IdProducto = lstProd[0].IdProducto;
                        pc.CodPro = TxtProducto;
                        productoSeleccionado = null;
                    }
                }
                else
                {
                    pc.IdProducto = ProductoSeleccionado.IdProducto;
                    pc.CodPro = ProductoSeleccionado.CodigoProd;
                    productoSeleccionado = null;
                }

                ubicacionCol.SelectedProduct = pc;
                almacen.UbicarProducto(pc.IdProducto);
                pc = null;
            }


        }


    }
}
