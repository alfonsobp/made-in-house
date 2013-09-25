using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.ComponentModel.Composition;

namespace MadeInHouse.ViewModels
{
    public class MainViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private WindowManager win = new WindowManager();

        #region Ventas

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

        public void AbrirListadoCliente()
        {
            win.ShowWindow(new Ventas.ListadoClienteViewModel { DisplayName = "Maestro de Clientes" });
        }

        public void AbrirNuevoCliente()
        {
            win.ShowWindow(new Ventas.RegistrarClienteViewModel { DisplayName = "Nuevo Cliente" });
        }


        public void AbrirListadoNotaCredito()
        {
            win.ShowWindow(new Ventas.ListadoNotaCreditoViewModel { DisplayName = "Maestro de Notas de Crédito" });
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

        

        #endregion Almacen

        #region Compras

        public void AbrirAgregarServicioViewModel()
        {
            ActivateItem(new Compras.agregarServicioViewModel { DisplayName = "Agregar" });
        }
        public void AbrirCatalogoProductoProveedorModel()
        {
            //ActivateItem(new Compras.CatalogoProductoProveedorViewModel { DisplayName = "Catalogo productos" });

            WindowManager win = new WindowManager();
            Compras.CatalogoProductoProveedorViewModel obj = new Compras.CatalogoProductoProveedorViewModel { DisplayName = "Mantenimiento Catalogo de Productos" };
            win.ShowWindow(obj);
        }
        public void AbrirEditarProveedorViewModel()
        {
            ActivateItem(new Compras.editarProveedorViewModel { DisplayName = "Editas" });
        }

        public void AbrirBuscadorProveedorViewModel() {
            WindowManager win = new WindowManager();
            Compras.BuscadorProveedorViewModel obj = new Compras.BuscadorProveedorViewModel { DisplayName = "Buscar Proveedor" };
            win.ShowWindow(obj);
        }

        public void AbrirEditarServicioViewModel()
        {
            ActivateItem(new Compras.editarServicioViewModel { DisplayName = "Editar" });
        }
        public void AbrirGenerarOrdenCompraViewModel()
        {
            WindowManager win = new WindowManager();
            Compras.generarOrdenCompraViewModel obj = new Compras.generarOrdenCompraViewModel { DisplayName = "Orden de compra" };
            win.ShowWindow(obj);
        }
        /*
        public void AbrirIngresarProveedorViewModel()
        {
            ActivateItem(new Compras.BuscadorProveedorViewModel { DisplayName = "Registrar" });
        }
        public void AbrirMantenerProductoViewModel()
        {
            ActivateItem(new Compras.mantenerProductoViewModel { DisplayName = "Productos" });
        }
         
         * 
          */
        public void AbrirMantenerProveedorViewModel() {

            WindowManager win = new WindowManager();
            Compras.MantenerProveedorViewModel obj = new Compras.MantenerProveedorViewModel { DisplayName = "Mantener Proveedor" };
            win.ShowWindow(obj);
        }

        public void AbrirMantenerSolicitudesAdquisicionViewModel()
        {
            WindowManager win = new WindowManager();
            Compras.mantenerSolicitudesAdquisicionViewModel obj = new Compras.mantenerSolicitudesAdquisicionViewModel { DisplayName = "Mantenimiento de Solicitudes de Adquisicion" };
            win.ShowWindow(obj);
        }
        
        public void AbrirSeleccionDeProveedoresViewModel() {
            WindowManager win = new WindowManager();
            Compras.SeleccionDeProveedoresViewModel obj = new Compras.SeleccionDeProveedoresViewModel { DisplayName = "Seleccion de proveedores" };
            win.ShowWindow(obj);
        }

        public void AbrirRegistrarDocumentosViewModel()
        {
            WindowManager win = new WindowManager();
            Compras.registrarDocumentosViewModel obj = new Compras.registrarDocumentosViewModel { DisplayName = "Registrar Documentos" };
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

        public void AbrirBuscadorEmpleado()
        {
            win.ShowWindow(new RRHH.BuscadorEmpleadoViewModel { DisplayName = "Buscar empleado" });

        }

        public void AbrirMantenerEmpleado()
        {
            win.ShowWindow(new RRHH.MantenerEmpleadoViewModel { DisplayName = "Mantener empleado" });
        }

        public void abrirControlarAsistencia()
        {
            ActivateItem(new RRHH.controlarAsistenciaViewModel { DisplayName = "Controlar asistencia" });
        }
        public void AbrirListadoServicio()
        {
            win.ShowWindow(new Compras.ListadoServicioViewModel());
        }

        #endregion RRHH

        #region Configuracion
        public void AbrirConfigurarUsuario()
        {
            win.ShowWindow(new Seguridad.ConfigurarUsuarioViewModel());
        }
        #endregion Configuracion
    }
}