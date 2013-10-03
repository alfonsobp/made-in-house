using Caliburn.Micro;
using MadeInHouse.Models;
using System.Collections.Generic;
using System.Windows;

namespace MadeInHouse.ViewModels
{
    public class MainViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private MyWindowManager win = new MyWindowManager();

        public static List<List<Window>> MinWin = new List<List<Window>>();

        #region Ventas
            public void AbrirListadoCliente()
            {
                win.ShowWindow(new Ventas.ClienteBuscarViewModel());
            }
            public void AbrirNuevoCliente()
            {
                win.ShowWindow(new Ventas.ClienteRegistrarViewModel());
            }

            public void AbrirListadoVenta()
            {
                win.ShowWindow(new Ventas.VentaBuscarViewModel());
            }

            public void AbrirNuevaVenta()
            {
                win.ShowWindow(new Ventas.VentaRegistrarViewModel());
            }
            public void AbrirNuevaVentaCajero()
            {
                win.ShowWindow(new Ventas.VentaCajeroRegistrarViewModel());
            }

            public void AbrirProforma()
            {
                win.ShowWindow(new Ventas.ProformaViewModel { DisplayName = "Proformas" });
            }
            public void AbrirListadoPrecios()
            {
                win.ShowWindow(new Ventas.PreciosBuscarViewModel());
            }
        
            public void AbrirListadoDevoluciones()
            {
                win.ShowWindow(new Ventas.DevolucionesBuscarViewModel());
            }
            public void AbrirRegistrarDevolucion()
            {
                win.ShowWindow(new Ventas.DevolucionesRegistrarViewModel());
            }


            public void AbrirListadoNotaCredito()
            {
                win.ShowWindow(new Ventas.ListadoNotaCreditoViewModel { DisplayName = "Maestro de Notas de Crédito" });
            }

            public void AbrirListadoPromoProducto()
            {
                win.ShowWindow(new Ventas.ListadoPromoProductoViewModel { DisplayName = "Promociones de Productos" });
            }

            public void AbrirListadoPromoServicio()
            {
                win.ShowWindow(new Ventas.ListadoPromoServicioViewModel { DisplayName = "Promociones de Servicios" });
            }

            public void AbrirNuevaPromoServicio()
            {
                win.ShowWindow(new Ventas.RegistrarPromoServicioViewModel { DisplayName = "Nueva Promoción de Servicio" });
            }

        public void AbrirNuevaPromoProducto()
        {
            win.ShowWindow(new Ventas.RegistrarPromoProductoViewModel());
        }

        #endregion Ventas

        #region Almacen

        public void AbrirBuscarGuiaDeRemision()
        {
           
            Almacen.BuscarGuiasRemisionViewModel buscarGuiaView = new Almacen.BuscarGuiasRemisionViewModel();
            win.ShowWindow(buscarGuiaView);
        }

        public void AbrirMantenerGuiaDeRemision()
        {

            Almacen.MantenerGuiaDeRemisionViewModel abrirGuiaView = new Almacen.MantenerGuiaDeRemisionViewModel();  
            win.ShowWindow(abrirGuiaView);     
        }

        public void AbrirBuscarNotas()
        {
            
            win.ShowWindow (new Almacen.BuscarNotasViewModel() );
            

        }

        public void AbrirMantenerNotaDeIngreso()
        {
            
            Almacen.MantenerNotaDeIngresoViewModel abrirNotaIView = new Almacen.MantenerNotaDeIngresoViewModel ();
            win.ShowWindow(abrirNotaIView);
            
        }
        public void AbrirMantenerNotaDeSalida()
        {
            
            Almacen.MantenerNotaDeSalidaViewModel abrirNotaIView = new Almacen.MantenerNotaDeSalidaViewModel ();
            win.ShowWindow(abrirNotaIView);
        }
        public void AbrirAnularDocumentos()
        {
            win.ShowWindow(new Almacen.AnularDocumentosViewModel ());
        }
        public void AbrirMantenimientoAlmacen()
        {

            win.ShowWindow(new Almacen.MantenerAlmacenViewModel());
            
            
        }
        
        public void AbrirBuscarProducto()
        {
            win.ShowWindow(new Almacen.ProductoBuscarViewModel ());
        }
        
