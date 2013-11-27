using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Compras;
using MadeInHouse.Validacion;
using MadeInHouse.ViewModels.Almacen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.ViewModels.Compras
{
    class ProductoViewModel : Screen
    {


        Producto p;

        public Producto P
        {
            get { return p; }
            set { p = value; NotifyOfPropertyChange("P"); }
        }


        ProveedorxProducto pp;


        public ProveedorxProducto Pp
        {
            get { return pp; }
            set { pp = value; NotifyOfPropertyChange("Pp"); }
        }

        bool esBuscar = true;

        public bool EsBuscar
        {
            get { return esBuscar; }
            set { esBuscar = value; NotifyOfPropertyChange("EsBuscar"); }
        }

        public ProductoViewModel(ProveedorxProducto c,CatalogoProductoProveedorViewModel catalogo) {

            Pp = c;
            P = Pp.Producto;
            cp = catalogo;
            Descripcion = Pp.Descripcion;
            CodComercial = Pp.CodComercial;
            Precio = Pp.Precio.ToString();

            if (P!=null) {
                EsBuscar = false;
            }
    
        }

       // int idProveedor;  

        string descripcion;
public string Descripcion
{
  get { return descripcion; }
  set { descripcion = value;NotifyOfPropertyChange("Descripcion"); }
}
        string codComercial;

public string CodComercial
{
  get { return codComercial; }
    set { codComercial = value; NotifyOfPropertyChange("CodComercial"); }
}

string precio;

public string Precio
{
    get { return precio; }
    set { precio = value; NotifyOfPropertyChange("Precio"); }
}

     

CatalogoProductoProveedorViewModel cp;

public bool Validar() {

    if (P == null) {
        MessageBox.Show("No ha seleccionado producto ", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
        return false;
    }

    Evaluador e = new Evaluador();

    if (!e.esNumeroReal(Precio)) {
        MessageBox.Show("El precio no es Numero real ", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
        return false;
    }

    if (!e.esPositivo(Convert.ToDouble(Precio)))
    {
        MessageBox.Show("El precio no es Negativo ", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
        return false;
    }

    if (String.IsNullOrEmpty(Descripcion)) {
        MessageBox.Show("La descripcion no debe ser vacia ", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
        return false;
    }

    if (String.IsNullOrEmpty(CodComercial))
    {
        MessageBox.Show("El codigo Comercial no puede ser Vacio", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
        return false;
    }

    return true; 

}

public void Buscar(){

    MyWindowManager m = new MyWindowManager();
    ProductoBuscarViewModel pb = new ProductoBuscarViewModel(m,this);
    m.ShowWindow(pb);
}

public void guardar() {

    
        if (Validar())
        {
            Pp.Producto = P;
            Pp.Precio = Convert.ToDouble(Precio);
            Pp.Descripcion = Descripcion;
            Pp.CodComercial = CodComercial;
            new ProveedorxProductoSQL().Insertar(Pp);
            MessageBox.Show("Los datos del producto fueron introducidos satisfactoriamente..", "AVISO");
            cp.Refrescar();
            this.TryClose();
        }
   
   
     

}

public void limpiar() {

    Descripcion = "";
    CodComercial = "";
    Precio = "";
  

}



    }
}
