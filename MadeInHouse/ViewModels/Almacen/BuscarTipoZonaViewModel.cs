using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.ViewModels.Almacen
{
    class BuscarTipoZonaViewModel : PropertyChangedBase
    {
        private DataObjects.Almacen.TipoZonaSQL gateway;

        private MyWindowManager win = new MyWindowManager();


        private List<TipoZona > listaTipoZona;
        private TipoZona tipoZonaSeleccionada = new TipoZona();

        public BuscarTipoZonaViewModel() {
            gateway = new DataObjects.Almacen.TipoZonaSQL();
            ListaTipoZona = gateway.BuscarZona();
        }

        public List<TipoZona> ListaTipoZona
        {
            get
            {
                return this.listaTipoZona;
            }

            private set
            {
                if (this.listaTipoZona == value)
                {
                    return;
                }

                this.listaTipoZona = value;
                this.NotifyOfPropertyChange(() => this.ListaTipoZona);
            }
        }

        public void AbrirNuevaZona()
        {
            win.ShowWindow(new Almacen.MantenerTipoZonaViewModel());

        }

        public void SelectedItemChanged(object sender)
        {
            tipoZonaSeleccionada = ((sender as DataGrid).SelectedItem as TipoZona);

        }
        
        public void EditarTipoZona()
        {
            Almacen.MantenerTipoZonaViewModel obj = new Almacen.MantenerTipoZonaViewModel (tipoZonaSeleccionada);
            win.ShowWindow(obj);
        }

        public void BuscarTipoZona(string codigo, string descripcion)
        {
            gateway = new DataObjects.Almacen.TipoZonaSQL();
            if (string.IsNullOrEmpty(codigo)) codigo="-1"; 
            listaTipoZona = gateway.BuscarZona(int.Parse(codigo), descripcion);
            NotifyOfPropertyChange("ListaTipoZona");
        }

    }
}
