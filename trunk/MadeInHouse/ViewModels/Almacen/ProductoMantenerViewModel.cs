using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.ViewModels.Almacen
{
    class ProductoMantenerViewModel : PropertyChangedBase
    {
        private string txtNombre;

        public string TxtNombre
        {
            get { return txtNombre; }
            set { txtNombre = value; NotifyOfPropertyChange(() => TxtNombre); }
        }
        private string txtCodigo;

        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }
        private string txtDescrip;

        public string TxtDescrip
        {
            get { return txtDescrip; }
            set { txtDescrip = value; NotifyOfPropertyChange(() => TxtDescrip); }
        }

        private Dictionary<string, int> lineasProducto = new Dictionary<string, int>();

        public List<string> CmbLinea
        {
            get
            {
                return new List<string>(lineasProducto.Keys);
            }
        }


        private BindableCollection<LineaProducto> lstLineasProducto;

        public BindableCollection<LineaProducto> LstLineasProducto
        {
            get { return lstLineasProducto; }
            set {
                if (this.lstLineasProducto == value)
                {
                    return;
                }
                this.lstLineasProducto = value;
                this.NotifyOfPropertyChange(() => this.lstLineasProducto);
            }
        }
            

        public ProductoMantenerViewModel()
        {
            LineaProductoSQL slpSQL = new LineaProductoSQL();
            LstLineasProducto = slpSQL.ObtenerLineasProducto();
        }














    }
}
