﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerSubLineaProductoViewModel :PropertyChangedBase
    {


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
            set { estado = value; NotifyOfPropertyChange(() => Estado); }
        }

        private string txtNombre;

        public string TxtNombre
        {
            get { return txtNombre; }
            set { txtNombre = value; NotifyOfPropertyChange(() => TxtNombre); }
        }

        private string txtAbrv;

        public string TxtAbrv
        {
            get { return txtAbrv; }
            set { txtAbrv = value; NotifyOfPropertyChange(() => TxtAbrv); }
        }



        public MantenerSubLineaProductoViewModel() { }

        /*Creacion a partir de linea producto*/
        public MantenerSubLineaProductoViewModel(MantenerLineaProductoViewModel lp)
        {

            Estado = false;
            lineasProducto.Add(lp.TxtNombre, 0);
            lpVM=lp;
        }

        public void GuardarSubLinea()
        {
            SubLineaProducto slp = new SubLineaProducto(); 
            slp.Nombre=txtNombre;
            slp.Abreviatura = txtAbrv;
            
            lpVM.ActualizaTablaSubLineas(slp);
        }

    }
}