        public void AbrirMantenimientoInventarioViewModel()
        {
           // ActivateItem(new Almacen.MantenimientoInventarioViewModel { DisplayName = "Mantenimiento inventario" });
        }
        public void AbrirNuevoProducto()
        {
            win.ShowWindow(new Almacen.MantenerNuevoProductoViewModel ()); 
            
        }
        
        public void AbrirSolicitudAbConsolidar()
        {
            win.ShowWindow(new Almacen.SolicitudAbConsolidarViewModel ()); 
            
        }
        public void AbrirSolicitudAbDetalle()
        {
            win.ShowWindow(new Almacen.SolicitudAbDetalleViewModel()); 
        }
        


        #endregion Almacen

        #region Compras

        public void AbrirBuscarDocumentos() {
          
            win.ShowWindow(new  Compras.BuscarDocumentoViewModel{ DisplayName = "Buscador Documentos de  Pago" });
           
        }

        public void AbrirBuscarOrdenCompra()
        {
          
            Compras.BuscarOrdenCompraViewModel obj = new Compras.BuscarOrdenCompraViewModel{ DisplayName = "Buscador Orden de compra" };
            win.ShowWindow(obj);
         
        }

      
        public void AbrirBuscadorServicio()
        {
           
            Compras.BuscadorServicioViewModel obj = new Compras.BuscadorServicioViewModel { DisplayName = "Buscador de Servicios" };
            win.ShowWindow(obj);
        }

        public void AbrirCatalogoProductoProveedor()
        {

          
            Compras.CatalogoProductoProveedorViewModel obj = new Compras.CatalogoProductoProveedorViewModel { DisplayName = "Mantenimiento Catalogo de Productos" };
            win.ShowWindow(obj);
        }

      

        public void AbrirBuscadorProveedor() {
           
            Compras.BuscadorProveedorViewModel obj = new Compras.BuscadorProveedorViewModel { DisplayName = "Buscar Proveedor" };
            win.ShowWindow(obj);
        }

        public void AbrirNuevoServicio()
        {
     
            Compras.agregarServicioViewModel obj = new Compras.agregarServicioViewModel { DisplayName = "Nuevo Servicio" };
            win.ShowWindow(obj);
        }

  
        public void AbrirGenerarOrdenCompra()
        {
           
            Compras.generarOrdenCompraViewModel obj = new Compras.generarOrdenCompraViewModel { DisplayName = "Orden de compra" };
            win.ShowWindow(obj);
        }
    
        public void AbrirMantenerProveedor() {

          
            Compras.MantenerProveedorViewModel obj = new Compras.MantenerProveedorViewModel { DisplayName = "Proveedor" };
            win.ShowWindow(obj);
        }

        public void AbrirMantenerSolicitudesAdquisicion()
        {
           
            Compras.mantenerSolicitudesAdquisicionViewModel obj = new Compras.mantenerSolicitudesAdquisicionViewModel { DisplayName = "Solicitudes de Adquisicion" };
            win.ShowWindow(obj);
        }

        public void AbrirBuscadorSolicitudesAdquisicion()
        {
         
            Compras.BuscadorSolicitudesAdquisicionViewModel obj = new Compras.BuscadorSolicitudesAdquisicionViewModel { DisplayName = "Buscador de Solicitudes de Adquisicion" };
            win.ShowWindow(obj);
        }
        
        public void AbrirSeleccionDeProveedores() {
            
            Compras.SeleccionDeProveedoresViewModel obj = new Compras.SeleccionDeProveedoresViewModel { DisplayName = "Seleccion de proveedores" };
            win.ShowWindow(obj);
        }

        public void AbrirRegistrarDocumentos()
        {
            
            Compras.registrarDocumentosViewModel obj = new Compras.registrarDocumentosViewModel { DisplayName = "Registrar Documentos" };
            win.ShowWindow(obj);
            
        }

        public void AbrirBuscarCotizacion()
        {
          
            Compras.BuscarCotizacionViewModel obj = new Compras.BuscarCotizacionViewModel{ DisplayName = "Buscador Cotizaciones" };
            win.ShowWindow(obj);
        }

        public void AbrirNuevaCotizacion()
        {
           
            Compras.NuevaCotizacionViewModel obj = new Compras.NuevaCotizacionViewModel { DisplayName = "Cotizacion" };
            win.ShowWindow(obj);
        }


        #endregion Compras

