using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;


namespace MadeInHouse.ViewModels.Almacen
{
    
    class MantenerLineaProductoViewModel : PropertyChangedBase
    {

        private MadeInHouse.Models.MyWindowManager win = new MadeInHouse.Models.MyWindowManager();
        private LineaProductoSQL lpSQL = null;
        

        private string txtNombre;

        public string TxtNombre
        {
            get { return txtNombre; }
            set { txtNombre = value; NotifyOfPropertyChange(() => TxtNombre); }
        }


        private string txtAbrv;

        public string TxtAbrv
        {
            get { return txtAbrv; }
            set { txtAbrv = value; NotifyOfPropertyChange(() => TxtAbrv); }
        }

        public void GuardarProducto()
        {
            LineaProducto lp = new LineaProducto();
            lp.Nombre = TxtNombre;
            lp.Abreviatura=TxtAbrv;

            lpSQL = new LineaProductoSQL();
            lpSQL.AgregarLineaProducto(lp);

        }

        private List<SubLineaProducto> lstSubLinea;

        public List<SubLineaProducto> LstSubLinea
        {
            get { return lstSubLinea; }
            set { lstSubLinea = value;
            NotifyOfPropertyChange(() => LstSubLinea); }
        }



        public void AgregarSubLinea()
        {
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
            NotifyOfPropertyChange("LstSubLinea");
        }


        public void GuardarLineaProducto()
        {

            lpSQL = new LineaProductoSQL();
            LineaProducto lp = new LineaProducto();
            lp.Nombre = TxtNombre;
            lp.Abreviatura = TxtAbrv;
            lp.Sublineas = LstSubLinea;
            lpSQL.AgregarLineaProducto(lp);


        }

    }
}
