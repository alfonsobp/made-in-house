using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.ViewModels.Almacen
{
    class BuscarNotasViewModel : Screen
    {
        private MyWindowManager win = new MyWindowManager();

        public BuscarNotasViewModel()
        {
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
                    g.Nota = NotaSel;
                    TryClose();
                }
            }

        }

        public void Buscar()
        {
            LstNotaIs = new NotaISSQL().BuscarNotas();
        }

    }
}
