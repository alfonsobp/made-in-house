using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Validacion
{
    class Evaluador
    {

        public bool esIgual(string l, int n) {

            return (l.Length == n);
        }

        public bool esNumeroLargo(string l) {
            Int64 x;

           return  Int64.TryParse(l,out  x);
        
        }


        public bool esMayor(string l, int n)
        {

            return (l.Length >= n);
        }

        public bool esMenor(string l, int n)
        {

            return (l.Length <= n);
        }

        public bool esNumeroEntero(string l) {
            int x;

            return int.TryParse(l,out x);
        
        }

        public bool esNumeroReal(string l)
        {
            double x;

            return double.TryParse(l, out x);

        }

        public bool esPositivo(int x) {

            return (x >= 0);
        }

        public bool esPositivo(double x)
        {

            return (x >= 0);
        }

        public bool evalString(string x) 
        {
            if (string.IsNullOrEmpty(x)) return false;
            if ( x.Contains('"')) return false;
            if (x.Contains('\'')) return false;
            return true;
        }

    }
}
