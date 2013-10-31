using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
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
        Almacen alm;

        internal Almacen Alm
        {
            get { return alm; }
            set { alm = value; }
        }
        GuiaRemision guia;

        internal GuiaRemision Guia
        {
            get { return guia; }
            set { guia = value; }
        }
        MotivoIS motivo;

        internal MotivoIS Motivo
        {
            get { return motivo; }
            set { motivo = value; }
        }
        List<ProductoxNotaIS> lstProducto;

        internal List<ProductoxNotaIS> LstProducto
        {
            get { return lstProducto; }
            set { lstProducto = value; }
        }
    }
}
