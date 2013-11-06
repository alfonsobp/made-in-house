using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel.Composition;
using MadeInHouse.Models;
using MadeInHouse.Models.Seguridad;
using MadeInHouse.DataObjects.Seguridad;
using System.Windows.Controls;

namespace MadeInHouse.ViewModels.RRHH
{
    public class MantenerRolViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private MyWindowManager win = new MyWindowManager();

        private Rol rolSeleccionado;

        public void SelectedItemChanged(object sender)
        {
            rolSeleccionado = ((sender as DataGrid).SelectedItem as Rol);
        }

        public MantenerRolViewModel()
        {
            ActualizarListaRol();
        }

        public void AbrirRegistrarRol()
        {
            win.ShowWindow(new RRHH.RegistrarRolViewModel { });
        }

        //GRILLA
        private List<Rol> lstRol;

        public List<Rol> LstRol
        {
            get { return lstRol; }
            set { lstRol = value; NotifyOfPropertyChange(() => LstRol); }
        }

        public void ActualizarListaRol()
        {
            LstRol = DataObjects.Seguridad.RolSQL.ListarRoles();
            NotifyOfPropertyChange("LstRol");
        }

        public void AbrirEditarRol()
        {

        }




        public void EliminarListaRol()
        {
            for (int i = 0; i < LstRol.Count; i++)
            {
                if (LstRol[i].Estado == 1)
                {
                    RolSQL.eliminarRol(LstRol[i]);
                }
            }
            ActualizarListaRol();
        }

        public void RefrescarRol()
        {
            ActualizarListaRol();
        }


    }
}
