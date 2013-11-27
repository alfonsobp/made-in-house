using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;
using MadeInHouse.ViewModels.Layouts;
using MadeInHouse.Views.Almacen;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading;
using System.Windows;

namespace MadeInHouse.ViewModels.Almacen
{
	[Export(typeof(SolicitudAbRegistrarViewModel))]
    class SolicitudAbRegistrarViewModel : Screen
    {
        #region constantes

        //Mantenimientos:
        const int DETALLE = 0;
        const int REGISTRO = 1;
        const int EDICION = 2;

        #endregion

        #region constructor

        [ImportingConstructor]
        public SolicitudAbRegistrarViewModel(IWindowManager windowmanager, SolicitudAbListadoViewModel window, int idSolicitud, int idTienda = -1, bool indEdicion = false)
        {
            _windowManager = windowmanager;
            this.idSolicitud = idSolicitud;
            this.window = window;
            TiendaSQL tiendSQL = new TiendaSQL();
            this.idTiendaUsuario = tiendSQL.obtenerTienda(Int32.Parse(Thread.CurrentPrincipal.Identity.Name));
            if (idSolicitud > 0)
            {
                ActualizarTabla();
                if (indEdicion)
                {
                    IndMantenimiento = EDICION;
                    IsReadOnly = false;
                    CanEdit = Visibility.Collapsed;
                    CanDelete = Visibility.Collapsed;
                    CanAtent = Visibility.Collapsed;
                    CanUpload = Visibility.Visible;
                    CanSave = Visibility.Visible;
                }
                else
                {
                    IndMantenimiento = DETALLE;
                    IsReadOnly = true;
                    CanEdit = Visibility.Visible;
                    CanDelete = Visibility.Visible;
                    CanAtent = (this.idTiendaUsuario == 0) ? Visibility.Visible : Visibility.Collapsed;
                    CanUpload = Visibility.Collapsed;
                    CanSave = Visibility.Collapsed;
                }
                this.idTienda = idTienda;
            }
            else
            {
                idTienda = idTiendaUsuario;
                IndMantenimiento = REGISTRO;
                IsReadOnly = false;
                CanEdit = Visibility.Collapsed;
                CanDelete = Visibility.Collapsed;
                CanAtent = Visibility.Collapsed;
                CanUpload = Visibility.Visible;
                CanSave = Visibility.Visible;
                this.idTienda = idTienda;
            }
        }

        #endregion

        #region overrides

        protected override void OnViewLoaded(object view)
        {
            if (idTienda <= 0)
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No se pudo obtener una tienda"));
                TryClose();
            }
        }

        #endregion

        #region atributos

        private readonly IWindowManager _windowManager;

        public int idTienda { get; set; }
        public int idTiendaUsuario { get; set; }
        public int idSolicitud { get; set; }
        public SolicitudAbListadoViewModel window { get; set; }

        private int indMantenimiento;
        public int IndMantenimiento
        {
            get { return indMantenimiento; }
            set
            {
                if (indMantenimiento == value) return;
                indMantenimiento = value;
                NotifyOfPropertyChange(() => IndMantenimiento);
            }
        }

