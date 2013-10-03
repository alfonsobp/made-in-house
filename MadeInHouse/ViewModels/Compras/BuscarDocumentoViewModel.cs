using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.ViewModels.Compras
{
    class BuscarDocumentoViewModel:Screen
    {
        private WindowManager win = new WindowManager();

        public void NuevoDocumento()
        {

            Compras.registrarDocumentosViewModel obj = new Compras.registrarDocumentosViewModel { DisplayName = "Nuevo Doc Pago" };
            win.ShowWindow(obj);
        }
        public void EditarDocumento()
        {


            Compras.registrarDocumentosViewModel obj = new Compras.registrarDocumentosViewModel { DisplayName = "Editar Doc pago" };
            win.ShowWindow(obj);
        }


    }
}
