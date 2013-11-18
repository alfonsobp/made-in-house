using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Views.Almacen;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using MadeInHouse.DataObjects.Almacen;

namespace MadeInHouse.ViewModels.Almacen
{
    class BuscarAlmacenViewModel:Screen
    {
        MantenerGuiaDeRemisionViewModel g;
        public BuscarAlmacenViewModel(MantenerGuiaDeRemisionViewModel g)
        {
            this.g = g;
        }

        //Atributos
        private string txtCodigo;
        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }

        private List<Almacenes> lstAlmacenes;
        public List<Almacenes> LstAlmacenes
        {
            get { return lstAlmacenes; }
            set { lstAlmacenes = value; NotifyOfPropertyChange(() => LstAlmacenes); }
        }

        private Almacenes almSelect;
        public Almacenes AlmSelect
        {
            get { return almSelect; }
            set { almSelect = value; NotifyOfPropertyChange(() => AlmSelect); }
        }


        //Funciones;
        public void SelectedItemChanged()
        {
            if (AlmSelect != null)
            {
                if (g != null)
                {
                    g.Alm = AlmSelect;
                    TryClose();
                }
            }
        }

        public void BuscarAlmacenes()
        {
            if (String.IsNullOrEmpty(TxtCodigo))
                LstAlmacenes = new AlmacenSQL().BuscarAlmacen();
            else
            {
                List<Almacenes> list = new List<Almacenes>();
                List<Almacenes> listAux = new AlmacenSQL().BuscarAlmacen();

                for (int i = 0; i < listAux.Count; i++)
                {
                    if (listAux[i].IdAlmacen == (Convert.ToInt32(TxtCodigo)))
                        list.Add(listAux[i]);
                }

                LstAlmacenes = new List<Almacenes>(list);
            }
        }
    }
}
