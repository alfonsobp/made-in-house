using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Caliburn.Micro;
using MadeInHouse.Models;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel.Composition;
using MadeInHouse.Models.Seguridad;
using MadeInHouse.Views.Seguridad;
using MadeInHouse.Models.RRHH;
using MadeInHouse.Views.RRHH;
using MadeInHouse.Views.Reportes;
using System.Diagnostics;
using MadeInHouse.ViewModels.Ventas;

namespace MadeInHouse.ViewModels
{
	[Export(typeof(MainViewModel))]
    public class MainViewModel : Screen
    {
		private readonly IWindowManager _windowManager;

        [ImportingConstructor]
        public MainViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
        }
		
        public static List<List<Window>> MinWin = new List<List<Window>>();

        int[] accVentana = new int[100];

        public void CargarAccesosRol(out int[] ventana)
        {
            ventana = new int[100];

            Usuario u = new Usuario();

            u = DataObjects.Seguridad.UsuarioSQL.buscarUsuarioPorIdUsuario(Int32.Parse(Thread.CurrentPrincipal.Identity.Name));

            DataObjects.Seguridad.AccesoSQL.cargarAccVentana(u.Rol.IdRol, out ventana);
        }

        public void Cerrar()
        {
            _windowManager.ShowWindow(new LoginViewModel(_windowManager));
            this.TryClose();
        }

        //***************************************************************************************************************

        //MODULO ALMACÉN: 1
        #region Almacen

