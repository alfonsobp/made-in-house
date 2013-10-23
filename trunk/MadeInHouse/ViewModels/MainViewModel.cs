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

        //Ventana Externa: 0.12
        public void AbrirMantenimientoInventarioViewModel()
        {
            if ((accesoRol.AccModulo[0] == 1) && (accesoRol.AccVentana[0, 12]) == 1)
            {
                // ActivateItem(new Almacen.MantenimientoInventarioViewModel { DisplayName = "Mantenimiento inventario" });
            }
        }

        //Ventana Externa: 0.13
        public void AbrirAnularDocumentos()
        {
            if ((accesoRol.AccModulo[0] == 1) && (accesoRol.AccVentana[0, 13]) == 1)
                win.ShowWindow(new Almacen.AnularDocumentosViewModel());
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

          
            Compras.CatalogoProductoProveedorViewModel obj = new Compras.CatalogoProductoProveedorViewModel();
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
            win.ShowWindow(new Ventas.PromoProductoBuscarViewModel { DisplayName = "Promociones de Productos" });
        }

        public void AbrirListadoPromoServicio()
        {
            win.ShowWindow(new Ventas.PromoServicioBuscarViewModel { DisplayName = "Promociones de Servicios" });
        }

        public void AbrirNuevaPromoServicio()
        {
            win.ShowWindow(new Ventas.PromoServicioRegistrarViewModel { DisplayName = "Nueva Promoción de Servicio" });
        }

        public void AbrirNuevaPromoProducto()
        {
            win.ShowWindow(new Ventas.PromoProductoRegistrarViewModel());
        }

        #endregion Ventas

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
            win.ShowWindow(new RRHH.MantenerRolViewModel { DisplayName = "Mantenimiento de Roles" });
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
        
        #region Seguridad

        public void AbrirRegistrarUsuario()
        {
            win.ShowWindow(new Seguridad.RegistrarUsuarioViewModel { DisplayName = "Registrar usuario" });
        }

        public void AbrirMantenerUsuario()
        {
            win.ShowWindow(new Seguridad.MantenerUsuarioViewModel { DisplayName = "Mantenimiento de usuario" });

        }
        
        #endregion Seguridad

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
        public void AbrirReportePromocionesTop()
        {
            win.ShowWindow(new Reportes.reportePromocionesFrecuentesViewModel { DisplayName = "Reporte de Promociones Top" });
        }
        public void AbrirReporteProductosCanjePuntos()
        {
            win.ShowWindow(new Reportes.reporteProductosCanjePuntosViewModel { DisplayName = "Reporte de Productos canjeados por Puntos" });
        }

        #endregion

        #region Configuracion
        public void AbrirConfigurarUsuario()
        {
            win.ShowWindow(new Seguridad.ConfigurarUsuarioViewModel());
        }
        public void AbrirMantenerTipoZona()
        {
            win.ShowWindow(new Almacen.BuscarTipoZonaViewModel());
        }
        public void AbrirMantenerLineaProducto()
        {
            win.ShowWindow(new Almacen.MantenerLineaProductoViewModel());
        }

        public void AbrirMantenerSubLineaProducto()
        {
            win.ShowWindow(new Almacen.MantenerSubLineaProductoViewModel());
        }
        public void AbrirMantenerTienda()
        {
            win.ShowWindow(new Almacen.MantenerTiendaViewModel());
        }

        public void AbrirMantenerModulo()
        {
            win.ShowWindow(new Seguridad.MantenerModuloViewModel());
        }

        #endregion Configuracion

    }
}