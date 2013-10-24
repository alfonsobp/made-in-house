using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel.Composition;
using MadeInHouse.Models;
using MadeInHouse.Models.Seguridad;
using MadeInHouse.Views.Seguridad;
using MadeInHouse.Models.RRHH;
using MadeInHouse.Views.RRHH;


namespace MadeInHouse.ViewModels
{
    public class MainViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private MyWindowManager win = new MyWindowManager();

        public static List<List<Window>> MinWin = new List<List<Window>>();

        private Acceso accesoRol = new Acceso();

        int[] accModulo = new int[7];
        int[,] accVentana = new int[7,50];
        int[,,] accVentanaInt = new int[7,50,10];

        Usuario u;
        int idRol;

        //MODULO ALMACÉN: 0
        #region Almacen
        //Ventana Externa: 0.0
        public void CargarAccesosRol(Usuario u, out Acceso accRol)
        {
            /*
            int k = 0;
            k = DataObjects.Seguridad.AccesoSQL.;
            */
            int idRol;
            accRol = new Acceso();

            //obtener el idRol que pertenece el usuario
            idRol = DataObjects.RrhhSQL.buscarIdRol(u); 
            
            //Obtener la lista de Módulos permitidos:
            DataObjects.Seguridad.AccesoSQL.buscarModulos(idRol, out accModulo);
            for (int i = 0; i < 7; i++)
                accRol.AccModulo[i] = accModulo[i];
            
            //Obtener la lista de Acceso a Ventanas Permitidas:
            DataObjects.Seguridad.AccesoSQL.buscarVentanas(accModulo, out accVentana);
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 30; j++)
                    accRol.AccVentana[i, j] = accVentana[i, j];
    
        }

