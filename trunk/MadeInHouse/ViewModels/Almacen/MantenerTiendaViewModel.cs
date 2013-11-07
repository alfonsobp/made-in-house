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

        private List<ProductoxAlmacen> lstProdAgregados;

        public List<ProductoxAlmacen> LstProdAgregados
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
            set { txtNombre = value; }
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
            set { txtTelef = value; }
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
            set { txtDir = value; }
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

        private List<ProductoxAlmacen> lstProductos;

        public List<ProductoxAlmacen> LstProductos
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
            set { txtNumRowsAnq = value; }
        }

        private string txtNumColumnsAnq;

        public string TxtNumColumnsAnq
        {
            get { return txtNumColumnsAnq; }
            set { txtNumColumnsAnq = value; }
        }



        private string txtAlturaAnq;

        public string TxtAlturaAnq
        {
            get { return txtAlturaAnq; }
            set { txtAlturaAnq = value; }
        }




        /*Deposito*/
        private string txtNumRowsDto;

        public string TxtNumRowsDto
        {
            get { return txtNumRowsDto; }
            set { txtNumRowsDto = value; }
        }

        private string txtNumColumnsDto;

        public string TxtNumColumnsDto
        {
            get { return txtNumColumnsDto; }
            set { txtNumColumnsDto = value; }
        }

        private string txtAlturaDto;

        public string TxtAlturaDto
        {
            get { return txtAlturaDto; }
            set { txtAlturaDto = value; }
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

        private ProductoxAlmacen selectedItem;

        public ProductoxAlmacen SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; }
        }
        #endregion



        List<TipoZona> lstZonasAnq;
        List<TipoZona> lstZonasDto;
        private int accion; /*1=ABRIR NUEVO , 2=ABRIR EDITAR*/
        private string content;

        public string Content
        {
            get { return content; }
            set { content = value;
            NotifyOfPropertyChange(() => Content);
            }
        }

        #region Constructores

        public MantenerTiendaViewModel(Tienda t) 
        {
            accion = 2;
            uSQL = new UbigeoSQL();
            tSQL = new TiendaSQL();
            pxaSQL = new ProductoSQL();
            aSQL = new AlmacenSQL();
            tzSQL = new TipoZonaSQL();

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
            Almacenes deposito =aSQL.BuscarAlmacen(-1,t.IdTienda,1);

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

            CmbZonas = (new TipoZonaSQL()).BuscarZona();
            CmbDpto = uSQL.BuscarDpto();
            LstProductos = new List<ProductoxAlmacen>();
            LstProdAgregados = new List<ProductoxAlmacen>();
            //LstProductos = pxaSQL.BuscarProductoxAlmacen();
            Content = "Generar distribución";


        }
        #endregion

        #region Métodos

        public void Distribuir(int tipo)
        {
            if (tipo == 0)
            {
                NumColumnsAnq = Int32.Parse(TxtNumColumnsAnq);
                NumRowsAnq = Int32.Parse(TxtNumRowsAnq);
                AlturaAnq = Int32.Parse(TxtAlturaAnq);
                
               
            }
            else
            {
                NumColumnsDto = Int32.Parse(TxtNumColumnsDto);
                NumRowsDto = Int32.Parse(TxtNumRowsDto);
                AlturaDto = Int32.Parse(TxtAlturaDto);
            }

            


        }



        public void BuscarProductos()
        {
            MadeInHouse.Models.MyWindowManager wm = new Models.MyWindowManager();
            wm.ShowWindow(new ProductoBuscarViewModel(this, 2));
        }

        public void Agregar()
        {

            if (TxtCodProducto == null || TxtStockIni == null || TxtStockMax == null || TxtStockMin == null || TxtPrecioV == null)
            {
                System.Windows.MessageBox.Show("Debe completar todos los campos");
            }
            else
            {
                ProductoxAlmacen pxa;
                List<Producto> lstAux = null;
                lstAux = pxaSQL.BuscarProducto(TxtCodProducto);

                if (lstAux != null)
                {

                    if ((pxa = LstProductos.Find(x => x.IdProducto == lstAux[0].IdProducto)) == null)
                    {

                        pxa = new ProductoxAlmacen();
                        pxa.CodProducto = lstAux[0].CodigoProd;
                        pxa.IdProducto = lstAux[0].IdProducto;
                        pxa.Nombre = lstAux[0].Nombre;
                        pxa.StockActual = Int32.Parse(TxtStockIni);
                        pxa.StockMin = Int32.Parse(TxtStockMin);
                        pxa.StockMax = Int32.Parse(TxtStockMax);
                        pxa.PrecioVenta = float.Parse(txtPrecioV);
                        pxa.Vigente = (ChkVigente == true) ? 1 : 0;
                        LstProductos.Add(pxa);
                        LstProductos = new List<ProductoxAlmacen>(LstProductos);
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
                LstProductos = new List<ProductoxAlmacen>(LstProductos);
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

                    ProductoxAlmacen pxa = new ProductoxAlmacen();
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
                    LstProductos = new List<ProductoxAlmacen>(LstProductos);
                }


            }
        }

        public void SelectedItemChanged(object sender)
        {

            SelectedItem = ((sender as DataGrid).SelectedItem as ProductoxAlmacen);
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
                    for (int m = 0; m < objeto.Ubicaciones[i][j].Count; m++)
                    {
                        for (int k = 0; k < lista.Count; k++)
                        {
                            if (lista[k] == objeto.Ubicaciones[i][j][m].IdTipoZona)
                            {
                                encontrado = true;
                                break;
                            }
                        }
                        if (!encontrado)
                            lista.Add(objeto.Ubicaciones[i][j][m].IdTipoZona);
                        encontrado = false;
                    }
                }
            }
            return lista;
        }


        public int GuardarTienda(MadeInHouse.Dictionary.DynamicGrid anaquel, MadeInHouse.Dictionary.DynamicGrid deposito)
        {

            /*saco todos los idTipoZona*/
            List<int> zonasAnaquel = ObtenerListaZonas(anaquel);
            List<int> zonasDeposito = ObtenerListaZonas(deposito);
            int exito = 1;

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
                int idTienda = gw.AgregarTienda(tienda);
                if (idTienda > 0)
                {

                    /*Se agregan las dos partes de la tienda*/
                    AlmacenSQL aSQL = new AlmacenSQL(db);

                    /*anaquel*/
                    Almacenes ana = new Almacenes();
                    ana.CodAlmacen = "ANA00" + idTienda.ToString();
                    ana.IdTienda = idTienda;
                    ana.Nombre = "Anaquel de TIENDA" + idTienda.ToString();
                    ana.Telefono = tienda.Telefono;
                    ana.Direccion = tienda.Direccion;
                    ana.NroColumnas = int.Parse(TxtNumColumnsAnq);
                    ana.NroFilas = int.Parse(TxtNumRowsAnq);
                    ana.Altura = int.Parse(TxtAlturaAnq);
                    ana.Tipo = 2;
                    int idAnaquel = aSQL.Agregar(ana);
                    if (idAnaquel > 0)
                    {

                        /*deposito*/
                        Almacenes dto = new Almacenes();
                        dto.CodAlmacen = "DTO00" + idTienda.ToString();
                        dto.IdTienda = idTienda;
                        dto.Nombre = "Deposito de TIENDA" + idTienda.ToString();
                        dto.Telefono = tienda.Telefono;
                        dto.Direccion = tienda.Direccion;
                        dto.NroColumnas = int.Parse(TxtNumColumnsDto);
                        dto.NroFilas = int.Parse(TxtNumRowsDto);
                        dto.Altura = int.Parse(TxtAlturaDto);
                        dto.Tipo = 1;
                        int idDeposito = aSQL.Agregar(dto);
                        if (idDeposito > 0)
                        {

                            /*Productos de la tienda*/
                            for (int i = 0; i < LstProductos.Count; i++)
                            {
                                LstProductos[i].IdAlmacen = idDeposito;
                                LstProductos[i].IdTienda = idTienda;
                                exito = pxaSQL.AgregarProductoxAlmacen(LstProductos[i]);
                                if (exito<=0) break;
                            }

                            if (exito > 0)
                            {
                                /*Agrego las zonas por almacen*/
                                for (int i = 0; i < zonasAnaquel.Count; i++)
                                {
                                    aSQL.AgregarZonas(zonasAnaquel[i], idAnaquel);
                                }

                                for (int i = 0; i < zonasDeposito.Count; i++)
                                {
                                    aSQL.AgregarZonas(zonasDeposito[i], idDeposito);
                                }

                                if (exito > 0)
                                {

                                    UbicacionSQL ubSQL = new UbicacionSQL(db);

                                    /*Ubicaciones del anaquel*/
                                    for (int m = 0; m < anaquel.Ubicaciones.Count; m++)
                                    {
                                        for (int n = 0; n < anaquel.Ubicaciones[m].Count; n++)
                                        {
                                            for (int p = 0; p < anaquel.Ubicaciones[m][n].Count; p++)
                                            {
                                                anaquel.Ubicaciones[m][n][p].IdAlmacen = idAnaquel;
                                                anaquel.Ubicaciones[m][n][p].CordX = m;
                                                anaquel.Ubicaciones[m][n][p].CordY = n;
                                                anaquel.Ubicaciones[m][n][p].CordZ = p;
                                                ubSQL.Agregar(anaquel.Ubicaciones[m][n][p]);
                                            }
                                        }

                                    }
                                    /*Ubicaciones del deposito*/

                                    for (int m = 0; m < deposito.Ubicaciones.Count; m++)
                                    {
                                        for (int n = 0; n < deposito.Ubicaciones[m].Count; n++)
                                        {
                                            for (int p = 0; p<deposito.Ubicaciones[m][n].Count; p++)
                                            {
                                                deposito.Ubicaciones[m][n][p].IdAlmacen = idDeposito;
                                                /*deposito.Ubicaciones[m][n].CordX = m;
                                                deposito.Ubicaciones[m][n].CordY = n;
                                                deposito.Ubicaciones[m][n].CordZ = Int32.Parse(txtAlturaDto);*/
                                                ubSQL.Agregar(deposito.Ubicaciones[m][n][p]);
                                            }
                                        }

                                    }
                                    trans.Commit();
                                    System.Windows.MessageBox.Show("Se creó la tienda correctamente");
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

                trans.Rollback();
                return -1;

               // System.Windows.MessageBox.Show("Se creo correctamente la tienda con id: " + idTienda.ToString() + " con anaquel id: " + idAnaquel.ToString() + " y con deposito id :" + idDeposito.ToString());


        }


        #endregion
    }

        

        


    }

