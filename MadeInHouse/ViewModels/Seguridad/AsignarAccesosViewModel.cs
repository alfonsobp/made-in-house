﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Seguridad;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel.Composition;
using MadeInHouse.Models;
using MadeInHouse.DataObjects.Seguridad;
using System.Diagnostics;


namespace MadeInHouse.ViewModels.Seguridad
{
    public class AsignarAccesosViewModel : Conductor<IScreen>.Collection.OneActive
    {

        private MyWindowManager win = new MyWindowManager();

        private Usuario u = new Usuario();

        public AsignarAccesosViewModel()
        {
            //u = DataObjects.Seguridad.UsuarioSQL.buscarUsuarioPorIdUsuario(Int32.Parse(Thread.CurrentPrincipal.Identity.Name));

            RolSQL rolSQL = new RolSQL();
            LstRol = rolSQL.ListarRol();

            AccModuloSQL moduloSQL = new AccModuloSQL();
            LstAccModulo = moduloSQL.ListarAccModulo();

            AccModulo am = new AccModulo();
            am.Nombre = "Todos";
            am.Estado = 1;
            am.IdAccModulo = 0;
            LstAccModulo.Insert(0,am);
            idAccModuloValue = 0;

            ActualizarListaAccVentanaAccModulo();
        }

        private int idAccModuloValue;

        public int IdAccModuloValue
        {
            get { return idAccModuloValue; }
            set { idAccModuloValue = value; }
        }

        private BindableCollection<AccModulo> lstAccModulo;

        public BindableCollection<AccModulo> LstAccModulo
        {
            get { return lstAccModulo; }
            set
            {
                if (this.lstAccModulo == value)
                {
                    return;
                }
                this.lstAccModulo = value;

                this.NotifyOfPropertyChange(() => this.lstAccModulo);
            }
        }

        private int idRolValue;

        public int IdRolValue
        {
            get { return idRolValue; }
            set { idRolValue = value; NotifyOfPropertyChange(() => IdRolValue); }
        }

        private BindableCollection<Rol> lstRol;

        public BindableCollection<Rol> LstRol
        {
            get { return lstRol; }
            set
            {
                if (this.lstRol == value)
                {
                    return;
                }
                this.lstRol = value;
                this.NotifyOfPropertyChange(() => this.lstRol);
            }
        }

        //GRILLA
        private List<AccVentana> lstAccVentanaAccModulo;

        public List<AccVentana> LstAccVentanaAccModulo
        {
            get { return lstAccVentanaAccModulo; }
            set { lstAccVentanaAccModulo = value; NotifyOfPropertyChange(() => LstAccVentanaAccModulo); }
        }

        private int allSelected;
        public int AllSelected
        {
            get { return allSelected; }
            set
            {
                allSelected = value;
                LstAccVentanaAccModulo.ForEach(x => x.Estado = value);
                NotifyOfPropertyChange(() => AllSelected);

                //NotifyPropertyChanged(m => m.AllSelected);
            }
        }

        public void GuardarRol()
        {
            //DataObjects.Seguridad.AccVentanaSQL.ActualizarRol(LstAccVentanaAccModulo, IdRolValue);
            DataObjects.Seguridad.AccVentanaSQL.QuitarAccesosVentana(IdRolValue);
            DataObjects.Seguridad.AccVentanaSQL.AsignarAccesosVentana(LstAccVentanaAccModulo, IdRolValue);
            MessageBox.Show("¡Accesos asignados con Éxito!");
        }

        public void ActualizarLstAccesos()
        {
            ActualizarListaAccVentanaAccModulo();
            LstAccVentanaAccModulo = DataObjects.Seguridad.AccVentanaSQL.ListarAccVentanaPorRol(LstAccVentanaAccModulo, IdRolValue, IdAccModuloValue);
            NotifyOfPropertyChange("LstAccVentanaAccModulo");
        }

        public void ActualizarListaAccVentanaAccModulo()
        {
            LstAccVentanaAccModulo = DataObjects.Seguridad.AccVentanaSQL.ListarAccVentana(IdAccModuloValue);
            NotifyOfPropertyChange("LstAccVentanaAccModulo");
        }


    }

}
