using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;
using System.ComponentModel.Composition;
using MadeInHouse.Models.Seguridad;
using System.Threading;
using MadeInHouse.DataObjects.Almacen;

namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(StockMinListadoViewModel))]
     class StockMinListadoViewModel : Screen
    {
        private List<Tienda> cmbTiendas;

        public List<Tienda> CmbTiendas
        {
            get { return cmbTiendas; }
            set { cmbTiendas = value;
            NotifyOfPropertyChange(() => CmbTiendas);
            }
        }

        

        private int index;

        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                NotifyOfPropertyChange(() => Index);
            }
        }

        private List<ProductoxTienda> lstProductos;

        public List<ProductoxTienda> LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value;
            NotifyOfPropertyChange(() => LstProductos);
            }
        }

        private int idTienda;
        private int idResponsable;
        private readonly IWindowManager _windowManager;

        private bool enable;

        public bool Enable
        {
            get { return enable; }
            set { enable = value;
            NotifyOfPropertyChange(() => Enable);
            }
        }

        [ImportingConstructor]
        public StockMinListadoViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
            Usuario u = new Usuario();
            u = DataObjects.Seguridad.UsuarioSQL.buscarUsuarioPorIdUsuario(Int32.Parse(Thread.CurrentPrincipal.Identity.Name));
            idTienda = u.IdTienda;
            idResponsable = u.IdUsuario;

            if (idTienda > 0) Enable = false;
            else Enable = true;


            Tienda central = new Tienda();
            central.Nombre = "ALMACEN CENTRAL";
            central.IdTienda = 0;

            TiendaSQL tSQL = new TiendaSQL();
            CmbTiendas = tSQL.BuscarTienda();
            CmbTiendas.Insert(0, central);

            Index = this.CmbTiendas.FindIndex(x => x.IdTienda == idTienda);



            if (idTienda > 0)
            {
                ProductoSQL pSQL = new ProductoSQL();
                LstProductos = pSQL.BuscarProductoxTienda(idTienda, true);
            }
            else
            {
                ProductoSQL pSQL = new ProductoSQL();
                LstProductos = pSQL.BuscarProductoxCentral(1,-1, true);
            }
        }

        private int selectedTienda;

        public int SelectedTienda
        {
            get { return selectedTienda; }
            set { selectedTienda = value;
            NotifyOfPropertyChange(() => SelectedTienda);
            }
        }



        public void Recargar()
        {
            if (SelectedTienda > 0)
            {
                ProductoSQL pSQL = new ProductoSQL();
                LstProductos = pSQL.BuscarProductoxTienda(SelectedTienda, true);
            }
            else
            {
                ProductoSQL pSQL = new ProductoSQL();
                LstProductos = pSQL.BuscarProductoxCentral(1, -1, true);
            }

        }




    }
}
