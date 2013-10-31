using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Compras;
using MadeInHouse.DataObjects.Compras;
using System.Windows;

namespace MadeInHouse.ViewModels.Compras
{
    class mantenerSolicitudesAdquisicionViewModel : PropertyChangedBase
    {
        BuscadorSolicitudesAdquisicionViewModel model;

        SolicitudAdquisicion p;
        
        public SolicitudAdquisicion P
        {
            get { return p; }
            set { p = value;  }
        }

      


        public mantenerSolicitudesAdquisicionViewModel(SolicitudAdquisicion p,  BuscadorSolicitudesAdquisicionViewModel model) {

            this.P = p;
            this.model = model;

        }

        public void Guardar() {

        ProductoxSolicitudAdSQL m = new ProductoxSolicitudAdSQL();

        foreach (ProductoxSolicitudAd pi in P.LstProductos ){

            
            
            m.Actualizar(pi);
            

        }

        new  SolicitudAdquisicionSQL().Actualizar(P);

        MessageBox.Show("Los datos fuerons Actualizados Satisfactoriamente ");

        model.Buscar();
        }

    }
}
