using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.RRHH;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel.Composition;
using MadeInHouse.Models;

namespace MadeInHouse.ViewModels.RRHH
{
    public class BuscarOrganigramaViewModel : Screen
    {
        private MyWindowManager win = new MyWindowManager();

        private String tienda;

        public String Tienda
        {
            get { return tienda; }
            set { tienda = value; NotifyOfPropertyChange(() => Tienda); }
        }

        private String areaTrabajo;

        public String AreaTrabajo
        {
            get { return areaTrabajo; }
            set { areaTrabajo = value; NotifyOfPropertyChange(() => AreaTrabajo); }
        }

        private void Tienda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tienda = ((ComboBox)sender).SelectedValue.ToString();
            MessageBox.Show(tienda);
        }

        public void BuscarOrganigrama()
        {
            MessageBox.Show(tienda);

            //lstVentarepor = DataObjects.ReportesSQL.GenerarReporVentas(null, null, null, null, null);
            //NotifyOfPropertyChange("LstVentarepor");
        }

    }
}
