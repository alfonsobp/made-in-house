using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.DataObjects;

namespace MadeInHouse.ViewModels.Almacen
{

    class ExtendedProduct : Producto
    {
        public string Linea { get; set; }
        public string SubLinea { get; set; }

    }


    class ProductoBuscarViewModel : PropertyChangedBase
    {


        private MadeInHouse.Models.MyWindowManager win = new MadeInHouse.Models.MyWindowManager();


        public ProductoBuscarViewModel()
        {
            LineaProductoSQL lpSQL = new LineaProductoSQL();
            LstLineasProducto = lpSQL.ObtenerLineasProducto();


        }

        private string txtCodigo;

        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }

        private List<ExtendedProduct> lstProductos;

        public List<ExtendedProduct> LstProductos
        {
            get { return lstProductos; }
            set
            {
                lstProductos = value;
                NotifyOfPropertyChange(() => LstProductos);
            }
        }



        private BindableCollection<LineaProducto> lstLineasProducto;

        public BindableCollection<LineaProducto> LstLineasProducto
        {
            get { return lstLineasProducto; }
            set
            {
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
            set { selectedIndex = value; NotifyOfPropertyChange(() => SelectedIndex); }
        }


        private int selectedValue;

        public int SelectedValue
        {
            get { return selectedValue; }
            set
            {
                selectedValue = value;
                SubLineaProductoSQL slpSQL = new SubLineaProductoSQL();
                LstSubLineasProducto = slpSQL.ObtenerSubLineas(selectedValue);
                SelectedIndex = 0;

            }
        }

        private int selectedValueSub;

        public int SelectedValueSub
        {
            get { return selectedValueSub; }
            set
            { selectedValueSub = value; }
        }

        private Producto productoSel;

        public Producto ProductoSel
        {
            get { return productoSel; }
            set { productoSel = value; }
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

        


        private ProductoSQL pSQL = new ProductoSQL();
        private LineaProductoSQL lsql;
        private SubLineaProductoSQL ssql;

        public void BuscarProductos()
        {
             List<Producto> lp;
             lp=pSQL.BuscarProducto(TxtCodigo, SelectedValue, SelectedValueSub);
             List<ExtendedProduct> LstProductosAux = new List<ExtendedProduct>();

             if (lp != null)
             {
                 foreach (Producto p in lp)
                 {
                     ExtendedProduct exp = new ExtendedProduct();
                     exp.IdProducto = p.IdProducto;
                     exp.CodigoProd = p.CodigoProd;
                     exp.Nombre = p.Nombre;
                     exp.Descripcion = p.Descripcion;
                     exp.Abreviatura = p.Abreviatura;
                     exp.Percepcion = p.Percepcion;
                     if (SelectedValue != 0) 
                         exp.Linea = GetLinea(SelectedValue).Nombre;

                     else
                         exp.Linea = GetLinea(p.IdLinea).Nombre;

                     if (SelectedValueSub != 0) exp.SubLinea = GetSubLinea(SelectedValueSub).Nombre;
                     else
                     {
                         SelectedIndex = -1;
                         SubLineaProductoSQL slpSQL = new SubLineaProductoSQL();
                         LstSubLineasProducto = slpSQL.ObtenerSubLineas(p.IdLinea);
                         exp.SubLinea = GetSubLinea(p.IdSubLinea).Nombre;
                     }
                     LstProductosAux.Add(exp);
                 }
                 LstProductos = LstProductosAux;
             }
             else
             {
                 LstProductos = null;
                 System.Windows.Forms.MessageBox.Show("No se encontro ningún producto");
             }
             
        }

        public void Actualizar()
        {
            ProductoMantenerViewModel pmVM = new ProductoMantenerViewModel(ProductoSel);
            win.ShowWindow(pmVM);


        }




    }
}
