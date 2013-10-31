using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class GuiaRemision
    {
        int idGuia;

        public int IdGuia
        {
            get { return idGuia; }
            set { idGuia = value; }
        }
        string camion;

        public string Camion
        {
            get { return camion; }
            set { camion = value; }
        }
        string codGuiaRem;

        public string CodGuiaRem
        {
            get { return codGuiaRem; }
            set { codGuiaRem = value; }
        }
        string conductor;

        public string Conductor
        {
            get { return conductor; }
            set { conductor = value; }
        }
        string dirLlegada;

        public string DirLlegada
        {
            get { return dirLlegada; }
            set { dirLlegada = value; }
        }
        string dirPartida;

        public string DirPartida
        {
            get { return dirPartida; }
            set { dirPartida = value; }
        }
        int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        DateTime fechaReg;

        public DateTime FechaReg
        {
            get { return fechaReg; }
            set { fechaReg = value; }
        }
        DateTime fechaTraslado;

        public DateTime FechaTraslado
        {
            get { return fechaTraslado; }
            set { fechaTraslado = value; }
        }
        string observaciones;

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }
        string tipo;

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        List<ProductoxGuiaRemision> lstProductos;

        public List<ProductoxGuiaRemision> LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value; }
        }
    }
}
