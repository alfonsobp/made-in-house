using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using MadeInHouse.Models;

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
            win.ShowWindow(new Ventas.ListadoVentaViewModel());
        }

        public void AbrirNuevaVenta()
        {
            win.ShowWindow(new Ventas.RegistrarVentaViewModel { DisplayName = "Nueva Venta" });
        }

        public void AbrirProforma()
        {
            win.ShowWindow(new Ventas.ProformaViewModel { DisplayName = "Proformas" });
        }

        
        public void AbrirListadoDevoluciones()
        {
            win.ShowWindow(new Ventas.DevolucionesBuscarViewModel());
        }
        public void AbrirRegistrarDevolucion()
        {
            win.ShowWindow(new Ventas.RegistrarDevolucionesViewModel());
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
            //WindowManager win = new WindowManager();
            Almacen.BuscarGuiasRemisionViewModel buscarGuiaView = new Almacen.BuscarGuiasRemisionViewModel { DisplayName = "Busqueda de guias de remisión" };
            win.ShowWindow(buscarGuiaView);



        }

        public void AbrirMantenerGuiaDeRemision()
        {
            WindowManager win = new WindowManager();
            Almacen.MantenerGuiaDeRemisionViewModel abrirGuiaView  = new Almacen.MantenerGuiaDeRemisionViewModel { DisplayName = "Mantenimiento de guias de remisión" };
            //abrirGuiaView. = true;
            win.ShowWindow(abrirGuiaView);
            
            
            
        }

        public void AbrirBuscarNotas()
        {
            WindowManager win = new WindowManager();
            Almacen.BuscarNotasViewModel buscarNotasView = new Almacen.BuscarNotasViewModel { DisplayName = "Buscqueda de notas de ingreso / salida" };
            win.ShowWindow(buscarNotasView);

        }

        public void AbrirMantenerNotaDeIngreso()
        {
            WindowManager win = new WindowManager();
            Almacen.MantenerNotaDeIngresoViewModel abrirNotaIView = new Almacen.MantenerNotaDeIngresoViewModel { DisplayName = "Mantenimiento de notas de ingreso" };
            win.ShowWindow(abrirNotaIView);
            
        }
        public void AbrirMantenerNotaDeSalida()
        {
            WindowManager win = new WindowManager();
            Almacen.MantenerNotaDeSalidaViewModel abrirNotaIView = new Almacen.MantenerNotaDeSalidaViewModel { DisplayName = "Mantenimiento de notas de salida" };
            win.ShowWindow(abrirNotaIView);
        }
        public void AbrirAnularDocumentos()
        {
            ActivateItem(new Almacen.AnularDocumentosViewModel { DisplayName = "Anular documentos" });
        }
        public void AbrirMantenimientoAlmacen()
        {

            win.ShowWindow(new Almacen.MantenerAlmacenViewModel());
            
            
        }
        
        public void AbrirBuscarProducto()
        {
            win.ShowWindow(new Almacen.BuscadorProductoViewModel { DisplayName = "Buscar Producto" });
        }
        
        public void AbrirMantenimientoInventarioViewModel()
        {
           // ActivateItem(new Almacen.MantenimientoInventarioViewModel { DisplayName = "Mantenimiento inventario" });
        }
        public void AbrirNuevoProducto()
        {
            win.ShowWindow(new Almacen.MantenerNuevoProductoViewModel { DisplayName = "Nuevo Producto" }); 
            
        }
        
        public void AbrirSolicitudAbConsolidar()
        {
            win.ShowWindow(new Almacen.SolicitudAbConsolidarViewModel { DisplayName = "Consolidar Solicitudes" }); 
            
        }
        public void AbrirSolicitudAbDetalle()
        {
            win.ShowWindow(new Almacen.SolicitudAbDetalleViewModel { DisplayName = "Detalle Solicitud" }); 
        }
        


        #endregion Almacen

        #region Compras

        public void AbrirBuscarDocumentos() {
            WindowManager winC = new WindowManager();
            winC.ShowWindow(new  Compras.BuscarDocumentoViewModel{ DisplayName = "Buscador Documentos de  Pago" });
           
        }

        public void AbrirBuscarOrdenCompra()
        {
            WindowManager winC = new WindowManager();
            Compras.BuscarOrdenCompraViewModel obj = new Compras.BuscarOrdenCompraViewModel{ DisplayName = "Buscador Orden de compra" };
            winC.ShowWindow(obj);
         
        }

      
        public void AbrirBuscadorServicio()
        {
            WindowManager winC = new WindowManager();
            Compras.BuscadorServicioViewModel obj = new Compras.BuscadorServicioViewModel { DisplayName = "Buscador de Servicios" };
            winC.ShowWindow(obj);
        }

        public void AbrirCatalogoProductoProveedor()
        {

            WindowManager winC = new WindowManager();
            Compras.CatalogoProductoProveedorViewModel obj = new Compras.CatalogoProductoProveedorViewModel { DisplayName = "Mantenimiento Catalogo de Productos" };
            winC.ShowWindow(obj);
        }

      

        public void AbrirBuscadorProveedor() {
            WindowManager winC = new WindowManager();
            Compras.BuscadorProveedorViewModel obj = new Compras.BuscadorProveedorViewModel { DisplayName = "Buscar Proveedor" };
            winC.ShowWindow(obj);
        }

        public void AbrirNuevoServicio()
        {
            WindowManager winC = new WindowManager();
            Compras.agregarServicioViewModel obj = new Compras.agregarServicioViewModel { DisplayName = "Nuevo Servicio" };
            winC.ShowWindow(obj);
        }

  
        public void AbrirGenerarOrdenCompra()
        {
            WindowManager winC = new WindowManager();
            Compras.generarOrdenCompraViewModel obj = new Compras.generarOrdenCompraViewModel { DisplayName = "Orden de compra" };
            winC.ShowWindow(obj);
        }
    
        public void AbrirMantenerProveedor() {

            WindowManager winC = new WindowManager();
            Compras.MantenerProveedorViewModel obj = new Compras.MantenerProveedorViewModel { DisplayName = "Proveedor" };
            winC.ShowWindow(obj);
        }

        public void AbrirMantenerSolicitudesAdquisicion()
        {
            WindowManager winC = new WindowManager();
            Compras.mantenerSolicitudesAdquisicionViewModel obj = new Compras.mantenerSolicitudesAdquisicionViewModel { DisplayName = "Solicitudes de Adquisicion" };
            winC.ShowWindow(obj);
        }

        public void AbrirBuscadorSolicitudesAdquisicion()
        {
            WindowManager winC = new WindowManager();
            Compras.BuscadorSolicitudesAdquisicionViewModel obj = new Compras.BuscadorSolicitudesAdquisicionViewModel { DisplayName = "Buscador de Solicitudes de Adquisicion" };
            winC.ShowWindow(obj);
        }
        
        public void AbrirSeleccionDeProveedores() {
            WindowManager winC = new WindowManager();
            Compras.SeleccionDeProveedoresViewModel obj = new Compras.SeleccionDeProveedoresViewModel { DisplayName = "Seleccion de proveedores" };
            winC.ShowWindow(obj);
        }

        public void AbrirRegistrarDocumentos()
        {
            WindowManager winC = new WindowManager();
            Compras.registrarDocumentosViewModel obj = new Compras.registrarDocumentosViewModel { DisplayName = "Registrar Documentos" };
            winC.ShowWindow(obj);
            
        }

        public void AbrirBuscarCotizacion()
        {
            WindowManager winC = new WindowManager();
            Compras.BuscarCotizacionViewModel obj = new Compras.BuscarCotizacionViewModel{ DisplayName = "Buscador Cotizaciones" };
            winC.ShowWindow(obj);
        }

        public void AbrirNuevaCotizacion()
        {
            WindowManager winC = new WindowManager();
            Compras.NuevaCotizacionViewModel obj = new Compras.NuevaCotizacionViewModel { DisplayName = "Cotizacion" };
            winC.ShowWindow(obj);
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

        public void AbrirBuscadorEmpleado()
        {
            win.ShowWindow(new RRHH.BuscadorEmpleadoViewModel { DisplayName = "Buscar Empleado" });

        }

        public void AbrirMantenerEmpleado()
        {
            win.ShowWindow(new RRHH.MantenerEmpleadoViewModel { DisplayName = "Registrar Empleado" });
        }

        public void AbrirControlarAsistenciaEmpleado()
        {
            win.ShowWindow(new RRHH.ControlarAsistenciaEmpleadoViewModel { DisplayName = "Controlar asistencia" });
        }
        
        public void AbrirListadoServicio()
        {
            win.ShowWindow(new Compras.ListadoServicioViewModel());
        }

        public void AbrirArmarHorario()
        {
            win.ShowWindow(new RRHH.ArmarHorarioViewModel() { DisplayName = "Armar Horario" });
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