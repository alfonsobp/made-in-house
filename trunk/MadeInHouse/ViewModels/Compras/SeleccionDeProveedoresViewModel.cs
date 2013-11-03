using Caliburn.Micro;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.Models;
using MadeInHouse.Models.Compras;
using MadeInHouse.Views.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MadeInHouse.ViewModels.Compras
{
    class SeleccionDeProveedoresViewModel:Screen
    {


        #region ATRIBUTOS

        string almacen;

        public string Almacen
        {
            get { return almacen; }
            set { almacen = value; NotifyOfPropertyChange("Almacen"); }
        }

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

        public Proveedor Prov
        {
            get { return prov; }
            set { prov = value; NotifyOfPropertyChange("Prov"); }
        }

        int cantidad=0;

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; NotifyOfPropertyChange("Cantidad"); }
        }

        int idAlmacen=4;

        string codigo;


        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; NotifyOfPropertyChange("Codigo"); }
        }

        #endregion

        #region METODOS
        public SeleccionDeProveedoresViewModel() {

            Almacen = "ALM-" + (1000000 + 4);
            LstConsolidado = new ConsolidadoSQL().Buscar(4) as List<Consolidado>;

        }


        public void SelectedItemChanged(object sender)
        {
            Consolidado p  = ((sender as DataGrid).SelectedItem as Consolidado);

            if (p.Cantidad != 0)
            {
                Cantidad = p.Cantidad;
                Codigo = ConsolidadoSelected.Producto.CodigoProd;
               
            }
            else {
                Cantidad = 0;      
            }

            Prov = null;
            
        }

     

        public void Buscar() { 
        
         MyWindowManager win = new MyWindowManager();

         if (ConsolidadoSelected != null)
         {
            // MessageBox.Show(""+ConsolidadoSelected.Producto.IdProducto);
             BuscarProveedorViewModel obj = new BuscarProveedorViewModel(consolidadoSelected.Producto.IdProducto,this);
             win.ShowWindow(obj);
         }
        
        }


        public Boolean Validar() {

            if (Prov == null)
                return false;

            if(consolidadoSelected==null)
                return false;

            if (ConsolidadoSelected.Cantidad < Cantidad || Cantidad <= 0)
                return false;



            return true;
        
        }

        public void Agregar() {


            if (Validar() == true) {

               Consolidado p = new Consolidado();
               p.Cantidad = Cantidad;
               p.Producto = ConsolidadoSelected.Producto;
               p.Prov = Prov;

               ConsolidadoSelected.Cantidad -= Cantidad;
               buscarRespuesta(p);
                      
               LstConsolidado = new List<Consolidado>(lstConsolidado);
               LstRespuesta = new List<Consolidado>(lstRespuesta);

               Cantidad = consolidadoSelected.Cantidad;
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

                Cantidad = 0;
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

            List<Proveedor> lstProveedor = Seleccion();
            MedioPagoViewModel obj = new MedioPagoViewModel(lstProveedor, LstRespuesta, 4,this);
            MyWindowManager win = new MyWindowManager();
            win.ShowWindow(obj);
            //TryClose();
        
        }
       
#endregion
    }
}

