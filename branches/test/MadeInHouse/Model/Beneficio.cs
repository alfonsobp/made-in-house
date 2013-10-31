using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Beneficio
    {
        int idBeneficio;

        public int IdBeneficio
        {
            get { return idBeneficio; }
            set { idBeneficio = value; }
        }
        string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        double factor;

        public double Factor
        {
            get { return factor; }
            set { factor = value; }
        }
        double montoFijo;

        public double MontoFijo
        {
            get { return montoFijo; }
            set { montoFijo = value; }
        }
        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
    }
}
