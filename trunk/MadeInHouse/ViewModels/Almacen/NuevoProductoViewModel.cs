using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Almacen
{
    class NuevoProductoViewModel : PropertyChangedBase
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






    }
}
