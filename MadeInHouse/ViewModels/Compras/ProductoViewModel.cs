using Caliburn.Micro;
using MadeInHouse.Model;
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


        ProveedorxProducto cp;

        public ProveedorxProducto Cp
        {
            get { return cp; }
            set { cp = value; }
        
        }

        private string estado;

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public ProductoViewModel(ProveedorxProducto cp) {

            Cp=cp;

            Estado = (Cp.Estado == 1) ? "ACTIVO" : "INACTIVO";
            
        }

    




    }
}
