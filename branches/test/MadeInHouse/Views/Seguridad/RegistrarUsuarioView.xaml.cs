﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MadeInHouse.Views.Seguridad
{
    /// <summary>
    /// Lógica de interacción para RegistrarUsuarioView.xaml
    /// </summary>
    public partial class RegistrarUsuarioView : UserControl
    {
        public RegistrarUsuarioView()
        {
            InitializeComponent();
        }

        private void TxtContrasenha_PasswordChanged(object sender, RoutedEventArgs e)
        {
            TxtContrasenhaTB.Text = TxtContrasenha.Password;
        }

        private void TxtContrasenha2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            TxtContrasenhaTB2.Text = TxtContrasenha2.Password;
        }
    }
}
