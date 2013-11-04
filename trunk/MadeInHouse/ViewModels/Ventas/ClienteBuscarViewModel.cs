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

namespace MadeInHouse.ViewModels.Ventas
{
    class ClienteBuscarViewModel : PropertyChangedBase
    {
        private MyWindowManager win = new MyWindowManager();


        public void AbrirRegistrarcliente()
        {
            win.ShowWindow(new Ventas.ClienteRegistrarViewModel());
        }

        public void AbrirEditarcliente()
        {
            win.ShowWindow(new Ventas.ClienteRegistrarViewModel(this, clienteSeleccionado.Cliente.Id) { DisplayName = "Editar Cliente" });
        }

        public ClienteBuscarViewModel()
        {
            clientes = DataObjects.Ventas.ClienteSQL.BuscarClientes(null,null,-1,null,null);
            NotifyOfPropertyChange("Clientes");
            //Console.WriteLine(Clientes);
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
