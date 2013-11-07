using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Compras;
using MadeInHouse.Models;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.DataObjects;
using System.Windows.Forms;

namespace MadeInHouse.ViewModels.Compras
{
    class generarOrdenCompraViewModel:Caliburn.Micro.Screen
    {

        bool esNuevo = true;
        
        List<ProductoxOrdenCompra> lst;

        public List<ProductoxOrdenCompra> Lst
        {
            get { return lst; }
            set { lst = value; NotifyOfPropertyChange("Lst"); }
        }

        Proveedor prov = null;

        public Proveedor Prov
        {
            get { return prov; }
            set { prov = value; NotifyOfPropertyChange("Prov"); }
        }

        List<String> lstEstados = new List<String>() { "FACTURA", "BOLETA" };

        public List<String> LstEstados
        {
            get { return lstEstados; }
            set { lstEstados = value; NotifyOfPropertyChange("LstEstados"); }
        }

        String estadoSelected = "FACTURA";

        public String EstadoSelected
        {
            get { return estadoSelected; }
            set { estadoSelected = value; NotifyOfPropertyChange("EstadoSelected"); }
        }
        

        private bool buscarDispo = true;

        public bool BuscarDispo
        {
            get { return buscarDispo; }
            set { buscarDispo = value; NotifyOfPropertyChange("BuscarDispo"); }
        }



        DateTime fechaAtencion = DateTime.Now;

        public DateTime FechaAtencion
        {
            get { return fechaAtencion; }
            set { fechaAtencion = value; NotifyOfPropertyChange("FechaAtencion"); }
        }

        

        string codOrden;

        public string CodOrden
        {
            get { return codOrden; }
            set { codOrden = value; NotifyOfPropertyChange("CodOrden"); }
        }

        string observaciones;

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; NotifyOfPropertyChange("Observaciones"); }
        }

        ProductoxOrdenCompra detalleSelected=null;

        public ProductoxOrdenCompra DetalleSelected
        {
            get { return detalleSelected; }
            set { detalleSelected = value; NotifyOfPropertyChange("DetalleSelected"); }
        }


        BuscarOrdenCompraViewModel model = null;

        internal BuscarOrdenCompraViewModel Model
        {
            get { return model; }
            set { model = value; }
        }

        public bool existe(ProveedorxProducto p) {

            foreach (ProductoxOrdenCompra o in Lst) {

                if (p.Producto.IdProducto == o.Producto.IdProducto) {


                    return true;
                }



               

            }

            return false;

        }

        public bool Validar(ProveedorxProducto p) {

            if(!existe(p) ){


                ProductoxOrdenCompra po = new ProductoxOrdenCompra();
                po.IdOrden = idOrden;
                po.PrecioUnitario = p.Precio;
                po.Producto = p.Producto;


                lst.Add(po);

                Lst = new List<ProductoxOrdenCompra>(lst);

                return true;

            }


            return false;
        
        }

        public void Buscar() {

            BuscadorProveedorViewModel obj = new  BuscadorProveedorViewModel(this);
            MyWindowManager my = new MyWindowManager();
            my.ShowWindow(obj);

        
        }

        public generarOrdenCompraViewModel() { }

        public generarOrdenCompraViewModel(BuscarOrdenCompraViewModel model) {


            Lst = new List<ProductoxOrdenCompra>();
            this.model = model;
            FechaAtencion = DateTime.Now;
        }

        OrdenCompra o;
        int idOrden = -1;
        int estado=-1;

        public generarOrdenCompraViewModel(OrdenCompra o,BuscarOrdenCompraViewModel model) {

            Lst = o.LstProducto;
            CodOrden = o.CodOrdenCompra;
            FechaAtencion = o.FechaSinAtencion;
            Observaciones = o.Observaciones;
            this.model = model;
            EstadoSelected = o.MedioPago;
            Prov = o.Proveedor;
            BuscarDispo = false;
            idOrden = o.IdOrden;
            esNuevo = false;
            estado= o.Estado;
        }

        public void agregar()
        {

            {
                if (Prov != null)
                {
                    BuscarProductoProveedorViewModel obj = new BuscarProductoProveedorViewModel(this, Prov.IdProveedor);

                    MyWindowManager w = new MyWindowManager();
                    w.ShowWindow(obj);

                }
            }

        }

        public void quitar() {


            if (DetalleSelected != null) {

                lst.Remove(DetalleSelected);
                Lst = new List<ProductoxOrdenCompra>(lst);
                DetalleSelected = null;
            }
            
        
        }

        public void Guardar() {

            
            OrdenCompra o = new OrdenCompra();
            o.IdOrden = idOrden;
            o.IdAlmacen = 4;
            o.LstProducto = Lst;
            o.MedioPago = EstadoSelected;
            o.Observaciones = Observaciones;
            o.FechaSinAtencion = FechaAtencion;
            o.Proveedor = Prov;
            

            OrdenCompraSQL oSQL = new OrdenCompraSQL();
            OrdenCompraxProductoSQL opSQL = new OrdenCompraxProductoSQL();

            if (esNuevo)
            {
                o.Estado = 2;
                oSQL.Agregar(o);

                int id = new UtilesSQL().ObtenerMaximoID("OrdenCompra","idOrden");

                foreach (ProductoxOrdenCompra oc in o.LstProducto) {
                    oc.IdOrden = id;
                    opSQL.Agregar(oc);
                
                }

            }
            else
            {
                o.Estado = estado;

                oSQL.Actualizar(o);
               


                if (estado == 1){

                    o.Estado = 2;
                    oSQL.Actualizar(o);             
                    opSQL.Eliminar(o);

                    foreach (ProductoxOrdenCompra oc in o.LstProducto)
                    {
                        oc.IdOrden = idOrden;
                        opSQL.Agregar(oc);

                    }


                
                }
            }
        
        
        }




    }
}
