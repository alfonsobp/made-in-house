using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;
using MadeInHouse.Models.Compras;
using MadeInHouse.Views.Compras;
using System.Windows;
using System.Collections.ObjectModel;

namespace MadeInHouse.ViewModels.Compras
{
    class agregarServicioViewModel : Screen
    {

        public agregarServicioViewModel(Servicio s)
        {

            txtCodigo = s.Codigo;
            txtNombre = s.Nombre;
            txtProveedor = s.Proveedor;
            txtDescripcion = s.Descripcion;

            //Editar servicio 
            indicador = 2;
        }


        public agregarServicioViewModel()
        {
            //Insertar servicio
            indicador = 1;
        }


        private int indicador;

        private string txtCodigo;

        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }

        private string txtNombre;

        public string TxtNombre
        {
            get { return txtNombre; }
            set { txtNombre = value; NotifyOfPropertyChange(() => TxtNombre); }
        }

        private string txtProveedor;

        public string TxtProveedor
        {
            get { return txtProveedor; }
            set { txtProveedor = value; NotifyOfPropertyChange(() => TxtProveedor); }
        }

        private string txtDescripcion;

        public string TxtDescripcion
        {
            get { return txtDescripcion; }
            set { txtDescripcion = value; NotifyOfPropertyChange(() => TxtDescripcion); }
        }


        public void GuardarServicio()
        {
            int k;
            Servicio s = new Servicio(txtCodigo, txtNombre, txtDescripcion, txtProveedor);


            if (indicador == 1)
            {
                k = DataObjects.ComprasSQL.agregarServicio(s);

                if (k == 0)
                    MessageBox.Show("Ocurrio un error");
                else
                    MessageBox.Show("Servicio Registrado \n\nCodigo = " + txtCodigo + "\nNombre = " + txtNombre +
                                    "\nProveedor = " + txtProveedor + "\nDescripcion = " + txtDescripcion);
            }

            if (indicador == 2)
            {

                k = DataObjects.ComprasSQL.editarServicio(s);

                if (k == 0)
                    MessageBox.Show("Ocurrio un error");
                else
                    MessageBox.Show("Servicio Registrado \n\nCodigo = " + txtCodigo + "\nNombre = " + txtNombre +
                                    "\nProveedor = " + txtProveedor + "\nDescripcion = " + txtDescripcion);

            }

        }

        public void EliminarServicio()
        {

        }

    }
}
