using Caliburn.Micro;
using MadeInHouse.Models.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.ViewModels.Compras
{
    class MedioPagoViewModel:Screen
    {

        List<Proveedor> lstProveedor;

        public List<Proveedor> LstProveedor
        {
            get { return lstProveedor; }
            set { lstProveedor = value; NotifyOfPropertyChange("LstProveedor"); }
        }

        List<Consolidado> lstConsolidado;

        public List<Consolidado> LstConsolidado
        {
            get { return lstConsolidado; }
            set { lstConsolidado = value; NotifyOfPropertyChange("LstConsolidado"); }
        }

        int idAlmacen;

        public int IdAlmacen
        {
            get { return idAlmacen; }
            set { idAlmacen = value; NotifyOfPropertyChange("IdAlmacen"); }
        }

        string codAlmacen;

        public string CodAlmacen
        {
            get { return codAlmacen; }
            set { codAlmacen = value; NotifyOfPropertyChange("CodAlmacen"); }
        }

        public MedioPagoViewModel(List<Proveedor> lstProveedor, List<Consolidado> lstConsolidado, int idAlmacen) {

            this.IdAlmacen = idAlmacen;
            this.LstConsolidado = lstConsolidado;
            this.LstProveedor = lstProveedor;
            CodAlmacen = "ALM-" + (idAlmacen + 10000000);
        
        }

    }
}
