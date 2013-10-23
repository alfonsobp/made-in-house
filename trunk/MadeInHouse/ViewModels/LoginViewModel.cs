using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Diagnostics;
using System.Windows;

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
        


        public void enter()
        {
            int k;

            k = DataObjects.Seguridad.UsuarioSQL.autenticarUsuario(TxtUser, TxtPasswordUser);

            if (k == 0)
                MessageBox.Show("Ocurrio un error");
            else{
                //MessageBox.Show("¡Bienvenido!");

                WindowManager win = new WindowManager();

                MainViewModel main = new MainViewModel();
                win.ShowWindow(main);
                this.TryClose();
            }

            //Trace.WriteLine("textooooooo");
            //Trace.WriteLine("<" + TxtUser + ">");
            //Trace.WriteLine("<" + TxtPasswordUser + ">");


        }
    }
}
