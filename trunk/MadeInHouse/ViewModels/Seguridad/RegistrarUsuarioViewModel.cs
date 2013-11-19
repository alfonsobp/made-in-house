using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Seguridad;
using MadeInHouse.Models.RRHH;
using MadeInHouse.Views.Seguridad;
using System.Windows;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;

using System.Net.Mail;
using System.Net.Mime;
using System.Net;

using MadeInHouse.DataObjects.Seguridad;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;

using System.ComponentModel;


namespace MadeInHouse.ViewModels.Seguridad
{
    class RegistrarUsuarioViewModel : Screen, IDataErrorInfo
    {
        public RegistrarUsuarioViewModel()
        {
            //REGISTRAR USUARIO DESDE MENU
            RolSQL rolSQL = new RolSQL();
            LstRol = rolSQL.ListarRol();
            indicador = 1;
            Util util = new Util();
            LstEstHabilitado = util.ListarEstados();
            IsEnabledResetContrasenha = false;
            IsEnabledCodEmpleado = true;
            IsEnabledVerificar = true;
            estHabilitadoValue = 1;

            Tienda deft = new Tienda();
            deft.Nombre = "ALMACEN CENTRAL";
            deft.IdTienda = 0;

            //deft.IdTienda = UsuarioSQL.buscarUsuarioPorIdUsuario(Int32.Parse(Thread.CurrentPrincipal.Identity.Name)).IdTienda;//Jalar del usuario
            TiendaSQL tSQL = new TiendaSQL();
            CmbTiendas = tSQL.BuscarTienda();
            CmbTiendas.Insert(0, deft);
            IndexTienda = 0;
            //SelectedTienda = 17;

        }
        public RegistrarUsuarioViewModel(MantenerUsuarioViewModel m)
        {
            //REGISTRAR USUARIO DESDE MANTENIMIENTO
            RolSQL rolSQL = new RolSQL();
            LstRol = rolSQL.ListarRol();
            indicador = 1;
            Util util = new Util();
            LstEstHabilitado = util.ListarEstados();
            IsEnabledResetContrasenha = false;
            IsEnabledCodEmpleado = true;
            IsEnabledVerificar = true;
            estHabilitadoValue = 1;

            Tienda deft = new Tienda();
            deft.Nombre = "ALMACEN CENTRAL";
            deft.IdTienda = 0;

            //deft.IdTienda = UsuarioSQL.buscarUsuarioPorIdUsuario(Int32.Parse(Thread.CurrentPrincipal.Identity.Name)).IdTienda;//Jalar del usuario
            TiendaSQL tSQL = new TiendaSQL();
            CmbTiendas = tSQL.BuscarTienda();
            CmbTiendas.Insert(0, deft);
            IndexTienda = 0;
            //SelectedTienda = 17;
        }
        public RegistrarUsuarioViewModel(MantenerUsuarioViewModel m, Usuario u)
        {
            //EDITAR USUARIO
            IPHostEntry IPHost = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            RolSQL rolSQL = new RolSQL();
            LstRol = rolSQL.ListarRol();
            txtCodUsuario = u.CodEmpleado.ToString();
            //Deshabilitar escritura en txtCodUsuario
            //BTxtCodUsuario = u.CodEmpleado;
            IdRolValue = u.Rol.IdRol;
            indicador = 2;
            Util util = new Util();
            LstEstHabilitado = util.ListarEstados();
            EstHabilitadoValue = u.EstadoHabilitado;
            IsEnabledCodEmpleado = false;
            IsEnabledResetContrasenha = true;
            IsEnabledVerificar = false;
            usuarioSeleccionado = u;

            Tienda deft = new Tienda();
            deft.Nombre = "ALMACEN CENTRAL";
            deft.IdTienda = 0;

            //deft.IdTienda = UsuarioSQL.buscarUsuarioPorIdUsuario(Int32.Parse(Thread.CurrentPrincipal.Identity.Name)).IdTienda;//Jalar del usuario
            TiendaSQL tSQL = new TiendaSQL();
            CmbTiendas = tSQL.BuscarTienda();
            CmbTiendas.Insert(0, deft);
            IndexTienda = 0;
            SelectedTienda = usuarioSeleccionado.IdTienda;
        }
        private Usuario usuarioSeleccionado;
        private int indicador = 0;
        //Binding TxtCodUsuario
        string bTxtCodUsuario;
        public string BTxtCodUsuario
        {
            get { return this.bTxtCodUsuario; }
            set
            {
                if (this.bTxtCodUsuario == value)
                    return;
                this.bTxtCodUsuario = value;
                NotifyOfPropertyChange("BTxtCodUsuario");
            }
        }
        private string lblError;
        public string LblError
        {
            get { return lblError; }
            set
            {
                if (lblError == value)
                    return;
                lblError = value; NotifyOfPropertyChange("LblError");
            }
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
        private bool isEnabledResetContrasenha;
        public bool IsEnabledResetContrasenha
        {
            get { return isEnabledResetContrasenha; }
            set
            {
                if (isEnabledResetContrasenha == value)
                    return;
                isEnabledResetContrasenha = value;
                NotifyOfPropertyChange("IsEnabledResetContrasenha");
            }
        }
        private bool isEnabledCodEmpleado;
        public bool IsEnabledCodEmpleado
        {
            get { return isEnabledCodEmpleado; }
            set
            {
                if (isEnabledCodEmpleado == value)
                    return;
                isEnabledCodEmpleado = value;
                NotifyOfPropertyChange("IsEnabledCodEmpleado");
            }
        }
        private bool isEnabledVerificar;
        public bool IsEnabledVerificar
        {
            get { return isEnabledVerificar; }
            set
            {
                if (isEnabledVerificar == value)
                    return;
                isEnabledVerificar = value;
                NotifyOfPropertyChange("IsEnabledVerificar");
            }
        }
        
        private List<EstadoHabilitado> lstEstHabilitado;
        public List<EstadoHabilitado> LstEstHabilitado
        {
            get { return lstEstHabilitado; }
            set
            {
                if (this.lstEstHabilitado == value)
                {
                    return;
                }
                this.lstEstHabilitado = value;
                this.NotifyOfPropertyChange(() => this.lstEstHabilitado);
            }
        }
        private int estHabilitadoValue;
        public int EstHabilitadoValue
        {
            get { return estHabilitadoValue; }
            set { estHabilitadoValue = value; NotifyOfPropertyChange(() => EstHabilitadoValue); }
        }

        private List<Tienda> cmbTiendas;

        public List<Tienda> CmbTiendas
        {
            get { return cmbTiendas; }
            set
            {
                if (this.cmbTiendas == value)
                {
                    return;
                }

                cmbTiendas = value;
                NotifyOfPropertyChange(() => CmbTiendas);
            }
        }

        private int selectedTienda;

        public int SelectedTienda
        {
            get { return selectedTienda; }
            set { selectedTienda = value; }
        }

        private int indexTienda;

        public int IndexTienda
        {
            get { return indexTienda; }
            set
            {
                indexTienda = value;
                NotifyOfPropertyChange(() => IndexTienda);
            }
        }

        #region metodos

        public void VerificarUsuario()
        {
            int k = 0;
            Empleado e = new Empleado();
            if (!String.IsNullOrWhiteSpace(TxtCodUsuario))
            {
                k = DataObjects.Seguridad.UsuarioSQL.GetIdUsuario(TxtCodUsuario);
                //Si el Empleado existe:
                if (k == 1)
                {
                    int dis = 0;
                    dis = DataObjects.Seguridad.UsuarioSQL.DisponibilidadUsuario(TxtCodUsuario);
                    if (dis == 1)
                    {
                        //Está disponible
                        e = DataObjects.RRHH.EmpleadoSQL.DatosBasicosEmpleado(TxtCodUsuario);
                        MessageBox.Show("Enhora buena, está Disponible!\n\n" + "Empleado:\n\t" + e.Nombre + " " + e.ApePaterno + " " + e.ApeMaterno);
                    }
                    else
                    {
                        //NO Puede, ya está asociado a un usuario
                        MessageBox.Show("El Empleado ya fue asignado a un usuario");
                    }
                }
                else
                    // k == 0:
                    MessageBox.Show("NO existe un Empleado con ese Código.");
            }
            else
                LblError = "Ingrese Código Usuario";
        }

        public void ResetContrasenha()
        {
            UsuarioSQL uSQL = new UsuarioSQL();
            Util util = new Util();
            string contrasenhaGenerada = util.generarContrasenha();

            Empleado e = new Empleado();
            e = DataObjects.RRHH.EmpleadoSQL.DatosBasicosEmpleado(TxtCodUsuario);

            Usuario u = new Usuario();
            u = uSQL.buscarUsuarioPorCodEmpleado(TxtCodUsuario);
            

            CifrarAES cifradoAES = new CifrarAES();
            string contrasenhaCifrada = cifradoAES.cifrarTextoAES(contrasenhaGenerada, "MadeInHouse",
                    "MadeInHouse", "MD5", 22, "1234567891234567", 128);

            u.Contrasenha = contrasenhaCifrada;
            UsuarioSQL.EditarUsuario(u);

            EnviarCorreo(e, contrasenhaGenerada);

            //MessageBox.Show("PASS: "+util.generarContrasenha());
            
        }
        public void GuardarUsuario()
        {
            if (validar())
            {

                //String.Compare(TxtContrasenhaTB, TxtContrasenhaTB2) == 0 && !String.IsNullOrWhiteSpace(TxtCodUsuario) && !String.IsNullOrWhiteSpace(TxtContrasenhaTB)
                Util util = new Util();
                int k = 0;
                CifrarAES cifradoAES = new CifrarAES();
                string contrasenhaGenerada = util.generarContrasenha();
                string contrasenhaCifrada = cifradoAES.cifrarTextoAES(contrasenhaGenerada, "MadeInHouse",
                        "MadeInHouse", "MD5", 22, "1234567891234567", 128);
                //MessageBox.Show("PASS: " + contrasenhaGenerada);

                Usuario u = new Usuario();
                u.CodEmpleado = txtCodUsuario;
                u.EstadoHabilitado = 1;
                u.Contrasenha = contrasenhaCifrada;
                u.Estado = 1;
                u.Rol = RolSQL.buscarRolPorId(IdRolValue);
                u.IdTienda = SelectedTienda;
                if (indicador == 1)
                {
                    //debe existir y estar disponible
                    if (IdRolValue != 0)
                    {
                        int existe = DataObjects.Seguridad.UsuarioSQL.GetIdUsuario(TxtCodUsuario);
                        //Empleado existente:
                        if (existe == 1)
                        {
                            int dis = 0;
                            dis = DataObjects.Seguridad.UsuarioSQL.DisponibilidadUsuario(TxtCodUsuario);
                            if (dis == 1)
                            {
                                //Está disponible

                                //FALTA VALIDACION DE ENVIO DE CORREO

                                k = DataObjects.Seguridad.UsuarioSQL.agregarUsuario(u);

                                if (k == 1)
                                {
                                    Empleado e = new Empleado();
                                    e = DataObjects.RRHH.EmpleadoSQL.DatosBasicosEmpleado(TxtCodUsuario);
                                    EnviarCorreo(e, contrasenhaGenerada);
                                    MessageBox.Show("¡Empleado registrado con Éxito!");
                                }

                                //1: Agregar, 2: Editar, 3: Eliminar, 4: Recuperar, 5: Desactivar
                                DataObjects.Seguridad.LogSQL.RegistrarActividad("Registrar Usuario", u.CodEmpleado, 1);
                            }
                            else
                                MessageBox.Show("El usuario NO está disponible");
                        }
                        else
                        {
                            MessageBox.Show("El Empleado NO Existe");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Seleccione un rol", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                //EDITAR USUARIO
                if (indicador == 2)
                {
                    usuarioSeleccionado.Contrasenha = UsuarioSQL.buscarPass(u.CodEmpleado);
                    usuarioSeleccionado.Rol.IdRol = IdRolValue;
                    usuarioSeleccionado.EstadoHabilitado = EstHabilitadoValue;
                    usuarioSeleccionado.IdTienda = SelectedTienda;
                    //MessageBox.Show("Id Tienda: " + SelectedTienda);
                    //usuarioSeleccionado.Estado = 1;
                    int e = UsuarioSQL.EditarUsuario(usuarioSeleccionado);

                    if (e == 1)
                    {
                        MessageBox.Show("¡Empleado modificado con Éxito!");
                    }
                }
            }
        }

        //Funcion para mandar correo al proveedor
        public void EnviarCorreo(Empleado empleado,  string contrasenha)
        {

                string time;

                if (DateTime.Now.Hour >= 12)
                    time = "tardes";
                else
                    time = "dias";

                string bodyMessage = "Buenos/as " + time + " "+empleado.Nombre + "\nSu usuario es: "+ empleado.CodEmpleado + "\nSu contraseña es: " + contrasenha;


                // Create a message and set up the recipients.

                MailMessage message = new MailMessage("madeinhouse.sw@gmail.com", empleado.EmailEmpresa,
                                                      "MadeInHouse - Contraseña generada", bodyMessage);

                message.IsBodyHtml = true;

                MessageBoxResult r = MessageBox.Show("¿Desea enviar el correo?", "EnviarCorreo",
                                                     MessageBoxButton.YesNo);

                if (r == MessageBoxResult.Yes)
                {

                    //Send the message.

                    var client = new SmtpClient("smtp.gmail.com", 587)
                    {
                        // Add credentials if the SMTP server requires them.
                      
                        Credentials = new NetworkCredential("madeinhouse.sw@gmail.com", "insignia"),//adp980407912
                        EnableSsl = true
                    };


                    try
                    {
                        client.Send(message);
                        MessageBox.Show("Mensaje enviado satisfactoriamente");
                    }

                    catch (Exception e)
                    {
                        MessageBox.Show(e.StackTrace.ToString());
                    }
                }

        }

        #endregion

        #region validacion

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case "TxtCodUsuario": if (string.IsNullOrEmpty(TxtCodUsuario)) result = "El código de usuario no puede ser vacio"; break;
                    
                };
                return result;
            }
        }

        public Boolean validar()
        {

            if (string.IsNullOrEmpty(TxtCodUsuario))
            {
                MessageBox.Show("El Código de usuario no puede ser vacio", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }


            return true;

        }

        #endregion
    }
}