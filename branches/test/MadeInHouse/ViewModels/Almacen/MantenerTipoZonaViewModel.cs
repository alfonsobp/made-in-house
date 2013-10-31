using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerTipoZonaViewModel : PropertyChangedBase
    {
        private TipoZona tipoZonaSeleccionada;


        private DataObjects.Almacen.ColorSQL gateway= new DataObjects.Almacen.ColorSQL();

        public TipoZona TipoZonaSeleccionada
        {
            get { return tipoZonaSeleccionada; }
            set { tipoZonaSeleccionada = value; }
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

        private string cmbColorSelected;

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
            set { txtIdTipoZona = value; }
        }
        private string txtNombre;

        public string TxtNombre
        {
            get { return txtNombre; }
            set { txtNombre = value; }
        }

        public MantenerTipoZonaViewModel(TipoZona tipoZonaSeleccionada)
        {

            //Modifica
            indicador = 2;
            // TODO: Complete member initialization
            cmbColor = gateway.BuscarZona();
            cambioColorNombre();
            //cmbColorSelected = gateway.BuscarZona(int.Parse(tipoZonaSeleccionada.Color));
            this.tipoZonaSeleccionada = tipoZonaSeleccionada;

        }

        public MantenerTipoZonaViewModel()
        {
            cmbColor = gateway.BuscarZona();
            cambioColorNombre();
            //Nuevo
            indicador = 1;
                    
        }

        public void AgregarTipoZona() {
            TipoZona zona = new TipoZona();
            zona.Nombre = this.txtNombre;
            zona.Color = this.cmbColorSelected;
            zona.IdColor = gateway.BuscarZona(this.cmbColorSelected).IdColor;
            TipoZonaSQL gw = new TipoZonaSQL();
            gw.agregarTipoZona(zona);

        
        }

    }
}
