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
using MadeInHouse.Views.RRHH;
using MadeInHouse.Validacion;

namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerTiendaViewModel : PropertyChangedBase
    {

        #region Atributos
        private UbigeoSQL uSQL;
        private TiendaSQL tSQL;
        private ProductoSQL pxaSQL;
        private AlmacenSQL aSQL;
        private UbicacionSQL ubSQL;
        private TipoZonaSQL tzSQL;
        private int idAlmacen;

        List<TipoZona> lstZonasAnq;
        List<TipoZona> lstZonasDto;
        
        private int accion; /*1=ABRIR NUEVO , 2=ABRIR EDITAR*/
        private string content;


        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                NotifyOfPropertyChange(() => Content);
            }
        }

        private int index1;

        public int Index1
        {
            get { return index1; }
            set { index1 = value;
            NotifyOfPropertyChange(() => Index1);
            }
        }

        private int index2;

        public int Index2
        {
            get { return index2; }
            set { index2 = value;
            NotifyOfPropertyChange(() => Index2);
            }
        }

        private int index3;

        public int Index3
        {
            get { return index3; }
            set { index3 = value;
            NotifyOfPropertyChange(() => Index3);
            }
        }

        private List<ProductoxTienda> lstProdAgregados;

        public List<ProductoxTienda> LstProdAgregados
        {
            get { return lstProdAgregados; }
            set { lstProdAgregados = value; }
        }

        private string txtNumColumns;

        public string TxtNumColumns
        {
            get { return txtNumColumns; }
            set
            {
                txtNumColumns = value;
            }
        }

        private string txtNumRows;

        public string TxtNumRows
        {
            get { return txtNumRows; }
            set
            {
                txtNumRows = value;
            }
        }

        private string txtNombre;

        public string TxtNombre
        {
            get { return txtNombre; }
            set { txtNombre = value;
            NotifyOfPropertyChange(() => TxtNombre);
            }
        }


        private string txtAdmin;

        public string TxtAdmin
        {
            get { return txtAdmin; }
            set { txtAdmin = value; }
        }

        private string txtTelef;

        public string TxtTelef
        {
            get { return txtTelef; }
            set { txtTelef = value;
            NotifyOfPropertyChange(() => TxtTelef);
            }
        }

        private List<Ubigeo> cmbDpto;

        public List<Ubigeo> CmbDpto
        {
            get { return this.cmbDpto; }
            private set
            {

                if (this.cmbDpto == value)
                {
                    return;
                }
                this.cmbDpto = value;
                this.NotifyOfPropertyChange(() => this.cmbDpto);
            }

        }
        private string selectedDpto;

        public string SelectedDpto
        {
            get { return selectedDpto; }
            set
            {
                selectedDpto = value;
                UbigeoSQL usql = new UbigeoSQL();
                CmbProv = usql.BuscarProv(selectedDpto);
            }
        }


        private List<Ubigeo> cmbProv;

        public List<Ubigeo> CmbProv
        {
            get { return this.cmbProv; }
            private set
            {

                if (this.cmbProv == value)
                {
                    return;
                }
                this.cmbProv = value;
                this.NotifyOfPropertyChange(() => this.cmbProv);


            }

        }

        private string selectedProv;

        public string SelectedProv
        {
            get { return selectedProv; }
            set
            {
                selectedProv = value;
                UbigeoSQL usql = new UbigeoSQL();
                CmbDist = usql.BuscarDist(selectedDpto, selectedProv);
            }
        }

        private List<Ubigeo> cmbDist;

        public List<Ubigeo> CmbDist
        {
            get { return this.cmbDist; }
            private set
            {
                if (this.cmbDist == value)
                {
                    return;
                }
                this.cmbDist = value;
                this.NotifyOfPropertyChange(() => this.cmbDist);


            }

        }


        private string selectedDist;

        public string SelectedDist
        {
            get { return selectedDist; }
            set { selectedDist = value; }
        }

        private string txtDir;

        public string TxtDir
        {
            get { return txtDir; }
            set { txtDir = value;
            NotifyOfPropertyChange(() => TxtDir);
            }
        }

        /*Configuracion de productos en tienda*/

        private string txtCodProducto;

        public string TxtCodProducto
        {
            get { return txtCodProducto; }
            set
            {
                txtCodProducto = value;
                NotifyOfPropertyChange(() => TxtCodProducto);
            }
        }

        private string txtStockIni;

        public string TxtStockIni
        {
            get { return txtStockIni; }
            set
            {
                txtStockIni = value;
                NotifyOfPropertyChange(() => TxtStockIni);

            }
        }

        private string txtStockMin;

        public string TxtStockMin
        {
            get { return txtStockMin; }
            set
            {
                txtStockMin = value;
                NotifyOfPropertyChange(() => TxtStockMin);
            }
        }

        private string txtStockMax;

        public string TxtStockMax
        {
            get { return txtStockMax; }
            set
            {
                txtStockMax = value;
                NotifyOfPropertyChange(() => TxtStockMax);
            }
        }

        private string txtPrecioV;

        public string TxtPrecioV
        {
            get { return txtPrecioV; }
            set
            {
                txtPrecioV = value;
                NotifyOfPropertyChange(() => TxtPrecioV);
            }
        }


        private bool chkVigente;

        public bool ChkVigente
        {
            get { return chkVigente; }
            set
            {
                chkVigente = value;
                NotifyOfPropertyChange(() => ChkVigente);
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


        /*Parametros para distribucion*/

        /*Anaqueles*/
        private string txtNumRowsAnq;

        public string TxtNumRowsAnq
        {
            get { return txtNumRowsAnq; }
            set { txtNumRowsAnq = value;
            NotifyOfPropertyChange(() => TxtNumRowsAnq);
            }
        }

        private string txtNumColumnsAnq;

        public string TxtNumColumnsAnq
        {
            get { return txtNumColumnsAnq; }
            set { txtNumColumnsAnq = value;
            NotifyOfPropertyChange(() => TxtNumColumnsAnq);
            }
        }



        private string txtAlturaAnq;

        public string TxtAlturaAnq
        {
            get { return txtAlturaAnq; }
            set { txtAlturaAnq = value;
            NotifyOfPropertyChange(() => TxtAlturaAnq);
            }
        }




        /*Deposito*/
        private string txtNumRowsDto;

        public string TxtNumRowsDto
        {
            get { return txtNumRowsDto; }
            set { txtNumRowsDto = value;
            NotifyOfPropertyChange(() => TxtNumRowsDto);
            }
        }

        private string txtNumColumnsDto;

        public string TxtNumColumnsDto
        {
            get { return txtNumColumnsDto; }
            set { txtNumColumnsDto = value;
            NotifyOfPropertyChange(() => TxtNumColumnsDto);
            }
        }

        private string txtAlturaDto;

        public string TxtAlturaDto
        {
            get { return txtAlturaDto; }
            set { txtAlturaDto = value;
            NotifyOfPropertyChange(() => TxtAlturaDto);
            }
        }

        private string fondo;

        public string Fondo
        {
            get { return fondo; }
            set
            {
                fondo = value;
                NotifyOfPropertyChange(() => Fondo);
            }
        }

        private int numColumnsAnq;

        public int NumColumnsAnq
        {
            get { return numColumnsAnq; }
            set
            {
                numColumnsAnq = value;
                NotifyOfPropertyChange(() => NumColumnsAnq);
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
            set { alturaAnq = value;
            NotifyOfPropertyChange(() => AlturaAnq);
            }
            
        }

  


        private int numColumnsDto;

        public int NumColumnsDto
        {
            get { return numColumnsDto; }
            set
            {
                numColumnsDto = value;
                NotifyOfPropertyChange(() => NumColumnsDto);
            }
        }

        private int numRowsDto;

        public int NumRowsDto
        {
            get { return numRowsDto; }
            set
            {
                numRowsDto = value;
                NotifyOfPropertyChange(() => NumRowsDto);

            }
        }

        private int alturaDto;

        public int AlturaDto
        {
            get { return alturaDto; }
            set { alturaDto = value;
            NotifyOfPropertyChange(() => AlturaDto);
            }
            
        }



        private int zonaAnaquel;

        public int ZonaAnaquel
        {
            get { return zonaAnaquel; }
            set
            {
                zonaAnaquel = value;
                NotifyOfPropertyChange(() => ZonaAnaquel);
            }
        }

        private int zonaDeposito;

        public int ZonaDeposito
        {
            get { return zonaDeposito; }
            set
            {
                zonaDeposito = value;
                NotifyOfPropertyChange(() => ZonaDeposito);
            }
        }

        private ObservableCollection<TipoZona> cmbZonas;

        public ObservableCollection<TipoZona> CmbZonas
        {
            get { return cmbZonas; }
            set { cmbZonas = value; }
        }

        private TipoZona selectedZona;

        public TipoZona SelectedZona
        {
            get { return selectedZona; }
            set
            {
                if (selectedZona == value)
                {
                    return;
                }
                selectedZona = value;
                Fondo = selectedZona.Color;
                ZonaDeposito = selectedZona.IdTipoZona;
                ZonaAnaquel = selectedZona.IdTipoZona;
            }
        }

        private int selectedIndex;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; }
        }

        private ProductoxTienda selectedItem;

        public ProductoxTienda SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; }
        }

        private bool editar=true;

        public bool Editar
        {
            get { return editar; }
            set { editar = value;
            NotifyOfPropertyChange(() => Editar);
            }
        }

        private int idTienda;
        private int idAnaquel;
        private int idDeposito;

        #endregion

        

        #region Constructores

        public MantenerTiendaViewModel(Tienda t) :this()
        {
            accion = 2;
            Editar = false;
            idTienda = t.IdTienda;

            /*carga de la informacion general*/
            TxtNombre = t.Nombre;
            TxtTelef=t.Telefono;
            TxtAdmin = t.Administrador;
            TxtDir = t.Direccion;
            List<Ubigeo> u = uSQL.buscarUbigeo2(t.IdUbigeo);

            /*carga de los combobox*/
            CmbDpto = uSQL.BuscarDpto();
            Index1=CmbDpto.FindIndex(x => x.CodDpto == u[0].CodDpto);
            SelectedDpto = u[0].CodDpto;
            Index2 = CmbProv.FindIndex(x => x.CodProv == u[0].CodProv);
            SelectedProv = u[0].CodProv;
            Index3 = CmbDist.FindIndex(x => x.CodDist == u[0].CodDist);
            
            /*Carga de los productos*/
            LstProductos = pxaSQL.BuscarProductoxTienda(t.IdTienda);

            /*Carga del deposito y los anaqueles*/
            Almacenes anaquel = aSQL.BuscarAlmacen(-1, t.IdTienda, 2);
            idAnaquel = anaquel.IdAlmacen;
            
            Almacenes deposito =aSQL.BuscarAlmacen(-1,t.IdTienda,1);
            idDeposito = deposito.IdAlmacen;

            Content = "Ver distribución";
            TxtNumColumnsAnq = anaquel.NroColumnas.ToString();
            TxtNumRowsAnq = anaquel.NroFilas.ToString();
            TxtAlturaAnq = anaquel.Altura.ToString();

            TxtNumColumnsDto = deposito.NroColumnas.ToString();
            TxtNumRowsDto = deposito.NroFilas.ToString();
            TxtAlturaDto = deposito.Altura.ToString();

            lstZonasAnq = tzSQL.ObtenerZonasxAlmacen(anaquel.IdAlmacen);
            lstZonasDto = tzSQL.ObtenerZonasxAlmacen(deposito.IdAlmacen);
            
        }

        public MantenerTiendaViewModel()
        {
            uSQL = new UbigeoSQL();
            tSQL = new TiendaSQL();
            pxaSQL = new ProductoSQL();
            aSQL = new AlmacenSQL();
            tzSQL = new TipoZonaSQL();

            CmbZonas = (new TipoZonaSQL()).BuscarZona();
            CmbDpto = uSQL.BuscarDpto();
            LstProductos = new List<ProductoxTienda>();
            LstProdAgregados = new List<ProductoxTienda>();
            //LstProductos = pxaSQL.BuscarProductoxTienda();
            Content = "Generar distribución";
            accion = 1;

        }
        #endregion

        #region Métodos

        public void Distribuir(int tipo, MadeInHouse.Dictionary.DynamicGrid dg)
        {

            Evaluador eva = new Evaluador();
            

            if (tipo == 0)
            {
                if (String.IsNullOrEmpty(TxtAlturaAnq) || String.IsNullOrEmpty(TxtNumColumnsAnq) || String.IsNullOrEmpty(TxtNumRowsAnq) ||
                   !eva.esNumeroEntero(TxtNumColumnsAnq) || !eva.esNumeroEntero(TxtNumRowsAnq) || !eva.esNumeroEntero(TxtAlturaAnq)
                )
                {
                    System.Windows.MessageBox.Show("Debes ingresar un numero entero para el numero de filas , columnas y altura");
                    return;
                }
                else if (int.Parse(TxtAlturaAnq) <= 0 ||  int.Parse(TxtNumColumnsAnq) <= 0 ||
                        int.Parse(TxtNumRowsAnq) <= 0  )
                    {
                    System.Windows.MessageBox.Show("Debes ingresar un numero entero para el numero de filas , columnas y altura");
                    return;
                    }


                NumColumnsAnq = Int32.Parse(TxtNumColumnsAnq);
                NumRowsAnq = Int32.Parse(TxtNumRowsAnq);
                AlturaAnq = Int32.Parse(TxtAlturaAnq);
                dg.RecreateGridCells();

                if (accion == 2)
                {
                    dg.cargarGrid(lstZonasAnq);
                }
                
               
            }
            else
            {

                if (String.IsNullOrEmpty(TxtAlturaAnq) || String.IsNullOrEmpty(TxtNumColumnsDto) || String.IsNullOrEmpty(TxtNumRowsDto)
                     || !eva.esNumeroEntero(TxtAlturaDto) || !eva.esNumeroEntero(TxtNumColumnsDto) || !eva.esNumeroEntero(TxtNumRowsDto))
                {
                    System.Windows.MessageBox.Show("Debes ingresar un numero entero para el numero de filas , columnas y altura");
                    return;
                }
                else if (int.Parse(TxtAlturaDto) <= 0 || int.Parse(TxtNumColumnsAnq) <= 0 ||
                    int.Parse(TxtNumRowsDto) <= 0)
                {
                    System.Windows.MessageBox.Show("Debes ingresar un numero entero para el numero de filas , columnas y altura");
                    return;
                }

                NumColumnsDto = Int32.Parse(TxtNumColumnsDto);
                NumRowsDto = Int32.Parse(TxtNumRowsDto);
                AlturaDto = Int32.Parse(TxtAlturaDto);
                dg.RecreateGridCells();

                if (accion == 2)
                {
                    dg.cargarGrid(lstZonasDto);
                }
            }
        }

        public void BuscarProductos()
        {
            MadeInHouse.Models.MyWindowManager wm = new Models.MyWindowManager();
            wm.ShowWindow(new ProductoBuscarViewModel(wm, this, 2));
        }

        public void Agregar()
        {

            if (TxtCodProducto == null || TxtStockMax == null || TxtStockMin == null || TxtPrecioV == null)
            {
                System.Windows.MessageBox.Show("Debe completar todos los campos");
            }
            else
            {
                ProductoxTienda pxa;
                List<Producto> lstAux = null;
                lstAux = pxaSQL.BuscarProducto(TxtCodProducto,-1,-1,idTienda);

                if (lstAux != null)
                {

                    if ((pxa = LstProductos.Find(x => x.IdProducto == lstAux[0].IdProducto)) == null)
                    {

                        pxa = new ProductoxTienda();
                        pxa.CodProducto = lstAux[0].CodigoProd;
                        pxa.IdProducto = lstAux[0].IdProducto;
                        pxa.Nombre = lstAux[0].Nombre;
                        pxa.StockActual = String.IsNullOrEmpty(TxtStockIni) ? 0 : Int32.Parse(TxtStockIni);
                        pxa.StockMin = Int32.Parse(TxtStockMin);
                        pxa.StockMax = Int32.Parse(TxtStockMax);
                        pxa.PrecioVenta = float.Parse(txtPrecioV);
                        pxa.Vigente = (ChkVigente == true) ? 1 : 0;
                        LstProductos.Add(pxa);
                        LstProductos = new List<ProductoxTienda>(LstProductos);
                    }
                    else if (accion == 2)
                    {
                        pxa.Vigente = (ChkVigente == true) ? 1 : 0;
                        pxa.StockMin = Int32.Parse(TxtStockMin);
                        pxa.StockMax = Int32.Parse(TxtStockMax);
                        pxa.PrecioVenta = float.Parse(txtPrecioV);
                        LstProductos = new List<ProductoxTienda>(LstProductos);
                        System.Windows.MessageBox.Show("El actualizaron los datos del producto correctamente");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("El producto que se quiere registrar ya existe en la tienda");
                    }

                }
                else
                {
                    System.Windows.MessageBox.Show("El código proporcionado no existe");
                }
            }
        }

        public void Quitar()
        {
            if (SelectedItem != null)
            {
                LstProductos.Remove(SelectedItem);
                LstProductos = new List<ProductoxTienda>(LstProductos);
                Limpiar();
            }
        }

        public void Importar()
        {
            System.Data.DataTableReader dt = MadeInHouse.Dictionary.ExcelUtiles.Importar("Productos");
            if (dt != null)
            {
                List<Producto> lstAux = null;
                lstAux = pxaSQL.BuscarProducto();
                bool exito = true;
                LstProdAgregados.Clear();

                while (dt.Read())
                {

                    ProductoxTienda pxa = new ProductoxTienda();
                    Producto pAux = (lstAux.Find(x => x.CodigoProd == dt["Codigo"].ToString()));
                    if (pAux == null)
                    {
                        System.Windows.MessageBox.Show("ERROR: No se pudo terminar la carga porque el producto " + dt["Codigo"].ToString() + " no ha sido registrado en la empresa");
                        exito = false;
                        break;
                    }
                    else if ((LstProductos.Find(x => x.IdProducto == pAux.IdProducto)) == null && (LstProdAgregados.Find(x => x.IdProducto == pAux.IdProducto)) == null)
                    {

                        pxa.CodProducto = pAux.CodigoProd;
                        pxa.Nombre = pAux.Nombre;
                        pxa.IdProducto = pAux.IdProducto;
                        pxa.IdAlmacen = idAlmacen;
                        pxa.StockActual = Convert.ToInt32(dt["StockActual"]);
                        pxa.StockMin = Convert.ToInt32(dt["StockMin"]);
                        pxa.StockMax = Convert.ToInt32(dt["StockMax"]);
                        pxa.PrecioVenta = float.Parse(dt["PrecioVenta"].ToString());
                        pxa.Vigente = int.Parse(dt["Vigente"].ToString());
                        LstProdAgregados.Add(pxa);
                    }
                    else
                    {
                        LstProdAgregados.Clear();
                        System.Windows.MessageBox.Show("ERROR: No se pudo terminar la carga porque el producto " + dt["Codigo"].ToString() + " ya se registro para la empresa o se repite en su lista");
                        exito = false;
                        break;
                    }
                }
                dt.Close();

                if (exito)
                {
                    for (int i = 0; i < LstProdAgregados.Count; i++)
                    {
                        LstProductos.Add(LstProdAgregados[i]);
                    }
                    LstProductos = new List<ProductoxTienda>(LstProductos);
                }


            }
        }

        public void SelectedItemChanged(object sender)
        {

            SelectedItem = ((sender as DataGrid).SelectedItem as ProductoxTienda);
            if (SelectedItem != null)
            {
                TxtCodProducto = SelectedItem.CodProducto;
                TxtStockIni = SelectedItem.StockActual.ToString();
                TxtStockMin = SelectedItem.StockMin.ToString();
                TxtStockMax = SelectedItem.StockMax.ToString();
                TxtPrecioV = SelectedItem.PrecioVenta.ToString();
                ChkVigente = SelectedItem.Vigente == 1 ? true : false;
            }
        }

        public void Limpiar()
        {
            TxtCodProducto = null;
            TxtStockMax = null;
            TxtStockIni = null;
            TxtStockMin = null;
            TxtPrecioV = null;
            ChkVigente = false;
        }

        public List<int> ObtenerListaZonas(MadeInHouse.Dictionary.DynamicGrid objeto)
        {
            List<int> lista = new List<int>();

            bool encontrado = false;
            for (int i = 0; i < objeto.Ubicaciones.Count; i++)
            {

                for (int j = 0; j < objeto.Ubicaciones[i].Count; j++)
                {
                    
                        for (int k = 0; k < lista.Count; k++)
                        {
                            if (lista[k] == objeto.Ubicaciones[i][j][0].IdTipoZona)
                            {
                                encontrado = true;
                                break;
                            }
                        }
                        if (!encontrado)
                            lista.Add(objeto.Ubicaciones[i][j][0].IdTipoZona);
                        encontrado = false;
                    
                }
            }
            return lista;
        }
        
        public DataTable CrearZonasDT()
        {
            /*Agrego las zonas por almacen*/
            DataTable zonaxAlmacenData = new DataTable("ZonaxAlmacen");

            // Create Column 1: idTipoZona

            DataColumn idZonaCol = new DataColumn();
            idZonaCol.DataType = Type.GetType("System.Int32");
            idZonaCol.ColumnName = "idTipoZona";


            // Create Column 2: idAlmacen
            DataColumn idAlmacenCol = new DataColumn();
            idAlmacenCol.DataType = Type.GetType("System.Int32");
            idAlmacenCol.ColumnName = "idAlmacen";

            // Create Column 3: nroBloquesDisp
            DataColumn nroBloquesDispCol = new DataColumn();
            nroBloquesDispCol.DataType = Type.GetType("System.Int32");
            nroBloquesDispCol.ColumnName = "nroBloquesDisp";

            // Create Column 3: nroBloquesTotal
            DataColumn nroBloquesTotalCol = new DataColumn();
            nroBloquesTotalCol.DataType = Type.GetType("System.Int32");
            nroBloquesTotalCol.ColumnName = "nroBloquesTotal";

            // Add the columns to the ProductSalesData DataTable
            zonaxAlmacenData.Columns.Add(idZonaCol);
            zonaxAlmacenData.Columns.Add(idAlmacenCol);
            zonaxAlmacenData.Columns.Add(nroBloquesDispCol);
            zonaxAlmacenData.Columns.Add(nroBloquesTotalCol);

            return zonaxAlmacenData;

        }

        public void AgregarFilasToZonasDT(DataTable data, Dictionary<int, int> dic, int id)
        
        {

            if (dic != null)
            {
                for (int i = 0; i < dic.Count; i++)
                {

                    if (dic.ElementAt(i).Value > 0)
                    {
                        DataRow zonaxAlmacenRow = data.NewRow();
                        zonaxAlmacenRow["idTipoZona"] = dic.ElementAt(i).Key;
                        zonaxAlmacenRow["idAlmacen"] = id;
                        zonaxAlmacenRow["nroBloquesDisp"] = dic.ElementAt(i).Value;
                        zonaxAlmacenRow["nroBloquesTotal"] = dic.ElementAt(i).Value;

                        // Add the row to the ProductSalesData DataTable
                        data.Rows.Add(zonaxAlmacenRow);

                    }

                }
            }
        }

        public DataTable CrearUbicacionesDT()
        {


            /*Agrego las zonas por almacen*/
            DataTable ubicacionesData = new DataTable("Ubicacion");

            // Create Column 1: idTipoZona

            DataColumn idZonaCol = new DataColumn();
            idZonaCol.DataType = Type.GetType("System.Int32");
            idZonaCol.ColumnName = "idTipoZona";


            // Create Column 2: idAlmacen
            DataColumn idAlmacenCol = new DataColumn();
            idAlmacenCol.DataType = Type.GetType("System.Int32");
            idAlmacenCol.ColumnName = "idAlmacen";

            // Create Column 3: cordX
            DataColumn cordXCol = new DataColumn();
            cordXCol.DataType = Type.GetType("System.Int32");
            cordXCol.ColumnName = "cordX";

            // Create Column 3: cordY
            DataColumn cordYCol = new DataColumn();
            cordYCol.DataType = Type.GetType("System.Int32");
            cordYCol.ColumnName = "cordY";

            // Create Column 3: cordY
            DataColumn cordZCol = new DataColumn();
            cordZCol.DataType = Type.GetType("System.Int32");
            cordZCol.ColumnName = "cordZ";

            DataColumn idUbicacionCol = new DataColumn();
            idUbicacionCol.DataType = Type.GetType("System.Int32");
            idUbicacionCol.ColumnName = "idUbicacion";
            
            // Add the columns to the ProductSalesData DataTable
            ubicacionesData.Columns.Add(idZonaCol);
            ubicacionesData.Columns.Add(idAlmacenCol);
            ubicacionesData.Columns.Add(cordXCol);
            ubicacionesData.Columns.Add(cordYCol);
            ubicacionesData.Columns.Add(cordZCol);
            ubicacionesData.Columns.Add(idUbicacionCol);
            

            return ubicacionesData;
        }

        public void AgregarFilasToUbicacionesDT(DataTable data, List<List<List<Ubicacion>>> filas, int id)
        {
            for (int m = 0; m < filas.Count; m++)
            {
                for (int n = 0; n < filas[m].Count; n++)
                {
                    for (int p = 0; p < filas[m][n].Count; p++)
                    {
                        filas[m][n][p].IdAlmacen = id;
                        DataRow ubicacionxAlmacenRow = data.NewRow();
                        ubicacionxAlmacenRow["idTipoZona"] = filas[m][n][p].IdTipoZona;
                        ubicacionxAlmacenRow["idAlmacen"] = id;
                        ubicacionxAlmacenRow["CordX"] = filas[m][n][p].CordX;
                        ubicacionxAlmacenRow["CordY"] = filas[m][n][p].CordY;
                        ubicacionxAlmacenRow["CordZ"] = filas[m][n][p].CordZ;
                        ubicacionxAlmacenRow["idUbicacion"] = filas[m][n][p].IdUbicacion;
                        // Add the row to the ProductSalesData DataTable
                        data.Rows.Add(ubicacionxAlmacenRow);

                        //ubSQL.Agregar(anaquel.Ubicaciones[m][n][p]);
                    }
                }

            }

        }

        public void BuscarEmpleado()
        {
            
        }


        public void LimpiarCampos()
        {
            TxtNombre = "";
            TxtTelef = "";
            TxtDir = "";
            TxtCodProducto = "";
            TxtStockMin = "";
            TxtStockMax = "";
            TxtPrecioV = "";
            LstProductos = null;

            TxtNumRowsAnq = "";
            TxtNumColumnsAnq = "";
            TxtAlturaAnq = "";

            TxtNumRowsDto = "";
            TxtNumColumnsDto = "";
            TxtAlturaDto = "";
        }


        public int GuardarTienda(MadeInHouse.Dictionary.DynamicGrid anaquel, MadeInHouse.Dictionary.DynamicGrid deposito)
        {

            Evaluador eva = new Evaluador();
            if (String.IsNullOrEmpty(TxtAlturaAnq) || String.IsNullOrEmpty(TxtNumColumnsAnq) || String.IsNullOrEmpty(TxtNumRowsAnq) ||
                 String.IsNullOrEmpty(TxtAlturaDto) || String.IsNullOrEmpty(TxtNumColumnsDto) || String.IsNullOrEmpty(TxtNumRowsDto) ||
                !eva.esNumeroEntero(TxtNumColumnsAnq) || !eva.esNumeroEntero(TxtNumColumnsDto) || !eva.esNumeroEntero(TxtNumRowsAnq) || !eva.esNumeroEntero(TxtNumRowsDto) ||
                !eva.esNumeroEntero(TxtAlturaAnq) || !eva.esNumeroEntero(TxtAlturaDto) || 
                int.Parse(TxtAlturaDto) <= 0 || int.Parse(TxtAlturaAnq) <= 0 || int.Parse(TxtNumColumnsAnq) <= 0 || int.Parse(TxtNumColumnsDto) <= 0 ||
                int.Parse(TxtNumRowsAnq) <= 0 || int.Parse(TxtNumRowsDto) <= 0  )
            {
                System.Windows.MessageBox.Show("Debes ingresar un numero entero para el numero de filas , columnas y altura");
                return 1;
            }

            int exito = 0;
                
            DBConexion db = new DBConexion();
            db.conn.Open();
            SqlTransaction trans = db.conn.BeginTransaction(IsolationLevel.Serializable);
            db.cmd.Transaction = trans;
            pxaSQL = new ProductoSQL(db);
            
            

                /*Agrega una tienda*/
                Tienda tienda = new Tienda();
                tienda.Estado = 1;
                tienda.Nombre = txtNombre;
                tienda.Direccion = txtDir;
                tienda.Telefono = txtTelef;
                tienda.Administrador = txtAdmin;
                Ubigeo seleccionado = new Ubigeo();
                UbigeoSQL uSQL = new UbigeoSQL(db);
                seleccionado = uSQL.buscarUbigeo(selectedDpto, selectedProv, selectedDist);
                tienda.IdUbigeo = seleccionado.IdUbigeo;
                tienda.FechaReg = DateTime.Today;
                TiendaSQL gw = new TiendaSQL(db);
                int idTienda=-1;
                
                if (accion == 1)
                    idTienda = gw.AgregarTienda(tienda);
                else if (accion == 2)
                {
                    tienda.IdTienda = this.idTienda;
                    exito = gw.ActualizarTienda(tienda);
                }
               if (idTienda > 0 || exito>0)
                {

                    /*Se agregan las dos partes de la tienda*/
                    AlmacenSQL aSQL = new AlmacenSQL(db);

                    /*anaquel*/
                    Almacenes ana = new Almacenes();
                    ana.CodAlmacen = "ANA00" + idTienda.ToString();
                    ana.IdTienda = idTienda;
                    ana.Nombre = "Anaquel de TIENDA" + ((accion == 1) ? idTienda.ToString() : this.idTienda.ToString());
                    ana.Telefono = tienda.Telefono;
                    ana.Direccion = tienda.Direccion;
                    ana.NroColumnas = int.Parse(TxtNumColumnsAnq);
                    ana.NroFilas = int.Parse(TxtNumRowsAnq);
                    ana.Altura = int.Parse(TxtAlturaAnq);
                    ana.Tipo = 2;
                    int idAnaquel=-1;
                    if (accion == 1)
                        idAnaquel = aSQL.Agregar(ana);
                    else if (accion == 2)
                    {
                        ana.IdAlmacen = this.idAnaquel;
                        exito = aSQL.Actualizar(ana);
                    }
                    if (idAnaquel > 0 || exito>0)
                    {

                        /*deposito*/
                        Almacenes dto = new Almacenes();
                        dto.CodAlmacen = "DTO00" + idTienda.ToString();
                        dto.IdTienda = idTienda;
                        dto.Nombre = "Deposito de TIENDA" + ((accion == 1) ? idTienda.ToString() : this.idTienda.ToString());
                        dto.Telefono = tienda.Telefono;
                        dto.Direccion = tienda.Direccion;
                        dto.NroColumnas = int.Parse(TxtNumColumnsDto);
                        dto.NroFilas = int.Parse(TxtNumRowsDto);
                        dto.Altura = int.Parse(TxtAlturaDto);
                        dto.Tipo = 1;
                        int idDeposito=-1;
                        if (accion == 1)
                            idDeposito = aSQL.Agregar(dto);
                        else if (accion == 2)
                        {
                            dto.IdAlmacen = this.idDeposito;
                            exito = aSQL.Actualizar(dto);
                        }

                        if (idDeposito > 0 || exito>0)
                        {
                            if (accion == 1)
                            {
                                /*Productos de la tienda*/
                                for (int i = 0; i < LstProductos.Count; i++)
                                {
                                    LstProductos[i].IdAlmacen = idDeposito;
                                    LstProductos[i].IdTienda = idTienda;
                                     exito = pxaSQL.AgregarProductoxAlmacen(LstProductos[i]);
                                     

                                    if (exito <= 0) break;
                                }
                            }
                            else if (accion == 2)
                            {
                                DataTable productoxAlmacenDT= pxaSQL.CrearProductoxAlmacenDT();
                                pxaSQL.AgregarFilasToDT(productoxAlmacenDT, LstProductos);
                                exito = pxaSQL.ActualizarProductoxAlmacen(productoxAlmacenDT,trans);
                            }



                            if (exito > 0)
                            {
                               DataTable zonaxAlmacenData = CrearZonasDT();
                                AgregarFilasToZonasDT(zonaxAlmacenData, anaquel.listaZonas, (accion==1) ? idAnaquel :this.idAnaquel );
                                AgregarFilasToZonasDT(zonaxAlmacenData, deposito.listaZonas, (accion==1) ? idDeposito : this.idDeposito );

                                if (accion==1)
                                    exito = aSQL.AgregarZonasMasivo(zonaxAlmacenData, trans);
                                else if (accion==2)
                                    exito = aSQL.ActualizarZonasMasivo(zonaxAlmacenData, trans);


                                if (exito > 0)
                                {

                                    UbicacionSQL ubSQL = new UbicacionSQL(db);

                                    /*Ubicaciones del anaquel*/
                                    DataTable ubicacionesData = CrearUbicacionesDT();
                                    AgregarFilasToUbicacionesDT(ubicacionesData, anaquel.Ubicaciones, (accion == 1) ? idAnaquel : this.idAnaquel);
                                    AgregarFilasToUbicacionesDT(ubicacionesData, deposito.Ubicaciones, (accion == 1) ? idDeposito : this.idDeposito);

                                    if (accion == 1)
                                        exito = ubSQL.AgregarMasivo(ubicacionesData, trans);
                                    else if (accion == 2)
                                        exito = ubSQL.ActualizarUbicacionMasivo(ubicacionesData, trans);


                                    if (exito > 0)
                                    {
                                        trans.Commit();
                                        if (accion == 1)
                                            System.Windows.MessageBox.Show("Se creó la tienda correctamente");
                                        else if (accion == 2)
                                            System.Windows.MessageBox.Show("Se editó la tienda correctamente");
                                        return 1;
                                    }
                                    else
                                    {
                                        System.Windows.MessageBox.Show("ERROR");
                                    }
                                }
                                else
                                {
                                    System.Windows.MessageBox.Show("ERROR");
                                }
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("ERROR");
                            }
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("ERROR");
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("ERROR");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("ERROR");
                }

                trans.Rollback();
                return -1;

               // System.Windows.MessageBox.Show("Se creo correctamente la tienda con id: " + idTienda.ToString() + " con anaquel id: " + idAnaquel.ToString() + " y con deposito id :" + idDeposito.ToString());


        }

        public void EditarTienda()
        {
            Editar = true;
        }

        #endregion
    }

    }

