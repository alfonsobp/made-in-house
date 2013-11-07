using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Validacion
{
    class Error
    {
        public static TipoError esNumero = new TipoError(1, "Existe un valor que no es número entero..");
        public static TipoError esReal = new TipoError(2, "Existe un valor que no es número real..");
        public static TipoError esInvalidoCadena = new TipoError(3, "La cadena introducida no cumple el tamaño estandar..");
        public static TipoError esNegativo = new TipoError(4, "Es un numero negativo el que intenta introducir..");

       

    }

    class TipoError { 
    
        public string titulo;
        public string mensaje;

        public TipoError(int i,string msg){
        
            titulo="ERROR | ERR-" + (1920000+i);
            mensaje = "DETALLE ERROR:\n"+msg;
        
        }
    
    }
}
