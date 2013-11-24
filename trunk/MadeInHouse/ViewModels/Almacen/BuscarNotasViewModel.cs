using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;
using System.Windows;

namespace MadeInHouse.ViewModels.Almacen
{
    class BuscarNotasViewModel : Screen
    {
        private MyWindowManager win = new MyWindowManager();

        public BuscarNotasViewModel()
        {
            cmbBox.Add("Entrada");
            cmbBox.Add("Salida");
            cmbBox.Add("MovimientoInterno");

        }

        MantenerGuiaDeRemisionViewModel g;
        public BuscarNotasViewModel(MantenerGuiaDeRemisionViewModel g)
        {
            this.g = g;
        }

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

        public void AbrirMantenerNotaDeIngreso()
        {
            win.ShowWindow(new Almacen.MantenerNotaDeIngresoViewModel());
        }

        public void SelectedItemChanged()
        {
            if (NotaSel != null)
            {

                if (g != null)
                {
                    if (!(new GuiaDeRemisionSQL().BuscarIDNota(NotaSel.IdNota)))
                    {
                        MessageBox.Show("chauuuuu");
                        g.Nota = NotaSel;
                        TryClose();
                    }
                    else
                    {
                        MessageBox.Show("La NOTA ya esta en una GUIA");
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
    }
}
