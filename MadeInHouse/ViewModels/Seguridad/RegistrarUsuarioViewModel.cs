using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Seguridad;
using MadeInHouse.Models.RRHH;
using MadeInHouse.Views.Seguridad;
using System.Windows;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using MadeInHouse.DataObjects.Seguridad;

namespace MadeInHouse.ViewModels.Seguridad
{
    class RegistrarUsuarioViewModel : PropertyChangedBase
    {
        public RegistrarUsuarioViewModel()
        {
            RolSQL rolSQL = new RolSQL();
            LstRol = rolSQL.ListarRol();
            indicador = 1;
        }


        public RegistrarUsuarioViewModel(MantenerUsuarioViewModel m)
        {
            RolSQL rolSQL = new RolSQL();
            LstRol = rolSQL.ListarRol();
            indicador = 1;

        }

        public RegistrarUsuarioViewModel(MantenerUsuarioViewModel m, Usuario u)
        {
            RolSQL rolSQL = new RolSQL();
            LstRol = rolSQL.ListarRol();
            txtCodUsuario = u.CodEmpleado.ToString();
            //Deshabilitar escritura en txtCodUsuario
            //BTxtCodUsuario = u.CodEmpleado;
            IdRolValue = u.Rol.IdRol;
            indicador = 2;
        }

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

        public void VerificarUsuario()
        {
            int k = 0;
            Empleado e = new Empleado();
            if (!String.IsNullOrWhiteSpace(TxtCodUsuario))
            {
                k = DataObjects.Seguridad.UsuarioSQL.BuscarUsuarioPorCodigo(TxtCodUsuario);

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


        public void GuardarUsuario()
        {
            //String.Compare(TxtContrasenhaTB, TxtContrasenhaTB2) == 0 && !String.IsNullOrWhiteSpace(TxtCodUsuario) && !String.IsNullOrWhiteSpace(TxtContrasenhaTB)
            if (String.Compare(TxtContrasenhaTB, TxtContrasenhaTB2) == 0 && !String.IsNullOrWhiteSpace(TxtCodUsuario) && !String.IsNullOrWhiteSpace(TxtContrasenhaTB) && IdRolValue != 0)
            {
                int k = 0;

                CifrarAES cifradoAES = new CifrarAES();

                string ContrasenhaCifrada = cifradoAES.cifrarTextoAES(TxtContrasenhaTB, "MadeInHouse",
                        "MadeInHouse", "MD5", 22, "1234567891234567", 128);

                Usuario u = new Usuario();
                u.CodEmpleado = txtCodUsuario;

                u.Contrasenha = ContrasenhaCifrada;

                //u.IdRol = IdRolValue;
                //MessageBox.Show(""+IdRolValue);
                u.Rol = RolSQL.buscarRolPorId(IdRolValue);
                //MessageBox.Show(""+u.Rol.IdRol);
                u.Estado = 1;

                //REGISTRAR USUARIO
                if (indicador == 1)
                {
                    //debe existir y estar disponible

                    int existe = DataObjects.Seguridad.UsuarioSQL.BuscarUsuarioPorCodigo(TxtCodUsuario);

                    //Empleado existente:
                    if (existe == 1)
                    {
                        int dis = 0;
                        dis = DataObjects.Seguridad.UsuarioSQL.DisponibilidadUsuario(TxtCodUsuario);

                        if (dis == 1)
                        {
                            //Está disponible
                            MessageBox.Show("¡Empleado Generado con Éxito!");
                            k = DataObjects.Seguridad.UsuarioSQL.agregarUsuario(u);
                        }
                        else
                            MessageBox.Show("El usario NO está disponible");
                    }
                    else
                    {
                        MessageBox.Show("El Empleado NO Existe");
                    }

                }
                //EDITAR USUARIO
                if (indicador == 2)
                {
                    if (TxtContrasenhaTB == null || TxtContrasenhaTB2 == null)
                        u.Contrasenha = DataObjects.Seguridad.UsuarioSQL.buscarPass(u.CodEmpleado);

                    k = DataObjects.Seguridad.UsuarioSQL.EditarUsuario(u);
                    if (k == 0)
                        MessageBox.Show("Ocurrio un error");
                }


                //else
                //MessageBox.Show("Usuario Registrado \n\nCodigo = " + txtCodUsuario + "\nContraseña = " + TxtContrasenhaTB);
            }

            else
            {
                MessageBox.Show("Contraseñas diferentes o campos vacíos");
            }


        }

    }
}
