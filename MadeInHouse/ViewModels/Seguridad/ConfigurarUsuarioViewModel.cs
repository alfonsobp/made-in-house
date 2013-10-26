using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Caliburn.Micro;
using System.Diagnostics;
using System.Windows; //MessageBox
using MadeInHouse.Models.Seguridad;

namespace MadeInHouse.ViewModels.Seguridad
{
    class ConfigurarUsuarioViewModel:Screen
    {
        private string txtPassActual;

        public string TxtPassActual
        {
            get {return txtPassActual; }
            set {txtPassActual=value; NotifyOfPropertyChange(() => TxtPassActual);}
        }

        private string txtPassNuevo1;

        public string TxtPassNuevo1
        {
            get { return txtPassNuevo1; }
            set { txtPassNuevo1 = value; NotifyOfPropertyChange(() => TxtPassNuevo1); }
        }

        private string txtPassNuevo2;

        public string TxtPassNuevo2
        {
            get { return txtPassNuevo2; }
            set { txtPassNuevo2 = value; NotifyOfPropertyChange(() => TxtPassNuevo2); }
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

        public int GuardarDatosUsuario(){
            //Validar campos

            if (!String.IsNullOrWhiteSpace(TxtPassActual) && !String.IsNullOrWhiteSpace(TxtPassNuevo1) && !String.IsNullOrWhiteSpace(TxtPassNuevo2))
            {
                //Validar Conntrasenha actual
                int verificado;

                verificado = DataObjects.Seguridad.UsuarioSQL.autenticarUsuario(Thread.CurrentPrincipal.Identity.Name, TxtPassActual);

                if (verificado == 1)
                {
                    //Guardar nueva contraseña
                    //Validar Contrasenha nuevas iguales

                    if (String.Compare(TxtPassNuevo1, TxtPassNuevo2) == 0)
                    {
                        CifrarAES cifradoAES = new CifrarAES();

                        string ContrasenhaCifrada = cifradoAES.cifrarTextoAES(TxtPassNuevo1, "MadeInHouse",
                                "MadeInHouse", "MD5", 22, "1234567891234567", 128);
                        Usuario u = new Usuario();
                        u = DataObjects.Seguridad.UsuarioSQL.buscarUsuarioPorCodEmpleado(Thread.CurrentPrincipal.Identity.Name);

                        u.Contrasenha = ContrasenhaCifrada;
                        u.FechaMod = DateTime.Now;
                        int k = DataObjects.Seguridad.UsuarioSQL.editarUsuario(u);

                        if (k == 0)
                            MessageBox.Show("Ocurrio un error");
                        else
                            MessageBox.Show("Usuario modificado \n\nCodigo = " + Thread.CurrentPrincipal.Identity.Name + "\nContraseña = " + u.Contrasenha);
                            //Response = "Usuario modificado con éxito";
                    }

                    else
                    {
                        Response = "Contraseñas diferentes";
                    }
                    
                }
                else
                {
                    
                    Response = "Contraseña incorrecta";
                }
                
                
            }

            else
            {
                Response = "Faltan datos";
            }
            Trace.WriteLine("PassActual: "+ TxtPassActual);
            Trace.WriteLine(Thread.CurrentPrincipal.Identity.Name);    
            
            return 1;
        }


    }
}
