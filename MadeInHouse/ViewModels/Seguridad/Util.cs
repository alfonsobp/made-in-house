using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MadeInHouse.Models.Seguridad;
namespace MadeInHouse.ViewModels.Seguridad
{
    class Util
    {
        public string generarContrasenha()
        {
            string contrasenha = "";
            Random r = new Random();
            for (int i = 0; i < 8; i++)
            {
                contrasenha += (char)(65 + r.Next(26));
            }
            return contrasenha;
        }
        public List<EstadoHabilitado> ListarEstados()
        {
            EstadoHabilitado est1 = new EstadoHabilitado();
            est1.Estado = 0;
            est1.Nombre = "Bloqueado";
            EstadoHabilitado est2 = new EstadoHabilitado();
            est2.Estado = 1;
            est2.Nombre = "Habilitado";
            List<EstadoHabilitado> lstEstado = new List<EstadoHabilitado>();
            lstEstado.Add(est1);
            lstEstado.Add(est2);
            return lstEstado;
        }

        public List<EstadoHabilitado> ListarEstadosOrdenDespacho()
        {
            EstadoHabilitado est1 = new EstadoHabilitado();
            est1.Estado = 0;
            est1.Nombre = "Pendiente";
            EstadoHabilitado est2 = new EstadoHabilitado();
            est2.Estado = 1;
            est2.Nombre = "Atendido";
            List<EstadoHabilitado> lstEstado = new List<EstadoHabilitado>();
            lstEstado.Add(est1);
            lstEstado.Add(est2);
            return lstEstado;
        }
    }
}