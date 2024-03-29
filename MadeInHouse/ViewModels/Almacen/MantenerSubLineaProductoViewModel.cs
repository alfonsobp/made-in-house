﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;
using System.ComponentModel.Composition;
using MadeInHouse.ViewModels.Layouts;

namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(SolicitudAbListadoViewModel))]
    class MantenerSubLineaProductoViewModel :PropertyChangedBase,IDataErrorInfo
    {
        #region constructores

        public MantenerSubLineaProductoViewModel(IWindowManager windowmanager) {
            _windowManager = windowmanager;
        }

        /*Creacion a partir de linea producto*/
        public MantenerSubLineaProductoViewModel(IWindowManager windowmanager, MantenerLineaProductoViewModel lp):this(windowmanager)
        {

            Estado = false;
            lineasProducto.Add(lp.TxtNombre, 0);

            lpVM = lp;
        }

        #endregion

        private readonly IWindowManager _windowManager;
        private Dictionary<string, int> lineasProducto = new Dictionary<string, int>();
        private MantenerLineaProductoViewModel lpVM;

        public List<string> cmbLineaProducto
        {
            get
            {
                return new List<string>(lineasProducto.Keys);
            }
        }

        /*Para el comboBox*/
        private bool estado;

        public bool Estado    
        {
            get { return estado; }
            set { estado = value;
            NotifyOfPropertyChange("Estado"); 
            }
        }

        private string txtNombre;

        public string TxtNombre
        {
            get { return txtNombre; }
            set { txtNombre = value;
                NotifyOfPropertyChange("TxtNombre"); }
        }

        private string txtAbrv;

        public string TxtAbrv
        {
            get { return txtAbrv; }
            set { txtAbrv = value; 
                NotifyOfPropertyChange("TxtAbrv"); 
            }
        }



        
        
        public void GuardarSubLinea()
        {
            if (string.IsNullOrWhiteSpace(TxtAbrv) || string.IsNullOrWhiteSpace(TxtNombre)) {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Ingrese valores validos en los campos"));
                return;
            }
            
            SubLineaProducto slp = new SubLineaProducto(); 
            slp.Nombre=txtNombre;
            slp.Abreviatura = txtAbrv;
            
            lpVM.ActualizaTablaSubLineas(slp);
        }

        public string this[string columName]
        {

            get
            {

                string result = string.Empty;
                switch (columName)
                {
                    case "TxtNombre": if (string.IsNullOrEmpty(TxtNombre)) result = "esta vacia"; break;
                    case "TxtAbrv": if (string.IsNullOrEmpty(TxtAbrv)) result = "esta vacia"; break;

                };
                return result;
            }
        }
        public string Error
        {
            get
            {
                return "Error Test!!!!";
            }
        }


    }
}
