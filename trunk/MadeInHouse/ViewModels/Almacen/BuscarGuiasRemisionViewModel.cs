using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;

namespace MadeInHouse.ViewModels.Almacen
{
    class BuscarGuiasRemisionViewModel : PropertyChangedBase

    {


        private MyWindowManager win = new MyWindowManager();

        public void AbrirMantenerGuiaDeRemision()
        {
            
            Almacen.MantenerGuiaDeRemisionViewModel abrirGuiaView = new Almacen.MantenerGuiaDeRemisionViewModel() ;
            win.ShowWindow(abrirGuiaView);


        }


    }
}
