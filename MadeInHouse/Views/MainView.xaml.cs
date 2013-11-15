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
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Threading;
using MadeInHouse.Models.RRHH;

namespace MadeInHouse.Views
{
    /// <summary>
    /// Lógica de interacción para MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {   

            InitializeComponent();
            List<Empleado> lstEmpleado = new List<Empleado>();            
            lstEmpleado = DataObjects.RRHH.EmpleadoSQL.BuscarEmpleadoId(Int32.Parse(Thread.CurrentPrincipal.Identity.Name));
            nombreUsuario.Content = "Usuario: " + lstEmpleado[0].Nombre + " " + lstEmpleado[0].ApePaterno + " " + lstEmpleado[0].ApeMaterno;
            
            
        }

        private void CloseWin_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void cambiarExpander(object sender, RoutedEventArgs e)
        {
            if (sender == butAlmacen)
            {
                mover(expanderAlmacen, butAlmacen);

                salir(expanderRRHH, butRRHH);
                salir(expanderSeguridad, butSeguridad);
                salir(expanderVentas, butVentas);
                salir(expanderCompras, butCompras);
                salir(expanderReportes, butReportes);
                salir(expanderConfiguracion, butConfiguracion);
                salir(expanderClima, butClima);
            }
            if (sender == butRRHH)
            {
                mover(expanderRRHH, butRRHH);

                salir(expanderAlmacen, butAlmacen);
                salir(expanderSeguridad, butSeguridad);
                salir(expanderVentas, butVentas);
                salir(expanderCompras, butCompras);
                salir(expanderReportes, butReportes);
                salir(expanderConfiguracion, butConfiguracion);
                salir(expanderClima, butClima);
            }
            if (sender == butSeguridad)
            {
                mover(expanderSeguridad, butSeguridad);
                salir(expanderAlmacen, butAlmacen);
                salir(expanderRRHH,butRRHH);
                salir(expanderVentas, butVentas);
                salir(expanderCompras, butCompras);
                salir(expanderReportes, butReportes);
                salir(expanderConfiguracion, butConfiguracion);
                salir(expanderClima, butClima);
                

            }
            if (sender == butCompras)
            {
                mover(expanderCompras, butCompras);
                salir(expanderAlmacen, butAlmacen);
                salir(expanderRRHH, butRRHH);
                salir(expanderVentas, butVentas);
                salir(expanderSeguridad, butSeguridad);
                salir(expanderReportes, butReportes);
                salir(expanderConfiguracion, butConfiguracion);
                salir(expanderClima, butClima);
            }
            if (sender == butVentas)
            {
                mover(expanderVentas, butVentas);
                salir(expanderAlmacen, butAlmacen);
                salir(expanderRRHH, butRRHH);
                salir(expanderSeguridad, butSeguridad);
                salir(expanderCompras, butCompras);
                salir(expanderReportes, butReportes);
                salir(expanderConfiguracion, butConfiguracion);
                salir(expanderClima, butClima);
            }
            if (sender == butReportes)
            {
                mover(expanderReportes, butReportes);
                salir(expanderAlmacen, butAlmacen);
                salir(expanderRRHH, butRRHH);
                salir(expanderVentas, butVentas);
                salir(expanderCompras, butCompras);
                salir(expanderSeguridad, butSeguridad);
                salir(expanderConfiguracion, butConfiguracion);
                salir(expanderClima, butClima);
            }
            if (sender == butConfiguracion)
            {
                mover(expanderConfiguracion, butConfiguracion);
                salir(expanderAlmacen, butAlmacen);
                salir(expanderRRHH, butRRHH);
                salir(expanderVentas, butVentas);
                salir(expanderCompras, butCompras);
                salir(expanderReportes, butReportes);
                salir(expanderSeguridad, butSeguridad);
                salir(expanderClima, butClima);
            }
            if (sender == butClima)
            {
                mover(expanderClima, butClima);
                salir(expanderAlmacen, butAlmacen);
                salir(expanderRRHH, butRRHH);
                salir(expanderVentas, butVentas);
                salir(expanderCompras, butCompras);
                salir(expanderReportes, butReportes);
                salir(expanderConfiguracion, butConfiguracion);
                salir(expanderSeguridad, butSeguridad);
            }
        }
        private void agrandar(object sender, RoutedEventArgs e)
        {

            Button B = (Button)sender;
            B.Width = 95;
            B.Margin = new Thickness(B.Margin.Left - 10, B.Margin.Top - 20, 0, 0);
            B.Height = 95;
        }
        private void achicar(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender;
            B.Width = 75;
            B.Margin = new Thickness(B.Margin.Left + 10, B.Margin.Top + 20, 0, 0);
            B.Height = 75;
        }


        private void mover(Expander e,Button b)
        {
            e.Height = 0;
            e.Margin = b.Margin;

            ThicknessAnimation anima = new ThicknessAnimation(e.Margin, new Thickness(0, 0, 0, 0), new Duration(TimeSpan.FromSeconds(0.15)), FillBehavior.HoldEnd);
            e.BeginAnimation(Border.MarginProperty, anima);

            DoubleAnimation animation = new DoubleAnimation(300, TimeSpan.FromSeconds(0.15));
            e.BeginAnimation(Expander.WidthProperty, animation);

            DoubleAnimation animation2 = new DoubleAnimation(1024.5, TimeSpan.FromSeconds(0.1));
            e.BeginAnimation(Expander.HeightProperty, animation2);

        }

        private void salir(Expander e, Button b)
        {
            e.Height = 1024.5;
            ThicknessAnimation anima = new ThicknessAnimation(e.Margin, b.Margin, new Duration(TimeSpan.FromSeconds(0.15)), FillBehavior.HoldEnd);
            e.BeginAnimation(Border.MarginProperty, anima);

            DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.18));
            e.BeginAnimation(Expander.WidthProperty, animation);

            DoubleAnimation animation2 = new DoubleAnimation(0, TimeSpan.FromSeconds(0.1));
            e.BeginAnimation(Expander.HeightProperty, animation2);

        }

    }
}
