using Caliburn.Micro;
using MadeInHouse.DataObjects;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;
using MadeInHouse.ViewModels.Layouts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;

namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(ProductoMantenerViewModel))]
    class ProductoMantenerViewModel : Screen, IDataErrorInfo
    {
        #region constructores

        [ImportingConstructor]
        public ProductoMantenerViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
            LineaProductoSQL lpSQL = new LineaProductoSQL();
            UnidadMedidaSQL umSQL = new UnidadMedidaSQL();
            LstLineasProducto = lpSQL.ObtenerLineasProducto();
            LstUnidadMedida = umSQL.BuscarUnidadMedida();
        }

        public ProductoMantenerViewModel(IWindowManager windowmanager, Producto p)
            : this(windowmanager)
        {
            TxtNombre = p.Nombre;
            TxtCodigo = p.CodigoProd;
            txtAbreviatura = p.Abreviatura;
            TxtDescrip = p.Descripcion;
            TxtStockMin = p.StockMin;
            txtStockMax = p.StockMax;
            Percepcion = p.Percepcion == 0 ? false : true;
            Editar = false;
            estado = p.IdProducto;
        }

        #endregion

        #region atributos

        private readonly IWindowManager _windowManager;
        private bool TxtAbreviaturaChanged = false;

        private string txtAbreviatura;
        public string TxtAbreviatura
        {
            get { return txtAbreviatura; }
            set
            {
                txtAbreviatura = value;
                TxtAbreviaturaChanged = true;
                NotifyOfPropertyChange(() => TxtAbreviatura);

                txtCodigo = CodigoProducto();
                NotifyOfPropertyChange(() => TxtCodigo);
            }
        }

        private bool TxtNombreChanged = false;
        private string txtNombre;
        public string TxtNombre
        {
            get { return txtNombre; }
            set
            {
                txtNombre = value;
                TxtNombreChanged = true;
                NotifyOfPropertyChange(() => TxtNombre);
            }
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

        private int txtStockMin;
        public int TxtStockMin
        {
            get { return txtStockMin; }
            set
            {
                txtStockMin = value;
                NotifyOfPropertyChange(() => TxtStockMin);
            }
        }

        private int txtStockMax;
        public int TxtStockMax
        {
            get { return txtStockMax; }
            set
            {
                txtStockMax = value;
                NotifyOfPropertyChange(() => TxtStockMax);
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
            set
            {
                selectedValueUnid = value;
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
            {

                selectedValueSub = value;
                TxtCodigo = CodigoProducto();
            }
        }

        private ProductoSQL pSQL = new ProductoSQL();

        private int estado = 0;

        private bool editar = true;
        public bool Editar
        {
            get { return editar; }
            set { editar = value; NotifyOfPropertyChange(() => Editar); }
        }

        Validacion.Evaluador eval = new Validacion.Evaluador();

        #endregion

        #region metodos

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

        public void GuardarProducto()
        {
            bool isCorrect = true;

            if (TxtNombre == null || TxtNombre.Equals(""))
            {
                TxtNombre = "";
                TxtNombreChanged = true;
                isCorrect = false;
            }

            if (TxtAbreviatura == null || TxtAbreviatura.Equals(""))
            {
                TxtAbreviatura = "";
                TxtAbreviaturaChanged = true;
                isCorrect = false;
            }

            if ((TxtStockMax < TxtStockMin) || string.IsNullOrWhiteSpace(TxtStockMin.ToString()) || string.IsNullOrWhiteSpace(TxtStockMax.ToString()) || !(eval.esNumeroEntero(TxtStockMax.ToString())) || !(eval.esNumeroEntero(TxtStockMin.ToString())))
            {
                isCorrect = false;
            }

            if ((TxtStockMin < 0) || TxtStockMax < 0)
            {
                isCorrect = false;
            }

            if (isCorrect)
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
                p.StockMin = TxtStockMin;
                p.StockMax = TxtStockMax;

                if (estado == 0)
                {
                    pSQL.AgregarProducto(p);
                    _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Se agregó el producto correctamente"));
                    TxtAbreviatura = "";
                    TxtNombre = "";
                    Percepcion = false;
                    TxtDescrip = "";
                    TxtStockMin = 0;
                    TxtStockMin = 0;
                }
                else
                {
                    p.IdProducto = estado;
                    pSQL.ActualizarProducto(p);
                    _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Se actualizó el producto correctamente"));
                }
            }
        }

        public void LimpiarCampos()
        {
            TxtAbreviatura = "";
            TxtNombre = "";
            Percepcion = false;
            TxtDescrip = "";
            TxtStockMin = 0;
            TxtStockMin = 0;
        }

        public void EditarProducto()
        {
            Editar = true;
        }

        private string CodigoProducto()
        {
            UtilesSQL util = new UtilesSQL();
            string cod = null;

            cod = GetLinea(SelectedValue).Abreviatura + GetSubLinea(selectedValueSub).Abreviatura;

            return cod;

        }

        #endregion

        #region Validaciones
        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case "TxtNombre": if (TxtNombreChanged && string.IsNullOrEmpty(TxtNombre)) result = "El nombre del producto no se ha definido"; break;
                    case "TxtAbreviatura": if (TxtAbreviaturaChanged && string.IsNullOrEmpty(TxtAbreviatura)) result = "No se ha definido la abreviatura"; break;
                    case "TxtStockMin": if (TxtStockMin<0) result = "El stock no puede ser negativo"; break;
                    case "TxtStockMax": if (TxtStockMax<TxtStockMin) result = "El stock maximo no puede ser menor al minimo"; break;
                    
                };
                return result;
            }
        }


        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        #endregion


    }
}
