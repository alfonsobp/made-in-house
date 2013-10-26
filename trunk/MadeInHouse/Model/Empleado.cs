using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Model
{
    class Empleado
    {
        int idEmpleado;
        string apeMaterno;
        string apePaterno;
        char celular;
        string codEmpleado;
        string cuentaBancaria;
        string dni;
        string email;
        string emailEmpresa;
        int estado;
        DateTime fechaReg;
        string foto;
        CategoriaSalarial categoria;
        Puesto puesto;
        string nombre;
        string ruc;
        int semVacacion;
        int sexo;
        string telefono;

        List<Beneficio> beneficios;
        List<Asistencia> asistencia;
    }
}
