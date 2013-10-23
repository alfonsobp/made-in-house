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

        internal TipoZona TipoZonaSeleccionada
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
            indicador = 2;    
            // TODO: Complete member initialization
            
            this.tipoZonaSeleccionada = tipoZonaSeleccionada;
        }

        public MantenerTipoZonaViewModel()
        {
            
            indicador = 1;
                    
        }

    }
}
