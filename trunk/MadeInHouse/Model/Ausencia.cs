using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Ausencia
    {
        int idAusencia;

        public int IdAusencia
        {
            get { return idAusencia; }
            set { idAusencia = value; }
        }
        Empleado empleado;

        internal Empleado Empleado
        {
            get { return empleado; }
            set { empleado = value; }
        }
        DateTime fechaIni;

        public DateTime FechaIni
        {
            get { return fechaIni; }
            set { fechaIni = value; }
        }
        DateTime fechaFin;

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; }
        }
        string motivo;

        public string Motivo
        {
            get { return motivo; }
            set { motivo = value; }
        }
    }
}
