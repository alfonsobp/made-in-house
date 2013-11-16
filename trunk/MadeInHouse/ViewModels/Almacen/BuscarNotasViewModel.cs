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
    class BuscarNotasViewModel : PropertyChangedBase
    {
        private MyWindowManager win = new MyWindowManager();
        NotaISSQL ntgw = new NotaISSQL();

        private List<NotaIS> lstNotaIs = new List<NotaIS>();

        public List<NotaIS> LstNotaIs
        {
            get { return lstNotaIs;}
            set { lstNotaIs = value;
            NotifyOfPropertyChange("LstNotaIS");
            }
        }

        private NotaIS notaSel = new NotaIS();

        public NotaIS NotaSel
        {
            get { return notaSel; }
            set { notaSel = value;
            NotifyOfPropertyChange("NotaSel");
            }
        }

        public BuscarNotasViewModel() {

            LstNotaIs = ntgw.BuscarNotas();
        }

        public void Buscar(){
            LstNotaIs = ntgw.BuscarNotas();
        }

        public void Acciones(object sender)
        {

        }

    }
}
