﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;
using MadeInHouse.Models.Compras;
using MadeInHouse.DataObjects.Compras;
using System.Windows;


namespace MadeInHouse.ViewModels.Compras
{
    class BuscadorSolicitudesAdquisicionViewModel : PropertyChangedBase
    {
        private MyWindowManager win = new MyWindowManager();

        DateTime fechaIni= new DateTime(DateTime.Now.Year,1,1);

        public DateTime FechaIni
        {
            get { return fechaIni; }
            set { fechaIni = value; NotifyOfPropertyChange("FechaIni"); }
        }
        DateTime fechaFin = new DateTime(DateTime.Now.Year, 12, 31);

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; NotifyOfPropertyChange("FechaFin"); }
        }

        List<String> lstEstado = new List<string> {"TODOS","NO ATENDIDA","ATENDIDA","ATENDIENDO"} ;

        public List<String> LstEstado
        {
            get { return lstEstado; }
            set { lstEstado = value; NotifyOfPropertyChange("LstEstado"); }
        }

        string idAlmacen = "ALM-" + (1000000 + 4);

        public string IdAlmacen
        {
            get { return idAlmacen; }
            set { idAlmacen = value; NotifyOfPropertyChange("IdAlmacen"); }
        }


        string tienda;

        public string Tienda
        {
            get { return tienda; }
            set { tienda = value; NotifyOfPropertyChange("Tienda"); }
        }

        private string estadoSelected="TODOS";

        public string EstadoSelected
        {
            get { return estadoSelected; }
            set { estadoSelected = value; NotifyOfPropertyChange("EstadoSelected"); }
        }

        SolicitudAdquisicion solicitudSelected;

        public SolicitudAdquisicion SolicitudSelected
        {
            get { return solicitudSelected; }
            set { solicitudSelected = value; NotifyOfPropertyChange("SolicitudSelected"); }
        }

        List<SolicitudAdquisicion> lstSolicitud;

        public List<SolicitudAdquisicion> LstSolicitud
        {
            get { return lstSolicitud; }
            set { lstSolicitud = value; NotifyOfPropertyChange("LstSolicitud"); }
        }

        public BuscadorSolicitudesAdquisicionViewModel() {
            LstSolicitud = new SolicitudAdquisicionSQL().Buscar(Tienda, getEstado(EstadoSelected), FechaIni, FechaFin) as List<SolicitudAdquisicion>;
        }

        public void Buscar() {

            LstSolicitud = new SolicitudAdquisicionSQL().Buscar(Tienda,getEstado( EstadoSelected), FechaIni, FechaFin) as List<SolicitudAdquisicion>;
        
        }

        public int getEstado(String n) { 
        
         if(n== "TODOS") return 4;
         if (n == "NO ATENDIDA") return 1;
         if (n == "ATENDIDA") return 3;
         if (n == "ATENDIENDO") return 2;

         return 0;

        }

        public void NuevaSolicitud()
        {


     
        }
        public void EditarSolicitud()
        {

            if (SolicitudSelected != null)
            {
                Compras.mantenerSolicitudesAdquisicionViewModel obj = new Compras.mantenerSolicitudesAdquisicionViewModel(SolicitudSelected, this);
                win.ShowWindow(obj);
            }
            else
            {
                MessageBox.Show("No ha seleccionado una Solicitud de adquisicion", "AVISO", MessageBoxButton.OK,MessageBoxImage.Exclamation);
            
            }
        }
    }
}
