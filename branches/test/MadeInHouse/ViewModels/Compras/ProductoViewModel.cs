using Caliburn.Micro;
using MadeInHouse.Manager;
using MadeInHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.ViewModels.Compras
{
    class ProductoViewModel : PropertyChangedBase
    {
       


        public ProductoViewModel(ProveedorxProducto c,CatalogoProductoProveedorViewModel catalogo) {

            this.cp = catalogo;
            descripcion = c.Descripcion;
            codComercial = c.CodComercial;
            precio = c.Precio.ToString();
            codProducto = c.Producto.CodProducto;
            idProveedor = c.IdProveedor;
        }

        int idProveedor;  

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

        string codProducto;


public string CodProducto
{
  get { return codProducto; }
    set { codProducto = value; NotifyOfPropertyChange("CodProducto"); }
}

CatalogoProductoProveedorViewModel cp;

public void guardar() { 

Producto p = new ProductoManager().Buscar_por_CodigoProducto(codProducto);
if(p != null ) {
ProveedorxProducto pp = new ProveedorxProducto();
pp.Producto = p;
pp.IdProveedor = cp.Prov.IdProveedor;
pp.CodComercial = codComercial;
pp.Precio = Convert.ToDouble(Precio);
pp.Descripcion = Descripcion;

new ProveedorxProductoManager().Insertar(pp);
cp.Refrescar();
MessageBox.Show("Ha ingresado adecuadamente el Producto al catalogo del Proveedor");
}
else
{
MessageBox.Show("No ha ingresado adecuadamente el Codigo del producto");

}


}

public void limpiar() {

    Descripcion = "";
    CodComercial = "";
    Precio = "";
    CodProducto = "";

}



    }
}
