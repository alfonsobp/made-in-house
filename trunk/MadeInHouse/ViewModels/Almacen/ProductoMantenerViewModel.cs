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

        private bool percepcion;

        public bool Percepcion
        {
            get { return percepcion; }
            set { percepcion = value; }
        }
        private bool interno;

        public bool Interno
        {
            get { return interno; }
            set { interno = value; }
        }
        private bool venta;

        public bool Venta
        {
            get { return venta; }
            set { venta = value; }
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
                TxtCodigo=CodigoProducto();
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
            return (from slp in LstSubLineasProducto
                    where slp.IdSubLinea == idSubLinea
                    select slp).FirstOrDefault();
        }


        private ProductoSQL pSQL;

        public void GuardarProducto()
        {
            pSQL = new ProductoSQL();
            Producto p = new Producto();
            p.Nombre = TxtNombre;
            p.CodigoProd = TxtCodigo;
            p.IdLinea = SelectedValue;
            p.IdSubLinea = SelectedValueSub;
            p.Percepcion = Percepcion==true ? 1:0;
            p.Descripcion = TxtDescrip;
            pSQL.AgregarProducto(p);

        }

        private string CodigoProducto()
        {
            UtilesSQL util = new UtilesSQL();
            string cod = GetLinea(SelectedValue).Abreviatura + GetSubLinea(selectedValueSub).Abreviatura+(util.ObtenerMaximoID("Producto","idProducto")+1).ToString();
            return cod;
        }



        public ProductoMantenerViewModel()
        {

            LineaProductoSQL lpSQL = new LineaProductoSQL();
            LstLineasProducto = lpSQL.ObtenerLineasProducto();
        }














    }
}
