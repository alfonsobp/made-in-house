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


namespace MadeInHouse.ViewModels.Almacen
{

    class MantenerLineaProductoViewModel : PropertyChangedBase,IDataErrorInfo
    {

        private MadeInHouse.Models.MyWindowManager win = new MadeInHouse.Models.MyWindowManager();
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
                MessageBox.Show("Inserte Valores validos o inserte minimo una SubLinea");
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
                MessageBox.Show("Ingrese en Nombre una cadena no vacia");
                return;
            }
            win.ShowWindow(new MantenerSubLineaProductoViewModel(this));   
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
                MessageBox.Show("Datos no validos, ingrese minimo una Sublinea");
                return;
            }
            if (LstSubLinea != null) {
                if (LstSubLinea.Count == 0) {
                    MessageBox.Show("Datos no validos, ingrese minimo una Sublinea");
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
                MessageBox.Show("No se ha seleccionado ninguna Zona");
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
