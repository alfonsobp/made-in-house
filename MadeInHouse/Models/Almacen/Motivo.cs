﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.Almacen
{
    class Motivo
    {
        
        public int Id { get; set; }
        public string NombreMotivo { get; set; }

        public Motivo() { }
        
        int idMotivo;

        public int IdMotivo
        {
            get { return idMotivo; }
            set { idMotivo = value; }
        }
        int docRef;

        public int DocRef
        {
            get { return docRef; }
            set { docRef = value; }
        }
        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
    }
}
