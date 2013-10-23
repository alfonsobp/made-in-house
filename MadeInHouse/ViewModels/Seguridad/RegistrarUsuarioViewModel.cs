using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Seguridad;
using MadeInHouse.Views.Seguridad;
using System.Windows;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;

namespace MadeInHouse.ViewModels.Seguridad
{
    class RegistrarUsuarioViewModel : Screen
    {

        public RegistrarUsuarioViewModel()
        {
        }


        private string txtCodUsuario;

        public string TxtCodUsuario
        {
            get { return txtCodUsuario; }
            set { txtCodUsuario = value; NotifyOfPropertyChange(() => TxtCodUsuario); }
        }

        private string txtContrasenhaTB;

        public string TxtContrasenhaTB
        {
            get { return txtContrasenhaTB; }
            set { txtContrasenhaTB = value; NotifyOfPropertyChange(() => TxtContrasenhaTB); }
        }

        private string txtContrasenhaTB2;

        public string TxtContrasenhaTB2
        {
            get { return txtContrasenhaTB2; }
            set { txtContrasenhaTB2 = value; NotifyOfPropertyChange(() => TxtContrasenhaTB2); }
        }

        public void GuardarUsuario()
        {
            Trace.WriteLine("textooooooo");
            Trace.WriteLine("<"+txtCodUsuario+">");
            Trace.WriteLine("<" + TxtContrasenhaTB + ">");
            Trace.WriteLine("<" + TxtContrasenhaTB2 + ">");

            if (String.Compare(TxtContrasenhaTB, TxtContrasenhaTB2) == 0 && !String.IsNullOrWhiteSpace(TxtCodUsuario) && !String.IsNullOrWhiteSpace(TxtContrasenhaTB) )
            {
                MessageBox.Show("Contraseñas iguales");

                int k;

                Usuario u = new Usuario();
                u.CodUsuario = txtCodUsuario;
                u.Contrasenha = TxtContrasenhaTB;
                u.Estado = 1;

                k = DataObjects.Seguridad.UsuarioSQL.agregarUsuario(u);

                if (k == 0)
                    MessageBox.Show("Ocurrio un error");
                else
                    MessageBox.Show("Usuario Registrado \n\nCodigo = " + txtCodUsuario + "\nContraseña = " + TxtContrasenhaTB);

            }

            else
            {
                MessageBox.Show("Contraseñas diferentes o campos vacíos");
            }

            
        }

    }
}
