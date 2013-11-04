using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Models.RRHH
{
    class Empleado
    {

        string dni;

        public string Dni
        {
            get { return dni; }
            set { dni = value; }
        }
        

        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        string apePaterno;

        public string ApePaterno
        {
            get { return apePaterno; }
            set { apePaterno = value; }
        }

        string apeMaterno;

        public string ApeMaterno
        {
            get { return apeMaterno; }
            set { apeMaterno = value; }
        }
        string telefono;

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        string celular;

        public string Celular
        {
            get { return celular; }
            set { celular = value; }
        }


        string emailEmpleado;

        public string EmailEmpleado
        {
            get { return emailEmpleado; }
            set { emailEmpleado = value; }
        }

        string emailEmpresa;

        public string EmailEmpresa
        {
            get { return emailEmpresa; }
            set { emailEmpresa = value; }
        }

        string cuentaBancaria;

        public string CuentaBancaria
        {
            get { return cuentaBancaria; }
            set { cuentaBancaria = value; }
        }

        string banco;

        public string Banco
        {
            get { return banco; }
            set { banco = value; }
        }

        int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        string fechaReg;

        public string FechaReg
        {
            get { return fechaReg; }
            set { fechaReg = value; }
        }

        string idPuesto;

        public string IdPuesto
        {
            get { return idPuesto; }
            set { idPuesto = value; }
        }

        string idCategoria;

        public string IdCategoria
        {
            get { return idCategoria; }
            set { idCategoria = value; }
        }

        string idEmpleado;

        public string IdEmpleado
        {
            get { return idEmpleado; }
            set { idEmpleado = value; }
        }
        string refFoto;

        public string RefFoto
        {
            get { return refFoto; }
            set { refFoto = value; }
        }

        string direccion;

        public string Direccion
        {
            get { return direccion;}
            set { direccion = value; }
        }
        string referencia;

        public string Referecia
        {
            get { return referencia; }
            set { referencia = value; }
        }

        string fechNacimiento;

        public string FechNacimiento
        {
            get { return fechNacimiento; }
            set { fechNacimiento = value; }
        }

        string tienda;
        public string Tienda
        {
            get { return tienda; }
            set { tienda = value; }
        }
        string puesto;
        public string Puesto
        {
            get { return puesto; }
            set { puesto = value; }
        }
        string area;
        public string Area
        {
            get { return area; }
            set { area = value; }
        }
        decimal sueldo;
        public decimal Sueldo
        {
            get { return sueldo; }
            set { sueldo = value; }
        }

        string sexo;

        public string Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }
        string codEmpleado;

        public string CodEmpleado
        {
            get { return codEmpleado; }
            set { codEmpleado = value; }
        }

        public Empleado()
        {

        }

        public Empleado(string dni, string sexo, string nombre, string apePaterno, string apeMaterno, string telefono,
                        string celular, string emailEmpleado, 
                        string cuentaBancaria, int estado, string fechaReg, string idPuesto, string idCategoria,
                        string idEmpleado, string refFoto, string direccion)
        {
            this.dni = dni;
            this.sexo = sexo;
            this.nombre = nombre;
            this.ApeMaterno = apePaterno;
            this.apeMaterno = apeMaterno;
            this.telefono = telefono;
            this.celular = celular;
            this.emailEmpleado = emailEmpleado;
            this.cuentaBancaria = cuentaBancaria;
            this.estado = estado;
            this.fechaReg = fechaReg;
            this.idPuesto = idPuesto;
            this.idCategoria = idCategoria;
            this.idEmpleado = idEmpleado;
            this.refFoto = refFoto;
            this.Direccion = direccion;
            //this.emailEmpresa = emailEmpresa;
            
        }

    }
}
