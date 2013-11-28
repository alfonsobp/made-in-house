using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;
using MadeInHouse.ViewModels.Layouts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(BuscarNotasViewModel))]
    class BuscarNotasViewModel : Screen
    {
        #region constructores

        [ImportingConstructor]
        public BuscarNotasViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
            cmbBox.Add("Entrada");
            cmbBox.Add("Salida");
            cmbBox.Add("MovimientoInterno");
        }

        public BuscarNotasViewModel(IWindowManager windowmanager, MantenerGuiaDeRemisionViewModel g):this(windowmanager)
        {
            this.g = g;
        }

        #endregion

        #region atributos

        MantenerGuiaDeRemisionViewModel g;

        private readonly IWindowManager _windowManager;
        private NotaIS notaSel;
        public NotaIS NotaSel
        {
            get { return notaSel; }
            set { notaSel = value; NotifyOfPropertyChange("NotaSel"); }
        }

        private List<NotaIS> lstNotaIs;
        public List<NotaIS> LstNotaIs
        {
            get { return lstNotaIs; }
            set { lstNotaIs = value; NotifyOfPropertyChange("LstNotaIs"); }
        }
        private List<String> cmbBox= new List<string>();

        public List<String> CmbBox
        {
            get { return cmbBox; }
            set { cmbBox = value;
            NotifyOfPropertyChange("CmbBox");
            }
        }
        private string cmbBoxSelected = null;

        public string CmbBoxSelected
        {
            get { return cmbBoxSelected; }
            set { cmbBoxSelected = value;
            NotifyOfPropertyChange("CmbBoxSelected");
            }
        }

        #endregion

        #region metodos

        public void AbrirMantenerNotaDeIngreso()
        {
            _windowManager.ShowWindow(new MantenerNotaDeIngresoViewModel(_windowManager));
        }

        public void SelectedItemChanged()
        {
            if (NotaSel != null)
            {

                if (g != null)
                {
                    if (!(new GuiaDeRemisionSQL().BuscarIDNota(NotaSel.IdNota)))
                    {
                       
                        _windowManager.ShowDialog(new AlertViewModel(_windowManager, "chauuuuu"));
                        //MessageBox.Show("chauuuuu");
                        g.Nota = NotaSel;
                        TryClose();
                    }
                    else
                    {
                        _windowManager.ShowDialog(new AlertViewModel(_windowManager, "La NOTA ya esta en una GUIA"));
                    }

                }
            }
        }

        public void Buscar()
        {
            string a = cmbBoxSelected;

            LstNotaIs = new NotaISSQL().BuscarNotas(a);
        }

        public Boolean EstaEnGuia(NotaIS nota)
        {
            List<GuiaRemision> list = new GuiaDeRemisionSQL().BuscarGuiaDeRemision(null, 0, null);
            for (int i=0; i<list.Count; i++)
            {
                if (list[i].Nota != null)
                {
                    if (list[i].Nota.IdNota == nota.IdNota)
                        return true;
                }
            }

            return false;
        }

        #endregion
    }
}
