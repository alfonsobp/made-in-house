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
            lstUsuario = DataObjects.Seguridad.UsuarioSQL.BuscarUsuario("Lalala", 0, DateTime.Today, DateTime.Today);//CodEmpleado, IdRol, FechaRegIni, FechaRegFin
            NotifyOfPropertyChange("LstUsuario");
        }

        public void AbrirEditarUsuario()
        {
            MessageBox.Show("Usuario seleccionado" + usuarioSeleccionado.CodEmpleado);
            win.ShowWindow(new Seguridad.RegistrarUsuarioViewModel(this,usuarioSeleccionado));

        }
    }
}