﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;
using System.Collections.ObjectModel;

namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerTiendaViewModel : PropertyChangedBase
    {


        private UbigeoSQL uSQL;
        private TiendaSQL tSQL;
        private ProductoSQL pxaSQL;

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
                SelectedIndex = 0;
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
              SelectedIndex = 0;
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
            set { txtCodProducto = value;
            NotifyOfPropertyChange(() => TxtCodProducto);
            }
        }

        private string txtStockIni;

        public string TxtStockIni
        {
            get { return txtStockIni; }
            set { txtStockIni = value;
            NotifyOfPropertyChange(() => TxtStockIni);

            }
        }

        private string txtStockMin;

        public string TxtStockMin
        {
            get { return txtStockMin; }
            set { txtStockMin = value;
            NotifyOfPropertyChange(() => TxtStockMin);
            }
        }

        private string txtStockMax;

        public string TxtStockMax
        {
            get { return txtStockMax; }
            set { txtStockMax = value;
            NotifyOfPropertyChange(() => TxtStockMax);
            }
        }

        private string txtPrecioV;

        public string TxtPrecioV
        {
            get { return txtPrecioV; }
            set { txtPrecioV = value;
            NotifyOfPropertyChange(() => TxtPrecioV);
            }
        }


        private bool chkVigente;

        public bool ChkVigente
        {
            get { return chkVigente; }
            set { chkVigente = value;
            NotifyOfPropertyChange(() => ChkVigente);
            }
        }

        private List<ProductoxAlmacen> lstProductos;

        public List<ProductoxAlmacen> LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value;
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

        

        private int zonaAnaquel;

        public int ZonaAnaquel
        {
            get { return zonaAnaquel; }
            set { zonaAnaquel = value;
            NotifyOfPropertyChange(() => ZonaAnaquel);
            }
        }

        private int zonaDeposito;

        public int ZonaDeposito
        {
            get { return zonaDeposito; }
            set { zonaDeposito = value;
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


        
        public MantenerTiendaViewModel() {
            uSQL = new UbigeoSQL();
            tSQL = new TiendaSQL();
            pxaSQL= new ProductoSQL();
            
            CmbZonas = (new TipoZonaSQL()).BuscarZona();
            CmbDpto=uSQL.BuscarDpto();
            //LstProductos = pxaSQL.BuscarProductoxAlmacen();
            

        }


        public void Distribuir(int tipo)
        {
            if (tipo == 0)
            {
                NumColumnsAnq = Int32.Parse(TxtNumColumnsAnq);
                NumRowsAnq = Int32.Parse(TxtNumRowsAnq);
            }
            else
            {
                NumColumnsDto = Int32.Parse(TxtNumColumnsDto);
                NumRowsDto = Int32.Parse(TxtNumRowsDto);
            }


        }



        public void BuscarProductos()
        {
            MadeInHouse.Models.MyWindowManager wm = new Models.MyWindowManager();
            wm.ShowWindow(new ProductoBuscarViewModel(this,2));
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
                    if (LstProductos == null) LstProductos = new List<ProductoxAlmacen>();
                    else if ((pxa = LstProductos.Find(x => x.IdProducto == lstAux[0].IdProducto)) == null)
                    {
                        pxa = new ProductoxAlmacen();
                        pxa.IdProducto = lstAux[0].IdProducto;
                        pxa.Nombre = lstAux[0].Nombre;
                        pxa.StockActual = Int32.Parse(TxtStockIni);
                        pxa.StockMin = Int32.Parse(TxtStockMin);
                        pxa.StockMax = Int32.Parse(TxtStockMax);
                        pxa.PrecioVenta = float.Parse(txtPrecioV);
                        pxa.Vigente = (ChkVigente ==true) ? 1:0;
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

        public void Quitar() {
            if (SelectedItem != null)
            {
                LstProductos.Remove(SelectedItem);
                LstProductos = new List<ProductoxAlmacen>(LstProductos);
            }
        }

        public void Importar()
        {
          System.Data.DataTableReader dt=  MadeInHouse.Dictionary.ExcelUtiles.Importar("Productos");
          if (dt != null) System.Windows.MessageBox.Show("Bien");
        }


        public List<int> ObtenerListaZonas(MadeInHouse.Dictionary.DynamicGrid objeto) 
        {
            List<int> lista= new List<int>();

            bool encontrado = false;
            for (int i = 0; i < objeto.Ubicaciones.Count; i++)
            {
                
                for (int j = 0; j < objeto.Ubicaciones[i].Count; j++)
                {
                    for(int k=0;k<lista.Count;k++) {
                        if (lista[k]==objeto.Ubicaciones[i][j].IdTipoZona) 
                        {
                            encontrado=true;
                            break;
                        }
                    }
                    if (!encontrado)
                        lista.Add(objeto.Ubicaciones[i][j].IdTipoZona);
                    encontrado = false;
                }
            }
            return lista;
        }


        public void GuardarTienda(MadeInHouse.Dictionary.DynamicGrid anaquel, MadeInHouse.Dictionary.DynamicGrid deposito)
        {

            /*saco todos los idTipoZona*/
            List<int> zonasAnaquel=ObtenerListaZonas(anaquel);
            List<int> zonasDeposito=ObtenerListaZonas(deposito);


            /*Agrega una tienda*/
            Tienda tienda = new Tienda();
            tienda.Estado = 1;
            tienda.Nombre = txtNombre;
            tienda.Direccion = txtDir;
            tienda.Telefono = txtTelef;
            tienda.Administrador = txtAdmin;
            Ubigeo seleccionado = new Ubigeo();
            seleccionado = uSQL.buscarUbigeo(selectedDpto, selectedProv, selectedDist);
            tienda.IdUbigeo = seleccionado.IdUbigeo;
            tienda.FechaReg = DateTime.Today;
            TiendaSQL gw = new TiendaSQL();
            int idTienda=gw.AgregarTienda(tienda);

            /*Se agregan las dos partes de la tienda*/
             AlmacenSQL aSQL = new AlmacenSQL();
                
             /*anaquel*/
             Almacenes ana = new Almacenes();
             ana.CodAlmacen = "ANA00" + idTienda.ToString();
             ana.IdTienda = idTienda;
             ana.Nombre = "Anaquel de TIENDA" + idTienda.ToString();
             ana.Telefono = tienda.Telefono;
             ana.Direccion = tienda.Direccion;
             ana.Tipo = "ANAQUEL";
             int idAnaquel=aSQL.Agregar(ana);

             /*deposito*/
             Almacenes dto = new Almacenes();
             dto.CodAlmacen = "DTO00" + idTienda.ToString();
             dto.IdTienda = idTienda;
             dto.Nombre = "Deposito de TIENDA" + idTienda.ToString();
             dto.Telefono = tienda.Telefono;
             dto.Direccion = tienda.Direccion;
             dto.Tipo = "DEPOSITO";
             int idDeposito=aSQL.Agregar(dto);
             UbicacionSQL ubSQL = new UbicacionSQL();

             /*Agrego las zonas por almacen*/
             for (int i = 0; i < zonasAnaquel.Count; i++)
             {
                 aSQL.AgregarZonas(zonasAnaquel[i], idAnaquel);
             }

             for (int i = 0; i < zonasDeposito.Count; i++)
             {
                 aSQL.AgregarZonas(zonasDeposito[i], idDeposito);
             }


             /*Ubicaciones del anaquel*/
             for (int m = 0; m < anaquel.Ubicaciones.Count; m++)
             {
                 for (int n = 0; n < anaquel.Ubicaciones[m].Count; n++)
                 {
                     anaquel.Ubicaciones[m][n].IdAlmacen = idAnaquel;
                     anaquel.Ubicaciones[m][n].CordX = m;
                     anaquel.Ubicaciones[m][n].CordY = n;
                     anaquel.Ubicaciones[m][n].CordZ = Int32.Parse(txtAlturaAnq);
                     ubSQL.Agregar(anaquel.Ubicaciones[m][n]);
                 }

             }
            /*Ubicaciones del deposito*/

             for (int m = 0; m < deposito.Ubicaciones.Count; m++)
             {
                 for (int n = 0; n < deposito.Ubicaciones[m].Count; n++)
                 {
                     deposito.Ubicaciones[m][n].IdAlmacen = idDeposito;
                     deposito.Ubicaciones[m][n].CordX = m;
                     deposito.Ubicaciones[m][n].CordY = n;
                     deposito.Ubicaciones[m][n].CordZ = Int32.Parse(txtAlturaDto);
                     ubSQL.Agregar(deposito.Ubicaciones[m][n]);
                 }

             }



             System.Windows.MessageBox.Show("Se creo correctamente la tienda con id: " + idTienda.ToString() +" con anaquel id: " + idAnaquel.ToString()+ " y con deposito id :"+ idDeposito.ToString() );
            

            }



        }

        

        


    }

