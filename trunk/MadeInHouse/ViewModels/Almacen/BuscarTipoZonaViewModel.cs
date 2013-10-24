using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
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
        public class ExtendedZona : TipoZona {
            private string nombreColor;

            public string NombreColor
            {
                get { return nombreColor; }
                set { nombreColor = value; }
            } 
              
        }

        private DataObjects.Almacen.TipoZonaSQL gateway;

        private DataObjects.Almacen.ColorSQL gw;
        private MyWindowManager win = new MyWindowManager();


        private ObservableCollection<ExtendedZona> listaTipoZona;
        private TipoZona tipoZonaSeleccionada = new TipoZona();


        public BuscarTipoZonaViewModel() {
            gateway = new DataObjects.Almacen.TipoZonaSQL();
            gw = new DataObjects.Almacen.ColorSQL();

            ObservableCollection<TipoZona> listaTipoZonaExt = new ObservableCollection<TipoZona>();
            listaTipoZonaExt = gateway.BuscarZona();

            listaTipoZona = new ObservableCollection<ExtendedZona>();
            foreach (TipoZona p in listaTipoZonaExt) {
                ExtendedZona exp = new ExtendedZona();
                exp.Color = p.Color;
                exp.IdColor = p.IdColor;
                exp.IdTipoZona = p.IdTipoZona;
                exp.Nombre = p.Nombre;
                exp.NombreColor = gw.BuscarZona(p.Color).Nombre;
                listaTipoZona.Add(exp);
            }
            
        }

        public ObservableCollection<ExtendedZona> ListaTipoZona
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
            gw = new DataObjects.Almacen.ColorSQL();
            if (string.IsNullOrEmpty(codigo)) codigo="-1";

            ObservableCollection<TipoZona> listaTipoZonaExt = new ObservableCollection<TipoZona>();

            listaTipoZonaExt = gateway.BuscarZona(int.Parse(codigo), descripcion);
            listaTipoZona = new ObservableCollection<ExtendedZona>();
            foreach (TipoZona p in listaTipoZonaExt)
            {
                ExtendedZona exp = new ExtendedZona();
                exp.Color = p.Color;
                exp.IdColor = p.IdColor;
                exp.IdTipoZona = p.IdTipoZona;
                exp.Nombre = p.Nombre;
                exp.NombreColor = gw.BuscarZona(p.Color).Nombre;
                listaTipoZona.Add(exp);
            }

            NotifyOfPropertyChange("ListaTipoZona");
        }
    }
}
