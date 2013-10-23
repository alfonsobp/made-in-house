using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
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
        
        private List<Color> cmbColor;

        public List<Color> CmbColor
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
        private Color cmbColorSelected;

        public Color CmbColorSelected
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

        public MantenerTipoZonaViewModel(TipoZona  tipoZonaSeleccionada)
        {
            //Modifica
            indicador = 2;    
            // TODO: Complete member initialization
            cmbColor = gateway.BuscarZona();
            cmbColorSelected = gateway.BuscarZona(int.Parse(tipoZonaSeleccionada.Color));
            this.tipoZonaSeleccionada = tipoZonaSeleccionada;
        }

        public MantenerTipoZonaViewModel()
        {
            cmbColor = gateway.BuscarZona();
            //Nuevo
            indicador = 1;
                    
        }

    }
}
