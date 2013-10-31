using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows;
using MadeInHouse.Models;
using MadeInHouse.Models.Ventas;

namespace MadeInHouse.ViewModels.Ventas
{
    class ClienteBuscarViewModel : PropertyChangedBase
    {
        private ClienteGateway cliGateway;
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
            cliGateway = new ClienteGateway();
            Clientes = cliGateway.BuscarClientes();
            
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

        private Dictionary<string, int> sexo = new Dictionary<string, int>()
        {
            { "Seleccionar", -1 }, { "No sabe", 0 }, { "Masculino", 1 }, { "Femenino", 2 }, { "No aplica", 9 }
        };

        public BindableCollection<string> cmbSexo
        {
            get
            {
                return new BindableCollection<string>(sexo.Keys);
            }
        }

        public void RealizarBusqueda(string tarjeta, string dni, string telefono, string nombre, string cmbSexo, string ruc, string razonSocial, string registroDesde, string registroHasta)
        {
            cliGateway = new ClienteGateway();
            Clientes = cliGateway.BuscarClientes(tarjeta, dni, telefono, nombre, sexo[cmbSexo], ruc, razonSocial, registroDesde, registroHasta);
        }
    }
}
