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
    class BuscarTiendaViewModel: Screen
    {

        #region Atributos
        private UbigeoSQL uSQL;
        private TiendaSQL tSQL;
        private Ubigeo deft;
        private int ventanaAccion;

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
                CmbProv.Insert(0, deft);
                Index2 = 0;
                
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
                CmbDist.Insert(0, deft);
                Index3 = 0;
                
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

        private List<Tienda> lstTiendas;

        public List<Tienda> LstTiendas
        {
            get { return lstTiendas; }
            set
            {
                lstTiendas = value;
                NotifyOfPropertyChange(() => LstTiendas);
            }
        }

        private Tienda tiendaSel;

        public Tienda TiendaSel
        {
            get { return tiendaSel; }
            set { tiendaSel = value; }
        }


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


        


        #endregion

        #region Constructores
        public BuscarTiendaViewModel()
        {
            uSQL = new UbigeoSQL();
            CmbDpto = uSQL.BuscarDpto();
            deft= new Ubigeo();
            deft.Nombre="TODOS";
            CmbDpto.Insert(0, deft);
            Index1 = 0;

        }

        private Reportes.reporteStockViewModel reporteStockViewModel;
        public BuscarTiendaViewModel(Reportes.reporteStockViewModel reporteStockViewModel, int ventanaAccion)
        {
            uSQL = new UbigeoSQL();
            CmbDpto = uSQL.BuscarDpto();
            deft= new Ubigeo();
            deft.Nombre="TODOS";
            CmbDpto.Insert(0, deft);
            Index1 = 0;
            this.reporteStockViewModel = reporteStockViewModel;
            this.ventanaAccion = ventanaAccion;
        }
        

        #endregion


        #region Métodos
        
        public void BuscarTiendas()
        {
            uSQL = new UbigeoSQL();
            tSQL = new TiendaSQL();
            List<Ubigeo> lstUbigeo;
            lstUbigeo=uSQL.buscarUbigeo2(-1,selectedDpto, SelectedProv, SelectedDist);
            string lista="";

            if (lstUbigeo == null)
            { 
                System.Windows.MessageBox.Show("ERROR: Se produjo un error");
            
            }
            else if (lstUbigeo.Count > 0)
            {
                for (int i = 0; i < lstUbigeo.Count-1; i++)
                {
                    lista += (lstUbigeo[i].IdUbigeo).ToString() +",";
                }
                lista += lstUbigeo[lstUbigeo.Count - 1].IdUbigeo.ToString();
                LstTiendas= tSQL.BuscarTienda(lista);
                if (LstTiendas == null)
                {
                    System.Windows.MessageBox.Show("ERROR: Se produjo un error");
                }
                else if (LstTiendas.Count == 0)
                {
                    System.Windows.MessageBox.Show("No se encontraron tiendas");
                }
                else
                {
                    System.Windows.MessageBox.Show("Se encontró: "+ LstTiendas.Count.ToString() +" tienda/as ");
                }

            }
        }

        public void Acciones(object sender) {

            if (ventanaAccion == 1)
            {
                Tienda tiendaSel = ((sender as DataGrid).SelectedItem as Tienda);
                if (reporteStockViewModel != null)
                {
                    reporteStockViewModel.Tien = tiendaSel;
                    this.TryClose();
                }
            }
            else
            {
                MantenerTiendaViewModel mtVM = new MantenerTiendaViewModel(TiendaSel);
                (new MadeInHouse.Models.MyWindowManager()).ShowWindow(mtVM);
            }
        }


        public void Limpiar()
        {
            Index1 = 0;

        }





        }

        #endregion

    }
