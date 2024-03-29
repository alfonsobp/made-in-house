﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class LineaProducto
    {
        int idLinea;

        public int IdLinea
        {
            get { return idLinea; }
            set { idLinea = value; }
        }
        string abreviatura;

        public string Abreviatura
        {
            get { return abreviatura; }
            set { abreviatura = value; }
        }
        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private List<SubLineaProducto> sublineas;

        internal List<SubLineaProducto> Sublineas
        {
            get { return sublineas; }
            set { sublineas = value; }
        }

    }
}
