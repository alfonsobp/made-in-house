using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Caliburn.Micro;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using MadeInHouse.Models;
using MadeInHouse.Models.Seguridad;
using MadeInHouse.DataObjects.Seguridad;
namespace MadeInHouse.ViewModels.Seguridad
{
    class MantenerUsuarioViewModel : Screen
    {
        public MantenerUsuarioViewModel()
        {
            Actualizar();
        }
        private MyWindowManager win = new MyWindowManager();
        private List<Usuario> lstUsuario;
        public List<Usuario> LstUsuario
        {
            get { return lstUsuario; }
            set { lstUsuario = value; NotifyOfPropertyChange(() => LstUsuario); }
        }

        private List<Usuario> lstUsuarioElim;
        public List<Usuario> LstUsuarioElim
        {
            get { return lstUsuarioElim; }
            set { lstUsuarioElim = value; NotifyOfPropertyChange(() => LstUsuarioElim); }
        }

        private Usuario usuarioSeleccionado;
        public void SelectedItemChanged(object sender)
        {
            usuarioSeleccionado = ((sender as DataGrid).SelectedItem as Usuario);
        }



        public void AbrirRegistrarUsuario()
        {
            win.ShowWindow(new Seguridad.RegistrarUsuarioViewModel(this));
        }

        public void Actualizar()
        {
            ActualizarListaUsuario();
            ActualizarListaUsuarioElim();
        }
        public void ActualizarListaUsuario()
        {
            lstUsuario = UsuarioSQL.BuscarUsuario("Lalala", 0, DateTime.Today, DateTime.Today);
            NotifyOfPropertyChange("LstUsuario");
        }
        public void ActualizarListaUsuarioElim()
        {
            //lstUsuarioElim = UsuarioSQL.BuscarUsuario("Lalala", 0, DateTime.Today, DateTime.Today);
            lstUsuarioElim = UsuarioSQL.BuscarUsuarioEliminado();
            NotifyOfPropertyChange("LstUsuarioElim");
        }


        public void AbrirEditarUsuario()
        {
            if (usuarioSeleccionado != null)
                win.ShowWindow(new Seguridad.RegistrarUsuarioViewModel(this, usuarioSeleccionado));
        }

        public void Guardar()
        {
            //Recuperar Usuarios:
            for (int i = 0; i < LstUsuarioElim.Count; i++)
            {
                if (lstUsuarioElim[i].Estado == 1)
                {
                    lstUsuarioElim[i].EstadoHabilitado = 1;
                    UsuarioSQL.ActualizarEstadoUsuarios(lstUsuarioElim[i]);
                    //1: Agregar, 2: Editar, 3: Eliminar, 4: Recuperar, 5: Desactivar
                    DataObjects.Seguridad.LogSQL.RegistrarActividad("Mantenimiento Usuario", lstUsuarioElim[i].CodEmpleado, 4);
                }
            }

            //Eliminar Usuarios:
            for (int i = 0; i < LstUsuario.Count; i++)
            {
                if (lstUsuario[i].Estado == 1)
                {
                    lstUsuario[i].Estado = 0;
                    lstUsuarioElim[i].EstadoHabilitado = 0;
                    UsuarioSQL.ActualizarEstadoUsuarios(lstUsuario[i]);
                    //1: Agregar, 2: Editar, 3: Eliminar, 4: Recuperar, 5: Desactivar
                    DataObjects.Seguridad.LogSQL.RegistrarActividad("Mantenimiento Usuario", lstUsuarioElim[i].CodEmpleado, 3);
                }
            }

            //Deshabilitar Usuarios:
            for (int i = 0; i < LstUsuario.Count; i++)
            {
                UsuarioSQL.DeshabilitarUsuario(lstUsuario[i]);
                if (lstUsuario[i].EstadoHabilitado == 1)
                {
                    UsuarioSQL.HabilitarUsuario(lstUsuario[i]);
                    //1: Agregar, 2: Editar, 3: Eliminar, 4: Recuperar, 5: Desactivar
                    DataObjects.Seguridad.LogSQL.RegistrarActividad("Mantenimiento Usuario", lstUsuarioElim[i].CodEmpleado, 5);
                }
            }
            Actualizar();
        }


        public void RecuperarUsuarios()
        {
            for (int i = 0; i < LstUsuarioElim.Count; i++)
            {
                if (lstUsuarioElim[i].Estado == 1)
                {
                    lstUsuarioElim[i].EstadoHabilitado = 1;
                    UsuarioSQL.ActualizarEstadoUsuarios(lstUsuarioElim[i]);
                    //1: Agregar, 2: Editar, 3: Eliminar, 4: Recuperar, 5: Desactivar
                    DataObjects.Seguridad.LogSQL.RegistrarActividad("Recuperar Usuario", lstUsuarioElim[i].CodEmpleado, 4);
                }
            }
            Actualizar();
        }


        public void EliminarUsuarios()
        {
            for (int i = 0; i < LstUsuario.Count; i++)
            {
                if (lstUsuario[i].Estado == 1)
                {
                    lstUsuario[i].Estado = 0;
                    lstUsuario[i].EstadoHabilitado = 0;
                    UsuarioSQL.ActualizarEstadoUsuarios(lstUsuario[i]);
                    //1: Agregar, 2: Editar, 3: Eliminar, 4: Recuperar, 5: Desactivar
                    DataObjects.Seguridad.LogSQL.RegistrarActividad("Mantenimiento Usuario", lstUsuarioElim[i].CodEmpleado, 3);
                }
            }
            Actualizar();
        }
    }
}