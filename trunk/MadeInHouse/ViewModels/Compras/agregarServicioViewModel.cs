using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Model;
using MadeInHouse.Views.Compras;
using System.Windows;
using System.Collections.ObjectModel;
using MadeInHouse.Manager;

namespace MadeInHouse.ViewModels.Compras
{
    class agregarServicioViewModel : Screen
    {

        public agregarServicioViewModel(Servicio s)
        {

            txtCodigo = s.CodServicio;
            txtNombre = s.Nombre;
            txtProveedor = Manager.ServicioManager.getCODfromProv(s.IdProveedor);
            txtDescripcion = s.Descripcion;

            //Editar servicio 
            indicador = 2;
        }


        public agregarServicioViewModel()
        {
            //Insertar servicio
            indicador = 1;
        }

        EntityManager eM = new TableManager().getInstance(EntityName.Servicio);
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
            Servicio s = new Servicio();
            s.IdProveedor = Manager.ServicioManager.getIDfromProv(TxtProveedor);
            s.Nombre = TxtNombre;
            s.Descripcion = TxtDescripcion;
            s.CodServicio = TxtCodigo;

            if (indicador == 1)
            {
                k = eM.Agregar(s);

                if (k == 0)
                    MessageBox.Show("Ocurrio un error");
                else
                    MessageBox.Show("Servicio Registrado \n\nCodigo = " + txtCodigo + "\nNombre = " + txtNombre +
                                    "\nProveedor = " + txtProveedor + "\nDescripcion = " + txtDescripcion);
            }

            if (indicador == 2)
            {
                
                k = eM.Actualizar(s);

                if (k == 0)
                    MessageBox.Show("Ocurrio un error");
                else
                    MessageBox.Show("Servicio Editado \n\nCodigo = " + txtCodigo + "\nNombre = " + txtNombre +
                                    "\nProveedor = " + txtProveedor + "\nDescripcion = " + txtDescripcion);

            }

        }

        public void EliminarServicio()
        {

        }

    }
}
