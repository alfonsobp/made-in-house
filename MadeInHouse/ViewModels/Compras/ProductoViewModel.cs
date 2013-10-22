using Caliburn.Micro;
using MadeInHouse.Models.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.ViewModels.Compras
{
    class ProductoViewModel : PropertyChangedBase
    {
        List<String> estados = new List<String> { "ACTIVO", "INACTIVO" };

        public List<String> Estados
        {
            get { return estados; }
            set { estados = value; NotifyOfPropertyChange("Estados"); }
        }


        CatalogoProveedor cp;

        public  CatalogoProveedor Cp
        {
            get { return cp; }
            set { cp = value; NotifyOfPropertyChange("Cp"); }
        }

        public ProductoViewModel(CatalogoProveedor cp) {

            Cp=cp;
        }

    




    }
}