/*

        //Ventana Externa: 0.0
        public void AbrirMantenimientoAlmacen()
        {
            CargarAccesosRol(u, out accesoRol);
            if ((accesoRol.AccModulo[0] == 1) && (accesoRol.AccVentana[0, 0]) == 1)
                win.ShowWindow(new Almacen.MantenerAlmacenViewModel());
        }
        
        //Ventana Externa: 0.1
        public void AbrirNuevoProducto()
        {
            if ((accesoRol.AccModulo[0] == 1) && (accesoRol.AccVentana[0, 1]) == 1)
                win.ShowWindow(new Almacen.MantenerNuevoProductoViewModel());
        }

        //Ventana Externa: 0.2
        public void AbrirBuscarProducto()
        {
            if ((accesoRol.AccModulo[0] == 1) && (accesoRol.AccVentana[0, 2]) == 1)
                win.ShowWindow(new Almacen.ProductoBuscarViewModel());
        }
        
        //Ventana Externa: 0.3
        public void AbrirBuscarGuiaDeRemision()
        {
            if ((accesoRol.AccModulo[0] == 1) && (accesoRol.AccVentana[0, 3]) == 1)
            {
                Almacen.BuscarGuiasRemisionViewModel buscarGuiaView = new Almacen.BuscarGuiasRemisionViewModel();
                win.ShowWindow(buscarGuiaView);
            }
        }

        //Ventana Externa: 0.4
        public void AbrirMantenerGuiaDeRemision()
        {
            if ((accesoRol.AccModulo[0] == 1) && (accesoRol.AccVentana[0, 4]) == 1)
            {
                Almacen.MantenerGuiaDeRemisionViewModel abrirGuiaView = new Almacen.MantenerGuiaDeRemisionViewModel();
                win.ShowWindow(abrirGuiaView);
            }
        }

        //Ventana Externa: 0.5
        public void AbrirBuscarNotas()
        {
            if ((accesoRol.AccModulo[0] == 1) && (accesoRol.AccVentana[0, 5]) == 1)
                win.ShowWindow(new Almacen.BuscarNotasViewModel());
        }

        //Ventana Externa: 0.6
        public void AbrirMantenerNotaDeIngreso()
        {
            if ((accesoRol.AccModulo[0] == 1) && (accesoRol.AccVentana[0, 6]) == 1)
            {
                Almacen.MantenerNotaDeIngresoViewModel abrirNotaIView = new Almacen.MantenerNotaDeIngresoViewModel();
                win.ShowWindow(abrirNotaIView);
            }
        }

        //Ventana Externa: 0.7
        public void AbrirMantenerNotaDeSalida()
        {
            if ((accesoRol.AccModulo[0] == 1) && (accesoRol.AccVentana[0, 7]) == 1)
            {
                Almacen.MantenerNotaDeSalidaViewModel abrirNotaIView = new Almacen.MantenerNotaDeSalidaViewModel();
                win.ShowWindow(abrirNotaIView);
            }
        }

        //Ventana Externa: 0.8
        public void AbrirBuscarZona()
        {
            if ((accesoRol.AccModulo[0] == 1) && (accesoRol.AccVentana[0, 8]) == 1)
            {
                Almacen.BuscarZonaViewModel buscarZona = new Almacen.BuscarZonaViewModel();
                win.ShowWindow(buscarZona);
            }
        }

        //Ventana Externa: 0.9
        public void AbrirSolicitudAbConsolidar()
        {
            if ((accesoRol.AccModulo[0] == 1) && (accesoRol.AccVentana[0, 9]) == 1)
                win.ShowWindow(new Almacen.SolicitudAbConsolidarViewModel());
        }

        //Ventana Externa: 0.10
        public void AbrirSolicitudAbDetalle()
        {
            if ((accesoRol.AccModulo[0] == 1) && (accesoRol.AccVentana[0, 10]) == 1)
                win.ShowWindow(new Almacen.SolicitudAbDetalleViewModel());
        }

        //Ventana Externa: 0.11
        public void AbrirMovimientos()
        {
            if ((accesoRol.AccModulo[0] == 1) && (accesoRol.AccVentana[0, 11]) == 1)
                win.ShowWindow(new Almacen.ProductoMovimientosViewModel());
        }

        */

        //Ventana Externa: 0.0
        public void AbrirMantenimientoAlmacen()
        {
               win.ShowWindow(new Almacen.MantenerAlmacenViewModel());
        }

        //Ventana Externa: 0.1
        public void AbrirNuevoProducto()
        {
                win.ShowWindow(new Almacen.ProductoMantenerViewModel());
        }

        //Ventana Externa: 0.2
        public void AbrirBuscarProducto()
        {
                win.ShowWindow(new Almacen.ProductoBuscarViewModel());
        }

        //Ventana Externa: 0.3
        public void AbrirBuscarGuiaDeRemision()
        {
                Almacen.BuscarGuiasRemisionViewModel buscarGuiaView = new Almacen.BuscarGuiasRemisionViewModel();
                win.ShowWindow(buscarGuiaView);
        }

        //Ventana Externa: 0.4
        public void AbrirMantenerGuiaDeRemision()
        {
                Almacen.MantenerGuiaDeRemisionViewModel abrirGuiaView = new Almacen.MantenerGuiaDeRemisionViewModel();
                win.ShowWindow(abrirGuiaView);
        }

        //Ventana Externa: 0.5
        public void AbrirBuscarNotas()
        {
                win.ShowWindow(new Almacen.BuscarNotasViewModel());
        }

        //Ventana Externa: 0.6
        public void AbrirMantenerNotaDeIngreso()
        {
                Almacen.MantenerNotaDeIngresoViewModel abrirNotaIView = new Almacen.MantenerNotaDeIngresoViewModel();
                win.ShowWindow(abrirNotaIView);
        }

        //Ventana Externa: 0.7
        public void AbrirMantenerNotaDeSalida()
        {
                Almacen.MantenerNotaDeSalidaViewModel abrirNotaIView = new Almacen.MantenerNotaDeSalidaViewModel();
                win.ShowWindow(abrirNotaIView);
        }

        //Ventana Externa: 0.8
        public void AbrirBuscarZona()
        {
                Almacen.BuscarZonaViewModel buscarZona = new Almacen.BuscarZonaViewModel();
                win.ShowWindow(buscarZona);
        }

        //Ventana Externa: 0.9
        public void AbrirSolicitudAbConsolidar()
        {
                win.ShowWindow(new Almacen.SolicitudAbConsolidarViewModel());
        }

        //Ventana Externa: 0.10
        public void AbrirSolicitudAbDetalle()
        {
                win.ShowWindow(new Almacen.SolicitudAbDetalleViewModel());
        }

        //Ventana Externa: 0.11
        public void AbrirMovimientos()
        {
                win.ShowWindow(new Almacen.ProductoMovimientosViewModel());
        }

        //Ventana Externa: 0.12
        public void AbrirMantenimientoInventarioViewModel()
        {
                // ActivateItem(new Almacen.MantenimientoInventarioViewModel { DisplayName = "Mantenimiento inventario" });
        }

        //Ventana Externa: 0.13
        public void AbrirAnularDocumentos()
        {
                win.ShowWindow(new Almacen.AnularDocumentosViewModel());
        }
                
        #endregion Almacen

        #region Compras

        //Ventana Externa: 1.0
        public void AbrirBuscadorProveedor()
        {
            Compras.BuscadorProveedorViewModel obj = new Compras.BuscadorProveedorViewModel { DisplayName = "Buscar Proveedor" };
            win.ShowWindow(obj);
        }

        //Ventana Externa: 1.1
        public void AbrirMantenerProveedor()
        {
            Compras.MantenerProveedorViewModel obj = new Compras.MantenerProveedorViewModel { DisplayName = "Proveedor" };
            win.ShowWindow(obj);
        }

        //Ventana Externa: 1.2
        public void AbrirCatalogoProductoProveedor()
        {
            Compras.CatalogoProductoProveedorViewModel obj = new Compras.CatalogoProductoProveedorViewModel();
            win.ShowWindow(obj);
        }

        //Ventana Externa: 1.3
        public void AbrirNuevoServicio()
        {
            Compras.agregarServicioViewModel obj = new Compras.agregarServicioViewModel { DisplayName = "Nuevo Servicio" };
            win.ShowWindow(obj);
        }

        //Ventana Externa: 1.4
        public void AbrirBuscadorServicio()
        {
            Compras.BuscadorServicioViewModel obj = new Compras.BuscadorServicioViewModel { DisplayName = "Buscador de Servicios" };
            win.ShowWindow(obj);
        }

        //Ventana Externa: 1.5
        public void AbrirBuscadorSolicitudesAdquisicion()
        {
            Compras.BuscadorSolicitudesAdquisicionViewModel obj = new Compras.BuscadorSolicitudesAdquisicionViewModel { DisplayName = "Buscador de Solicitudes de Adquisicion" };
            win.ShowWindow(obj);
        }

        //Ventana Externa: 1.6
        public void AbrirSeleccionDeProveedores()
        {
            Compras.SeleccionDeProveedoresViewModel obj = new Compras.SeleccionDeProveedoresViewModel { DisplayName = "Seleccion de proveedores" };
            win.ShowWindow(obj);
        }

        //Ventana Externa: 1.7
        public void AbrirGenerarOrdenCompra()
        {
            Compras.generarOrdenCompraViewModel obj = new Compras.generarOrdenCompraViewModel { DisplayName = "Orden de compra" };
            win.ShowWindow(obj);
        }

        //Ventana Externa: 1.8
        public void AbrirBuscarOrdenCompra()
        {
            Compras.BuscarOrdenCompraViewModel obj = new Compras.BuscarOrdenCompraViewModel { DisplayName = "Buscador Orden de compra" };
            win.ShowWindow(obj);
        }

        //Ventana Externa: 1.9
        public void AbrirNuevaCotizacion()
        {
            Compras.NuevaCotizacionViewModel obj = new Compras.NuevaCotizacionViewModel { DisplayName = "Cotizacion" };
            win.ShowWindow(obj);
        }

        //Ventana Externa: 1.10
        public void AbrirBuscarCotizacion()
        {
            Compras.BuscarCotizacionViewModel obj = new Compras.BuscarCotizacionViewModel { DisplayName = "Buscador Cotizaciones" };
            win.ShowWindow(obj);
        }

        //Ventana Externa: 1.11
        public void AbrirRegistrarDocumentos()
        {
            Compras.registrarDocumentosViewModel obj = new Compras.registrarDocumentosViewModel { DisplayName = "Registrar Documentos" };
            win.ShowWindow(obj);
        }

        //Ventana Externa: 1.12
        public void AbrirBuscarDocumentos()
        {
            win.ShowWindow(new Compras.BuscarDocumentoViewModel { DisplayName = "Buscador Documentos de  Pago" });
        }

        //Ventana Externa: 1.13 - ESTA NO ESTÁ IMPLEMENTADA!
        public void AbrirMantenerSolicitudesAdquisicion()
        {
            Compras.mantenerSolicitudesAdquisicionViewModel obj = new Compras.mantenerSolicitudesAdquisicionViewModel { DisplayName = "Solicitudes de Adquisicion" };
            win.ShowWindow(obj);
        }

        #endregion Compras
        
        #region Ventas

        //Ventana Externa: 2.0
        public void AbrirListadoCliente()
        {
            win.ShowWindow(new Ventas.ClienteBuscarViewModel());
        }

        //Ventana Externa: 2.1
        public void AbrirNuevoCliente()
        {
            win.ShowWindow(new Ventas.ClienteRegistrarViewModel());
        }

        //Ventana Externa: 2.2
        public void AbrirListadoVenta()
        {
            win.ShowWindow(new Ventas.VentaBuscarViewModel());
        }

        //Ventana Externa: 2.3
        public void AbrirNuevaVenta()
        {
            win.ShowWindow(new Ventas.VentaRegistrarViewModel());
        }

        //Ventana Externa: 2.4
        public void AbrirNuevaVentaCajero()
        {
            win.ShowWindow(new Ventas.VentaCajeroRegistrarViewModel());
        }

        //Ventana Externa: 2.5
        public void AbrirProforma()
        {
            win.ShowWindow(new Ventas.ProformaViewModel { DisplayName = "Proformas" });
        }

        //Ventana Externa: 2.6
        public void AbrirListadoDevoluciones()
        {
            win.ShowWindow(new Ventas.DevolucionesBuscarViewModel());
        }

        //Ventana Externa: 2.7
        public void AbrirRegistrarDevolucion()
        {
            win.ShowWindow(new Ventas.DevolucionesRegistrarViewModel());
        }

        //Ventana Externa: 2.8
        public void AbrirListadoNotaCredito()
        {
            win.ShowWindow(new Ventas.ListadoNotaCreditoViewModel { DisplayName = "Maestro de Notas de Crédito" });
        }

        //Ventana Externa: 2.9
        public void AbrirListadoPromoProducto()
        {
            win.ShowWindow(new Ventas.PromoProductoBuscarViewModel { DisplayName = "Promociones de Productos" });
        }

        //Ventana Externa: 2.10
        public void AbrirNuevaPromoProducto()
        {
            win.ShowWindow(new Ventas.PromoProductoRegistrarViewModel());
        }

        //Ventana Externa: 2.11
        public void AbrirListadoPromoServicio()
        {
            win.ShowWindow(new Ventas.PromoServicioBuscarViewModel { DisplayName = "Promociones de Servicios" });
        }

        //Ventana Externa: 2.12
        public void AbrirNuevaPromoServicio()
        {
            win.ShowWindow(new Ventas.PromoServicioRegistrarViewModel { DisplayName = "Nueva Promoción de Servicio" });
        }

        //Ventana Externa: 2.13
        public void AbrirListadoPrecios()
        {
            win.ShowWindow(new Ventas.PreciosBuscarViewModel());
        }


        



        #endregion Ventas

        #region RRHH

        //Ventana Externa: 3.0
        public void AbrirRegistrarEmpleado()
        {
            win.ShowWindow(new RRHH.RegistrarEmpleadoViewModel { DisplayName = "Registrar Empleado" });
        }

        //Ventana Externa: 3.1
        public void AbrirMantenerEmpleado()
        {
            win.ShowWindow(new RRHH.MantenerEmpleadoViewModel { DisplayName = "Mantenimiento Empleado" });
        }

        //Ventana Externa: 3.2
        public void AbrirCargarOrganigrama()
        {
            win.ShowWindow(new RRHH.CargarOrganigramaViewModel { DisplayName = "Cargar Organigrama" });
        }

        //Ventana Externa: 3.3
        public void AbrirBuscarEmpleado()
        {
            win.ShowWindow(new RRHH.BuscadorEmpleadoViewModel { DisplayName = "Buscar Empleado" });
        }

        //Ventana Externa: 3.4
        public void AbrirBuscarOrganigrama()
        {
            win.ShowWindow(new RRHH.BuscarOrganigramaViewModel { DisplayName = "Buscar Organigrama" });
        }

        #endregion RRHH
        
        #region Seguridad

        //Ventana Externa: 4.0
        public void AbrirRegistrarUsuario()
        {
            win.ShowWindow(new Seguridad.RegistrarUsuarioViewModel { DisplayName = "Registrar usuario" });
        }

        //Ventana Externa: 4.1
        public void AbrirMantenerUsuario()
        {
            win.ShowWindow(new Seguridad.MantenerUsuarioViewModel { DisplayName = "Mantenimiento de usuario" });

        }

        //Ventana Externa: 4.2
        public void AbrirConfigurarUsuario()
        {
            win.ShowWindow(new Seguridad.ConfigurarUsuarioViewModel());
        }
        
        #endregion Seguridad

        #region Reportes

        //Ventana Externa: 5.0
        public void AbrirReporteTardanzas()
        {
            win.ShowWindow(new Reportes.reporteTardanzasViewModel { DisplayName = "Reporte de Tardanzas" });
        }

        //Ventana Externa: 5.1
        public void AbrirReporteAcciones()
        {
            win.ShowWindow(new Reportes.reporteAccionesViewModel { DisplayName = "Logs de Acciones de Usuarios" });
        }

        //Ventana Externa: 5.2
        public void AbrirReporteVentas()
        {
            win.ShowWindow(new Reportes.reporteVentasViewModel { DisplayName = "Reporte de Ventas" });
        }

        //Ventana Externa: 5.3
        public void AbrirReporteServicios()
        {
            win.ShowWindow(new Reportes.reporteServiciosViewModel { DisplayName = "Reporte de Servicios" });
        }

        //Ventana Externa: 5.4
        public void AbrirReporteDevoluciones()
        {
            win.ShowWindow(new Reportes.reporteDevolucionesViewModel { DisplayName = "Reporte de Devoluciones" });
        }

        //Ventana Externa: 5.5
        public void AbrirReporteCompra()
        {
            win.ShowWindow(new Reportes.reporteComprasViewModel { DisplayName = "Reporte de Compras" });
        }

        //Ventana Externa: 5.6
        public void AbrirReporteEntradaSalidaProd()
        {
            win.ShowWindow(new Reportes.reporteEnSaProductosViewModel { DisplayName = "Reporte de Ent/Sal de productos" });
        }

        //Ventana Externa: 5.7
        public void AbrirReporteSolicitudes()
        {
            win.ShowWindow(new Reportes.reporteSolicitudesViewModel { DisplayName = "Reporte de Solicitudes de compra" });
        }

        //Ventana Externa: 5.8
        public void AbrirReporteStock()
        {
            win.ShowWindow(new Reportes.reporteStockViewModel { DisplayName = "Reporte de Stock de productos" });
        }

        //Ventana Externa: 5.9
        public void AbrirReportePromocionesTop()
        {
            win.ShowWindow(new Reportes.reportePromocionesFrecuentesViewModel { DisplayName = "Reporte de Promociones Top" });
        }

        //Ventana Externa: 5.10
        public void AbrirReporteProductosCanjePuntos()
        {
            win.ShowWindow(new Reportes.reporteProductosCanjePuntosViewModel { DisplayName = "Reporte de Productos canjeados por Puntos" });
        }

        #endregion

        #region Configuracion

        //Ventana Externa: 6.0
        public void AbrirMantenerTienda()
        {
            win.ShowWindow(new Almacen.MantenerTiendaViewModel());
        }

        //Ventana Externa: 6.1
        public void AbrirControlarAsistenciaEmpleado()
        {
            win.ShowWindow(new RRHH.ControlarAsistenciaEmpleadoViewModel { DisplayName = "Controlar asistencia" });
        }

        //Ventana Externa: 6.2
        public void AbrirMantenerTipoZona()
        {
            win.ShowWindow(new Almacen.BuscarTipoZonaViewModel());
        }

        //Ventana Externa: 6.3
        public void AbrirMantenerLineaProducto()
        {
            win.ShowWindow(new Almacen.MantenerLineaProductoViewModel());
        }

        //Ventana Externa: 6.4
        public void AbrirMantenerSubLineaProducto()
        {
            win.ShowWindow(new Almacen.MantenerSubLineaProductoViewModel());
        }

        //Ventana Externa: 6.5
        public void AbrirMantenerModulo()
        {
            win.ShowWindow(new Seguridad.MantenerModuloViewModel());
        }

        //Ventana Externa: 6.6
        public void AbrirMantenerMotivo()
        {
            win.ShowWindow(new Almacen.MantenerMotivoViewModel());
        }
        #endregion Configuracion

    }
}