using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel.Composition;
using MadeInHouse.Models;
using MadeInHouse.Models.RRHH;
using MadeInHouse.Views.RRHH;
using MadeInHouse.DataObjects.Seguridad;

using MadeInHouse.Models.Seguridad;

namespace MadeInHouse.ViewModels.RRHH
{
    public class RegistrarRolViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private MyWindowManager win = new MyWindowManager();

        private int indicador;
        //indicador= 1: Insertar
        //indicador= 2: Editar

        public int Indicador
        {
            get { return indicador; }
        }
        
        private string txtNombRol;

        public string TxtNombRol
        {
            get { return txtNombRol; }
            set { txtNombRol = value; NotifyOfPropertyChange(() => TxtNombRol); }
        }

        private string txtDesc;

        public string TxtDesc
        {
            get { return txtDesc; }
            set { txtDesc = value; NotifyOfPropertyChange(() => TxtDesc); }
        }

        private int cmbModulo;

        public int CmbModulo
        {
            get { return cmbModulo; }
            set { cmbModulo = value; NotifyOfPropertyChange(() => CmbModulo); }
        }

        /*
        private int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        */
        
        //Insertar:
        public RegistrarRolViewModel()
        {
            indicador = 1;

            AccModuloSQL moduloSQL = new AccModuloSQL();
            LstModulo = moduloSQL.ListarAccModulo();
        }

        //Editar:
        public RegistrarRolViewModel(Rol r)
        {
            indicador = 2;

            txtNombRol = r.Nombre;
            txtDesc = r.Descripcion;
            //estado = r.Estado;
        }

        private int idModuloValue;

        public int IdModuloValue
        {
            get { return idModuloValue; }
            set { idModuloValue = value; NotifyOfPropertyChange(() => IdModuloValue); }
        }

        private BindableCollection<AccModulo> lstModulo;

        public BindableCollection<AccModulo> LstModulo
        {
            get { return lstModulo; }
            set
            {
                if (this.lstModulo == value)
                {
                    return;
                }
                this.lstModulo = value;

                this.NotifyOfPropertyChange(() => this.lstModulo);
            }
        }


        public void GuardarRol()
        {
            int k;
            Rol r = new Rol();

            //idRol: autogenerado
            r.Nombre = txtNombRol;
            r.Descripcion = txtDesc;
            //r.Modulo = IdModuloValue;
            r.Estado = 1;   //Existencia Lógica


            //INSERTAR NUEVO MÓDULO:
            if (indicador == 1)
            {
                k = DataObjects.Seguridad.RolSQL.insertarRol(r);

                if (k == 0)
                    MessageBox.Show("Ocurrio un error");
                else
                {
                    MessageBox.Show("Rol Registrado \n\n Módulo: " + txtNombRol + "\n Descripcion: " + txtDesc +
                                "\n Módulo = " + "HARDCODEADO xD");
                    //int maxIdRol = DataObjects.UtilesSQL.ObtenerMaximoID("Rol","idRol");
                    //int j = DataObjects.RrhhSQL.insertarRolxAccModulo(maxIdRol, IdModuloValue);
                }
            }

            //ACTUALIZA UN MÓDULO:
            if (indicador == 2)
            {
                k = DataObjects.Seguridad.RolSQL.actualizarRol(r);

                if (k == 0)
                    MessageBox.Show("Ocurrio un error");
                else
                    MessageBox.Show("Rol Registrado \n\n Módulo: " + txtNombRol + "\n Descripcion: " + txtDesc +
                                "\n Módulo = " + "HARDCODEADO xD");
            }
        }
    }
}
