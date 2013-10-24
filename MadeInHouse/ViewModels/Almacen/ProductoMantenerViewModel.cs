using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects;

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

        private BindableCollection<SubLineaProducto> lstSubLineasProducto;

        public BindableCollection<SubLineaProducto> LstSubLineasProducto
        {
            get { return lstSubLineasProducto; }
            set
            {
                if (this.lstSubLineasProducto == value)
                {
                    return;
                }
                this.lstSubLineasProducto = value;
                this.NotifyOfPropertyChange(() => this.lstSubLineasProducto);
            }
        }



        private int selectedIndex;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; NotifyOfPropertyChange(() => SelectedIndex ); }
        }


        private int selectedValue;

        public int SelectedValue
        {
            get { return selectedValue; }
            set { 
                   selectedValue = value;
                   SubLineaProductoSQL slpSQL = new SubLineaProductoSQL();
                   LstSubLineasProducto= slpSQL.ObtenerSubLineas(selectedValue);
                   SelectedIndex = 0;
                   
            }
        }

        private int selectedValueSub;

        public int SelectedValueSub
        {
            get { return selectedValueSub; }
            set
            {
                selectedValueSub = value;
            }
        }

        private LineaProducto GetLinea(int idLinea)
        {
            return (from lp in LstLineasProducto
                    where lp.IdLinea == idLinea
                    select lp).FirstOrDefault();
        }
        private SubLineaProducto GetSubLinea(int idSubLinea)
        {
            return (from lp in LstSubLineasProducto
                    where lp.IdSubLinea == idSubLinea
                    select lp).FirstOrDefault();
        }


        private ProductoSQL pSQL;

        public void GuardarProducto()
        {
            pSQL = new ProductoSQL();
            Producto p = new Producto();
            p.Nombre = TxtNombre;
            p.IdLinea = SelectedValue;
            p.IdSubLinea = SelectedValueSub;
            pSQL.AgregarProducto(p);

        }

        private string CodigoProducto()
        {
            UtilesSQL util = new UtilesSQL();
            string cod = GetLinea(SelectedValue).Abreviatura;

            return cod;
        }



        public ProductoMantenerViewModel()
        {

            LineaProductoSQL slpSQL = new LineaProductoSQL();
            LstLineasProducto = slpSQL.ObtenerLineasProducto();
        }














    }
}
