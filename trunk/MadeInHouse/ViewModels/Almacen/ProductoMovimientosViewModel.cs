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
    class ProductoMovimientosViewModel:PropertyChangedBase
    {


        private ObservableCollection<TipoZona> cmbZonas;
        public ObservableCollection<TipoZona> CmbZonas
        {
            get { return cmbZonas; }
            set { cmbZonas = value; }
        }

        public ProductoMovimientosViewModel()
        {
            CmbZonas = (new TipoZonaSQL()).BuscarZona();
        }

    }
}
