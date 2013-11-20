using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class GuiaRemision
    {

        private int idGuia;

        public int IdGuia
        {
            get { return idGuia; }
            set { idGuia = value; }
        }

        private string codGuiaRem;

        public string CodGuiaRem
        {
            get { return codGuiaRem; }
            set { codGuiaRem = value; }
        }

        private string camion;

        public string Camion
        {
            get { return camion; }
            set { camion = value; }
        }

        private string conductor;

        public string Conductor
        {
            get { return conductor; }
            set { conductor = value; }
        }

        private DateTime fechaReg;

        public DateTime FechaReg
        {
            get { return fechaReg; }
            set { fechaReg = value; }
        }


        private DateTime fechaTraslado;

        public DateTime FechaTraslado
        {
            get { return fechaTraslado; }
            set { fechaTraslado = value; }
        }

        private string tipo;

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private string observaciones;

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        private int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }


        private Almacenes almacen;

        public Almacenes Almacen
        {
            get { return almacen; }
            set { almacen = value; }
        }

        private NotaIS nota;

        public NotaIS Nota
        {
            get { return nota; }
            set { nota = value; }
        }

        private OrdenDespacho orden;

        public OrdenDespacho Orden
        {
            get { return orden; }
            set { orden = value; }
        }

        private Almacenes almOrigen;
        public Almacenes AlmOrigen
        {
            get { return almOrigen; }
            set { almOrigen = value; }
        }
    }
}
