using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Views.Almacen;
using System.Windows;
using System.Collections.ObjectModel;
using MadeInHouse.ViewModels.Ventas;
using MadeInHouse.Models.Ventas;
using MadeInHouse.DataObjects;
using MadeInHouse.DataObjects.Almacen;

namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerGuiaDeRemisionViewModel:PropertyChangedBase
    {
        //Constructores
        public MantenerGuiaDeRemisionViewModel(GuiaRemision g) {

            Nota = g.Nota;
            Alm = g.Almacen;
            TxtCodigo = g.CodGuiaRem;
            TxtFechaReg = g.FechaReg;
            SeleccionadoTipo=g.Tipo;
            TxtConductor = g.Conductor;
            SeleccionadoCamion = g.Camion;
            TxtObservaciones = g.Observaciones;

            if (g.Estado == 1)
                TxtEstado = "EMITIDA";
            else
                TxtEstado = "ATENDIDA";

            indicador = 2;
        }


        public MantenerGuiaDeRemisionViewModel()
        {
            int k=0;
            indicador = 1;

            k = new UtilesSQL().ObtenerMaximoID("GuiaRemision", "idGuia");
            TxtCodigo = "GR-" + (1000000 + k + 1).ToString();
            TxtEstado = "POR EMITIR";
        }



        //Atributos
        private int indicador; //1=insertar, 2=detalle;
        public int Indicador
        {
            get { return indicador; }
        }

        private string txtCodigo;
        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }

        private List<string> cbTipo = new List<string>() { "GR-ORDEN DESPACHO", "GR-TRASLADO EXTERNO" };
        public List<string> CbTipo
        {
            get { return cbTipo; }
            set { cbTipo = value; NotifyOfPropertyChange(() => CbTipo); }
        }

        private DateTime txtFechaReg = DateTime.Now;
        public DateTime TxtFechaReg
        {
            get { return txtFechaReg; }
            set { txtFechaReg = value; NotifyOfPropertyChange(() => TxtFechaReg); }
        }


        private string txtCodigoCT;
        public string TxtCodigoCT
        {
            get { return txtCodigoCT; }
            set { txtCodigoCT = value; NotifyOfPropertyChange(() => TxtCodigoCT); }
        }

        private string txtTienda;
        public string TxtTienda
        {
            get { return txtTienda; }
            set { txtTienda = value; NotifyOfPropertyChange(() => TxtTienda); }
        }


        private string txtDirPartida;
        public string TxtDirPartida
        {
            get { return txtDirPartida; }
            set { txtDirPartida = value; NotifyOfPropertyChange(() => TxtDirPartida); }
        }

        private string txtConductor;
        public string TxtConductor
        {
            get { return txtConductor; }
            set { txtConductor = value; NotifyOfPropertyChange(() => TxtConductor); }
        }

        private List<string> cbCamion = new List<string>() { "CAMION RH-A1", "CAMION RT-V5", "CAMION HL-H8" };
        public List<string> CbCamion
        {
            get { return cbCamion; }
            set { cbCamion = value; NotifyOfPropertyChange(() => CbCamion); }
        }

        private string txtObservaciones;
        public string TxtObservaciones
        {
            get { return txtObservaciones; }
            set { txtObservaciones = value; NotifyOfPropertyChange(() => TxtObservaciones); }
        }

        private string txtAlmacenOrigen;
        public string TxtAlmacenOrigen
        {
            get { return txtAlmacenOrigen; }
            set { txtAlmacenOrigen = value; NotifyOfPropertyChange(() => TxtAlmacenOrigen); }
        }

        private string seleccionadoTipo;
        public string SeleccionadoTipo
        {
            get { return seleccionadoTipo; }
            set { seleccionadoTipo = value; NotifyOfPropertyChange(() => SeleccionadoTipo); }
        }

        private string seleccionadoCamion;
        public string SeleccionadoCamion
        {
            get { return seleccionadoCamion; }
            set { seleccionadoCamion = value; NotifyOfPropertyChange(() => SeleccionadoCamion); }
        }

        private string selectedNotaOrden;
        public string SelectedNotaOrden
        {
            get { return selectedNotaOrden; }
            set { selectedNotaOrden = value; NotifyOfPropertyChange(() => SelectedNotaOrden); }
        }

        private string txtDirLlegada;
        public string TxtDirLlegada
        {
            get { return txtDirLlegada; }
            set { txtDirLlegada = value; NotifyOfPropertyChange(() => TxtDirLlegada); }
        }

        private string txtEstado;
        public string TxtEstado
        {
            get { return txtEstado; }
            set { txtEstado = value; NotifyOfPropertyChange(() => TxtEstado); }
        }

        private int txtCantidad;
        public int TxtCantidad
        {
            get { return txtCantidad; }
            set { txtCantidad = value; NotifyOfPropertyChange(() => TxtCantidad); }
        }

        private NotaIS nota;
        public NotaIS Nota
        {
            get { return nota; }
            set { nota = value; NotifyOfPropertyChange(() => Nota); }
        }

        private Almacenes alm;
        public Almacenes Alm
        {
            get { return alm; }
            set { alm = value; NotifyOfPropertyChange(() => Alm); }
        }

        private List<GuiaRemxProducto> lstProductos;
        public List<GuiaRemxProducto> LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value; NotifyOfPropertyChange(() => LstProductos); }
        }

        public void Refrescar()
        {
            if (Nota.Tipo == 2)
            {

                List<Almacenes> list = new AlmacenSQL().BuscarAlmacen();
                for (int i = 0; i < list.Count; i++)
                    if (list[i].IdAlmacen == Nota.IdAlmacen)
                    {
                        TxtDirPartida = list[i].Direccion;
                        TxtAlmacenOrigen = list[i].Nombre;
                        break;
                    }

                List<ProductoxNotaIS> l = new NotaISSQL().BuscarNotasXProductos();
                List<GuiaRemxProducto> lAux = new List<GuiaRemxProducto>();
                int cantTotal = 0;

                for (int i = 0; i < l.Count; i++)
                    if (l[i].IdNota == Nota.IdNota)
                    {
                        GuiaRemxProducto gp = new GuiaRemxProducto();
                        Producto p = new ProductoSQL().Buscar_por_CodigoProducto(l[i].IdProducto);
                        gp.CodProd = p.CodigoProd;
                        gp.Nombre = p.Nombre;
                        gp.Cantidad = l[i].Cantidad;
                        cantTotal += l[i].Cantidad;
                        lAux.Add(gp);
                    }

                LstProductos = new List<GuiaRemxProducto>(lAux);
                TxtCantidad = cantTotal;
            }

            else
            {
                MessageBox.Show("Transacción única para NOTAS DE SALIDA");
            }


        }

        public void RefrescarAlm()
        {
            TxtDirLlegada = Alm.Direccion;
            TxtTienda = (getTiendafromID(Alm.IdTienda)).Nombre;
        }

        public void BuscarAlmacen()
        {
            if (SeleccionadoTipo.Equals("GR-TRASLADO EXTERNO"))
            {
                MyWindowManager w = new MyWindowManager();
                w.ShowWindow(new BuscarAlmacenViewModel(this));
            }

            else
                MessageBox.Show("Es un traslado por ORDEN DE DESPACHO \nNo entre ALMACENES");

        }

        public void BuscarNota()
        {
            MyWindowManager w = new MyWindowManager();
            w.ShowWindow(new BuscarNotasViewModel(this));
        }

        public void BuscarOrden()
        {
            //MyWindowManager w = new MyWindowManager();
            //w.ShowWindow(new Bus(this));
        }

        public void BuscarNotaOrden()
        {
            if (String.IsNullOrEmpty(SeleccionadoTipo))
            {
                MessageBox.Show("Seleccione el TIPO de traslado");
            }

            else
            {
                if (SeleccionadoTipo.Equals("GR-ORDEN DESPACHO"))
                {
                    BuscarOrden();
                }

                if (SeleccionadoTipo.Equals("GR-TRASLADO EXTERNO"))
                {
                    BuscarNota();
                }
            }


        }

        public void GuardarGuiaDeRemision()
        {

            int k = 1;
            GuiaRemision g = new GuiaRemision();

            g.Almacen = Alm;
            g.Nota = Nota;
            g.CodGuiaRem = txtCodigo;
            g.FechaTraslado = txtFechaReg;
            g.FechaReg = DateTime.Now;
            g.Tipo = SeleccionadoTipo;
            g.Conductor = txtConductor;
            g.Camion = SeleccionadoCamion;
            g.Observaciones = txtObservaciones;

            if (indicador == 1)
            {
                if ((LstProductos != null) || (!String.IsNullOrEmpty(TxtConductor)) || (!String.IsNullOrEmpty(SeleccionadoCamion)))
                {
                    if ((Nota != null) && (TxtTienda != null))
                    {

                    k = new GuiaDeRemisionSQL().agregarGuiaDeRemision(g);

                    if (k == 0)
                        MessageBox.Show("Ocurrio un error");
                    else
                        MessageBox.Show("Guia de Remision Registrada \n\nCodigo = " + txtCodigo + "\nFecha de Registro = " + txtFechaReg + "\nDireccion Partida = " + txtDirPartida +
                                        "\nTipo Guia = " + seleccionadoTipo + "\nConductor = " + txtConductor + "\nCamion= " +
                                        seleccionadoCamion + "\nObservaciones = " + txtObservaciones);
                    }

                    else
                    {
                        MessageBox.Show("Ingrese ALMACEN DESTINO del traslado");
                    }

                }

                else
                {
                    MessageBox.Show("Ingrese datos VALIDOS, por favor REVISAR");
                }
            }

            if (indicador == 2)
            {

                //k = DataObjects.Almacen.GuiaDeRemisionSQL.editarGuiaDeRemision(g);

                if (k == 0)
                    MessageBox.Show("Ocurrio un error");
                else
                    MessageBox.Show("Guia de Remision Actualizada \n\nCodigo = " + txtCodigo + "\nFecha de Registro = " + txtFechaReg + "\nDireccion Partida = " + txtDirPartida +
                                    "\nTipo Guia = " + cbTipo + "\nConductor = " + txtConductor + "\nCamion= " +
                                     cbCamion + "\nObservaciones = " + txtObservaciones);
            }
            
        }

        public Tienda getTiendafromID(int id)
        {
            List<Tienda> list = new TiendaSQL().BuscarTienda();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].IdTienda == id)
                    return list[i];
            }

            return null;
        }

    }
}
