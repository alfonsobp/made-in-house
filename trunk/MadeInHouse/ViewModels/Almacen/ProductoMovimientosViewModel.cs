using Caliburn.Micro;
using MadeInHouse.DataObjects;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Dictionary;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Seguridad;
using MadeInHouse.Validacion;
using MadeInHouse.ViewModels.Layouts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;

namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(ProductoMovimientosViewModel))]
    class ProductoMovimientosViewModel : PropertyChangedBase
    {
        #region constructores

        [ImportingConstructor]
        public ProductoMovimientosViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
            CmbZonas = (new TipoZonaSQL()).BuscarZona();
            Usuario u = new Usuario();
            u = DataObjects.Seguridad.UsuarioSQL.buscarUsuarioPorIdUsuario(Int32.Parse(Thread.CurrentPrincipal.Identity.Name));
            idTienda = u.IdTienda;
            idResponsable = u.IdUsuario;

            aSQL = new AlmacenSQL();
            Almacenes deposito = aSQL.BuscarAlmacen(-1, idTienda, 1);
            Almacenes anaquel = aSQL.BuscarAlmacen(-1, idTienda, 2);
            idDeposito = deposito.IdAlmacen;
            idAnaquel = anaquel.IdAlmacen;

            NumColumnAnq = anaquel.NroColumnas;
            NumRowsAnq = anaquel.NroFilas;
            AlturaAnq = anaquel.Altura;

            NumColumns = deposito.NroColumnas;
            NumRows = deposito.NroFilas;
            Altura = deposito.Altura;

            tzSQL = new TipoZonaSQL();
            LstZonas = tzSQL.ObtenerZonasxAlmacen(idDeposito);
            LstZonasAnq = tzSQL.ObtenerZonasxAlmacen(idAnaquel, 2);

            Accion2 = 2;
            Accion1 = 2;

            LstProductos = new List<ProductoCant>();
            Atendido = false;
        }

        #endregion

        private readonly IWindowManager _windowManager;

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

        private int selectedZona;

        public int SelectedZona
        {
            get { return selectedZona; }
            set
            {
                selectedZona = value;
                NotifyOfPropertyChange(() => SelectedZona);

            }
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
            set
            {
                txtProducto = value;

                NotifyOfPropertyChange(() => TxtProducto);
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
            set
            {
                txtCantidad = value;
                NotifyOfPropertyChange(() => TxtCantidad);
            }
        }

        private LinearGradientBrush colorAnt;

        public LinearGradientBrush ColorAnt
        {
            get { return colorAnt; }
            set
            {
                colorAnt = value;
                NotifyOfPropertyChange(() => ColorAnt);
            }
        }
        private List<ProductoCant> lstProductos;

        public List<ProductoCant> LstProductos
        {
            get { return lstProductos; }
            set
            {
                lstProductos = value;
                NotifyOfPropertyChange(() => LstProductos);
            }
        }

        private int numColumnAnq;

        public int NumColumnAnq
        {
            get { return numColumnAnq; }
            set
            {
                numColumnAnq = value;
                NotifyOfPropertyChange(() => NumColumnAnq);
            }
        }

        private int numRowsAnq;

        public int NumRowsAnq
        {
            get { return numRowsAnq; }
            set
            {
                numRowsAnq = value;
                NotifyOfPropertyChange(() => NumRowsAnq);
            }
        }

        private int alturaAnq;

        public int AlturaAnq
        {
            get { return alturaAnq; }
            set
            {
                alturaAnq = value;
                NotifyOfPropertyChange(() => AlturaAnq);
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

        private int idZona;

        public int IdZona
        {
            get { return idZona; }
            set { idZona = value; }
        }

        private int txtStockActual;

        public int TxtStockActual
        {
            get { return txtStockActual; }
            set
            {
                txtStockActual = value;
                NotifyOfPropertyChange(() => TxtStockActual);
            }
        }

        private string txtVolOcupado;

        public string TxtVolOcupado
        {
            get { return txtVolOcupado; }
            set
            {
                txtVolOcupado = value;
                NotifyOfPropertyChange(() => TxtVolOcupado);
            }
        }

        private int txtCapacidad;

        public int TxtCapacidad
        {
            get { return txtCapacidad; }
            set
            {
                txtCapacidad = value;
                NotifyOfPropertyChange(() => TxtCapacidad);
            }
        }

        private bool enable;

        public bool Enable
        {
            get { return enable; }
            set
            {
                enable = value;
                NotifyOfPropertyChange(() => Enable);
            }
        }


        private AlmacenSQL aSQL;
        private TipoZonaSQL tzSQL;
        private int idTienda;
        private int idDeposito;
        private int idAnaquel;
        private int idResponsable;



        public void BuscarProductos(MadeInHouse.Dictionary.DynamicGrid almacen, MadeInHouse.Dictionary.DynamicGrid ubicacionCol)
        {

            if (TxtProducto == null || TxtProducto == "")
            {
                _windowManager.ShowWindow(new ProductoBuscarViewModel(_windowManager, this, 7, idTienda));
            }

            else
            {

                ProductoCant pc = new ProductoCant();
                //Buscar producto del textBox Inicial:
                if (productoSeleccionado == null)
                {
                    ProductoSQL prodSQL = new ProductoSQL();
                    List<Producto> lstProd;
                    lstProd = prodSQL.BuscarProducto(TxtProducto);
                    if (lstProd == null)
                        _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Producto no existente con ese código"));
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

            Evaluador eva = new Evaluador();

            int exito = 0;
            if (String.IsNullOrEmpty(TxtCantidad))
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Debe ingresar una cantidad"));
                return;
            }
            else if (!eva.esNumeroEntero(TxtCantidad))
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Debe ingresar un número"));
                return;
            }
            else if (int.Parse(TxtCantidad) < 0)
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Debe ingresar una cantidad mayor a cero"));
                return;
            }
            else if (ubicacionCol.SelectedProduct != null)
            {

                exito = ubicacionCol.DisminuirProductos(int.Parse(TxtCantidad), ubicacionCol.SelectedProduct.IdProducto);
                if (exito == 1)
                {
                    int index = LstProductos.FindIndex(x => x.IdProducto == ubicacionCol.SelectedProduct.IdProducto);
                    if (index >= 0)
                    {
                        LstProductos[index].CanAtender = "" + (int.Parse(LstProductos[index].CanAtender) + int.Parse(TxtCantidad));
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

        private ProductoCant selectedProduct;

        public ProductoCant SelectedProduct
        {
            get { return selectedProduct; }
            set { selectedProduct = value; }
        }

        private bool atendido;

        public bool Atendido
        {
            get { return atendido; }
            set
            {
                atendido = value;
                NotifyOfPropertyChange(() => Atendido);
            }
        }

        private bool enbl;

        public bool Enbl
        {
            get { return enbl; }
            set
            {
                enbl = value;
                NotifyOfPropertyChange(() => Enbl);
            }
        }

        public void SelectedItemChanged(object sender, MadeInHouse.Dictionary.DynamicGrid anaquel)
        {
            SelectedProduct = ((sender as DataGrid).SelectedItem as ProductoCant);
            anaquel.SelectedProduct = SelectedProduct;
            if (selectedZona > 0)
            {
                if (SelectedProduct != null)
                {

                    anaquel.UbicarSector(SelectedProduct.IdProducto);
                    TxtCapacidad = anaquel.CapacidadActual;
                    TxtVolOcupado = anaquel.VolOcupadoActual + "%";
                    TxtStockActual = anaquel.StockActual;
                    Atendido = SelectedProduct.Atendido;
                    //(sender as DataGrid).SelectedItem = null;

                }
            }
            else
            {
                (sender as DataGrid).SelectedItem = null;
            }
        }

        public void GuardarCantidadParcial()
        {

            TipoZona tz = LstZonasAnq.Find(x => x.IdTipoZona == SelectedZona);
            if (tz != null)
            {

                Sector sector = tz.LstSectores.Find(x => x.IdProducto == SelectedProduct.IdProducto);
                if (sector == null)
                {
                    _windowManager.ShowDialog(new AlertViewModel(_windowManager, "El producto no tiene un espacio separado en los anaqueles , separe un espacio"));
                    return;
                }
                else
                {
                    sector.Cantidad += int.Parse(SelectedProduct.CanAtender);
                    if (sector.Cantidad > sector.Capacidad)
                    {
                        _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Se superó la capacidad , debe separar más espacio en los anaqueles"));
                        sector.Cantidad -= int.Parse(SelectedProduct.CanAtender);
                        return;
                    }
                    sector.CantidadIngresada = int.Parse(SelectedProduct.CanAtender);
                    sector.VolOcupado = (int)(((double)sector.Cantidad / sector.Capacidad) * 100);
                    TxtVolOcupado = sector.VolOcupado + "%";
                    TxtStockActual = sector.Cantidad;
                    TxtCapacidad = sector.Capacidad;
                    SelectedProduct.Atendido = false;
                    Atendido = false;
                }
            }


        }

        public void GuardarMovimiento(DynamicGrid anaquel, DynamicGrid deposito)
        {
            int exito;

            /*Inicializacion de la transacción*/
            DBConexion db = new DBConexion();
            try
            {
                db.conn.Open();
                SqlTransaction trans = db.conn.BeginTransaction(IsolationLevel.ReadCommitted);

                db.cmd.Transaction = trans;

                SectorSQL sectorSQL = new SectorSQL(db);
                UbicacionSQL ubicacionSQL = new UbicacionSQL(db);

                /*Tablas temporales*/
                DataTable sectoresDT = sectorSQL.CrearSectoresDT();
                DataTable ubicacionesDT = ubicacionSQL.CrearUbicacionesDT();

                List<Sector> lstSectores = new List<Sector>();
                List<Ubicacion> ubicacionesModificadas = new List<Ubicacion>();

                /*Agrupo todos los sectores y ubicaciones modificadas*/
                for (int i = 0; i < anaquel.lstZonas.Count; i++)
                {
                    lstSectores.AddRange(anaquel.lstZonas[i].LstSectores);
                    for (int j = 0; j < anaquel.lstZonas[i].LstSectores.Count; j++)
                    {
                        ubicacionesModificadas.AddRange(anaquel.lstZonas[i].LstSectores[j].LstUbicaciones);
                    }

                }

                /*Agrego las filas a los DT*/
                sectorSQL.AgregarFilasToSectoresDT(sectoresDT, lstSectores);
                ubicacionSQL.AgregarFilasToUbicacionesDT(ubicacionesDT, ubicacionesModificadas);

                /*empieza el guardado en la bd*/
                exito = sectorSQL.AgregarMasivo(sectoresDT, trans); //insertados en TemporalSector
                if (exito > 0)
                {
                    exito = sectorSQL.ActualizarSectorMasivo(); //actualizados en tabla Sector
                    if (exito > 0)
                    {

                        exito = sectorSQL.ActualizarIdSector(); //actualizo idSector en tabla sector
                        if (exito > 0)
                        {
                            /*Agrego las ubicaciones de almacen de salida*/
                            ubicacionSQL.AgregarFilasToUbicacionesDT(ubicacionesDT, deposito.Ubicaciones, deposito.Ubicaciones[0][0][0].IdAlmacen);
                            exito = ubicacionSQL.AgregarMasivo(ubicacionesDT, trans);
                            if (exito > 0)
                            {
                                exito = ubicacionSQL.ActualizarIdSector();
                                if (exito > 0)
                                {
                                    exito = ubicacionSQL.ActualizarUbicacionMasivo();
                                    if (exito > 0)
                                    {
                                        /*Agrego el movimiento como un "todo" a la bd*/
                                        NotaISSQL notaSQL = new NotaISSQL(db);
                                        
                                        /*Salida para el depósito*/
                                        NotaIS nota = new NotaIS();
                                        nota.IdMotivo = 8;
                                        nota.Tipo = 2;
                                        nota.Observaciones = "";
                                        nota.IdDoc = 0;
                                        nota.IdAlmacen = idDeposito;
                                        nota.IdResponsable = idResponsable;

                                        /*Entrada para el anaquel*/
                                        NotaIS notaAn = new NotaIS();
                                        notaAn.IdMotivo = 8;
                                        notaAn.Tipo = 1;
                                        notaAn.Observaciones = "";
                                        notaAn.IdDoc = 0;
                                        notaAn.IdAlmacen = idAnaquel;
                                        notaAn.IdResponsable = idResponsable;

                                        int idNotaDeposito = notaSQL.AgregarNota(nota, 1);
                                        int idNotaAnaquel = notaSQL.AgregarNota(notaAn, 1);
                                        
                                        if (idNotaDeposito > 0 && idNotaAnaquel> 0)
                                        {
                                            exito = sectorSQL.ActualizarTemporalSector(idNotaDeposito);

                                            if (exito > 0)
                                            {
                                                exito = notaSQL.AgregarNotaxSector();

                                                if (exito > 0)
                                                {
                                                    exito = sectorSQL.ActualizarTemporalSector(idNotaAnaquel);

                                                    if (exito > 0)
                                                    {
                                                        exito = notaSQL.AgregarNotaxSector();
                                                        if (exito > 0)
                                                        {
                                                            UtilesSQL util = new UtilesSQL(db);
                                                            util.LimpiarTabla("TemporalUbicacion");
                                                            util.LimpiarTabla("TemporalSector");

                                                            trans.Commit();
                                                            _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Los productos fueron transferidos correctamente"));
                                                            return;
                                                        }

                                                    }

                                                    
                                                }
                                            }

                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Lo sentimos , se produjo un error"));
                trans.Rollback();
            }
            catch (SqlException e)
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Se fue la conexión"));
            }
        }
    }
}