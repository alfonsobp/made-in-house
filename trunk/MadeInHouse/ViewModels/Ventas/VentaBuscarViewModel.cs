using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using MadeInHouse.Models;
using MadeInHouse.Models.Ventas;
using MadeInHouse.DataObjects.Ventas;
using MadeInHouse.Validacion;
using System.Data.SqlClient;
using System.Data;
using MadeInHouse.DataObjects;

namespace MadeInHouse.ViewModels.Ventas
{
    class VentaBuscarViewModel : PropertyChangedBase
    {
        private MyWindowManager win = new MyWindowManager();

        public void AbrirRegistrarVenta()
        {
            win.ShowWindow(new Ventas.VentaRegistrarViewModel ());
        }
        public void AbrirEditarVenta()
        {
            win.ShowWindow(new Ventas.VentaEditarViewModel());
        }
        public void AbrirBuscarCliente()
        {
            win.ShowWindow(new Ventas.ClienteBuscarViewModel(this));
        }

        public Cliente client=null;


        private List<string> lstEstado = new List<string>() { "TODOS", "REALIZADA", "ANULADA" };

        public int getEstado(string l) {

            if (l == "TODOS") return -1;
            if (l == "REALIZADA") return 1;
            if (l == "ANULADA") return 0;

            return -1;
        
        }

        public List<string> LstEstado
        {
            get { return lstEstado; }
            set { lstEstado = value; NotifyOfPropertyChange("LstEstado"); }
        }


        private string identificacion = "";

        public string Identificacion
        {
            get { return identificacion; }
            set { identificacion = value; NotifyOfPropertyChange("Identificacion"); }
        }



        string selectedEstado = "TODOS";

        public string SelectedEstado
        {
            get { return selectedEstado; }
            set { selectedEstado = value; NotifyOfPropertyChange("SelectedEstado"); }
        }




        private Venta ventaSeleccionada=null;

        public Venta VentaSeleccionada
        {
            get { return ventaSeleccionada; }
            set { ventaSeleccionada = value; NotifyOfPropertyChange("VentaSeleccionada"); }
        }

        private DateTime fechaInicio = new DateTime(DateTime.Now.Year, 1, 1);

        public DateTime FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; NotifyOfPropertyChange(() => FechaInicio); }
        }


        private DateTime fechaFin = new DateTime(DateTime.Now.Year, 12, 31);

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; NotifyOfPropertyChange(() => FechaFin); }
        }

        private string txtDocPago;

        public string TxtDocPago
        {
            get { return txtDocPago; }
            set { txtDocPago = value; NotifyOfPropertyChange(() => TxtDocPago); }
        }

        private string dniRuc;

        public string DniRuc
        {
            get { return dniRuc; }
            set { dniRuc = value; NotifyOfPropertyChange(() => DniRuc); }
        }

       
        


        private string montoMin;

        public string MontoMin
        {
            get { return montoMin; }
            set { montoMin = value; NotifyOfPropertyChange(() => MontoMin); }
        }

        private string montoMax;

        public string MontoMax
        {
            get { return montoMax; }
            set { montoMax = value; NotifyOfPropertyChange(() => MontoMax); }
        }

        private List<Venta> lstVentas = null;

        public List<Venta> LstVentas
        {
            get { return lstVentas; }
            set { lstVentas = value; NotifyOfPropertyChange(() => LstVentas); }
        }

        public void BuscarVentas()
        {
            if (Validar())
            {

                LstVentas = new VentaSQL().Buscar(TxtDocPago,  MontoMin, MontoMax,client,fechaInicio,fechaFin,getEstado(SelectedEstado) ) as List<Venta>;
            }
           
           
        }

        public bool Validar() {

            Evaluador e = new Evaluador();

            if (!e.esNumeroReal(MontoMax)&& !String.IsNullOrEmpty(MontoMax)) {
                MessageBox.Show("No ha ingresado un valor correcto en el Monto máximo", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            
            
            }

            if (!e.esNumeroReal(MontoMin) && !String.IsNullOrEmpty(MontoMin))
            {
                MessageBox.Show("No ha ingresado un valor correcto en el Monto mínimo", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;


            }

            if (!e.esNumeroReal(DniRuc)&& !String.IsNullOrEmpty(DniRuc))
            {
                MessageBox.Show("El DNI/RUC es un valor numérico", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;

            }



            return true;
        
        }


        public void Limpiar() {


            DniRuc = "";
            Identificacion = "";
            client = null;
            MontoMax = "";
            MontoMin = "";
            FechaInicio = new DateTime(DateTime.Now.Year, 1, 1);
            FechaFin = new DateTime(DateTime.Now.Year, 12, 31);

        }

        public void Actualizar() {

            this.BuscarVentas();
         
        }

        public void AnularVenta()
        {
            
            int resultado;

            if (VentaSeleccionada != null)
            {
                if (ventaSeleccionada.TipoVenta.Equals("Tienda"))
                {
                    DBConexion db = new DBConexion();
                    db.conn.Open();
                    SqlTransaction trans = db.conn.BeginTransaction(IsolationLevel.Serializable);
                    db.cmd.Transaction = trans;
                    VentaSQL vsql = new VentaSQL(db);
                    resultado = vsql.AnularVentaTienda(ventaSeleccionada);
                    if (resultado == 0)
                    {
                        trans.Rollback();
                        MessageBox.Show("No se pudo anular la venta", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        trans.Commit();
                        MessageBox.Show("Se ha anulado la venta satistactoriamente");
                    }
                }
                else
                {
                    VentaSQL vsql = new VentaSQL();
                    resultado = vsql.AnularVentaObra(ventaSeleccionada);
                    if (resultado == 0)
                    {
                        MessageBox.Show("No se pudo anular la venta", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    if (resultado == 3)
                    {
                        MessageBox.Show("No se puede anular esta venta, pues ya fue atendida", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    if(resultado == 2)
                    {
                        MessageBox.Show("La venta ha sido anulada satistactoriamente");
                    }
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado ninguna Venta", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
