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

        private int numRows;

        public int NumRows
        {
            get { return numRows; }
            set
            {
                numRows = value;
                NotifyOfPropertyChange(() => NumRows);
            }
        }

        private int numColumns;

        public int NumColumns
        {
            get { return numColumns; }
            set
            {
                numColumns = value;
                NotifyOfPropertyChange(() => NumColumns);
            }
        }

        private int altura;

        public int Altura
        {
            get { return altura; }
            set
            {
                altura = value;
                NotifyOfPropertyChange(() => Altura);
            }
        }

        private List<TipoZona> lstZonasAnq;

        public List<TipoZona> LstZonasAnq
        {
            get { return lstZonasAnq; }
            set
            {
                lstZonasAnq = value;
                NotifyOfPropertyChange(() => LstZonasAnq);
            }
        }

        private List<Producto> lstProductos;

        public List<Producto> LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value; }
        }

        private List<Tienda> cmbTiendas;

        public List<Tienda> CmbTiendas
        {
            get { return cmbTiendas; }
            set
            {
                if (this.cmbTiendas == value)
                {
                    return;
                }

                cmbTiendas = value;
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

            TiendaSQL tSQL = new TiendaSQL();
            CmbTiendas = tSQL.BuscarTienda();
            Index = this.CmbTiendas.FindIndex(x => x.IdTienda == idTienda);

            AlmacenSQL aSQL = new AlmacenSQL();
            Almacenes anaquel = aSQL.BuscarAlmacen(-1, idTienda, 2);
            
            idAnaquel = anaquel.IdAlmacen;

            NumColumns = anaquel.NroColumnas;
            NumRows = anaquel.NroFilas;
            Altura = anaquel.Altura;

            
            TipoZonaSQL tzSQL = new TipoZonaSQL();
            LstZonasAnq = tzSQL.ObtenerZonasxAlmacen(idAnaquel, 2);
            ProductoSQL pSQL = new ProductoSQL();
            LstProductos = new List<Producto>();
            

        }





    }
}
