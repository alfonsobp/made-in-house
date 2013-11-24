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
        
            int tipo;

            public int Tipo
            {
                get { return tipo; }
                set { tipo = value; }
            }
        
        
        DateTime fechaReg;

        public DateTime FechaReg
        {
            get { return fechaReg; }
            set { fechaReg = value; }
        }


            int idAlmacen;

            public int IdAlmacen
            {
                get { return idAlmacen; }
                set { idAlmacen = value; }
            }


            int idMotivo;

            internal int IdMotivo
            {
                get { return idMotivo; }
                set { idMotivo = value; }
            }
        
       
            List<ProductoCant> lstProducto;

            public List<ProductoCant> LstProducto
            {
                get { return lstProducto; }
                set { lstProducto = value; }
            }

            string observaciones;

            public string Observaciones
            {
                get { return observaciones; }
                set { observaciones = value; }
            }
            int idResponsable;

            public int IdResponsable
            {
                get { return idResponsable; }
                set { idResponsable = value; }
            }

            int idDoc;

            public int IdDoc
            {
                get { return idDoc; }
                set { idDoc = value; }
            }

            private Almacenes alm;
            public Almacenes Alm
            {
                get { return alm; }
                set { alm = value; }
            }
            private string idNotaString;

            public string IdNotaString
            {
                get { return idNotaString; }
                set { idNotaString = value; }
            }

            private string tipoString;

            public string TipoString
            {
                get { return tipoString; }
                set { tipoString = value; }
            }

            private string idAlmacenString;

            public string IdAlmacenString
            {
                get { return idAlmacenString; }
                set { idAlmacenString = value; }
            }

        
        }
}
