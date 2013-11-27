using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;
using MadeInHouse.ViewModels.Almacen;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Reportes;
using MadeInHouse.DataObjects;

namespace MadeInHouse.ViewModels.Reportes
{
    class reporteStockViewModel:Screen
    {
        private MyWindowManager win = new MyWindowManager();


        private Almacenes deft;
        private AlmacenSQL uSQL;

        public reporteStockViewModel()
        {
            uSQL = new AlmacenSQL();
            CmbDpto = uSQL.BuscarAlmacen();
            deft= new Almacenes();
            deft.Nombre="TODOS";
            CmbDpto.Insert(0, deft);
            Index1 = 0;

        }

        private DateTime fechaDesde = new DateTime(DateTime.Now.Year, 1, 1);

        public DateTime FechaDesde
        {
            get { return fechaDesde; }
            set { fechaDesde = value; NotifyOfPropertyChange(() => FechaDesde); }
        }

        private DateTime fechaHasta = new DateTime(DateTime.Now.Year, 12, 31);

        public DateTime FechaHasta
        {
            get { return fechaHasta; }
            set { fechaHasta = value; NotifyOfPropertyChange(() => FechaHasta); }
        }

        private int txtProducto;

        public int TxtProducto
        {
            get { return txtProducto; }
            set { txtProducto = value; NotifyOfPropertyChange(() => TxtProducto); }
        }

        private String txtTienda;

        public String TxtTienda
        {
            get { return txtTienda; }
            set { txtTienda = value; NotifyOfPropertyChange(() => TxtTienda); }
        }

        private Producto prod;

        public Producto Prod
        {
            get { return prod; }
            set { prod = value; NotifyOfPropertyChange(() => Prod); TxtProducto = prod.IdProducto; }
        }


        private Tienda tien;

        public Tienda Tien
        {
            get { return tien; }
            set { tien = value; NotifyOfPropertyChange(() => Tien); TxtTienda = tien.Nombre; }
        }

        public void BuscarTienda()
        {
            MyWindowManager w = new MyWindowManager();
            w.ShowWindow(new BuscarTiendaViewModel(w, this, 1));
        }

        private List<Almacenes> cmbDpto;

        public List<Almacenes> CmbDpto
        {
            get { return this.cmbDpto; }
            private set
            {

                if (this.cmbDpto == value)
                {
                    return;
                }
                this.cmbDpto = value;
                this.NotifyOfPropertyChange(() => this.cmbDpto);
            }
        }

        private List<StockRepor> listStock;

        public List<StockRepor> ListStock
        {
            get { return this.listStock; }
            private set
            {

                if (this.listStock == value)
                {
                    return;
                }
                this.listStock = value; this.NotifyOfPropertyChange(() => this.ListStock);
            }
        }

        private int selectedDpto;

        public int SelectedDpto
        {
            get { return selectedDpto; }
            set
            {
                selectedDpto = value;
            }
        }

        private int index1;

        public int Index1
        {
            get { return index1; }
            set
            {
                index1 = value;
                NotifyOfPropertyChange(() => Index1);
            }
        }

        public void GeneraReporteStock()
        {
            if (tien != null)
            {
                ListStock = DataObjects.ReportesSQL.GenerarReporStock(selectedDpto, tien.IdTienda, fechaDesde, FechaHasta);
            }
            else
            {
                ListStock = DataObjects.ReportesSQL.GenerarReporStock(selectedDpto, -1, fechaDesde, FechaHasta);
            }
        }
    }
}
