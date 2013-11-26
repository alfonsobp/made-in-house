using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Ventas;
using MadeInHouse.DataObjects.Ventas;

namespace MadeInHouse.ViewModels.Ventas
{
    class MantenerIgvPuntosViewModel : Screen
    {
        public MantenerIgvPuntosViewModel()
        {
            TraerDatos();
        }

        string txtIgv;

        public string TxtIgv
        {
            get { return txtIgv; }
            set { txtIgv = value; NotifyOfPropertyChange(() => TxtIgv); }
        }

        string txtPuntos;

        public string TxtPuntos
        {
            get { return txtPuntos; }
            set { txtPuntos = value; NotifyOfPropertyChange(() => TxtPuntos); }
        }

        public void Guardar()
        {
            VentaSQL vsql = new VentaSQL();
            vsql.ActualizarIgvPuntos(TxtIgv,TxtPuntos);
        }

        private void TraerDatos()
        {
            IgvPuntos ip = new IgvPuntos();
            VentaSQL vsql = new VentaSQL();
            ip = vsql.sacarDatos();
            TxtIgv = ip.Igv.ToString();
            TxtPuntos = ip.Puntos.ToString();
        }
    }
}
