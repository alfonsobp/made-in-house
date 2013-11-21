using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerTipoZonaViewModel : PropertyChangedBase, IDataErrorInfo
    {
        private TipoZona tipoZonaSeleccionada;
        
        private DataObjects.Almacen.ColorSQL gateway= new DataObjects.Almacen.ColorSQL();

        public TipoZona TipoZonaSeleccionada
        {
            get { return tipoZonaSeleccionada; }
            set { tipoZonaSeleccionada = value;
            NotifyOfPropertyChange("TipoZonaSeleccionada");
            }
        }

        private int indicador;

        public int Indicador
        {
            get { return indicador; }
            set { indicador = value; }
        }
        
        private ObservableCollection<Color> cmbColor;

        public ObservableCollection<Color> CmbColor
        {
            get
            {
                return this.cmbColor;
            }

            private set
            {
                if (this.cmbColor == value)
                {
                    return;
                }

                this.cmbColor = value;
                
                this.NotifyOfPropertyChange(() => this.CmbColor);
            }

        }
        public void cambioColorNombre() {
            for (int i = 0; i < cmbColor.Count; i++) {
                cmbColorNombre = new List<string>();
                cmbColorNombre.Add(cmbColor.ElementAt(i).Nombre);
                cmbColorHex = new List<string>();
                cmbColorHex.Add(cmbColor.ElementAt(i).CodHex);
            }
        }

        private List<string> cmbColorNombre;
        
        public List<string> CmbColorNombre
        {
            get { return this.cmbColorNombre; }
            private set {
                if (this.cmbColorNombre == value) return;
                this.cmbColorNombre = value;
                this.NotifyOfPropertyChange(() => this.CmbColorNombre);
            }
        }
        private List<string> cmbColorHex;

        public List<string> CmbColorHex
        {
            get { return this.cmbColorHex; }
            private set
            {
                if (this.cmbColorHex == value) return;
                this.cmbColorHex = value;
                this.NotifyOfPropertyChange(() => this.CmbColorHex);
            }
        }

        private string cmbColorSelected=null;

        public string CmbColorSelected
        {
            get { return this.cmbColorSelected; }
            private set {
                if (this.cmbColorSelected == value) { return;}
                cmbColorSelected = value;
                this.NotifyOfPropertyChange(() => this.CmbColorSelected);
            }
        }

        private int txtIdTipoZona;
        
        public int TxtIdTipoZona
        {
            get { return txtIdTipoZona; }
            set { txtIdTipoZona = value;
            NotifyOfPropertyChange("TxtIdTipoZona");
            }
        }
        private string txtNombre;

        public string TxtNombre
        {
            get { return txtNombre; }
            set { txtNombre = value;
            NotifyOfPropertyChange("TxtNombre");
            }
        }

       
        public MantenerTipoZonaViewModel()
        {
            cmbColor = gateway.BuscarZona();
            cambioColorNombre();
            //Nuevo
            indicador = 1;
                    
        }
        private BuscarTipoZonaViewModel buscarTipoZonaViewModel;
        private int accion=0;
        public MantenerTipoZonaViewModel(BuscarTipoZonaViewModel buscarTipoZonaViewModel, int p):this()
        {
            // TODO: Complete member initialization
            this.buscarTipoZonaViewModel = buscarTipoZonaViewModel;
            this.CmbColorSelected = null;
            this.TxtNombre = buscarTipoZonaViewModel.TipoZonaSeleccionada.Nombre;
            this.TxtIdTipoZona = buscarTipoZonaViewModel.TipoZonaSeleccionada.IdTipoZona;
            this.accion = p;
        }
        Validacion.Evaluador eval = new Validacion.Evaluador();

        public void AgregarTipoZona()
        {

            if (accion==0 ){
                TipoZona zona = new TipoZona();
                zona.Nombre = this.txtNombre;
                zona.Color = this.cmbColorSelected;
                
                if (!eval.evalString(zona.Nombre))
                {
                    MessageBox.Show("Algunos de los valores no es correcto, verifique e intente de nuevo");
                    return;
                }
                
                if (cmbColorSelected == null) {
                    MessageBox.Show("Algunos de los valores no es correcto, verifique e intente de nuevo");
                    return;
                }

                zona.IdColor = gateway.BuscarZona(this.cmbColorSelected).IdColor;
                TipoZonaSQL gw = new TipoZonaSQL();
                gw.agregarTipoZona(zona);
                MessageBox.Show("Zona agregada Correctamete");
            }
            if (accion == 1) {
                TipoZona zona = new TipoZona();
                zona.Nombre = this.TxtNombre;
                zona.IdTipoZona = this.TxtIdTipoZona;

                if (!eval.evalString(zona.Nombre))
                {
                    MessageBox.Show("Algunos de los valores no es correcto, verifique e intente de nuevo");
                    return;
                }

                if (cmbColorSelected == null)
                {
                    MessageBox.Show("Algunos de los valores no es correcto, verifique e intente de nuevo");
                    return;
                }

                zona.IdColor = gateway.BuscarZona(this.cmbColorSelected).IdColor;
                zona.Color = this.cmbColorSelected;
                TipoZonaSQL gw = new TipoZonaSQL();
                gw.modificarTipoZona(zona);
                MessageBox.Show("Zona agregada Correctamete");
            }
        }

        public string this[string columName] {

            get {

                string result = string.Empty;
                switch (columName) {
                    case "TxtNombre": if (string.IsNullOrEmpty(TxtNombre)) result = "esta vacia"; break;
                };
                return result;
            }
        }
        public string Error{
            get
            {
                return "Error Test!!!!";
            }
        }
    }
}
