using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Seguridad;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel.Composition;
using MadeInHouse.Models;
using MadeInHouse.DataObjects.Seguridad;


namespace MadeInHouse.ViewModels.Seguridad
{
    public class AsignarAccesosViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private MyWindowManager win = new MyWindowManager();

        public AsignarAccesosViewModel()
        {
            RolSQL rolSQL = new RolSQL();
            LstRol = rolSQL.ListarRol();

            RolSQL moduloSQL = new RolSQL();
            //LstModulo = mo
        }

        private int idModuloValue;

        public int IdModuloValue
        {
            get { return idModuloValue; }
            set { idModuloValue = value; }
        }

        private BindableCollection<Modulo> lstModulo;

        public BindableCollection<Modulo> LstModulo
        {
            get { return lstModulo; }
            set
            {
                if (this.lstModulo == value)
                {
                    return;
                }
                this.lstModulo = value;

                this.NotifyOfPropertyChange(() => this.lstModulo);
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

    }

}
