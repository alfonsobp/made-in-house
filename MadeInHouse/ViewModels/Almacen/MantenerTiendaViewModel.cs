using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerTiendaViewModel : PropertyChangedBase
    {


        private UbigeoSQL uSQL;
        private TiendaSQL tSQL;

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



        /*Parametros para distribucion*/

        /*Anaqueles*/
        private string txtNumRowsAnq;

        public string TxtNumRowsAnq
        {
            get { return txtNumRowsAnq; }
            set { txtNumRowsAnq = value; 
                    NotifyOfPropertyChange(()=>TxtNumRowsAnq);
            }
        }

        private string txtNumColumnsAnq;

        public string TxtNumColumnsAnq
        {
            get { return txtNumColumnsAnq; }
            set { txtNumColumnsAnq = value; 
                NotifyOfPropertyChange(()=>TxtNumColumnsAnq);
            }
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
            set
            {
                txtNumRowsDto = value;
                NotifyOfPropertyChange(() => TxtNumRowsDto);
            }
        }

        private string txtNumColumnsDto;

        public string TxtNumColumnsDto
        {
            get { return txtNumColumnsDto; }
            set
            {
                txtNumColumnsDto = value;
                NotifyOfPropertyChange(() => TxtNumColumnsDto);
            }
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


        public void Distribuir(object sender, int tipo)
        {
            if (tipo == 0)
            {
                NumColumns = Int32.Parse(TxtNumColumnsAnq);
                NumRows = Int32.Parse(TxtNumRowsAnq);
                (sender as MadeInHouse.Dictionary.DynamicGrid).RecreateGridCells();
            }
            else
            {
                NumColumns = Int32.Parse(TxtNumColumnsDto);
                NumRows = Int32.Parse(TxtNumRowsDto);
                (sender as MadeInHouse.Dictionary.DynamicGrid).RecreateGridCells();
            }
            
            
        }

        private List<string> cmbColores;

        public List<string> CmbColores
        {
            get { return cmbColores; }
            set { cmbColores = value; }
        }

        private string selectedColor;

        public string SelectedColor
        {
            get { return selectedColor; }
            set
            {
                selectedColor = value;
                Fondo = selectedColor;
            }
        }

        
        public MantenerTiendaViewModel() {
            uSQL = new UbigeoSQL();
            tSQL = new TiendaSQL();
            this.CmbColores = new List<string>();
            this.CmbColores.Add("Red");
            this.CmbColores.Add("Blue");
            this.CmbColores.Add("Green");
            CmbDpto=uSQL.BuscarDpto();
        }


        private int selectedIndex;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; }
        }

        public void GuardarTienda() {

            Tienda tienda = new Tienda();
            tienda.Estado = 1;
           // tienda.Nombre = this.txtNombre;
            tienda.Direccion = this.txtDir;
            Ubigeo seleccionado = new Ubigeo();
            seleccionado = uSQL.buscarUbigeo(selectedDpto, selectedProv, selectedDist);
            tienda.Ubigeo = seleccionado;
            tienda.FechaReg = DateTime.Today;
            TiendaSQL gw = new TiendaSQL();
            gw.AgregarTienda(tienda);


        }

        

        private int numColumns;

        public int NumColumns
        {
            get { return numColumns; }
            set { numColumns = value;
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


    }
}