        //Ventana Externa: 1.1
        public void AbrirMantenimientoAlmacen()
        {
            //CargarAccesosRol(u, out accesoRol);
            CargarAccesosRol(out accVentana);
            if (accVentana[1] == 1)
                _windowManager.ShowWindow(new Almacen.MantenerAlmacenViewModel());
        }
        //Ventana Externa: 1.2
        public void AbrirNuevoProducto()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[2] == 1)
                _windowManager.ShowWindow(new Almacen.ProductoMantenerViewModel());
        }

        //Ventana Externa: 1.3
        public void AbrirBuscarProducto()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[3] == 1)
                _windowManager.ShowWindow(new Almacen.ProductoBuscarViewModel());
        }

        //Ventana Externa: 1.4
        public void AbrirBuscarGuiaDeRemision()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[4] == 1)
                _windowManager.ShowWindow(new Almacen.BuscarGuiasRemisionViewModel());
        }

        //Ventana Externa: 1.5
        public void AbrirMantenerGuiaDeRemision()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[5] == 1)
                _windowManager.ShowWindow(new Almacen.MantenerGuiaDeRemisionViewModel());
        }

        //Ventana Externa: 1.6
        public void AbrirBuscarNotas()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[6] == 1)
                _windowManager.ShowWindow(new Almacen.BuscarNotasViewModel());
        }

        //Ventana Externa: 1.7
        public void AbrirMantenerNotaDeIngreso()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[7] == 1)
                _windowManager.ShowWindow(new Almacen.MantenerNotaDeIngresoViewModel());
        }

        //Ventana Externa: 1.8
        public void AbrirMantenerNotaDeSalida()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[8] == 1)
				_windowManager.ShowWindow(new Almacen.MantenerNotaDeSalidaViewModel());
        }

        //Ventana Externa: 1.9
        public void AbrirBuscarZona()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[9] == 1)
                _windowManager.ShowWindow(new Almacen.BuscarZonaViewModel());
        }

        //Ventana Externa: 1.10
        public void AbrirSolicitudAbConsolidar()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[10] == 1)
                _windowManager.ShowWindow(new Almacen.SolicitudAbConsolidarViewModel());
        }

        //Ventana Externa: 1.11
        public void AbrirMovimientos()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[11] == 1)
                _windowManager.ShowWindow(new Almacen.ProductoMovimientosViewModel());
        }
		
		//Ventana Externa: 1.12
        public void AbrirAnularDocumentos()
        {
			CargarAccesosRol(out accVentana);
            if (accVentana[12] == 1)
				_windowManager.ShowWindow(new Almacen.AnularDocumentosViewModel());
        }
		
		public void RegistrarSolicitud()
        {
			CargarAccesosRol(out accVentana);
            if (accVentana[12] == 1)
				_windowManager.ShowWindow(new Almacen.SolicitudAbRegistrarViewModel(_windowManager, null, 0));
        }

        public void ListadoSolicitud()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[12] == 1)
                _windowManager.ShowWindow(new Almacen.SolicitudAbListadoViewModel(_windowManager));
        }

        #endregion Almacen

        //MODULO COMPRAS: 2
        #region Compras

        //Ventana Externa: 2.1
        public void AbrirBuscadorProveedor()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[13] == 1)
                _windowManager.ShowWindow(new Compras.BuscadorProveedorViewModel());
        }

        //Ventana Externa: 2.2
        public void AbrirMantenerProveedor()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[14] == 1)
                _windowManager.ShowWindow(new Compras.MantenerProveedorViewModel());
        }

        //Ventana Externa: 2.3
        public void AbrirCatalogoProductoProveedor()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[15] == 1)
                _windowManager.ShowWindow(new Compras.CatalogoProductoProveedorViewModel());
        }

        //Ventana Externa: 2.4
        public void AbrirNuevoServicio()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[16] == 1)
                _windowManager.ShowWindow(new Compras.agregarServicioViewModel());
        }

        //Ventana Externa: 2.5
        public void AbrirBuscadorServicio()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[17] == 1)
                _windowManager.ShowWindow(new Compras.BuscadorServicioViewModel());
        }

        //Ventana Externa: 2.6
        public void AbrirBuscadorSolicitudesAdquisicion()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[18] == 1)
                _windowManager.ShowWindow(new Compras.BuscadorSolicitudesAdquisicionViewModel {  });
        }

        //Ventana Externa: 2.7
        public void AbrirSeleccionDeProveedores()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[19] == 1)
                _windowManager.ShowWindow(new Compras.SeleccionDeProveedoresViewModel { DisplayName = "Seleccion de proveedores" });
        }

        //Ventana Externa: 2.8
        public void AbrirGenerarOrdenCompra()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[20] == 1)
                _windowManager.ShowWindow(new Compras.generarOrdenCompraViewModel { DisplayName = "Orden de compra" });
        }

        //Ventana Externa: 2.9
        public void AbrirBuscarOrdenCompra()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[21] == 1)
            {
                Compras.BuscarOrdenCompraViewModel obj = new Compras.BuscarOrdenCompraViewModel { DisplayName = "Buscador Orden de compra" };
                _windowManager.ShowWindow(obj);
            }
        }

        //Ventana Externa: 2.10
        public void AbrirNuevaCotizacion()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[18] == 1)
            {
                Compras.NuevaCotizacionViewModel obj = new Compras.NuevaCotizacionViewModel { };
                _windowManager.ShowWindow(obj);
            }
        }

        //Ventana Externa: 2.11
        public void AbrirBuscarCotizacion()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[22] == 1)
            {
                Compras.BuscarCotizacionViewModel obj = new Compras.BuscarCotizacionViewModel { };
                _windowManager.ShowWindow(obj);
            }
        }

        //Ventana Externa: 2.12
        public void AbrirRegistrarDocumentos()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[23] == 1)
            {
                Compras.registrarDocumentosViewModel obj = new Compras.registrarDocumentosViewModel();
                _windowManager.ShowWindow(obj);
            }
        }

        //Ventana Externa: 2.13
        public void AbrirBuscarDocumentos()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[24] == 1)
            {
                Compras.BuscarDocumentoViewModel obj = new Compras.BuscarDocumentoViewModel();
                _windowManager.ShowWindow(obj);
            }
        }



        #endregion Compras

        //MODULO VENTAS: 3
        #region Ventas

        //Ventana Externa: 3.1
        public void AbrirListadoCliente()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[25] == 1)
                _windowManager.ShowWindow(new Ventas.ClienteBuscarViewModel());
        }

        //Ventana Externa: 3.2
        public void AbrirNuevoCliente()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[26] == 1)
                _windowManager.ShowWindow(new Ventas.ClienteRegistrarViewModel());
        }

        //Ventana Externa: 3.3
        public void AbrirListadoVenta()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[27] == 1)
                _windowManager.ShowWindow(new Ventas.VentaBuscarViewModel());
        }

        //Ventana Externa: 3.4
        public void AbrirNuevaVenta()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[28] == 1)
                _windowManager.ShowWindow(new Ventas.VentaRegistrarViewModel());
        }

        //Ventana Externa: 3.5
        public void AbrirNuevaVentaCajero()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[29] == 1)
                _windowManager.ShowWindow(new Ventas.VentaCajeroRegistrarViewModel());
        }

        //Ventana Externa: 3.6
        public void AbrirProforma()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[30] == 1)
                _windowManager.ShowWindow(new Ventas.ProformaViewModel { DisplayName = "Proformas" });
        }

        //Ventana Externa: 3.7
        public void AbrirListadoDevoluciones()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[31] == 1)
                _windowManager.ShowWindow(new DevolucionesBuscarViewModel(_windowManager));
        }

        //Ventana Externa: 3.8
        public void AbrirRegistrarDevolucion()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[32] == 1)
                _windowManager.ShowWindow(new Ventas.DevolucionesRegistrarViewModel(_windowManager, null, 0));
        }

        //Ventana Externa: 3.9
        public void AbrirListadoNotaCredito()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[33] == 1)
                _windowManager.ShowWindow(new Ventas.ListadoNotaCreditoViewModel { DisplayName = "Maestro de Notas de Crédito" });
        }

        //Ventana Externa: 3.10
        public void AbrirListadoPromoProducto()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[34] == 1)
                _windowManager.ShowWindow(new Ventas.PromoProductoBuscarViewModel { DisplayName = "Promociones de Productos" });
        }

        //Ventana Externa: 3.11
        public void AbrirNuevaPromoProducto()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[35] == 1)
                _windowManager.ShowWindow(new Ventas.PromoProductoRegistrarViewModel());
        }

        //Ventana Externa: 3.12
        public void AbrirListadoPromoServicio()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[36] == 1)
                _windowManager.ShowWindow(new Ventas.PromoServicioBuscarViewModel { DisplayName = "Promociones de Servicios" });
        }

        //Ventana Externa: 3.13
        public void AbrirNuevaPromoServicio()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[37] == 1)
                _windowManager.ShowWindow(new Ventas.PromoServicioRegistrarViewModel { DisplayName = "Nueva Promoción de Servicio" });
        }

        //Ventana Externa: 3.13
        public void AbrirListadoPrecios()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[38] == 1)
                _windowManager.ShowWindow(new Ventas.PreciosBuscarViewModel());
        }


        #endregion Ventas

        //MODULO RRHH: 4
        #region RRHH

        //Ventana Externa: 4.1
        public void AbrirRegistrarEmpleado()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[39] == 1)
                _windowManager.ShowWindow(new RRHH.RegistrarEmpleadoViewModel { DisplayName = "Registrar Empleado" });
        }

        //Ventana Externa: 4.2
        public void AbrirMantenerEmpleado()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[40] == 1)
                _windowManager.ShowWindow(new RRHH.MantenerEmpleadoViewModel { DisplayName = "Mantenimiento Empleado" });
        }

        //Ventana Externa: 4.3
        public void AbrirCargarOrganigrama()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[41] == 1)
                _windowManager.ShowWindow(new RRHH.CargarOrganigramaViewModel { DisplayName = "Cargar Organigrama" });
        }

        #endregion RRHH

        //MODULO CLIMA_LABORAL: 5
        #region Clima_Laboral
        //Ventana Externa: 5.1
        public void AbrirBuscarEmpleado()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[42] == 1)
                _windowManager.ShowWindow(new RRHH.BuscadorEmpleadoViewModel { DisplayName = "Buscar Empleado" });
        }

        //Ventana Externa: 5.2
        public void AbrirBuscarOrganigrama()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[43] == 1)
                _windowManager.ShowWindow(new RRHH.BuscarOrganigramaViewModel { DisplayName = "Buscar Organigrama" });
        }
        #endregion Clima_Laboral

        //MODULO SEGURIDAD: 6
        #region Seguridad

        //Ventana Externa: 6.1
        public void AbrirRegistrarUsuario()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[44] == 1)
                _windowManager.ShowWindow(new Seguridad.RegistrarUsuarioViewModel { });
        }

        //Ventana Externa: 6.2
        public void AbrirMantenerUsuario()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[45] == 1)
                _windowManager.ShowWindow(new Seguridad.MantenerUsuarioViewModel { DisplayName = "Mantenimiento de usuario" });

        }

        //Ventana Externa: 6.3
        public void AbrirMantenerRol()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[46] == 1)
                _windowManager.ShowWindow(new RRHH.MantenerRolViewModel { DisplayName = "Mantenimiento Rol" });
        }

        //Ventana Externa: 6.4
        public void AsignarAccesos()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[47] == 1)
                _windowManager.ShowWindow(new Seguridad.AsignarAccesosViewModel { });
        }

        //Ventana Externa: 6.5
        public void AbrirConfigurarUsuario()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[48] == 1)
                _windowManager.ShowWindow(new Seguridad.ConfigurarUsuarioViewModel());
        }



        #endregion Seguridad

        //MODULO REPORTES: 7
        #region Reportes

        //Ventana Externa: 7.1
        public void AbrirReporteTardanzas()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[49] == 1)
                _windowManager.ShowWindow(new Reportes.reporteTardanzasViewModel { DisplayName = "Reporte de Tardanzas" });
        }

        //Ventana Externa: 7.2
        public void AbrirReporteAcciones()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[50] == 1)
                _windowManager.ShowWindow(new Reportes.reporteAccionesViewModel { DisplayName = "Logs de Acciones de Usuarios" });
        }

        //Ventana Externa: 7.3
        public void AbrirReporteVentas()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[51] == 1)
                _windowManager.ShowWindow(new Reportes.reporteVentasViewModel { DisplayName = "Reporte de Ventas" });
        }

        //Ventana Externa: 7.4
        public void AbrirReporteServicios()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[52] == 1)
                _windowManager.ShowWindow(new Reportes.reporteServiciosViewModel { DisplayName = "Reporte de Servicios" });
        }

        //Ventana Externa: 7.5
        public void AbrirReporteDevoluciones()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[53] == 1)
                _windowManager.ShowWindow(new Reportes.reporteDevolucionesViewModel { DisplayName = "Reporte de Devoluciones" });
        }

        //Ventana Externa: 7.6
        public void AbrirReporteCompra()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[54] == 1)
                _windowManager.ShowWindow(new Reportes.reporteComprasViewModel { DisplayName = "Reporte de Compras" });
        }

        //Ventana Externa: 7.7
        public void AbrirReporteEntradaSalidaProd()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[55] == 1)
                _windowManager.ShowWindow(new Reportes.reporteEnSaProductosViewModel { DisplayName = "Reporte de Ent/Sal de productos" });
        }

        //Ventana Externa: 7.8
        public void AbrirReporteSolicitudes()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[56] == 1)
                _windowManager.ShowWindow(new Reportes.reporteSolicitudesViewModel { DisplayName = "Reporte de Solicitudes de compra" });
        }

        //Ventana Externa: 7.9
        public void AbrirReporteStock()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[57] == 1)
                _windowManager.ShowWindow(new Reportes.reporteStockViewModel { DisplayName = "Reporte de Stock de productos" });
        }

        //Ventana Externa: 7.10
        public void AbrirReportePromocionesTop()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[58] == 1)
                _windowManager.ShowWindow(new Reportes.reportePromocionesFrecuentesViewModel { DisplayName = "Reporte de Promociones Top" });
        }

        //Ventana Externa: 7.11
        public void AbrirReporteProductosCanjePuntos()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[59] == 1)
                _windowManager.ShowWindow(new Reportes.reporteProductosCanjePuntosViewModel { DisplayName = "Reporte de Productos canjeados por Puntos" });
        }


        #endregion

        //MODULO CONFIGURACION: 8
        #region Configuracion

        //Ventana Externa: 8.1
        public void AbrirMantenerTienda()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[60] == 1)
                _windowManager.ShowWindow(new Almacen.MantenerTiendaViewModel());
        }

        //Ventana Externa: 8.2
        public void AbrirControlarAsistenciaEmpleado()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[61] == 1)
                _windowManager.ShowWindow(new RRHH.ControlarAsistenciaEmpleadoViewModel { DisplayName = "Controlar asistencia" });
        }

        //Ventana Externa: 8.3
        public void AbrirMantenerTipoZona()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[62] == 1)
                _windowManager.ShowWindow(new Almacen.BuscarTipoZonaViewModel());
        }

        //Ventana Externa: 8.4
        public void AbrirMantenerLineaProducto()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[63] == 1)
                _windowManager.ShowWindow(new Almacen.MantenerLineaProductoViewModel());
        }

        //Ventana Externa: 8.5
        public void AbrirMantenerSubLineaProducto()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[64] == 1)
                _windowManager.ShowWindow(new Almacen.MantenerSubLineaProductoViewModel());
        }

        //Ventana Externa: 8.6
        public void AbrirMantenerMotivo()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[65] == 1)
                _windowManager.ShowWindow(new Almacen.MantenerMotivoViewModel());
        }

        //Ventana Externa: 8.7
        public void AbrirMantenerUnidadMedida()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[66] == 1)
                _windowManager.ShowWindow(new Almacen.MantenerUnidadMedidaViewModel());
        }

        public void AbrirBuscarTienda()
        {
            //CargarAccesosRol(out accVentana);
            //if (accVentana[67] == 1)
                _windowManager.ShowWindow(new Almacen.BuscarTiendaViewModel());
        }

        #endregion Configuracion


        //Ventana COMPRAS - ESTA NO ESTÁ IMPLEMENTADA!
        public void AbrirMantenerSolicitudesAdquisicion()
        {
            CargarAccesosRol(out accVentana);
            if (accVentana[25] == 1)
            {
                //Compras.mantenerSolicitudesAdquisicionViewModel obj = new Compras.mantenerSolicitudesAdquisicionViewModel {};
                //win.ShowWindow(obj);
            }
        }




        //***************************************************************************************************************
        /*
        //MODULO ALMACÉN: 1
        #region Almacen
        //Ventana Externa: 1.1
        public void AbrirMantenimientoAlmacen()
        {
            win.ShowWindow(new Almacen.MantenerAlmacenViewModel());
        }

        //Ventana Externa: 1.2
        public void AbrirNuevoProducto()
        {
            win.ShowWindow(new Almacen.ProductoMantenerViewModel());
        }

        //Ventana Externa: 1.3
        public void AbrirBuscarProducto()
        {
            win.ShowWindow(new Almacen.ProductoBuscarViewModel());
        }

        //Ventana Externa: 1.4
        public void AbrirBuscarGuiaDeRemision()
        {
            Almacen.BuscarGuiasRemisionViewModel buscarGuiaView = new Almacen.BuscarGuiasRemisionViewModel();
            win.ShowWindow(buscarGuiaView);
        }

        //Ventana Externa: 1.5
        public void AbrirMantenerGuiaDeRemision()
        {
            Almacen.MantenerGuiaDeRemisionViewModel abrirGuiaView = new Almacen.MantenerGuiaDeRemisionViewModel();
            win.ShowWindow(abrirGuiaView);
        }

        //Ventana Externa: 1.6
        public void AbrirBuscarNotas()
        {
            win.ShowWindow(new Almacen.BuscarNotasViewModel());
        }

        //Ventana Externa: 1.7
        public void AbrirMantenerNotaDeIngreso()
        {
            Almacen.MantenerNotaDeIngresoViewModel abrirNotaIView = new Almacen.MantenerNotaDeIngresoViewModel();
            win.ShowWindow(abrirNotaIView);
        }

        //Ventana Externa: 1.8
        public void AbrirMantenerNotaDeSalida()
        {
            Almacen.MantenerNotaDeSalidaViewModel abrirNotaIView = new Almacen.MantenerNotaDeSalidaViewModel();
            win.ShowWindow(abrirNotaIView);
        }

        //Ventana Externa: 1.9
        public void AbrirBuscarZona()
        {
            Almacen.BuscarZonaViewModel buscarZona = new Almacen.BuscarZonaViewModel();
            win.ShowWindow(buscarZona);
        }

        //Ventana Externa: 1.10
        public void AbrirSolicitudAbConsolidar()
        {
            win.ShowWindow(new Almacen.SolicitudAbConsolidarViewModel());
        }

        //Ventana Externa: 1.11
        public void AbrirSolicitudAbDetalle()
        {
            win.ShowWindow(new Almacen.SolicitudAbAtenderViewModel());
        }

        //Ventana Externa: 1.12
        public void AbrirMovimientos()
        {
            win.ShowWindow(new Almacen.ProductoMovimientosViewModel());
        }

        //Ventana Externa: 1.13
        public void AbrirMantenimientoInventarioViewModel()
        {
            // ActivateItem(new Almacen.MantenimientoInventarioViewModel { DisplayName = "Mantenimiento inventario" });
        }

        //Ventana Externa: 1.14
        public void AbrirAnularDocumentos()
        {
            win.ShowWindow(new Almacen.AnularDocumentosViewModel());
        }

        #endregion Almacen

        //MODULO COMPRAS: 2
        #region Compras

        //Ventana Externa: 2.1
        public void AbrirBuscadorProveedor()
        {
            Compras.BuscadorProveedorViewModel obj = new Compras.BuscadorProveedorViewModel();
            win.ShowWindow(obj);
        }

        //Ventana Externa: 2.2
        public void AbrirMantenerProveedor()
        {
            Compras.MantenerProveedorViewModel obj = new Compras.MantenerProveedorViewModel();
            win.ShowWindow(obj);
        }

        //Ventana Externa: 2.3
        public void AbrirCatalogoProductoProveedor()
        {
            Compras.CatalogoProductoProveedorViewModel obj = new Compras.CatalogoProductoProveedorViewModel();
            win.ShowWindow(obj);
        }

        //Ventana Externa: 2.4
        public void AbrirNuevoServicio()
        {
            Compras.agregarServicioViewModel obj = new Compras.agregarServicioViewModel();
            win.ShowWindow(obj);
        }

        //Ventana Externa: 2.5
        public void AbrirBuscadorServicio()
        {
            Compras.BuscadorServicioViewModel obj = new Compras.BuscadorServicioViewModel();
            win.ShowWindow(obj);
        }

        //Ventana Externa: 2.6
        public void AbrirBuscadorSolicitudesAdquisicion()
        {
            Compras.BuscadorSolicitudesAdquisicionViewModel obj = new Compras.BuscadorSolicitudesAdquisicionViewModel { DisplayName = "Buscador de Solicitudes de Adquisicion" };
            win.ShowWindow(obj);
        }

        //Ventana Externa: 2.7
        public void AbrirSeleccionDeProveedores()
        {
            Compras.SeleccionDeProveedoresViewModel obj = new Compras.SeleccionDeProveedoresViewModel { DisplayName = "Seleccion de proveedores" };
            win.ShowWindow(obj);
        }

        //Ventana Externa: 2.9
        public void AbrirBuscarOrdenCompra()
        {
            Compras.BuscarOrdenCompraViewModel obj = new Compras.BuscarOrdenCompraViewModel();
            win.ShowWindow(obj);
        }

        //Ventana Externa: 2.10
        public void AbrirNuevaCotizacion()
        {
            Compras.NuevaCotizacionViewModel obj = new Compras.NuevaCotizacionViewModel();
            win.ShowWindow(obj);
        }
                    win.ShowWindow(obj);
                }

        //Ventana Externa: 2.10
        public void AbrirNuevaCotizacion()
        {
            Compras.NuevaCotizacionViewModel obj = new Compras.NuevaCotizacionViewModel { DisplayName = "Cotizacion" };
            win.ShowWindow(obj);
        }

        //Ventana Externa: 2.12
        public void AbrirRegistrarDocumentos()
        {
            Compras.registrarDocumentosViewModel obj = new Compras.registrarDocumentosViewModel();
            win.ShowWindow(obj);
        }

        //Ventana Externa: 2.13
        public void AbrirBuscarDocumentos()
        {
            win.ShowWindow(new Compras.BuscarDocumentoViewModel());
        }

        //Ventana Externa: 2.13
        public void AbrirBuscarDocumentos()
        {
            win.ShowWindow(new Compras.BuscarDocumentoViewModel { DisplayName = "Buscador Documentos de  Pago" });
        }

        //Ventana Externa: 2.14 - ESTA NO ESTÁ IMPLEMENTADA!
        public void AbrirMantenerSolicitudesAdquisicion()
        {
            Compras.mantenerSolicitudesAdquisicionViewModel obj = new Compras.mantenerSolicitudesAdquisicionViewModel { DisplayName = "Solicitudes de Adquisicion" };
            win.ShowWindow(obj);
        }

        #endregion Compras

        //MODULO VENTAS: 3
        #region Ventas

        //Ventana Externa: 3.1
        public void AbrirListadoCliente()
        {
            win.ShowWindow(new Ventas.ClienteBuscarViewModel());
        }

        //Ventana Externa: 3.2
        public void AbrirNuevoCliente()
        {
            win.ShowWindow(new Ventas.ClienteRegistrarViewModel());
        }

        //Ventana Externa: 3.3
        public void AbrirListadoVenta()
        {
            win.ShowWindow(new Ventas.VentaBuscarViewModel());
        }

        //Ventana Externa: 3.4
        public void AbrirNuevaVenta()
        {
            win.ShowWindow(new Ventas.VentaRegistrarViewModel());
        }

        //Ventana Externa: 3.5
        public void AbrirNuevaVentaCajero()
        {
            win.ShowWindow(new Ventas.VentaCajeroRegistrarViewModel());
        }

        //Ventana Externa: 3.6
        public void AbrirProforma()
        {
            win.ShowWindow(new Ventas.ProformaViewModel { DisplayName = "Proformas" });
        }

        //Ventana Externa: 3.7
        public void AbrirListadoDevoluciones()
        {
            win.ShowWindow(new Ventas.DevolucionesBuscarViewModel());
        }

        //Ventana Externa: 3.8
        public void AbrirRegistrarDevolucion()
        {
            win.ShowWindow(new Ventas.DevolucionesRegistrarViewModel());
        }

        //Ventana Externa: 3.9
        public void AbrirListadoNotaCredito()
        {
            win.ShowWindow(new Ventas.ListadoNotaCreditoViewModel { DisplayName = "Maestro de Notas de Crédito" });
        }

        //Ventana Externa: 3.10
        public void AbrirListadoPromoProducto()
        {
            win.ShowWindow(new Ventas.PromoProductoBuscarViewModel { DisplayName = "Promociones de Productos" });
        }

        //Ventana Externa: 3.11
        public void AbrirNuevaPromoProducto()
        {
            win.ShowWindow(new Ventas.PromoProductoRegistrarViewModel());
        }

        //Ventana Externa: 3.12
        public void AbrirListadoPromoServicio()
        {
            win.ShowWindow(new Ventas.PromoServicioBuscarViewModel { DisplayName = "Promociones de Servicios" });
        }

        //Ventana Externa: 3.13
        public void AbrirNuevaPromoServicio()
        {
            win.ShowWindow(new Ventas.PromoServicioRegistrarViewModel { DisplayName = "Nueva Promoción de Servicio" });
        }

        //Ventana Externa: 3.13
        public void AbrirListadoPrecios()
        {
            win.ShowWindow(new Ventas.PreciosBuscarViewModel());
        }


        #endregion Ventas

        //MODULO RRHH: 4
        #region RRHH

        //Ventana Externa: 4.1
        public void AbrirRegistrarEmpleado()
        {
            win.ShowWindow(new RRHH.RegistrarEmpleadoViewModel { DisplayName = "Registrar Empleado" });
        }

        //Ventana Externa: 4.2
        public void AbrirMantenerEmpleado()
        {
            win.ShowWindow(new RRHH.MantenerEmpleadoViewModel { DisplayName = "Mantenimiento Empleado" });
        }

        //Ventana Externa: 4.3
        public void AbrirCargarOrganigrama()
        {
            win.ShowWindow(new RRHH.CargarOrganigramaViewModel { DisplayName = "Cargar Organigrama" });
        }

        #endregion RRHH

        //MODULO CLIMA_LABORAL: 5
        #region Clima_Laboral
        //Ventana Externa: 5.1
        public void AbrirBuscarEmpleado()
        {
            win.ShowWindow(new RRHH.BuscadorEmpleadoViewModel { DisplayName = "Buscar Empleado" });
        }

        //Ventana Externa: 5.2
        public void AbrirBuscarOrganigrama()
        {
            win.ShowWindow(new RRHH.BuscarOrganigramaViewModel { DisplayName = "Buscar Organigrama" });
        }
        #endregion Clima_Laboral

        //MODULO SEGURIDAD: 6
        #region Seguridad

        //Ventana Externa: 6.1
        public void AbrirRegistrarUsuario()
        {
            win.ShowWindow(new Seguridad.RegistrarUsuarioViewModel { });
        }

        //Ventana Externa: 6.2
        public void AbrirMantenerUsuario()
        {
            win.ShowWindow(new Seguridad.MantenerUsuarioViewModel { DisplayName = "Mantenimiento de usuario" });

        }

        //Ventana Externa: 6.3
        public void AbrirMantenerRol()
        {
            win.ShowWindow(new RRHH.MantenerRolViewModel { DisplayName = "Mantenimiento Rol" });
        }

        //Ventana Externa: 6.4
        public void AsignarAccesos()
        {
            win.ShowWindow(new Seguridad.AsignarAccesosViewModel { });
        }

        //Ventana Externa: 6.5
        public void AbrirConfigurarUsuario()
        {
            win.ShowWindow(new Seguridad.ConfigurarUsuarioViewModel());
        }



        #endregion Seguridad

        //MODULO REPORTES: 7
        #region Reportes

        //Ventana Externa: 7.1
        public void AbrirReporteTardanzas()
        {
            win.ShowWindow(new Reportes.reporteTardanzasViewModel { DisplayName = "Reporte de Tardanzas" });
        }

        //Ventana Externa: 7.2
        public void AbrirReporteAcciones()
        {
            win.ShowWindow(new Reportes.reporteAccionesViewModel { DisplayName = "Logs de Acciones de Usuarios" });
        }

        //Ventana Externa: 7.3
        public void AbrirReporteVentas()
        {
            win.ShowWindow(new Reportes.reporteVentasViewModel { DisplayName = "Reporte de Ventas" });
        }

        //Ventana Externa: 7.4
        public void AbrirReporteServicios()
        {
            win.ShowWindow(new Reportes.reporteServiciosViewModel { DisplayName = "Reporte de Servicios" });
        }

        //Ventana Externa: 7.5
        public void AbrirReporteDevoluciones()
        {
            win.ShowWindow(new Reportes.reporteDevolucionesViewModel { DisplayName = "Reporte de Devoluciones" });
        }

        //Ventana Externa: 7.6
        public void AbrirReporteCompra()
        {
            win.ShowWindow(new Reportes.reporteComprasViewModel { DisplayName = "Reporte de Compras" });
        }

        //Ventana Externa: 7.7
        public void AbrirReporteEntradaSalidaProd()
        {
            win.ShowWindow(new Reportes.reporteEnSaProductosViewModel { DisplayName = "Reporte de Ent/Sal de productos" });
        }

        //Ventana Externa: 7.8
        public void AbrirReporteSolicitudes()
        {
            win.ShowWindow(new Reportes.reporteSolicitudesViewModel { DisplayName = "Reporte de Solicitudes de compra" });
        }

        //Ventana Externa: 7.9
        public void AbrirReporteStock()
        {
            win.ShowWindow(new Reportes.reporteStockViewModel { DisplayName = "Reporte de Stock de productos" });
        }

        //Ventana Externa: 7.10
        public void AbrirReportePromocionesTop()
        {
            win.ShowWindow(new Reportes.reportePromocionesFrecuentesViewModel { DisplayName = "Reporte de Promociones Top" });
        }

        //Ventana Externa: 7.11
        public void AbrirReporteProductosCanjePuntos()
        {
            win.ShowWindow(new Reportes.reporteProductosCanjePuntosViewModel { DisplayName = "Reporte de Productos canjeados por Puntos" });
        }

        #endregion*/

        
    }
}