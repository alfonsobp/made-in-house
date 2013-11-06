using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            ActualizarListaUsuario();
        }
        private MyWindowManager win = new MyWindowManager();
        private List<Usuario> lstUsuario;
        public List<Usuario> LstUsuario
        {
            get { return lstUsuario; }
            set { lstUsuario = value; NotifyOfPropertyChange(() => LstUsuario); }
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
        public void ActualizarListaUsuario()
        {
            lstUsuario = UsuarioSQL.BuscarUsuario("Lalala", 0, DateTime.Today, DateTime.Today);
            NotifyOfPropertyChange("LstUsuario");
        }
        public void AbrirEditarUsuario()
        {
            if (usuarioSeleccionado != null)
                win.ShowWindow(new Seguridad.RegistrarUsuarioViewModel(this, usuarioSeleccionado));
        }
        public void Guardar()
        {
            for (int i = 0; i < LstUsuario.Count; i++)
            {
                if (lstUsuario[i].Estado == 1)
                {
                    lstUsuario[i].Estado = 0;
                    UsuarioSQL.EliminarUsuarios(lstUsuario[i]);
                }
            }
            for (int i = 0; i < LstUsuario.Count; i++)
            {
                UsuarioSQL.DeshabilitarUsuario(lstUsuario[i]);
                if (lstUsuario[i].EstadoHabilitado == 1)
                    UsuarioSQL.HabilitarUsuario(lstUsuario[i]);
            }
            ActualizarListaUsuario();
        }
        

        public void EliminarUsuarios()
        {
            for (int i = 0; i < LstUsuario.Count; i++)
            {
                if (lstUsuario[i].Estado == 1)
                {
                    lstUsuario[i].Estado = 0;
                    UsuarioSQL.EliminarUsuarios(lstUsuario[i]);
                }
            }
            ActualizarListaUsuario();
        }
    }
}