        #region Seguridad

        public void AbrirBuscadorUsuario()
        {
            win.ShowWindow(new Seguridad.BuscadorUsuarioViewModel { DisplayName = "Buscar usuario" });
        }

        public void AbrirMantenerUsuario()
        {
            win.ShowWindow(new Seguridad.MantenerUsuarioViewModel { DisplayName = "Mantener usuario" });

        }
        
        #endregion Seguridad

        #region RRHH

        public void AbrirRegistrarEmpleado()
        {
            win.ShowWindow(new RRHH.RegistrarEmpleadoViewModel { DisplayName = "Registrar Empleado" });
        }

        public void AbrirMantenerEmpleado()
        {
            win.ShowWindow(new RRHH.MantenerEmpleadoViewModel { DisplayName = "Mantenimiento Empleado" });
        }

        public void AbrirControlarAsistenciaEmpleado()
        {
            win.ShowWindow(new RRHH.ControlarAsistenciaEmpleadoViewModel { DisplayName = "Controlar asistencia" });
        }
        
        public void AbrirMantenerRol()
        {
            win.ShowWindow(new RRHH.MantenerRolViewModel { DisplayName="Mantenimiento de Roles"});
        }


        public void AbrirCargarOrganigrama()
        {
            win.ShowWindow(new RRHH.CargarOrganigramaViewModel { DisplayName = "Cargar Organigrama" });
        }

        public void AbrirBuscarEmpleado()
        {
            win.ShowWindow(new RRHH.BuscadorEmpleadoViewModel { DisplayName = "Buscar Empleado" });
        }

        public void AbrirBuscarOrganigrama()
        {
            win.ShowWindow(new RRHH.BuscarOrganigramaViewModel { DisplayName = "Buscar Organigrama" });
        }

        #endregion RRHH

        #region Configuracion
        public void AbrirConfigurarUsuario()
        {
            win.ShowWindow(new Seguridad.ConfigurarUsuarioViewModel());
        }
        public void AbrirMantenerTipoZona()
        {
            win.ShowWindow(new Almacen.BuscarTipoZonaViewModel { DisplayName = "Mantener Tipo Zona" });
        }
        public void AbrirMantenerLineaProducto()
        {
            win.ShowWindow(new Almacen.MantenerLineaProductoViewModel());
        }
        
        public void AbrirMantenerSubLineaProducto()
        {
            win.ShowWindow(new Almacen.MantenerSubLineaProductoViewModel ());
        }
        public void AbrirMantenerTienda()
        {
            win.ShowWindow(new Almacen.MantenerTiendaViewModel());
        }

        #endregion Configuracion

        #region Reportes

        public void AbrirReporteTardanzas()
        {
            win.ShowWindow(new Reportes.reporteTardanzasViewModel { DisplayName = "Reporte de Tardanzas" });
        }
        public void AbrirReporteVentas()
        {
            win.ShowWindow(new Reportes.reporteVentasViewModel { DisplayName = "Reporte de Ventas" });
        }
        public void AbrirReporteServicios()
        {
            win.ShowWindow(new Reportes.reporteServiciosViewModel { DisplayName = "Reporte de Servicios" });
        }
        public void AbrirReporteDevoluciones()
        {
            win.ShowWindow(new Reportes.reporteDevolucionesViewModel { DisplayName = "Reporte de Devoluciones" });
        }
        public void AbrirReporteAcciones()
        {
            win.ShowWindow(new Reportes.reporteAccionesViewModel { DisplayName = "Logs de Acciones de Usuarios" });
        }
        public void AbrirReporteCompra()
        {
            win.ShowWindow(new Reportes.reporteComprasViewModel { DisplayName = "Reporte de Compras" });
        }
        public void AbrirReporteEntradaSalidaProd()
        {
            win.ShowWindow(new Reportes.reporteEnSaProductosViewModel { DisplayName = "Reporte de Ent/Sal de productos" });
        }
        public void AbrirReporteSolicitudes()
        {
            win.ShowWindow(new Reportes.reporteSolicitudesViewModel { DisplayName = "Reporte de Solicitudes de compra" });
        }
        public void AbrirReporteStock()
        {
            win.ShowWindow(new Reportes.reporteStockViewModel { DisplayName = "Reporte de Stock de productos" });
        }

        #endregion
    }
}