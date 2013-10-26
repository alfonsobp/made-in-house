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

        public int IdEmpleado
        {
            get { return idEmpleado; }
            set { idEmpleado = value; }
        }
        string apeMaterno;

        public string ApeMaterno
        {
            get { return apeMaterno; }
            set { apeMaterno = value; }
        }
        string apePaterno;

        public string ApePaterno
        {
            get { return apePaterno; }
            set { apePaterno = value; }
        }
        string celular;

        public string Celular
        {
            get { return celular; }
            set { celular = value; }
        }
        string codEmpleado;

        public string CodEmpleado
        {
            get { return codEmpleado; }
            set { codEmpleado = value; }
        }
        string cuentaBancaria;

        public string CuentaBancaria
        {
            get { return cuentaBancaria; }
            set { cuentaBancaria = value; }
        }
        string dni;

        public string Dni
        {
            get { return dni; }
            set { dni = value; }
        }
        string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        string emailEmpresa;

        public string EmailEmpresa
        {
            get { return emailEmpresa; }
            set { emailEmpresa = value; }
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
        string foto;

        public string Foto
        {
            get { return foto; }
            set { foto = value; }
        }
        CategoriaSalarial categoria;

        public CategoriaSalarial Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
        Puesto puesto;

        public Puesto Puesto
        {
            get { return puesto; }
            set { puesto = value; }
        }
        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        string ruc;

        public string Ruc
        {
            get { return ruc; }
            set { ruc = value; }
        }
        int semVacacion;

        public int SemVacacion
        {
            get { return semVacacion; }
            set { semVacacion = value; }
        }
        int sexo;

        public int Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }
        string telefono;

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        List<Beneficio> beneficios;

        public List<Beneficio> Beneficios
        {
            get { return beneficios; }
            set { beneficios = value; }
        }
        List<Asistencia> asistencia;

        public List<Asistencia> Asistencia
        {
            get { return asistencia; }
            set { asistencia = value; }
        }
    }
}
