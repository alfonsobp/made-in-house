using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Manager
{
    class TableManager
    {

        public EntityManager getInstance(int indexTable) {
            EntityManager eM=null;

          

if (indexTable == EntityName.Proveedor )
    eM = new ProveedorManager();
if (indexTable == EntityName.MotivoIS)
    eM = new MotivoISManager();
if (indexTable == EntityName.AccModulo)
    eM = new AccModuloManager();
if (indexTable == EntityName.AccVentana)
    eM = new AccVentanaManager();
if (indexTable == EntityName.AccVentanaInterna)
    eM = new AccVentanaInternaManager();
if (indexTable == EntityName.Almacen)
    eM = new AlmacenManager();
if (indexTable == EntityName.AlmacenxProducto)
    eM = new AlmacenxProductoManager();
if (indexTable == EntityName.Asistencia)
    eM = new AsistenciaManager();
if (indexTable == EntityName.Ausencia)
    eM = new AusenciaManager();
if (indexTable == EntityName.Beneficio)
    eM = new BeneficioManager();
if (indexTable == EntityName.CategoriaSalarial)
    eM = new CategoriaSalarialManager();
if (indexTable == EntityName.Cliente)
    eM = new ClienteManager();
if (indexTable == EntityName.Color)
    eM = new ColorManager();
if (indexTable == EntityName.Cotizacion)
    eM = new CotizacionManager();
if (indexTable == EntityName.CotizacionxProducto)
    eM = new CotizacionxProductoManager();
if (indexTable == EntityName.DetalleDevolucion)
    eM = new DetalleDevolucionManager();
if (indexTable == EntityName.DetalleVenta)
    eM = new DetalleVentaManager();
if (indexTable == EntityName.Devolucion)
    eM = new DevolucionManager();
if (indexTable == EntityName.DocPagoProveedor)
    eM = new DocPagoProveedorManager();
if (indexTable == EntityName.Empleado)
    eM = new EmpleadoManager();
if (indexTable == EntityName.GuiaRemision)
    eM = new GuiaRemisionManager();
if (indexTable == EntityName.Horario)
    eM = new HorarioManager();
if (indexTable == EntityName.LineaProducto)
    eM = new LineaProductoManager();

if (indexTable == EntityName.LogPuntos)
    eM = new LogPuntosManager();
if (indexTable == EntityName.ModoPago)
    eM = new ModoPagoManager();
if (indexTable == EntityName.MotivoIS)
    eM = new MotivoISManager();
if (indexTable == EntityName.NotaIS)
    eM = new NotaISManager();
if (indexTable == EntityName.OrdenCompra)
    eM = new OrdenCompraManager();
if (indexTable == EntityName.Pago)
    eM = new PagoManager();
if (indexTable == EntityName.Producto)
    eM = new ProductoManager();
if (indexTable == EntityName.ProductoxGuiaRemision)
    eM = new ProductoxGuiaRemisionManager();
if (indexTable == EntityName.ProductoxNotaIS)
    eM = new ProductoxNotaISManager();
if (indexTable == EntityName.ProductoxOrdenCompra)
    eM = new ProductoxOrdenCompraManager();
if (indexTable == EntityName.ProductoxSolicitudAb)
    eM = new ProductoxSolicitudAbManager();
if (indexTable == EntityName.ProductoxSolicitudAd)
    eM = new ProductoxSolicitudAdManager();
if (indexTable == EntityName.Puesto)
    eM = new ProductoManager();
if (indexTable == EntityName.Puntos)
    eM = new PuntosManager();
if (indexTable == EntityName.Rol)
    eM = new RolManager();
if (indexTable == EntityName.Servicio)
    eM = new ServicioManager();
if (indexTable == EntityName.ServicioxProducto)
    eM = new ServicioxProductoManager();
if (indexTable == EntityName.SolicitudAbastecimiento)
    eM = new SolicitudAbastecimientoManager();
if (indexTable == EntityName.SolicitudAdquisicion)
    eM = new SolicitudAdquisicionManager();
if (indexTable == EntityName.SubLineaProducto)
    eM = new SubLineaProductoManager();
if (indexTable == EntityName.Tarjeta)
    eM = new  TarjetaManager();
if (indexTable == EntityName.Tienda)
    eM = new TiendaManager();
if (indexTable == EntityName.TipoZona)
    eM = new TipoZonaManager();
if (indexTable == EntityName.Ubicacion)
    eM = new UbicacionManager();
if (indexTable == EntityName.UnidadMedida)
    eM = new UnidadMedidaManager();
if (indexTable == EntityName.Usuario)
    eM = new UsuarioManager();
if (indexTable == EntityName.Venta)
    eM = new VentaManager();
if (indexTable == EntityName.ZonaxAlmacen)
    eM = new ZonaxAlmacenManager();


        

            return eM;
        }
    }
}
