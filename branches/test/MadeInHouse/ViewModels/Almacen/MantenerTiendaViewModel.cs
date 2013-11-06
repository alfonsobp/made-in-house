﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerTiendaViewModel : PropertyChangedBase
    {

        public MantenerTiendaViewModel() {
            this.cmbDpto=gateway.BuscarDpto();
        }

        private UbigeoSQL gateway = new UbigeoSQL();

        private string txtNumColumns;

        public string TxtNumColumns
        {
            get { return txtNumColumns; }
            set { txtNumColumns = value;
            
            }
        }
        private string txtNumRows;

        public string TxtNumRows
        {
            get { return txtNumRows; }
            set { txtNumRows = value;
            

            }
        }

        public void Refrescar(object sender)
        {
            
            NumColumns = Int32.Parse(TxtNumColumns);
            NumRows = Int32.Parse(TxtNumRows);
            (sender as MadeInHouse.Dictionary.DynamicGrid).RecreateGridCells();
        }


        private string txtDir;

        public string TxtDir
        {
            get { return txtDir; }
            set { txtDir = value; }
        }
        private int selectedIndex;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; }
        }

        private string selectedValue;

        public string SelectedValue
        {
            get { return selectedValue; }
            set
            {
                selectedValue = value;

                cmbProv = gateway.BuscarProv(selectedValue);
                SelectedIndex = 0;

            }
        }

        private List<Ubigeo> cmbDpto;

        public List<Ubigeo> CmbDpto
        {
            get { return this.cmbDpto; }
            private set { 
            if (this.cmbDpto == value) return;
            this.cmbDpto = value;
            this.cmbProv = gateway.BuscarProv(this.cmbDptoSeleccionado.CodDpto);
            this.NotifyOfPropertyChange(() => this.CmbDpto);
            this.NotifyOfPropertyChange(() => this.CmbProv);
            }
            
        }
        private List<Ubigeo> cmbProv;

        public List<Ubigeo> CmbProv
        {
            get { return this.cmbProv; }
            private set
            {
                if (this.cmbProv == value) return;
                this.cmbProv = value;
                this.cmbDist = gateway.BuscarDist(this.cmbDptoSeleccionado.CodDpto, this.cmbProvSeleccionado.CodProv);
                this.NotifyOfPropertyChange(() => this.CmbDist);
                this.NotifyOfPropertyChange(() => this.CmbProv);
            }
            
        }
        private List<Ubigeo> cmbDist;

        public List<Ubigeo> CmbDist
        {
            get { return this.cmbDist; }
            private set
            {
                if (this.cmbDist == value) return;
                this.cmbDist = value;
                this.NotifyOfPropertyChange(() => this.CmbDist);
            }
            
        }


        private Ubigeo cmbDptoSeleccionado;

        public Ubigeo CmbDptoSeleccionado
        {
            get { return cmbDptoSeleccionado; }
            set { cmbDptoSeleccionado = value; }
        }

        private Ubigeo cmbProvSeleccionado;

        public Ubigeo CmbProvSeleccionado
        {
            get { return cmbProvSeleccionado; }
            set { cmbProvSeleccionado = value; }
        }

        private Ubigeo cmbDistSeleccionado;

        public Ubigeo CmbDistSeleccionado
        {
            get { return cmbDistSeleccionado; }
            set { cmbDistSeleccionado = value; }
        }

        public void GuardarTienda() {

            Tienda tienda = new Tienda();
            tienda.Estado = 1;
           // tienda.Nombre = this.txtNombre;
            tienda.Direccion = this.txtDir;
            Ubigeo seleccionado = new Ubigeo();
            seleccionado = gateway.buscarUbigeo(cmbDistSeleccionado.CodDpto, cmbDptoSeleccionado.CodProv, cmbProvSeleccionado.CodDist);
            tienda.Ubigeo = seleccionado;
            tienda.FechaReg = DateTime.Today;
            TiendaSQL gw = new TiendaSQL();
            gw.AgregarTienda(tienda);


        }

        private string fondo;

        public string Fondo
        {
            get { return fondo; }
            set { fondo = value;
            NotifyOfPropertyChange(() => Fondo);
            }
        }

        private int numColumns;

        public int NumColumns
        {
            get { return numColumns; }
            set { numColumns = value;
            NotifyOfPropertyChange(() => NumColumns);
            }
        }

        private int numRows;

        public int NumRows
        {
            get { return numRows; }
            set { numRows = value;
            NotifyOfPropertyChange(() => NumRows);
            
            }
        }



        public void OnClick()
        {
            Fondo = "Red";
        }


    }
}