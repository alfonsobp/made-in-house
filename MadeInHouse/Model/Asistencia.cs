using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Asistencia
    {
        Empleado empleado;

        internal Empleado Empleado
        {
            get { return empleado; }
            set { empleado = value; }
        }
        DateTime fecha;

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        string horaEntrada;

        public string HoraEntrada
        {
            get { return horaEntrada; }
            set { horaEntrada = value; }
        }
        string horaSalida;

        public string HoraSalida
        {
            get { return horaSalida; }
            set { horaSalida = value; }
        }
    }
}
