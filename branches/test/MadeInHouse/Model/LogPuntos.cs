using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class LogPuntos
    {
        int idLogP;

        public int IdLogP
        {
            get { return idLogP; }
            set { idLogP = value; }
        }
        int idPunto;

        public int IdPunto
        {
            get { return idPunto; }
            set { idPunto = value; }
        }
        DateTime fechaReg;

        public DateTime FechaReg
        {
            get { return fechaReg; }
            set { fechaReg = value; }
        }

    }
}
