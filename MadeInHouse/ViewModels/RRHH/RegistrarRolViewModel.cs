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
using MadeInHouse.Models.RRHH;
using MadeInHouse.Views.RRHH;

namespace MadeInHouse.ViewModels.RRHH
{
    public class RegistrarRolViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private MyWindowManager win = new MyWindowManager();

        private int indicador;
        //indicador= 1: Insertar
        //indicador= 2: Editar

        public int Indicador
        {
            get { return indicador; }
        }
        
        private string txtNombRol;

        public string TxtNombRol
        {
            get { return txtNombRol; }
            set { txtNombRol = value; NotifyOfPropertyChange(() => TxtNombRol); }
        }

        private string txtDesc;

        public string TxtDesc
        {
            get { return txtDesc; }
            set { txtDesc = value; NotifyOfPropertyChange(() => TxtDesc); }
        }

        private string cmbModulo;

        public string CmbModulo
        {
            get { return cmbModulo; }
            set { cmbModulo = value; NotifyOfPropertyChange(() => CmbModulo); }
        }

        private int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        
        //Insertar:
        public RegistrarRolViewModel()
        {
            indicador = 1;
        }

        //Editar:
        public RegistrarRolViewModel(Rol r)
        {
            indicador = 2;

            txtNombRol = r.NombRol;
            txtDesc = r.Descripcion;
            cmbModulo = r.Modulo;
            estado = r.Estado;
        }

        public void GuardarRol()
        {
            {
                int k;
                Rol r = new Rol();

                //idRol: autogenerado
                r.NombRol = txtNombRol;
                r.Descripcion = txtNombRol;
                r.Modulo = cmbModulo;
                //estado: 1

                //INSERTAR NUEVO MÓDULO:
                if (indicador == 1)
                {
                    k = DataObjects.RrhhSQL.insertarRol(r);

                    if (k == 0)
                        MessageBox.Show("Ocurrio un error");
                    else
                        MessageBox.Show("Rol Registrado \n\n Módulo: " + txtNombRol + "\n Descripcion: " + txtDesc +
                                    "\n Módulo = " + "HARDCODEADO xD");
                }

                //ACTUALIZA UN MÓDULO:
                if (indicador == 2)
                {
                    k = DataObjects.RrhhSQL.actualizarRol(r);

                    if (k == 0)
                        MessageBox.Show("Ocurrio un error");
                    else
                        MessageBox.Show("Rol Registrado \n\n Módulo: " + txtNombRol + "\n Descripcion: " + txtDesc +
                                    "\n Módulo = " + "HARDCODEADO xD");
                }

            }
        }

    }

}