        private bool isReadOnly;
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                if (isReadOnly == value) return;
                isReadOnly = value;
                NotifyOfPropertyChange(() => IsReadOnly);
            }
        }

        private Visibility canEdit;
        public Visibility CanEdit
        {
            get { return canEdit; }
            set
            {
                if (canEdit == value) return;
                canEdit = value;
                NotifyOfPropertyChange(() => CanEdit);
            }
        }

        private Visibility canDelete;
        public Visibility CanDelete
        {
            get { return canDelete; }
            set
            {
                if (canDelete == value) return;
                canDelete = value;
                NotifyOfPropertyChange(() => CanDelete);
            }
        }

        private Visibility canAtent;
        public Visibility CanAtent
        {
            get { return canAtent; }
            set
            {
                if (canAtent == value) return;
                canAtent = value;
                NotifyOfPropertyChange(() => CanAtent);
            }
        }

        private Visibility canUpload;
        public Visibility CanUpload
        {
            get { return canUpload; }
            set
            {
                if (canUpload == value) return;
                canUpload = value;
                NotifyOfPropertyChange(() => CanUpload);
            }
        }

        private Visibility canSave;
        public Visibility CanSave
        {
            get { return canSave; }
            set
            {
                if (canSave == value) return;
                canSave = value;
                NotifyOfPropertyChange(() => CanSave);
            }
        }

        private List<AbastecimientoProducto> _productos;
        public List<AbastecimientoProducto> Productos
        {
            get { return _productos; }
            set
            {
                if (_productos == value) return;
                _productos = value;
                NotifyOfPropertyChange(() => Productos);
            }
        }

        #endregion

        #region metodos

        public void ActualizarTabla()
        {
            AbastecimientoSQL abSQL = new AbastecimientoSQL();
            Productos = abSQL.buscarProductosAbastecimiento(idSolicitud, idTienda);
        }

        public void EditarSolicitud()
        {
            IndMantenimiento = EDICION;
            IsReadOnly = false;
            CanEdit = Visibility.Collapsed;
            CanDelete = Visibility.Collapsed;
            CanAtent = Visibility.Collapsed;
            CanUpload = Visibility.Visible;
            CanSave = Visibility.Visible;
        }

        public void AtenderSolicitud()
        {
            _windowManager.ShowWindow(new SolicitudAbAtenderViewModel(_windowManager, this, idSolicitud));
        }

        public void SeleccionarProductos()
        {
            _windowManager.ShowWindow(new ProductoBuscarViewModel(_windowManager, this));
        }

        public void GuardarSolicitud()
        {
            AbastecimientoModel solModel = new AbastecimientoModel();
            int id;
            int idUsuario = Int32.Parse(Thread.CurrentPrincipal.Identity.Name);
            string message = "Hubo error en el proceso";
            switch (IndMantenimiento)
            {
                case REGISTRO:
                    message = solModel.registrarAbastecimiento(idUsuario, Productos, out id);
                    idSolicitud = id;
                    break;
                case EDICION:
                    message = solModel.editarAbastecimiento(idUsuario, idSolicitud, Productos);
                    break;
                default:
                    break;
            }
            _windowManager.ShowDialog(new AlertViewModel(_windowManager, message));
            IndMantenimiento = DETALLE;
            IsReadOnly = true;
            CanEdit = Visibility.Visible;
            CanDelete = Visibility.Visible;
            CanAtent = (this.idTiendaUsuario == 0) ? Visibility.Visible : Visibility.Collapsed;
            CanUpload = Visibility.Collapsed;
            CanSave = Visibility.Collapsed;
        }

        public void addProducto(AbastecimientoProducto prod)
        {
            List<AbastecimientoProducto> aux = new List<AbastecimientoProducto>();
            if (Productos != null)
            {
                foreach (AbastecimientoProducto item in Productos)
                {
                    if (prod.idProducto != item.idProducto)
                        aux.Add(item);
                }
            }
            aux.Add(prod);
            Productos = aux;
        }

        public void AnularSolicitud()
        {
            AbastecimientoSQL solSQL = new AbastecimientoSQL();
            int idUsuario = 17;
            string message = "No se pudo anular la solicitud";
            if (solSQL.eliminarAbastecimiento(idSolicitud, idUsuario) > 0)
            {
                message = "La operacion fue exitosa";
            }
            _windowManager.ShowDialog(new AlertViewModel(_windowManager, message));
            if (window != null)
                window.ActualizarTabla();
            ((Window) this.GetView()).Owner.Focus();
            TryClose();
        }

        #endregion
    }

#region fumada xD Ocultar columna de datagrid mediante un radiobutton
    /*
    public class DataGridColumnVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SolicitudAbRegistrarViewModel viewModel = (SolicitudAbRegistrarViewModel)value;

            switch (parameter.ToString().ToLower())
            {
                case "data1":
                    return (!String.IsNullOrEmpty(viewModel.Data1)) ? Visibility.Visible : Visibility.Collapsed;
                case "data2":
                    return (!String.IsNullOrEmpty(viewModel.Data2)) ? Visibility.Visible : Visibility.Collapsed;
                case "data3":
                    return (!String.IsNullOrEmpty(viewModel.Data3)) ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Visible;
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !String.IsNullOrEmpty((string)value) ? Visibility.Visible : Visibility.Collapsed;
        }
    }*/

#endregion
}