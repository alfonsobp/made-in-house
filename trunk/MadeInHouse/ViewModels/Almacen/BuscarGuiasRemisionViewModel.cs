using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Almacen
{
    class BuscarGuiasRemisionViewModel:Screen

    {
        public void AbrirMantenerGuiaDeRemision()
        {
            WindowManager win = new WindowManager();
            Almacen.MantenerGuiaDeRemisionViewModel abrirGuiaView = new Almacen.MantenerGuiaDeRemisionViewModel { DisplayName = "Mantenimiento de guias de remisión" };
            win.ShowWindow(abrirGuiaView);



        }


    }
}
