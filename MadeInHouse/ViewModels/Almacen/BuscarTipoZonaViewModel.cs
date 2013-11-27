using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;
using System.ComponentModel.Composition;
using MadeInHouse.ViewModels.Layouts;

namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(BuscarTipoZonaViewModel))]
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

        #region constructores

        [ImportingConstructor]
        public BuscarTipoZonaViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
            gateway = new DataObjects.Almacen.TipoZonaSQL();
            gw = new DataObjects.Almacen.ColorSQL();

            ObservableCollection<TipoZona> listaTipoZonaExt = new ObservableCollection<TipoZona>();
            listaTipoZonaExt = gateway.BuscarZona();

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

        }

        #endregion

        private readonly IWindowManager _windowManager;
        private DataObjects.Almacen.TipoZonaSQL gateway;

        private DataObjects.Almacen.ColorSQL gw;


        private ObservableCollection<ExtendedZona> listaTipoZona;
        private ExtendedZona tipoZonaSeleccionada = null;

        public ExtendedZona TipoZonaSeleccionada
        {
            get { return tipoZonaSeleccionada; }
            set { tipoZonaSeleccionada = value; }
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
            _windowManager.ShowWindow(new MantenerTipoZonaViewModel());
        }

        public void Eliminar()
        {
            ExtendedZona tz;
            tz = tipoZonaSeleccionada;
            if (tz != null)
            {
                int a;
                a = gateway.eliminarTipoZona(tz as TipoZona);
                if (a > 0) listaTipoZona.Remove(tz);
                else {
                    _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No se pudo borrar borrar el tipo de zona porque esta siendo usado"));
                }
            }
            else {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No se ha seleccionado ninguna Zona"));
            }
        }


        public void SelectedItemChanged(object sender)
        {
            tipoZonaSeleccionada = ((sender as DataGrid).SelectedItem as ExtendedZona);

        }
        
        public void EditarTipoZona()
        {
            if (tipoZonaSeleccionada == null) {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No se ha seleccionado ninguna zona a modificar"));
                return; 
            }
            _windowManager.ShowWindow(new MantenerTipoZonaViewModel(_windowManager, this, 1));
        }
        string codigo;

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value;
            NotifyOfPropertyChange("Codigo");
            }
        }
        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value;
            NotifyOfPropertyChange("Descripcion");
            }
        }

        public void BuscarTipoZona()
        {
            Validacion.Evaluador eval = new Validacion.Evaluador();
            gateway = new DataObjects.Almacen.TipoZonaSQL();
            gw = new DataObjects.Almacen.ColorSQL();
            if (string.IsNullOrEmpty(codigo)) codigo="-1";

            ObservableCollection<TipoZona> listaTipoZonaExt = new ObservableCollection<TipoZona>();
            if (!eval.esNumeroEntero(codigo)) {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Ingrese en codigo un numero entero valido"));
                return;
            }
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
            if (listaTipoZona.Count() == 0)
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "La busqueda no retorno items. Intente con nuevos parametro"));
            NotifyOfPropertyChange("ListaTipoZona");
        }

        

    }
}
