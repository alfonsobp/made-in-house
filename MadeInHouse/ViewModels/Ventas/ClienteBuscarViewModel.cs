using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows;
using MadeInHouse.Models;
using MadeInHouse.Models.Ventas;
using MadeInHouse.DataObjects.Ventas;

namespace MadeInHouse.ViewModels.Ventas
{
    class ClienteBuscarViewModel : PropertyChangedBase
    {
        private ClienteModel cliGateway;
        private ClienteSQL cliSQL;
        private MyWindowManager win = new MyWindowManager();

        public void AbrirRegistrarcliente()
        {
            win.ShowWindow(new Ventas.ClienteRegistrarViewModel());
        }

        public void AbrirEditarcliente()
        {
            win.ShowWindow(new Ventas.ClienteEditarViewModel { DisplayName = "Editar Cliente" });
        }

        public ClienteBuscarViewModel()
        {
            cliSQL = new ClienteSQL();
            Clientes = cliSQL.BuscarClientes();

            
            Console.WriteLine(Clientes);
        }
        
        private List<Tarjeta> clientes;

        public List<Tarjeta> Clientes
        {
            get
            {
                return this.clientes;
            }

            private set
            {
                if (this.clientes == value)
                {
                    return;
                }

                this.clientes = value;
                this.NotifyOfPropertyChange(() => this.Clientes);
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

        public void RealizarBusqueda(string dni, string nombre, string cmbTipoCliente, string registroDesde, string registroHasta)
        {
            cliSQL = new ClienteSQL();
            Clientes = cliSQL.BuscarClientes(dni, nombre, tipoCliente[cmbTipoCliente], registroDesde, registroHasta);
        }
    }
}
