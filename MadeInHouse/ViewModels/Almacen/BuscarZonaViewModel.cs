using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;
using System.Collections.ObjectModel;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Seguridad;
using System.Threading;

namespace MadeInHouse.ViewModels.Almacen
{
    class BuscarZonaViewModel:Screen
    {

        

        private ObservableCollection<TipoZona> cmbZonas;
        public ObservableCollection<TipoZona> CmbZonas
        {
            get { return cmbZonas; }
            set { cmbZonas = value; }
        }

        private int selectedZona;

        public int SelectedZona
        {
            get { return selectedZona; }
            set
            {
                selectedZona = value;
                NotifyOfPropertyChange(() => SelectedZona);

            }
        }

        private int idTienda;
        private int idResponsable;
        private int idAnaquel;

        public BuscarZonaViewModel()
        {
            CmbZonas = (new TipoZonaSQL()).BuscarZona();
            Usuario u = new Usuario();
            u = DataObjects.Seguridad.UsuarioSQL.buscarUsuarioPorIdUsuario(Int32.Parse(Thread.CurrentPrincipal.Identity.Name));
            idTienda = u.IdTienda;
            idResponsable = u.IdUsuario;

            AlmacenSQL aSQL = new AlmacenSQL();
            Almacenes anaquel = aSQL.BuscarAlmacen(-1, idTienda, 2);
            
            idAnaquel = anaquel.IdAlmacen;

         /*   NumColumnAnq = anaquel.NroColumnas;
            NumRowsAnq = anaquel.NroFilas;
            AlturaAnq = anaquel.Altura;

            NumColumns = deposito.NroColumnas;
            NumRows = deposito.NroFilas;
            Altura = deposito.Altura;

            tzSQL = new TipoZonaSQL();
            LstZonas = tzSQL.ObtenerZonasxAlmacen(idDeposito);
            LstZonasAnq = tzSQL.ObtenerZonasxAlmacen(idAnaquel, 2);

            Accion2 = 2;
            Accion1 = 2;

            LstProductos = new List<ProductoCant>();
            Atendido = false;*/



        }





    }
}
