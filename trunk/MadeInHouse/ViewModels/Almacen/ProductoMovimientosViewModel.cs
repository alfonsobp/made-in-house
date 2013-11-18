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
using MadeInHouse.Dictionary;
using System.Windows.Media;

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


        private string txtCantidad;

        public string TxtCantidad
        {
            get { return txtCantidad; }
            set { txtCantidad = value;
            NotifyOfPropertyChange(() => TxtCantidad);
            }
        }

        private LinearGradientBrush colorAnt;

        public LinearGradientBrush ColorAnt
        {
            get { return colorAnt; }
            set { colorAnt = value;
            NotifyOfPropertyChange(() => ColorAnt);
            }
        }
        private List<ProductoCant> lstProductos;

        public List<ProductoCant> LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value;
            NotifyOfPropertyChange(() => LstProductos);
            }
        }

        private int numColumnAnq;

        public int NumColumnAnq
        {
            get { return numColumnAnq; }
            set { numColumnAnq = value;
            NotifyOfPropertyChange(() => NumColumnAnq);
            }
        }

        private int numRowsAnq;

        public int NumRowsAnq
        {
            get { return numRowsAnq; }
            set { numRowsAnq = value;
            NotifyOfPropertyChange(() => NumRowsAnq);
            }
        }

        private int alturaAnq;

        public int AlturaAnq
        {
            get { return alturaAnq; }
            set { alturaAnq = value;
            NotifyOfPropertyChange(() => AlturaAnq);
            }
        }

        private List<TipoZona> lstZonasAnq;

        public List<TipoZona> LstZonasAnq
        {
            get { return lstZonasAnq; }
            set { lstZonasAnq = value;
            NotifyOfPropertyChange(() => LstZonasAnq);
            }
        }


        private AlmacenSQL aSQL;
        private TipoZonaSQL tzSQL;
        private int idTienda;



        public ProductoMovimientosViewModel()
        {
            CmbZonas = (new TipoZonaSQL()).BuscarZona();
            Usuario u = new Usuario();
            u = DataObjects.Seguridad.UsuarioSQL.buscarUsuarioPorIdUsuario(Int32.Parse(Thread.CurrentPrincipal.Identity.Name));
            idTienda = u.IdTienda;

            aSQL = new AlmacenSQL();
            Almacenes deposito = aSQL.BuscarAlmacen(-1, idTienda, 1);
            Almacenes anaquel = aSQL.BuscarAlmacen(-1, idTienda, 2);

            NumColumnAnq = anaquel.NroColumnas;
            NumRowsAnq = anaquel.NroFilas;
            AlturaAnq = anaquel.Altura;

            NumColumns = deposito.NroColumnas;
            NumRows = deposito.NroFilas;
            Altura = deposito.Altura;

            tzSQL = new TipoZonaSQL();
            LstZonas = tzSQL.ObtenerZonasxAlmacen(deposito.IdAlmacen);
            LstZonasAnq = tzSQL.ObtenerZonasxAlmacen(anaquel.IdAlmacen);

            Accion2 = 2;
            Accion1 = 2;

            LstProductos=new List<ProductoCant>();



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
                        pc.Nombre = lstProd[0].Nombre;
                        pc.IdProducto = lstProd[0].IdProducto;
                        pc.CodigoProd = TxtProducto;
                        productoSeleccionado = null;
                    }
                }
                else
                {
                    pc.Nombre = ProductoSeleccionado.Nombre;
                    pc.IdProducto = ProductoSeleccionado.IdProducto;
                    pc.CodigoProd = ProductoSeleccionado.CodigoProd;
                    productoSeleccionado = null;
                }

                ubicacionCol.SelectedProduct = pc;
                almacen.UbicarProducto(pc.IdProducto);
                pc = null;
            }


        }

        public void Disminuir(DynamicGrid ubicacionCol, DynamicGrid almacen)
        {

            int exito = 0;
            if (String.IsNullOrEmpty(TxtCantidad))
            {
                MessageBox.Show("Debe ingresar una cantidad");
                return;
            }
            else if (ubicacionCol.SelectedProduct!=null)
            {
                    exito = ubicacionCol.DisminuirProductos(int.Parse(TxtCantidad), ubicacionCol.SelectedProduct.IdProducto);
                    if (exito == 1)
                    {
                        int index = LstProductos.FindIndex(x=>x.IdProducto==ubicacionCol.SelectedProduct.IdProducto);
                        if (index >= 0)
                        {
                            LstProductos[index].CanAtender = ""+(int.Parse(LstProductos[index].CanAtender) + int.Parse(TxtCantidad));
                        }
                        else
                        {
                            ubicacionCol.SelectedProduct.CanAtender = TxtCantidad;
                            LstProductos.Add(ubicacionCol.SelectedProduct);
                        }
                        LstProductos = new List<ProductoCant>(LstProductos);

                    }
            }




        }





    }
}
