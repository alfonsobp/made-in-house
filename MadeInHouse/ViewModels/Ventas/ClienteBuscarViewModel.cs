using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows;
using MadeInHouse.Models;
using MadeInHouse.Models.Ventas;
using System.Windows.Controls;
using MadeInHouse.DataObjects.Ventas;
using MadeInHouse.ViewModels.Reportes;

namespace MadeInHouse.ViewModels.Ventas
{
    class ClienteBuscarViewModel : Screen
    {
        private MyWindowManager win = new MyWindowManager();
        private int ventanaAccion = 0;

       

        VentaBuscarViewModel vbvm = null;

        public ClienteBuscarViewModel(VentaBuscarViewModel vbvm)
        {
            Clientes = DataObjects.Ventas.ClienteSQL.BuscarClientes(null, null, -1, null, null);
            this.vbvm = vbvm ;
            ventanaAccion = 3;
        }


        public void AbrirRegistrarcliente()
        {
            win.ShowWindow(new Ventas.ClienteRegistrarViewModel());
        }

        public void AbrirEditarcliente()
        {
            win.ShowWindow(new Ventas.ClienteRegistrarViewModel(this, clienteSeleccionado.Cliente.Id) { DisplayName = "Editar Cliente" });
        }

        public void Eliminarcliente()
        {
            int w = DataObjects.Ventas.ClienteSQL.EliminarCliente(clienteSeleccionado.Cliente.Id);
        }

        public ClienteBuscarViewModel()
        {
            Clientes = DataObjects.Ventas.ClienteSQL.BuscarClientes(null,null,-1,null,null);
            NotifyOfPropertyChange("Clientes");
            //Console.WriteLine(Clientes);
        }

        public ClienteBuscarViewModel(VentaRegistrarViewModel ventaRegistrarViewModel, int ventanaAccion)
        {
            // TODO: Complete member initialization
            this.ventaRegistrarViewModel = ventaRegistrarViewModel;
            this.ventanaAccion = ventanaAccion;

            clientes = DataObjects.Ventas.ClienteSQL.BuscarClientes(null, null, -1, null, null);
            NotifyOfPropertyChange("Clientes");
        }

        public ClienteBuscarViewModel(reporteServiciosViewModel reporteServiciosVM, int ventanaAccion)
        {
            // TODO: Complete member initialization
            this.reporteServiciosViewModel = reporteServiciosVM;
            this.ventanaAccion = ventanaAccion;

            clientes = DataObjects.Ventas.ClienteSQL.BuscarClientes(null, null, -1, null, null);
            NotifyOfPropertyChange("Clientes");
        }

        private List<Tarjeta> clientes;

        public List<Tarjeta> Clientes
        {
            get
            {
                return clientes;
            }

            set
            {
                if (clientes == value)
                {
                    return;
                }
                clientes = value;
                NotifyOfPropertyChange(() => Clientes);
            }
        }

        private Dictionary<string, int> tipoCliente = new Dictionary<string, int>()
        {
            { "Seleccionar", -1 }, { "Persona", 0 }, { "Empresa", 1 }
        };

        public BindableCollection<string> cmbTipoCliente
        {
            get
            {
                return new BindableCollection<string>(tipoCliente.Keys);
            }
        }

        private Tarjeta clienteSeleccionado;
        private VentaRegistrarViewModel ventaRegistrarViewModel;
        private reporteServiciosViewModel reporteServiciosViewModel;

        public void Acciones(object sender)
        {

        
            if (ventanaAccion == 1)
            {
                clienteSeleccionado = ((sender as DataGrid).SelectedItem as Tarjeta);
                if (ventaRegistrarViewModel != null)
                {
                    ventaRegistrarViewModel.Cli = clienteSeleccionado;
                    this.TryClose();
                }
            }
            else if (ventanaAccion == 2)
            {
                clienteSeleccionado = ((sender as DataGrid).SelectedItem as Tarjeta);
                if (reporteServiciosViewModel != null)
                {
                    reporteServiciosViewModel.Cli = clienteSeleccionado;
                    this.TryClose();
                }
            }
            else if (ventanaAccion == 3)
            {
                clienteSeleccionado = ((sender as DataGrid).SelectedItem as Tarjeta);
                if (vbvm != null){

                    vbvm.client = clienteSeleccionado.Cliente;
                    vbvm.Identificacion = (vbvm.client.TipoCliente == 0) ? vbvm.client.Nombre : vbvm.client.RazonSocial;
                    vbvm.DniRuc = (vbvm.client.TipoCliente == 0) ? vbvm.client.Dni : vbvm.client.Ruc;
                    this.TryClose();
                }

            }
            else
            {
                clienteSeleccionado = ((sender as DataGrid).SelectedItem as Tarjeta);
                AbrirEditarcliente();
            }
        }


        public void SelectedItemChanged(object sender)
        {
            clienteSeleccionado = ((sender as DataGrid).SelectedItem as Tarjeta);

        }

        public void RealizarBusqueda(string dni, string nombre, string cmbTipoCliente, string registroDesde, string registroHasta)
        {
            Clientes = DataObjects.Ventas.ClienteSQL.BuscarClientes(dni, nombre, tipoCliente[cmbTipoCliente], registroDesde, registroHasta);
            NotifyOfPropertyChange("Clientes");
        }
    }
}
