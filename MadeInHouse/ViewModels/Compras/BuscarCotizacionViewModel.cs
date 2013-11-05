using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Views.Compras;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.Models;
using System.Data.OleDb;
using System.Data;
using MadeInHouse.Models.Compras;

namespace MadeInHouse.ViewModels.Compras
{
    class BuscarCotizacionViewModel:PropertyChangedBase
    {
        
        //Costructores

        public BuscarCotizacionViewModel()
        {
            ActualizarCotizacion();
        }




        //Atributos de la clase

        private MyWindowManager win = new MyWindowManager();

        private Cotizacion cotizacionSeleccionada;




        //Funciones de la clase

        public void SelectedItemChanged(object sender)
        {
            cotizacionSeleccionada = ((sender as DataGrid).SelectedItem as Cotizacion);
        }

        public void NuevaCotizacion()
        {
            Compras.NuevaCotizacionViewModel obj = new Compras.NuevaCotizacionViewModel(this);
            win.ShowWindow(obj);
        }

        public void EditarCotizacion()
        {

            Compras.NuevaCotizacionViewModel obj = new Compras.NuevaCotizacionViewModel(cotizacionSeleccionada, this);
            win.ShowWindow(obj);
        }

        public void ActualizarCotizacion()
        {

        }

    }
}
