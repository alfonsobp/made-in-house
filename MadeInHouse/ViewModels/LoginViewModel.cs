using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Diagnostics;
using System.Windows;
using System.Security.Principal;
using System.ComponentModel.Composition;
using MadeInHouse.DataObjects.Seguridad;
using MadeInHouse.Models.Seguridad;
using System.Net;

namespace MadeInHouse.ViewModels
{
	[Export(typeof(LoginViewModel))]
    public class LoginViewModel : Conductor<IScreen>.Collection.OneActive
    {
		private readonly IWindowManager _windowManager;

        [ImportingConstructor]
        public LoginViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
        }

        private string txtUser;
        public string TxtUser
        {
            get { return txtUser; }
            set { txtUser = value; NotifyOfPropertyChange(() => TxtUser); }
        }

        private string txtPasswordUser;
        public string TxtPasswordUser
        {
            get { return txtPasswordUser; }
            set { txtPasswordUser = value; NotifyOfPropertyChange(() => TxtPasswordUser); }
        }

        private string lblError;
        public string LblError
        {
            get { return lblError; }
            set { lblError = value; NotifyOfPropertyChange(() => LblError); }
        }

        private string response;
        public string Response
        {
            get { return this.response; }
            set
            {
                if (this.response == value) return;
                this.response = value;
                NotifyOfPropertyChange("Response");
            }
        }

        public void enter()
        {
            UsuarioSQL userSQL = new UsuarioSQL();
            Usuario u = new Usuario();

            int verificado;
            Response = "";
            //Verificar existencia de usuario

            if (userSQL.ProbarConexion())
            {
                if (TxtUser != null && TxtPasswordUser != null)
                {
                    if (UsuarioSQL.existeUsuario(TxtUser) == 1)
                    {
                        verificado = DataObjects.Seguridad.UsuarioSQL.autenticarUsuario(TxtUser, TxtPasswordUser);

                        //0 = Incorrecto
                        //1 = Correcto

                        u = userSQL.buscarUsuarioPorCodEmpleado(TxtUser);

                        if (verificado == 1)
                        {
                            if (u.EstadoHabilitado == 1)
                            {
                                AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.UnauthenticatedPrincipal);

                                IIdentity usuario = new GenericIdentity("" + UsuarioSQL.GetIdUsuario(TxtUser), "Database");

                                string[] rol = { "rol", };

                                GenericPrincipal credencial = new GenericPrincipal(usuario, rol);

                                System.Threading.Thread.CurrentPrincipal = credencial;

                                _windowManager.ShowWindow(new MainViewModel(_windowManager));

                                this.TryClose();
                            }
                            else
                            {
                                MessageBox.Show("Usted fue inhabilitado");
                            }
                            u.NumIntentos = 0;
                            DataObjects.Seguridad.UsuarioSQL.ActualizarNumIntentos(u);
                        }

                        else
                        {
                            u.NumIntentos = DataObjects.Seguridad.UsuarioSQL.GetNumIntentos(u);
                            u.NumIntentos++;
                            if (u.NumIntentos >= 4)
                            {
                                u.EstadoHabilitado = 0;
                                u.NumIntentos = 0;
                            }
                            DataObjects.Seguridad.UsuarioSQL.ActualizarNumIntentos(u);
                            //Response = "Datos incorrectos";
                            MessageBox.Show("Datos incorrectos");
                        }
                    }
                    else
                    {
                        //Response = "Datos incorrectos";
                        MessageBox.Show("Usuario inexistente");
                    }
                }
                else
                {
                    MessageBox.Show("Campos vacíos");
                }
            }
            //else
            //{
            //    MessageBox.Show("No es posible conectarse. Verifique su conexíon");
            //}
            
        }
 

    }
}
