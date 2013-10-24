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

namespace MadeInHouse.ViewModels.Seguridad
{
    class RegistrarModuloViewModel : Screen
    {
        private int indicador;

        public int Indicador
        {
            get { return indicador; }
        }

        private string txtNombModulo;

        public string TxtNombModulo
        {
            get { return txtNombModulo; }
            set { txtNombModulo = value; NotifyOfPropertyChange(() => TxtNombModulo); }
        }

        private string txtDescripcion;

        public string TxtDescripcion
        {
            get { return txtDescripcion; }
            set { txtDescripcion = value; NotifyOfPropertyChange(() => TxtDescripcion); }
        }

        //Insertar:
        public RegistrarModuloViewModel()
        {
            indicador = 1;
        }

        public RegistrarModuloViewModel(Modulo m)
        {
            indicador = 2;

            txtDescripcion = m.Descripcion;
        }

        public void GuardarModulo()
        {
            int k;
            Modulo m = new Modulo();

            //idModulo: autogenerado
            m.Descripcion = txtDescripcion;
            m.Estado = 1;   //Existencia Lógica

            //INSERTAR NUEVO MÓDULO:
            if (indicador == 1)
            {
                k = DataObjects.RrhhSQL.insertarModulo(m);

                if (k == 0)
                    MessageBox.Show("Ocurrio un error");
                else
                    MessageBox.Show("Módulo Registrado \n\n Módulo: " + txtNombModulo + "\n Descripcion: " + txtDescripcion);
            }

            //ACTUALIZA UN MÓDULO:
            if (indicador == 2)
            {
                k = DataObjects.RrhhSQL.actualizarModulo(m);

                if (k == 0)
                    MessageBox.Show("Ocurrio un error");
                else
                    MessageBox.Show("Módulo Registrado \n\n Módulo: " + txtNombModulo + "\n Descripcion: " + txtDescripcion);
            }
        }

    }
}
