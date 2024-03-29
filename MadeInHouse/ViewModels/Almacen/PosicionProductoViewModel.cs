﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;
using System.Windows.Controls;
using MadeInHouse.Dictionary;
using System.Data;
using MadeInHouse.DataObjects;
using System.Data.SqlClient;
using MadeInHouse.Models.Seguridad;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel.Composition;
using MadeInHouse.ViewModels.Layouts;

namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(SolicitudAbListadoViewModel))]
    class PosicionProductoViewModel : PropertyChangedBase
    {
        #region constructores

        [ImportingConstructor]
        public PosicionProductoViewModel(IWindowManager windowmanager, object sender, int accion)
        {
            _windowManager = windowmanager;
            this.accion = accion;
            if (accion == 1)
            {
                this.mantenerNotaDeIngresoViewModel = (sender as MantenerNotaDeIngresoViewModel);
                this.LstProductos = (sender as MantenerNotaDeIngresoViewModel).LstProductos;
                ImSource = "/Assets/add.png";
                Ejecutar = "Agregar";

            }
            else
            {
                this.mantenerNotaDeSalidaViewModel = (sender as MantenerNotaDeSalidaViewModel);
                this.LstProductos = (sender as MantenerNotaDeSalidaViewModel).LstProductos;
                ImSource = "/Assets/minus.png";
                Ejecutar = "Disminuir";

            }

            Usuario u = new Usuario();
            u = DataObjects.Seguridad.UsuarioSQL.buscarUsuarioPorIdUsuario(Int32.Parse(Thread.CurrentPrincipal.Identity.Name));

            idTienda = u.IdTienda;
            aSQL = new AlmacenSQL();
            Almacenes deposito = aSQL.BuscarAlmacen(-1, idTienda == 0 ? -1 : idTienda, idTienda == 0 ? 3 : 1);

            id = deposito.IdAlmacen;

            NumColumns = deposito.NroColumnas;
            NumRows = deposito.NroFilas;
            Altura = deposito.Altura;

            tzSQL = new TipoZonaSQL();
            LstZonas = tzSQL.ObtenerZonasxAlmacen(deposito.IdAlmacen);

            Accion2 = 2;
            Accion1 = 2;
            Enable = true;
        }

        #endregion

        private readonly IWindowManager _windowManager;
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
          set   { cantIngresar = value;
          NotifyOfPropertyChange(() => CantIngresar);
          }
        }

        private string volIngresar;

        public string VolIngresar
        {
            get { return volIngresar; }
            set { volIngresar = value;
            NotifyOfPropertyChange(() => VolIngresar);
            }
        }

        private List<Ubicacion> columnaU;

        public List<Ubicacion> ColumnaU
        {
            get { return columnaU; }
            set { columnaU = value;
            NotifyOfPropertyChange(() => ColumnaU);
            }
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
        private int id;

        private string imSource;

        public string ImSource
        {
            get { return imSource; }
            set { imSource = value;
            NotifyOfPropertyChange(() => ImSource);
            }
        }

        private string ejecutar;

        public string Ejecutar
        {
            get { return ejecutar; }
            set { ejecutar = value;
            NotifyOfPropertyChange(() => Ejecutar);
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


        private int accion;

        public int Accion
        {
            get { return accion; }
            set { accion = value; }
        }

        public void Disminuir(DynamicGrid ubicacionCol, DynamicGrid almacen)
        {
            _windowManager.ShowDialog(new AlertViewModel(_windowManager, "funciono"));
        }

        private MantenerNotaDeSalidaViewModel mantenerNotaDeSalidaViewModel;

        

        private bool enable;

        public bool Enable
        {
            get { return enable; }
            set { enable = value;
            NotifyOfPropertyChange(() => Enable);
            
            }
        }

        public void Agregar(DynamicGrid ubicacionCol , DynamicGrid almacen)
        {

            int exito=0;
            if (String.IsNullOrEmpty(CantIngresar))
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Debe ingresar una cantidad"));
                return;
            }
            else if (int.Parse(CantIngresar) <0)
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Debe ingresar una cantidad positiva"));
                return;
            }


            if (selectedProduct != null)
            {
                if (int.Parse(selectedProduct.CanAtender) < int.Parse(CantIngresar))
                {
                    if (accion == 1)
                        _windowManager.ShowDialog(new AlertViewModel(_windowManager, "La cantidad que se intenta ingresar es mayor a la cantidad pendiente"));
                    else
                        _windowManager.ShowDialog(new AlertViewModel(_windowManager, "La cantidad que se intenta retirar es mayor a la cantidad pendiente"));
                }
                
                else
                {
                    if (String.IsNullOrEmpty(VolIngresar)) VolIngresar = "0";
                    if (accion == 1) exito = ubicacionCol.AgregarProductos(int.Parse(CantIngresar), int.Parse(VolIngresar), SelectedProduct.IdProducto);
                    else exito=ubicacionCol.DisminuirProductos(int.Parse(CantIngresar), SelectedProduct.IdProducto);
                    
                    if (exito > 0)
                        selectedProduct.CanAtender = (int.Parse(selectedProduct.CanAtender) - int.Parse(CantIngresar)).ToString();
                   
                    LstProductos = new List<ProductoCant>(LstProductos);
                }
            }

            VolIngresar = "";
            CantIngresar = "";
        }



        public void SelectedItemChanged(object sender, MadeInHouse.Dictionary.DynamicGrid almacen, MadeInHouse.Dictionary.DynamicGrid ubicacionCol)
        {

            
            SelectedProduct = ((sender as DataGrid).SelectedItem as ProductoCant);
            ubicacionCol.SelectedProduct = SelectedProduct;

            if (SelectedProduct != null)
            {
                almacen.UbicarProducto(SelectedProduct.IdProducto);
            }
        }

        public void Guardar(DynamicGrid almacenDG )
        {
            DBConexion db = new DBConexion();
            db.conn.Open();
            SqlTransaction trans = db.conn.BeginTransaction(IsolationLevel.Serializable);
            db.cmd.Transaction = trans;
            uSQL = new UbicacionSQL(db);
            DataTable temporal= uSQL.CrearUbicacionesDT();
            uSQL.AgregarFilasToUbicacionesDT(temporal, almacenDG.Ubicaciones, id);
            uSQL.AgregarMasivo(temporal, trans);
            int exito= uSQL.ActualizarUbicacionMasivo();
            if (exito > 0)
            {
                UtilesSQL util = new UtilesSQL(db);
                util.LimpiarTabla("TemporalUbicacion");
                trans.Commit();
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Se guardo el stock"));
                
            }
            else
            {
                trans.Rollback();
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Error: Revisar conexión al servidor"));
            }
        }


    }
}
