using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class NotaIS
    {
            int idNota;

            public int IdNota
            {
                get { return idNota; }
                set { idNota = value; }
            }
            string codNota;

            public string CodNota
            {
                get { return codNota; }
                set { codNota = value; }
            }
            DateTime fechaMod;

            public DateTime FechaMod
            {
                get { return fechaMod; }
                set { fechaMod = value; }
            }
            DateTime fechaReg;

            public DateTime FechaReg
            {
                get { return fechaReg; }
                set { fechaReg = value; }
            }
            int alm;

            internal int Alm
            {
                get { return alm; }
                set { alm = value; }
            }
            int guia;

            internal int Guia
            {
                get { return guia; }
                set { guia = value; }
            }
            int motivo;

            internal int Motivo
            {
                get { return motivo; }
                set { motivo = value; }
            }
            List<int> lstProducto;

            internal List<int> LstProducto
            {
                get { return lstProducto; }
                set { lstProducto = value; }
            }
        }
}
