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

namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerGuiaDeRemisionViewModel:PropertyChangedBase
    {
        public MantenerGuiaDeRemisionViewModel(GuiaRemision g) {

            txtCodigo = g.CodGuiaRem;
            txtFechaReg = g.FechaReg;
            SeleccionadoTipo=g.Tipo;
            txtDireccion = g.DirLlegada;
            txtDirPartida = g.DirPartida;
            txtDirLlegada = g.DirLlegada;
            txtConductor = g.Conductor;
            SeleccionadoCamion = g.Camion;
            txtObservaciones = g.Observaciones;
            //Doc Ref ?
            //Tienda ?


            indicador = 2;
        }


        public MantenerGuiaDeRemisionViewModel()
        {
            //Insertar
            indicador = 1;
        }


        private int indicador;
        //indicador = 1 para insertar
        //indicador = 2 para editar

        public int Indicador
        {
            get { return indicador; }
        }

        private string txtCodigo;

        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }

        private List<string> cbTipo = new List<string>() { "GR-TRASLADO INTERNO", "GR-TRASLADO EXTERNO" };

        public List<string> CbTipo
        {
            get { return cbTipo; }
            set { cbTipo = value; NotifyOfPropertyChange(() => CbTipo); }
        }

     

        private DateTime txtFechaReg;

        public DateTime TxtFechaReg
        {
            get { return txtFechaReg; }
            set { txtFechaReg = value; NotifyOfPropertyChange(() => TxtFechaReg); }
        }


        private string txtDocRef;

        public string TxtDocRef
        {
            get { return txtDocRef; }
            set { txtDocRef = value; NotifyOfPropertyChange(() => TxtDocRef); }
        }

        private string txtTienda;

        public string TxtTienda
        {
            get { return txtTienda; }
            set { txtTienda = value; NotifyOfPropertyChange(() => TxtTienda); }
        }

        private string txtDireccion;

        public string TxtDireccion
        {
            get { return txtDireccion; }
            set { txtDireccion = value; NotifyOfPropertyChange(() => TxtDireccion); }
        }

        private string txtDirLlegada;

        public string TxtDirLlegada
        {
            get { return txtDirLlegada; }
            set { txtDirLlegada = value; NotifyOfPropertyChange(() => TxtDirLlegada); }
        }

        private string txtDirPartida;

        public string TxtDirPartida
        {
            get { return txtDirPartida; }
            set { txtDirPartida = value; NotifyOfPropertyChange(() => TxtDirPartida); }
        }

        private string txtConductor;

        public string TxtConductor
        {
            get { return txtConductor; }
            set { txtConductor = value; NotifyOfPropertyChange(() => TxtConductor); }
        }

        private List<string> cbCamion = new List<string>() { "Camion pesado", "Camion liviano" };

        public List<string> CbCamion
        {
            get { return cbCamion; }
            set { cbCamion = value; NotifyOfPropertyChange(() => CbCamion); }
        }

        private string txtObservaciones;

        public string TxtObservaciones
        {
            get { return txtObservaciones; }
            set { txtObservaciones = value; NotifyOfPropertyChange(() => TxtObservaciones); }
        }

        private string seleccionadoTipo;

        public string SeleccionadoTipo
        {
            get { return seleccionadoTipo; }
            set { seleccionadoTipo = value; NotifyOfPropertyChange(() => SeleccionadoTipo); }
        }

        private string seleccionadoCamion;
        
        public string SeleccionadoCamion
        {
            get { return seleccionadoCamion; }
            set { seleccionadoCamion = value; NotifyOfPropertyChange(() => SeleccionadoCamion); }
        }

        public void GuardarGuiaDeRemision()
        {
            int k;
            GuiaRemision g = new GuiaRemision();
            g.CodGuiaRem = txtCodigo;
            g.FechaReg = txtFechaReg;
            g.Tipo = SeleccionadoTipo;
            g.Conductor = txtConductor;
            g.Camion = SeleccionadoCamion;
            g.DirPartida = txtDirPartida;
            g.DirLlegada = txtDirLlegada;
            g.Observaciones = txtObservaciones;

            if (indicador == 1)
            {
                MessageBox.Show("codGuia :" + g.CodGuiaRem + " fechaReg: " + g.FechaReg + " tipoGuia: " + seleccionadoTipo + " Camion: " + seleccionadoCamion);
                 k = DataObjects.Almacen.GuiaDeRemisionSQL.agregarGuiaDeRemision(g);

                if (k == 0)
                    MessageBox.Show("Ocurrio un error");
                else
                    MessageBox.Show("Guia de Remision Registrada \n\nCodigo = " + txtCodigo + "\nFecha de Registro = " + txtFechaReg + "\nDireccion Partida = " + txtDirPartida +
                                    "\nDireccion Llegada = " + txtDirLlegada + "\nTipo Guia = " + seleccionadoTipo + "\nConductor = " + txtConductor + "\nCamion= " +
                                    seleccionadoCamion + "\nObservaciones = " + txtObservaciones);
                 
                                       

            }

            if (indicador == 2)
            {

                k = DataObjects.Almacen.GuiaDeRemisionSQL.editarGuiaDeRemision(g);

                if (k == 0)
                    MessageBox.Show("Ocurrio un error");
                else
                    MessageBox.Show("Guia de Remision Registrada \n\nCodigo = " + txtCodigo + "\nFecha de Registro = " + txtFechaReg + "\nDireccion Partida = " + txtDirPartida +
                                    "\nDireccion Llegada = " + txtDirLlegada + "\nTipo Guia = " + cbTipo + "\nConductor = " + txtConductor + "\nCamion= " +
                                     cbCamion + "\nObservaciones = " + txtObservaciones);

            }
            
        }

    }
}
