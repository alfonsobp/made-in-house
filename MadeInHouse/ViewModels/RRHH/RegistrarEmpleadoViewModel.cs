﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

using System.Windows;
using System.Windows.Input;
using System.ComponentModel.Composition;
using MadeInHouse.Models;
using MadeInHouse.Views.RRHH;

namespace MadeInHouse.ViewModels.RRHH 
{
    public class RegistrarEmpleadoViewModel:Screen
    {
        private MyWindowManager win = new MyWindowManager();

        public void AbrirArmarHorario()
        {
            win.ShowWindow(new RRHH.ArmarHorarioViewModel { DisplayName = "Armar Horario" });
        }

        private string txtCodEmp;

        public string TxtCodEmp
        {
            get { return txtCodEmp; }
            set { txtCodEmp = value; NotifyOfPropertyChange(() => TxtCodEmp); }
        }

        private string txtDni;

        public string TxtDni
        {
            get { return txtDni; }
            set { txtDni = value; NotifyOfPropertyChange(() => TxtDni); }
        }

        private string txtNomb;

        public string TxtNomb
        {
            get { return txtNomb; }
            set { txtNomb = value; NotifyOfPropertyChange(() => TxtNomb); }
        }

        private string txtApePat;

        public string TxtApePat
        {
            get { return txtApePat; }
            set { txtApePat = value; NotifyOfPropertyChange(() => TxtApePat); }
        }

        private string txtApeMat;

        public string TxtApeMat
        {
            get { return txtApeMat; }
            set { txtApeMat = value; NotifyOfPropertyChange(() => TxtApePat); }
        }

        private string txtFechNac;

        public string TxtFechNac
        {
            get { return txtFechNac; }
            set { txtFechNac = value; NotifyOfPropertyChange(() => TxtFechNac); }
        }

        private string txtRdbMasc;

        public string TxtRdbMasc
        {
            get { return txtRdbMasc; }
            set { txtRdbMasc = value; NotifyOfPropertyChange(() => TxtRdbMasc); }
        }

        private string txtRdbFem;

        public string TxtRdbFem
        {
            get { return txtRdbFem; }
            set { txtRdbFem = value; NotifyOfPropertyChange(() => TxtRdbFem); }
        }

        private string txtDir;

        public string TxtDir
        {
            get { return txtDir; }
            set { txtDir = value; NotifyOfPropertyChange(() => TxtDir); }
        }

        private string txtRef;

        public string TxtRef
        {
            get { return txtRef; }
            set { txtRef = value; NotifyOfPropertyChange(() => TxtRef); }
        }

        private string txtRutFoto;

        public string TxtRutFoto
        {
            get { return txtRutFoto; }
            set { txtRutFoto = value; NotifyOfPropertyChange(() => TxtRutFoto); }
        }

        private string txtTelef;

        public string TxtTelef
        {
            get { return txtTelef; }
            set { txtTelef = value; NotifyOfPropertyChange(() => TxtTelef); }
        }

        private string txtCel;

        public string TxtCel
        {
            get { return txtCel; }
            set { txtCel = value; NotifyOfPropertyChange(() => TxtCel); }
        }

        private string txtEmail;

        public string TxtEmail
        {
            get { return txtEmail; }
            set { txtEmail = value; NotifyOfPropertyChange(() => TxtEmail); }
        }

    }
}
