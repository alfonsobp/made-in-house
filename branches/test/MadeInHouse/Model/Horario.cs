using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Horario
    {
        Empleado empleado;

        public Empleado Empleado
        {
            get { return empleado; }
            set { empleado = value; }
        }
        Horario horarioEmpleado;

        internal Horario HorarioEmpleado
        {
            get { return horarioEmpleado; }
            set { horarioEmpleado = value; }
        }
        DateTime dia;

        public DateTime Dia
        {
            get { return dia; }
            set { dia = value; }
        }
        string idEmpleado;

        public string IdEmpleado
        {
            get { return idEmpleado; }
            set { idEmpleado = value; }
        }
        string horaInicio;

        public string HoraInicio
        {
            get { return horaInicio; }
            set { horaInicio = value; }
        }
        string horaFin;

        public string HoraFin
        {
            get { return horaFin; }
            set { horaFin = value; }
        }
    }
}
