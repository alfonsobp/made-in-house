using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Caliburn.Micro;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using MadeInHouse.Models;
using MadeInHouse.Models.Seguridad;
using MadeInHouse.DataObjects.Seguridad;

namespace MadeInHouse.ViewModels.Reportes
{
    class reporteAccionesViewModel:Screen
    {
        public reporteAccionesViewModel()
        {

        }

        private List<Log> lstLogUsuario;

        public List<Log> LstLogUsuario
        {
            get { return lstLogUsuario; }
            set { lstLogUsuario = value; NotifyOfPropertyChange(() => LstLogUsuario); }
        }





        private Log logUsuarioSeleccionado;
        public void SelectedItemChanged(object sender)
        {
            logUsuarioSeleccionado = ((sender as DataGrid).SelectedItem as Log);
        }

        public void BuscarAcciones()
        {
            lstLogUsuario = LogSQL.BuscarAcciones();
            NotifyOfPropertyChange("LstLogUsuario");
        }

    }
}
