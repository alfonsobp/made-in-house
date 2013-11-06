using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Diagnostics;
using System.Windows;
using System.Security.Principal;
using MadeInHouse.DataObjects.Seguridad;
using MadeInHouse.Models.Seguridad;
using System.Threading;

namespace MadeInHouse.ViewModels
{
    public class LoginViewModel : Conductor<IScreen>.Collection.OneActive
    {
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

        string response;

        public string Response
        {

            get { return this.response; }

            set
            {
                if (this.response == value)
                    return;

                this.response = value;
                NotifyOfPropertyChange("Response");
            }
        }


        public void enter()
        {
            Usuario u = new Usuario();

            //INGRESO POR DEFECTO COMO USUARIO ADMIN
            if (String.IsNullOrWhiteSpace(TxtUser) || String.IsNullOrWhiteSpace(TxtPasswordUser)) 
            {
                AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.UnauthenticatedPrincipal);

                IIdentity usuario = new GenericIdentity("" + DataObjects.Seguridad.UsuarioSQL.buscarUsuarioPorCodEmpleado("ADMIN").IdUsuario, "Database");
                

                string[] rol = { "idRolAllenar", "otrorol" };

                GenericPrincipal credencial = new GenericPrincipal(usuario, rol);

                System.Threading.Thread.CurrentPrincipal = credencial;

                WindowManager win = new WindowManager();

                MainViewModel main = new MainViewModel();
                win.ShowWindow(main);
                this.TryClose();
            }

            else
            {
                int verificado;

                verificado = DataObjects.Seguridad.UsuarioSQL.autenticarUsuario(TxtUser, TxtPasswordUser);
                u = DataObjects.Seguridad.UsuarioSQL.buscarUsuarioPorCodEmpleado(TxtUser);
                //0 = Incorrecto
                //1 = Correcto

                if (verificado == 1)
                {
                    if (u.EstadoHabilitado == 1)
                    {
                        AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.UnauthenticatedPrincipal);

                        IIdentity usuario = new GenericIdentity("" + DataObjects.Seguridad.UsuarioSQL.buscarUsuarioPorCodEmpleado(TxtUser).IdUsuario, "Database");

                        string[] rol = { "idRolAllenar", "otrorol" };

                        GenericPrincipal credencial = new GenericPrincipal(usuario, rol);

                        System.Threading.Thread.CurrentPrincipal = credencial;

                        WindowManager win = new WindowManager();

                        MainViewModel main = new MainViewModel();
                        win.ShowWindow(main);
                        this.TryClose();
                    }
                    else
                        MessageBox.Show("Usted fue inhabilitado");
                }

                else
                {
                    Response = "Datos incorrectos";
                }
            }

        }
 

    }
}
