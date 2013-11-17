using Caliburn.Micro;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.Models;
using MadeInHouse.Models.Compras;
using MadeInHouse.Validacion;
using MadeInHouse.Views.Compras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MadeInHouse.ViewModels.Compras
{
    class SeleccionDeProveedoresViewModel : Screen, IDataErrorInfo
    {


        #region ATRIBUTOS

       

        Consolidado respuestaSelected;

        public Consolidado RespuestaSelected
        {
            get { return respuestaSelected; }
            set { respuestaSelected = value; NotifyOfPropertyChange("RespuestaSelected"); }
        }
        Consolidado consolidadoSelected;

        public Consolidado ConsolidadoSelected
        {
            get { return consolidadoSelected; }
            set { consolidadoSelected = value; NotifyOfPropertyChange("ConsolidadoSelected"); }
        }
        List<Consolidado> lstConsolidado;

        public List<Consolidado> LstConsolidado
        {
            get { return lstConsolidado; }
            set { lstConsolidado = value; NotifyOfPropertyChange("LstConsolidado"); }
        }
        List<Consolidado> lstRespuesta = new List<Consolidado>();

        public List<Consolidado> LstRespuesta
        {
            get { return lstRespuesta; }
            set { lstRespuesta = value; NotifyOfPropertyChange("LstRespuesta"); }
        }
        Proveedor prov;
        public Double Costo;

        public Proveedor Prov
        {
            get { return prov; }
            set { prov = value; NotifyOfPropertyChange("Prov"); }
        }

        string cantidad = "0";

        public string Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; NotifyOfPropertyChange("Cantidad"); }
        }

       
        

        string codigo;


        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; NotifyOfPropertyChange("Codigo"); }
        }

        public List<int> Solicitudes;

        #endregion

        #region METODOS

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case "Cantidad": result = evaluarCantidad(); break;
                   
                };
                return result;
            }
        }

        public string evaluarCantidad() {

            Evaluador e = new Evaluador();

            if (string.IsNullOrEmpty(Cantidad)) {

                return "La cantidád está vacia";
            } 


            if (!e.esNumeroEntero(Cantidad)) {

                return "La cantidad ingresada no es un número Entero.";
            
            }

            if (int.Parse(Cantidad) <= 0)
            {
                return "No puedo ingresar cantidades que son cero.";
            }

            if (ConsolidadoSelected != null)
            {
                if (ConsolidadoSelected.Cantidad < int.Parse(Cantidad))
                {
                    return "No puede ingresar una cantidad mayor a la que dispone.";
                }

            }


            return String.Empty;
        }

        public SeleccionDeProveedoresViewModel() {

           
            LstConsolidado = new ConsolidadoSQL().Buscar() as List<Consolidado>;
            Solicitudes = new ConsolidadoSQL().getSolicitudes();

        }


        public void SelectedItemChanged(object sender)
        {
            Consolidado p  = ((sender as DataGrid).SelectedItem as Consolidado);

            if (p != null)
            {
                if (p.Cantidad != 0)
                {
                    Cantidad = p.Cantidad.ToString();
                    Codigo = ConsolidadoSelected.Producto.CodigoProd;
                    

                }
                else
                {
                    Cantidad = "0";
                }
            }
            //Prov = null;
            
        }

     

        public void Buscar() { 
        
         MyWindowManager win = new MyWindowManager();

         if (ConsolidadoSelected != null)
         {
             // MessageBox.Show(""+ConsolidadoSelected.Producto.IdProducto);
             BuscarProveedorViewModel obj = new BuscarProveedorViewModel(consolidadoSelected.Producto.IdProducto, this);
             win.ShowWindow(obj);

         }
         else {
             MessageBox.Show("No ha seleccionado ningun registro", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
         }
        
        }


        public Boolean Validar() {

            if (Prov == null)
            {
                MessageBox.Show("No ha seleccionado ningun proveedor", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (ConsolidadoSelected == null)
            {
                MessageBox.Show("No ha seleccionado algún elemento de la tabla ", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            Evaluador e = new Evaluador();

            if (!e.esNumeroEntero(Cantidad))
            {
                MessageBox.Show("La cantidad ingresada no es un número Entero.", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;

            }

            if (int.Parse(Cantidad) <= 0)
            {
                MessageBox.Show("No puedo ingresar cantidades que son cero.", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (ConsolidadoSelected != null)
            {

                if (ConsolidadoSelected.Cantidad < int.Parse(Cantidad))
                {

                    MessageBox.Show("No puede ingresar una cantidad mayor a la que dispone.", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

            }


            return true;
        
        }

        public void Agregar() {


            if (Validar() == true) {

               Consolidado p = new Consolidado();
               p.Cantidad = Convert.ToInt32(Cantidad);
               p.Producto = ConsolidadoSelected.Producto;
               p.Prov = Prov;
               p.Costo = Costo;

               ConsolidadoSelected.Cantidad -= Convert.ToInt32(Cantidad);
               buscarRespuesta(p);
                      
               LstConsolidado = new List<Consolidado>(lstConsolidado);
               LstRespuesta = new List<Consolidado>(lstRespuesta);

               Cantidad = consolidadoSelected.Cantidad.ToString();
               Prov = null;
          
            }

        }

        public void Quitar() {


            if (RespuestaSelected != null) {

                Consolidado p = buscarConsolidado(RespuestaSelected);

                if (p != null) {
                    
                    p.Cantidad += respuestaSelected.Cantidad;
                    lstRespuesta.Remove(respuestaSelected);
                    LstConsolidado = new List<Consolidado>(lstConsolidado);
                    LstRespuesta = new List<Consolidado>(lstRespuesta);
                
                   
                }

                Cantidad = "0";
                Prov = null;
               
            }
        
        }

        public void buscarRespuesta(Consolidado p) { 
        
            foreach (Consolidado pi in LstRespuesta){

                if (p.Prov.IdProveedor == pi.Prov.IdProveedor && pi.Producto.IdProducto == p.Producto.IdProducto)
                {
                    pi.Cantidad += p.Cantidad;
                    return;
                }
              
                    
                
            }

            lstRespuesta.Add(p);
        }

        public Consolidado buscarConsolidado(Consolidado p) { 
        
            foreach( Consolidado pi in LstConsolidado){
                
                if(p.Producto.IdProducto == pi.Producto.IdProducto){
                return pi;
                }
            
            }

            return null;
        
        }

        public List<Proveedor> Seleccion() {

            List<Proveedor> Lista = new List<Proveedor>();

            foreach (Consolidado pi in LstRespuesta) { 
            
                if(!existe(pi,Lista)){
                Lista.Add(pi.Prov);
                }
            
            }

            return Lista;
            
        
        }

        public bool existe(Consolidado p, List<Proveedor> l) {

            foreach (Proveedor pi in l) {

                if (pi.IdProveedor == p.Prov.IdProveedor) {
                    return true;
                }
            }
            return false;
        
        }

        public void MedioPago() {

            if (validarMedio())
            {

                List<Proveedor> lstProveedor = Seleccion();

                MedioPagoViewModel obj = new MedioPagoViewModel(lstProveedor, LstRespuesta, 4, this);
                MyWindowManager win = new MyWindowManager();
                win.ShowWindow(obj);
                //TryClose();
            }
                    
                    
        }

        

        public bool validarMedio() { 

          if (LstConsolidado == null){
          
           MessageBox.Show("No hay consolidado alguna para realizar la transaccion.", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
           return false;
          }

          if (LstRespuesta == null)
          {
              MessageBox.Show("No hay respuesta alguna generada.", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
              return false;
          }

          if (LstRespuesta.Count <= 0)
          {
              MessageBox.Show("No hay respuesta alguna generada.", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
              return false;
          }

          foreach (Consolidado e in LstConsolidado) {

              if (e.Cantidad != 0)
              {
                  MessageBox.Show("No ha completado la transaccion , hay productos aun no atendidos en su totalidad.", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                  return false;
              }
          }


          return true;
        }

      
       
#endregion

        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}

