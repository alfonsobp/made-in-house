using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;
using System.Collections.ObjectModel;
using MadeInHouse.DataObjects;
using System.Data.SqlClient;
using System.Data;


namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerAlmacenViewModel:Screen
    {


        #region Atributos

        private UbigeoSQL uSQL;
        private TiendaSQL tSQL;
        private ProductoSQL pxaSQL;
        private AlmacenSQL aSQL;
        private UbicacionSQL ubSQL;
        private TipoZonaSQL tzSQL;

        private int index1;

        public int Index1
        {
            get { return index1; }
            set
            {
                index1 = value;
                NotifyOfPropertyChange(() => Index1);
            }
        }

        private int index2;

        public int Index2
        {
            get { return index2; }
            set
            {
                index2 = value;
                NotifyOfPropertyChange(() => Index2);
            }
        }

        private int index3;

        public int Index3
        {
            get { return index3; }
            set
            {
                index3 = value;
                NotifyOfPropertyChange(() => Index3);
            }
        }

        private string txtCodigo;

        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; }
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

        private string txtTelefono;

        public string TxtTelefono
        {
            get { return txtTelefono; }
            set { txtTelefono = value; }
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

        private int zona;

        public int Zona
        {
            get { return zona; }
            set
            {
                zona = value;
                NotifyOfPropertyChange(() => Zona);
            }
        }


        private ObservableCollection<TipoZona> cmbZonas;

        public ObservableCollection<TipoZona> CmbZonas
        {
            get { return cmbZonas; }
            set { cmbZonas = value; }
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
                Zona = selectedZona.IdTipoZona;
                
            }
        }

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

        private string txtNumColumns;

        public string TxtNumColumns
        {
            get { return txtNumColumns; }
            set { txtNumColumns = value; }
        }

        private string txtNumRows;

        public string TxtNumRows
        {
            get { return txtNumRows; }
            set { txtNumRows = value; }
        }

        private string txtAltura;

        public string TxtAltura
        {
            get { return txtAltura; }
            set { txtAltura = value; }
        }

        private int accion=1;
        

        #endregion

        public MantenerAlmacenViewModel()
        {
            uSQL = new UbigeoSQL();
            tSQL = new TiendaSQL();
            pxaSQL = new ProductoSQL();
            

            CmbZonas = (new TipoZonaSQL()).BuscarZona();
            CmbDpto = uSQL.BuscarDpto();
            Content = "Generar distribución";

        }


            public void Distribuir(int tipo, MadeInHouse.Dictionary.DynamicGrid dg)
            {
                
                    NumColumns = Int32.Parse(TxtNumColumns);
                    NumRows = Int32.Parse(TxtNumRows);
                    Altura = Int32.Parse(TxtAltura);

                    if (accion == 2)
                    {
                        //dg.cargarGrid(lstZonas);
                    }
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

                // Add the columns to the ProductSalesData DataTable
                ubicacionesData.Columns.Add(idZonaCol);
                ubicacionesData.Columns.Add(idAlmacenCol);
                ubicacionesData.Columns.Add(cordXCol);
                ubicacionesData.Columns.Add(cordYCol);
                ubicacionesData.Columns.Add(cordZCol);

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

                            // Add the row to the ProductSalesData DataTable
                            data.Rows.Add(ubicacionxAlmacenRow);

                            //ubSQL.Agregar(anaquel.Ubicaciones[m][n][p]);
                        }
                    }

                }

            }


            public int GuardarAlmacen(MadeInHouse.Dictionary.DynamicGrid deposito)
                int exito = 1;

                DBConexion db = new DBConexion();
                db.conn.Open();
                SqlTransaction trans = db.conn.BeginTransaction(IsolationLevel.Serializable);
                db.cmd.Transaction = trans;
                
                /*Agrega una tienda*/
                Almacenes central = new Almacenes();
                central.Estado = 1;
                central.CodAlmacen = "xxx";
                central.Nombre = TxtNombre;
                central.Direccion = TxtDir;
                central.Telefono = TxtTelefono;
                central.Administrador = txtAdmin;
                Ubigeo seleccionado = new Ubigeo();
                UbigeoSQL uSQL = new UbigeoSQL(db);
                seleccionado = uSQL.buscarUbigeo(selectedDpto, selectedProv, selectedDist);
                central.IdUbigeo = seleccionado.IdUbigeo;
                central.FechaReg = DateTime.Today;
                AlmacenSQL aSQL = new AlmacenSQL(db);
                if (TxtNumColumns != null) central.NroColumnas = int.Parse(TxtNumColumns);
                else return -1;
                central.NroFilas = int.Parse(TxtNumRows);

                if (TxtAltura != null) central.Altura = int.Parse(TxtAltura);
                else return -1;
                central.Tipo = 3;
                int idAlmacen = aSQL.Agregar(central);
                central.IdAlmacen = idAlmacen;
                central.CodAlmacen = "CENTRAL00" + idAlmacen.ToString();
                int up = aSQL.Actualizar(central);

                    if (idAlmacen > 0 && up>0)
                    {
                        DataTable zonaxAlmacenData = CrearZonasDT();
                        AgregarFilasToZonasDT(zonaxAlmacenData, deposito.listaZonas, idAlmacen);
                        /*Agrego las zonas por almacen*/

                        exito = aSQL.AgregarZonasMasivo(zonaxAlmacenData, trans);
                         
                        if (exito > 0)
                          {
                              UbicacionSQL ubSQL = new UbicacionSQL(db);

                              /*Ubicaciones del anaquel*/
                              DataTable ubicacionesData = CrearUbicacionesDT();
                              AgregarFilasToUbicacionesDT(ubicacionesData, deposito.Ubicaciones, idAlmacen);
                              exito = ubSQL.AgregarMasivo(ubicacionesData, trans);

                              trans.Commit();
                              System.Windows.MessageBox.Show("Se creó el almacen central correctamente correctamente");
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
                
             
                trans.Rollback();
                return -1;

            }




    }
}
