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
using MadeInHouse.Validacion;

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


        bool esEditable = false;

        public bool EsEditable
        {
            get { return esEditable; }
            set { esEditable = value; NotifyOfPropertyChange("EsEditable"); }
        }


        BuscarOrdenCompraViewModel model = null;

        public BuscarOrdenCompraViewModel Model
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

        public generarOrdenCompraViewModel() {
            Lst = new List<ProductoxOrdenCompra>();
            model=null;
            FechaAtencion = DateTime.Now;
            esNuevo=true;
            EsEditable = true;
           
        
        }

        public generarOrdenCompraViewModel(BuscarOrdenCompraViewModel model) {


            Lst = new List<ProductoxOrdenCompra>();
            this.model = model;
            FechaAtencion = DateTime.Now;
            esNuevo = true;
            EsEditable = true;
            
        }

        bool esGuardable=true;

public bool EsGuardable
{
  get { return esGuardable; }
  set { esGuardable = value;NotifyOfPropertyChange("EsGuardable"); }
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

            if (estado == 1) { EsEditable = true; };
            if(estado == 0 ) { esGuardable = false;}
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
                else 
                {
                    MessageBox.Show("Primero , Seleccione un proveedor..", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        }

        public void quitar() {


            if (DetalleSelected != null)
            {

                lst.Remove(DetalleSelected);
                Lst = new List<ProductoxOrdenCompra>(lst);
                DetalleSelected = null;
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningun producto..", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
            }
            
        
        }

        public bool Validar(OrdenCompra o) {


            if (String.IsNullOrEmpty(o.Observaciones))
            {
                MessageBox.Show( "El campo se encuentra vacio,Corregir..","AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (o.Proveedor == null) {


                MessageBox.Show( "No ha Seleccionado un proveedor..","AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return false;
            }

            if (o.LstProducto.Count == 0) {

                MessageBox.Show( "Ingrese algún producto..","AVISO", MessageBoxButtons.OK,  MessageBoxIcon.Exclamation);
                return false;
            
            }


            foreach (ProductoxOrdenCompra oc in o.LstProducto) {
                Evaluador e = new Evaluador();

                if (String.IsNullOrEmpty(oc.Cantidad))
                {
                    MessageBox.Show( "Hay cantidades que  se encuentran vacias,Corregir..","AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }


                if (!e.esNumeroEntero(oc.Cantidad))
                {
                    MessageBox.Show(Error.esNumero.mensaje, Error.esNumero.titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;

                }

                if(!e.esPositivo(Convert.ToInt32(oc.Cantidad) )){
                
                    MessageBox.Show(Error.esNegativo.mensaje, Error.esNegativo.titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                
                }

                if (oc.CantAtendida > Convert.ToInt32(oc.Cantidad)) {

                    MessageBox.Show("La cantidad emitida no puede ser menor a la cantidad Atendida","AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }



            
            }


            return true;
        
        }

        public void Guardar()
        {


            OrdenCompra o = new OrdenCompra();
            o.IdOrden = idOrden;
            o.IdAlmacen = 4;
            o.LstProducto = Lst;
            o.MedioPago = EstadoSelected;
            o.Observaciones = Observaciones;
            o.FechaSinAtencion = FechaAtencion;
            o.Proveedor = Prov;

          

                if (Validar(o))
                {

                    OrdenCompraSQL oSQL = new OrdenCompraSQL();
                    OrdenCompraxProductoSQL opSQL = new OrdenCompraxProductoSQL();



                    if (esNuevo)
                    {

                        DialogResult result = MessageBox.Show("Esta generando una Orden de compra por defecto es NO EMITIDA , desea EMITIRLA  ? ", "AVISO",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            o.Estado = 2;

                        }
                        else
                        {
                            o.Estado = 1;
                        }


                        oSQL.Agregar(o);

                        int id = new UtilesSQL().ObtenerMaximoID("OrdenCompra", "idOrden");

                        foreach (ProductoxOrdenCompra oc in o.LstProducto)
                        {
                            oc.IdOrden = id;
                            opSQL.Agregar(oc);
                           
                        }

                        MessageBox.Show("Se Generó Exitosamente la Orden de Compra", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (model != null)
                        {
                            model.Buscar();
                        }

                        this.TryClose();

                    }
                    else
                    {
                        o.Estado = estado;

                        oSQL.Actualizar(o);

                        if (o.Estado == 1)
                        {
                            opSQL.Eliminar(o);
                            foreach (ProductoxOrdenCompra oc in o.LstProducto)
                            {
                                oc.IdOrden = idOrden;
                                opSQL.Agregar(oc);

                            }

                            DialogResult result = MessageBox.Show("Esta Editando una Orden de compra NO EMITIDA , desea EMITIRLA  ? ", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                o.Estado = 2;
                                oSQL.Actualizar(o);

                            }
                        }
                        else
                        {


                            foreach (ProductoxOrdenCompra oc in o.LstProducto)
                            {
                                oc.IdOrden = idOrden;
                                opSQL.Actualizar(oc);

                            }


                        }

                        MessageBox.Show("Se Editó adecuadamente la Orden de Compra", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (model != null)
                        {
                            model.Buscar();
                        }
                        this.TryClose();

                    }

                }

            }
        
      


    
    }
}
