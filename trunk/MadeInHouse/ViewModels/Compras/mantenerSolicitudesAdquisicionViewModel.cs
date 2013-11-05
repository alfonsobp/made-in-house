using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Compras;
using MadeInHouse.DataObjects.Compras;
using System.Windows;
using MadeInHouse.Validacion;

namespace MadeInHouse.ViewModels.Compras
{
    class mantenerSolicitudesAdquisicionViewModel : Screen
    {
        BuscadorSolicitudesAdquisicionViewModel model;

        SolicitudAdquisicion p;
        
        public SolicitudAdquisicion P
        {
            get { return p; }
            set { p = value;  }
        }

      


        public mantenerSolicitudesAdquisicionViewModel(SolicitudAdquisicion p,  BuscadorSolicitudesAdquisicionViewModel model) {

            this.P = p;
            this.model = model;
            Lst = p.LstProductos;

        }

        string cantidad;

        public string Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; NotifyOfPropertyChange("Cantidad"); }
        }

        List<ProductoxSolicitudAd> lst;

        public List<ProductoxSolicitudAd> Lst
        {
            get { return lst; }
            set { lst = value; NotifyOfPropertyChange("Lst"); }
        }

       

        ProductoxSolicitudAd seleccionado;

        public ProductoxSolicitudAd Seleccionado
        {
            get { return seleccionado; }
            set { seleccionado = value; NotifyOfPropertyChange("Seleccionado"); }
        }


        public void Guardar() {

            if (EsValido())
            {
                ProductoxSolicitudAdSQL m = new ProductoxSolicitudAdSQL();

                foreach (ProductoxSolicitudAd pi in Lst)
                {



                    m.Actualizar(pi);

                }


                new SolicitudAdquisicionSQL().Actualizar(P);

                MessageBox.Show("Los datos fuerons Actualizados Satisfactoriamente ", "AVISO", MessageBoxButton.OK, MessageBoxImage.Information);

                model.Buscar();
                this.TryClose();

            }
        }

        public void Agregar() {
            if (EsValido()){
               
                    if (seleccionado != null) {

                        if ( Validar()) {

                            Seleccionado.CantidadAtendida = Convert.ToInt32(Cantidad);
                            Lst = new List<ProductoxSolicitudAd>(lst);
                        }
            
                    }

            }
        
        }

        public bool Validar() {

            Evaluador e = new Evaluador();

            if (!e.esNumeroEntero(Cantidad)) {
                MessageBox.Show(Error.esNumero.mensaje, Error.esNumero.titulo, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;

            }

            if(!e.esPositivo( Convert.ToInt32(Cantidad))){
            MessageBox.Show(Error.esNegativo.mensaje, Error.esNegativo.titulo, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true ;

           
        }

        public bool EsValido() {

            if (p.Estado == 1)
                return true;
            else
                return false;
        
        }

    }
}
