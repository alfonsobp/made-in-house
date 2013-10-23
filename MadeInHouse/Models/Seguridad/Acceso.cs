using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MadeInHouse.Models.Seguridad
{
    public class Acceso
    {
        int[] accModulo = new int[7]; //es un arreglo de enteros que contiene 0 y 1

        public int[] AccModulo
        {
            get { return accModulo; }
            set { accModulo = value; }
        }

        
        int[,] accVentana = new int[5, 50];

        public int[,] AccVentana
        {
            get { return accVentana; }
            set { accVentana = value; }
        }

        int[, ,] accVentanaInterna = new int[5, 50, 10];

        public int[, ,] AccVentanaInterna
        {
            get { return accVentanaInterna; }
            set { accVentanaInterna = value; }
        }

        public Acceso()
        {

        }

        public Acceso(int[] accModulo, int[,] accVentana, int[, ,] accVentanaInterna)
        {
            this.accModulo = accModulo;
            this.accVentana = accVentana;
            this.accVentanaInterna = accVentanaInterna;
        }
    }
}
