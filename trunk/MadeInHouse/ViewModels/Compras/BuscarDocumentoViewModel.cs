using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Views.Compras;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.Models;
using System.Data.OleDb;
using System.Data;
using MadeInHouse.Models.Compras;

namespace MadeInHouse.ViewModels.Compras
{
    class BuscarDocumentoViewModel:PropertyChangedBase
    {
        //Costructores
        public BuscarDocumentoViewModel()
        {
            ActualizarDocumentos();
            lstOpciones = new List<string>();
            lstOpciones.Add("COMPLETO");
            lstOpciones.Add("PENDIENTE");
            lstOpciones.Add("CANCELADO");
        }




        //Atributos de la clase
        private MyWindowManager win = new MyWindowManager();
        private DocPagoProveedor docSeleccionado;
        DocPagoProveedorSQL eM = new DocPagoProveedorSQL();
        PagoParcialSQL eMPP = new PagoParcialSQL();
        List<PagoParcial> list = new List<PagoParcial>();

        private List<string> lstOpciones;

        public List<string> LstOpciones
        {
            get { return lstOpciones; }
            set { lstOpciones = value; NotifyOfPropertyChange(() => LstOpciones); }
        }


        private string selectedEst;

        public string SelectedEst
        {
            get { return selectedEst; }
            set { selectedEst = value; NotifyOfPropertyChange(() => SelectedEst); }
        }

        private string txtCodigo;

        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }

        private string txtProveedor;

        public string TxtProveedor
        {
            get { return txtProveedor; }
            set { txtProveedor = value; NotifyOfPropertyChange(() => TxtProveedor); }
        }

        private Proveedor prov;

        public Proveedor Prov
        {
            get { return prov; }
            set { prov = value; NotifyOfPropertyChange(() => Prov); }
        }

        
        private string txtPago;

        public string TxtPago
        {
            get { return txtPago; }
            set { txtPago = value; NotifyOfPropertyChange(() => TxtPago); }
        }

        private double txtTotalPago;

        public double TxtTotalPago
        {
            get { return txtTotalPago; }
            set { txtTotalPago = value; NotifyOfPropertyChange(() => TxtTotalPago); }
        }

        private List<DocPagoProveedor> lstDocsPago;

        public List<DocPagoProveedor> LstDocsPago
        {
            get { return lstDocsPago; }
            set { lstDocsPago = value; NotifyOfPropertyChange(() => LstDocsPago); }
        }

        private List<PagoParcial> lstPagosParciales;

        public List<PagoParcial> LstPagosParciales
        {
            get { return lstPagosParciales; }
            set { lstPagosParciales = value; NotifyOfPropertyChange(() => LstPagosParciales); }
        }


        //Funciones de la clase
        public void SelectedItemChanged(object sender)
        {
            docSeleccionado = ((sender as DataGrid).SelectedItem as DocPagoProveedor);

            if (docSeleccionado != null)
                LstPagosParciales = ActualizarPagos(docSeleccionado);
        }


        public void NuevoDocumento()
        {

            Compras.registrarDocumentosViewModel obj = new Compras.registrarDocumentosViewModel();
            win.ShowWindow(obj);
        }
        public void EditarDocumento()
        {

            Compras.registrarDocumentosViewModel obj = new Compras.registrarDocumentosViewModel(docSeleccionado, this);
            win.ShowWindow(obj);
        }

        public void ActualizarDocumentos()
        {
            LstDocsPago = eM.Buscar() as List<DocPagoProveedor>;
        }

        public void CancelarDocumento()
        {
            int k = new DocPagoProveedorSQL().Eliminar(docSeleccionado);
        }

        public void Limpiar()
        {
            TxtCodigo = null;
            Prov = null;
            TxtPago = null;
            LstPagosParciales = null;
            LstDocsPago = null;
            TxtTotalPago = 0;
        }

        public List<PagoParcial> ActualizarPagos(DocPagoProveedor d)
        {
            double totalPago = 0;
            list = eMPP.BuscarPagos(d);

            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                    totalPago += list[i].Monto;
            }

            TxtTotalPago = d.MontoTotal-totalPago;
            if (TxtTotalPago <= 0)
                TxtTotalPago = 0;

            return list;
        }


        public void BuscarDocsPago()
        {            
            if ((String.IsNullOrEmpty(TxtProveedor)) && (Prov != null))
                TxtProveedor = Prov.CodProveedor;

            LstDocsPago = eM.Buscar(TxtCodigo, TxtProveedor, SelectedEst) as List<DocPagoProveedor>;
        }

        public void BuscarProveedor()
        {
            MadeInHouse.Models.MyWindowManager w = new MadeInHouse.Models.MyWindowManager();
            w.ShowWindow(new BuscadorProveedorViewModel(this));
        }

        public void pagoParcial()
        {
            string pago = TxtPago.ToString();
            Boolean formatoOK = true;

            for (int i = 0; i < pago.Length; i++)
            {
                if ((!((pago[i] >= '0') && (pago[i] <= '9'))) && (!((pago[i] == '.') && (i>0) && (i!=(pago.Length-1)))))
                {
                    formatoOK = false;
                    break;
                }
            }

            if (formatoOK)
            {
                if ((docSeleccionado != null) && (Convert.ToDouble(TxtPago) != 0))
                {
                    int k, y;
                    PagoParcial p = new PagoParcial();

                    docSeleccionado.SaldoPagado += Convert.ToDouble(TxtPago);

                    if (docSeleccionado.SaldoPagado >= docSeleccionado.MontoTotal)
                        docSeleccionado.SaldoPagado = docSeleccionado.MontoTotal;

                    p.Monto = Convert.ToDouble(TxtPago);
                    p.DocPago = docSeleccionado;
                    p.FechaPago = DateTime.Now;


                    k = eM.Actualizar(docSeleccionado);


                    y = eMPP.Agregar(p);
                    list.Add(p);
                    
                    TxtTotalPago = TxtTotalPago - p.Monto;
                    if (TxtTotalPago <= 0)
                        TxtTotalPago = 0;

                    if ((k != 0) && (y != 0))
                    {
                        MessageBox.Show("Doc de Pago = " + docSeleccionado.CodDoc + "\nMonto Pagado = " + TxtPago +
                                        "\nMonto Faltante = " + (docSeleccionado.MontoTotal - docSeleccionado.SaldoPagado));
                    }

                    else
                    {
                        MessageBox.Show("No se pudo registrar el monto a pagar, revisar conexiones");
                    }
                }

                else
                {
                    MessageBox.Show("Seleccione un documento y/o monto a pagar");
                }


                ActualizarDocumentos();
                LstPagosParciales = new List<PagoParcial>(list);

            }
            else
            {
                MessageBox.Show("Ingrese el formato correcto \nFormato: Número Real (Num.Dec)");
            }
        }

    }
}
