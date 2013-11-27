using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel.Composition;
using MadeInHouse.ViewModels.Layouts;


namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(MantenerLineaProductoViewModel))]
    class MantenerLineaProductoViewModel : PropertyChangedBase,IDataErrorInfo
    {
        [ImportingConstructor]
        public MantenerLineaProductoViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
        }

        private readonly IWindowManager _windowManager;

        private LineaProductoSQL lpSQL = null;
        
        private string txtNombre;

        public string TxtNombre
        {
            get { return txtNombre; }
            set { txtNombre = value;
                NotifyOfPropertyChange("TxtNombre"); 
            }
        }


        private string txtAbrv;

        public string TxtAbrv
        {
            get { return txtAbrv; }
            set { txtAbrv = value; 
                NotifyOfPropertyChange("TxtAbrv"); 
            }

        }

        public void GuardarProducto()
        {
            if (string.IsNullOrWhiteSpace(TxtNombre) || string.IsNullOrWhiteSpace(TxtAbrv) || LstSubLinea == null) {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Inserte Valores validos o inserte minimo una SubLinea"));
                return;
            }
            LineaProducto lp = new LineaProducto();
            lp.Nombre = TxtNombre;
            lp.Abreviatura=TxtAbrv;
            lpSQL = new LineaProductoSQL();
            lpSQL.AgregarLineaProducto(lp);



        }

        private List<SubLineaProducto> lstSubLinea=null;

        public List<SubLineaProducto> LstSubLinea
        {
            get { return lstSubLinea; }
            set { lstSubLinea = value;
            NotifyOfPropertyChange("LstSubLinea"); 
            }
        }



        public void AgregarSubLinea()
        {
            if (string.IsNullOrWhiteSpace(TxtNombre)) {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Ingrese en Nombre una cadena no vacia"));
                return;
            }
            _windowManager.ShowWindow(new MantenerSubLineaProductoViewModel(_windowManager, this));
        }

        public void ActualizaTablaSubLineas(SubLineaProducto slp)
        {
            if (LstSubLinea != null)
            {

                List<SubLineaProducto> lstAux = new List<SubLineaProducto>();
                foreach (SubLineaProducto s in LstSubLinea)
                {
                    lstAux.Add(s);
                }
                lstAux.Add(slp);
                LstSubLinea.Clear();
                LstSubLinea = lstAux;
            }
            else
            {
                LstSubLinea = new List<SubLineaProducto>();
                LstSubLinea.Add(slp);
            }
        }

        public void GuardarLineaProducto()
        {
            if (string.IsNullOrWhiteSpace(TxtAbrv) || string.IsNullOrWhiteSpace(TxtNombre) || LstSubLinea == null) {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Datos no validos, ingrese minimo una Sublinea"));
                return;
            }
            if (LstSubLinea != null) {
                if (LstSubLinea.Count == 0) {
                    _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Datos no validos, ingrese minimo una Sublinea"));
                    return;
                }
            }

            lpSQL = new LineaProductoSQL();
            LineaProducto lp = new LineaProducto();
            lp.Nombre = TxtNombre;
            lp.Abreviatura = TxtAbrv;
            lp.Sublineas = LstSubLinea;
            lpSQL.AgregarLineaProducto(lp);
        }
        private SubLineaProducto selectedSubLinea = null;

        public SubLineaProducto SelectedSubLinea
        {
            get { return selectedSubLinea; }
            set { selectedSubLinea = value;
            NotifyOfPropertyChange("SelectedSubLinea");
            }
        }
        public void SelectedItemChanged(object sender)
        {
            selectedSubLinea = ((sender as DataGrid).SelectedItem as SubLineaProducto);
        }
            
        public void Borrar()
        {
            
            SubLineaProducto tz;
            tz = selectedSubLinea;
            if (tz != null)
            {
                LstSubLinea.Remove(tz);
                LstSubLinea = new List<SubLineaProducto>(LstSubLinea);   
            }
            else {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No se ha seleccionado ninguna Zona"));
            }
        }
 
        public string this[string columName]
        {

            get
            {

                string result = string.Empty;
                switch (columName)
                {
                    case "TxtNombre": if (string.IsNullOrEmpty(TxtNombre)) result = "esta vacia"; break;
                    case "TxtAbrv": if (string.IsNullOrEmpty(TxtAbrv)) result = "esta vacia"; break;
                        
                };
                return result;
            }
        }
        public string Error
        {
            get
            {
                return "Error Test!!!!";
            }
        }

    }
}
