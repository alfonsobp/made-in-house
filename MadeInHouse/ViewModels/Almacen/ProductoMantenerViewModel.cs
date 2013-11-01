using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects;
using System.Windows;

namespace MadeInHouse.ViewModels.Almacen
{
    class ProductoMantenerViewModel : PropertyChangedBase
    {


        private string txtAbreviatura;

        public string TxtAbreviatura
        {
            get { return txtAbreviatura; }
            set { txtAbreviatura = value; 
                NotifyOfPropertyChange(() => TxtAbreviatura);

                txtCodigo = CodigoProducto();
                NotifyOfPropertyChange(() => TxtCodigo);
            }
        }

        

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

        private List<UnidadMedida> lstUnidadMedida;

        public List<UnidadMedida> LstUnidadMedida
        {
            get { return lstUnidadMedida; }
            set
            {
                if (this.lstUnidadMedida == value)
                {
                    return;
                }
                this.lstUnidadMedida = value;
                this.NotifyOfPropertyChange(() => this.lstUnidadMedida);
            }
        }


        private int selectedValueUnid;

        public int SelectedValueUnid
        {
            get { return selectedValueUnid; }
            set { selectedValueUnid = value;
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


        private ProductoSQL pSQL = new ProductoSQL();

        private bool editar = true;

        private int estado = 0;

        public bool Editar
        {
            get { return editar; }
            set { editar = value; NotifyOfPropertyChange(() => Editar); }
        }

        public void GuardarProducto()
        {
            if (TxtNombre == null || TxtNombre.Equals(""))
            {
                MessageBox.Show("Debe ingresar el nombre del producto");
            }
            else if (TxtAbreviatura == null || TxtAbreviatura.Equals(""))
            {
                MessageBox.Show("Debe ingresar la abreviatura del nombre");
            }
            else
            {
                
                Producto p = new Producto();
                
                p.Nombre = TxtNombre;
                p.Abreviatura = TxtAbreviatura;
                p.CodigoProd = TxtCodigo;
                p.IdLinea = SelectedValue;
                p.IdSubLinea = SelectedValueSub;
                p.IdUnidad = SelectedValueUnid;
                p.Percepcion = Percepcion == true ? 1 : 0;
                p.Descripcion = TxtDescrip;
                if (estado == 0)
                {
                    pSQL.AgregarProducto(p);
                    MessageBox.Show("Se agregó el producto correctamente");
                    TxtAbreviatura = "";
                    TxtNombre = "";
                    Percepcion = false;
                    TxtDescrip = "";
                }
                else
                {
                    p.IdProducto = estado;
                    pSQL.ActualizarProducto(p);
                    MessageBox.Show("Se actualizó el producto correctamente");
                }


                
            }

        }

        public void EditarProducto()
        {
            Editar = true;
            
        }

        private string CodigoProducto()
        {
            UtilesSQL util = new UtilesSQL();
            string cod=null;
            
            cod= GetLinea(SelectedValue).Abreviatura + GetSubLinea(selectedValueSub).Abreviatura + txtAbreviatura;
          
            return cod;
        }



        public ProductoMantenerViewModel()
        {

            LineaProductoSQL lpSQL = new LineaProductoSQL();
            UnidadMedidaSQL umSQL = new UnidadMedidaSQL();
            LstLineasProducto = lpSQL.ObtenerLineasProducto();
            LstUnidadMedida = umSQL.BuscarUnidadMedida();
        }

        public ProductoMantenerViewModel(Producto p)
        {

            LineaProductoSQL lpSQL = new LineaProductoSQL();
            UnidadMedidaSQL umSQL = new UnidadMedidaSQL();
            LstLineasProducto = lpSQL.ObtenerLineasProducto();
            LstUnidadMedida = umSQL.BuscarUnidadMedida();
            TxtNombre = p.Nombre;
            TxtCodigo = p.CodigoProd;
            txtAbreviatura = p.Abreviatura;
            TxtDescrip = p.Descripcion;
            Percepcion = p.Percepcion==0 ? false:true;
            Editar = false;
            estado = p.IdProducto;
        }


    }
}
