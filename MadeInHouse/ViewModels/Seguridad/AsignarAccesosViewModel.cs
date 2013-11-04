using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Seguridad;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel.Composition;
using MadeInHouse.Models;
using MadeInHouse.DataObjects.Seguridad;
using System.Diagnostics;


namespace MadeInHouse.ViewModels.Seguridad
{
    public class AsignarAccesosViewModel : Conductor<IScreen>.Collection.OneActive
    {

        private MyWindowManager win = new MyWindowManager();

        private Usuario u = new Usuario();

        public AsignarAccesosViewModel()
        {
            u = DataObjects.Seguridad.UsuarioSQL.buscarUsuarioPorIdUsuario(Int32.Parse(Thread.CurrentPrincipal.Identity.Name));

            RolSQL rolSQL = new RolSQL();
            LstRol = rolSQL.ListarRol();

            AccModuloSQL moduloSQL = new AccModuloSQL();
            LstAccModulo = moduloSQL.ListarAccModulo();

            ActualizarListaAccVentanaAccModulo(); 
        }

        private int idAccModuloValue;

        public int IdAccModuloValue
        {
            get { return idAccModuloValue; }
            set { idAccModuloValue = value; }
        }

        private BindableCollection<AccModulo> lstAccModulo;

        public BindableCollection<AccModulo> LstAccModulo
        {
            get { return lstAccModulo; }
            set
            {
                if (this.lstAccModulo == value)
                {
                    return;
                }
                this.lstAccModulo = value;

                this.NotifyOfPropertyChange(() => this.lstAccModulo);
            }
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

        //GRILLA

        private List<AccVentana> lstAccVentanaAccModulo;

        public List<AccVentana> LstAccVentanaAccModulo
        {
            get { return lstAccVentanaAccModulo; }
            set { lstAccVentanaAccModulo = value; NotifyOfPropertyChange(() => LstAccVentanaAccModulo); }
        }

        public void GuardarRol()
        {
            //DataObjects.Seguridad.AccVentanaSQL.ActualizarRol(LstAccVentanaAccModulo, IdRolValue);
            DataObjects.Seguridad.AccVentanaSQL.QuitarAccesosVentana(IdRolValue);
            DataObjects.Seguridad.AccVentanaSQL.AsignarAccesosVentana(LstAccVentanaAccModulo, IdRolValue);
        }

        public void ActualizarLstAccesos()
        {
            LstAccVentanaAccModulo = DataObjects.Seguridad.AccVentanaSQL.ListarAccVentanaPorRol(LstAccVentanaAccModulo, IdRolValue);
            NotifyOfPropertyChange("LstAccVentanaAccModulo");
        }

        public void ActualizarListaAccVentanaAccModulo()
        {
            LstAccVentanaAccModulo = DataObjects.Seguridad.AccVentanaSQL.ListarAccVentana();
            NotifyOfPropertyChange("LstAccVentanaAccModulo");
        }


    }

}
