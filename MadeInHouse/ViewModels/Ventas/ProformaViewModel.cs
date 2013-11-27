using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Ventas;
using System.Windows.Controls;

namespace MadeInHouse.ViewModels.Ventas
{
    class ProformaViewModel:Screen
    {
        private WindowManager win = new WindowManager();

        public ProformaViewModel()
        {
            lstProforma = new List<Proforma>();
            prod = new Producto();
        }

        private Proforma detalleSeleccionado;

        public void SelectedItemChanged(object sender)
        {
            detalleSeleccionado = ((sender as DataGrid).SelectedItem as Proforma);
        }

        private List<Proforma> lstProforma;

        public List<Proforma> LstProforma
        {
            get { return lstProforma; }
            set { lstProforma = value; NotifyOfPropertyChange(() => LstProforma); }
        }

        private string txtTelefono;

        public string TxtTelefono
        {
            get { return txtTelefono; }
            set { txtTelefono = value; NotifyOfPropertyChange(() => TxtTelefono); }
        }

        private string txtProducto;

        public string TxtProducto
        {
            get { return txtProducto; }
            set { txtProducto = value; NotifyOfPropertyChange(() => TxtProducto); }
        }

        Producto prod;

        public Producto Prod
        {
            get { return prod; }
            set { prod = value; NotifyOfPropertyChange(() => Prod); TxtProducto = prod.Nombre; }
        }

        private string txtCantidad;

        public string TxtCantidad
        {
            get { return txtCantidad; }
            set { txtCantidad = value; NotifyOfPropertyChange(() => TxtCantidad); }
        }

        public void AbrirBuscarProducto()
        {
            MyWindowManager w = new MyWindowManager();
            w.ShowWindow(new Almacen.ProductoBuscarViewModel(w,this,5) );
        }

        public void AgregarDetalle()
        {
            Proforma pf = new Proforma();
            //Producto p = new DetalleVentaSQL().Buscar(TxtProducto);
            pf.Producto = prod.Nombre;
            pf.Subtotal = prod.Precio * Convert.ToDouble(TxtCantidad);
            pf.Cantidad = Convert.ToInt32(TxtCantidad);

            List<Proforma> aux = new List<Proforma>();
            foreach (Proforma item in LstProforma)
            {
                aux.Add(item);
            }
            aux.Add(pf);
            LstProforma = aux;
        }

        private string txtCliente;

        public string TxtCliente
        {
            get { return txtCliente; }
            set { txtCliente = value; NotifyOfPropertyChange(() => TxtCliente); }
        }
    }
}
