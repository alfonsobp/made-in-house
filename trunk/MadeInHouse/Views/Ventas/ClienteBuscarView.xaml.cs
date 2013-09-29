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
using MadeInHouse.ViewModels;

namespace MadeInHouse.Views.Ventas
{
    /// <summary>
    /// Lógica de interacción para ClienteBuscarView.xaml
    /// </summary>
    public partial class ClienteBuscarView : UserControl
    {
        public ClienteBuscarView()
        {
            InitializeComponent();
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Window.GetWindow(this).Width != 200)
                Window.GetWindow(this).DragMove();
        }

        private void CloseWin_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
            if (Window.GetWindow(this).Width == 200)
                fixTabs();
        }

        private void MinimizeWin_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this).Width != 200)
            {
                Window.GetWindow(this).SizeToContent = SizeToContent.Manual;
                Window.GetWindow(this).Width = 200;
                Window.GetWindow(this).Height = 20;
                if (MainViewModel.MinWin.Count == 0)
                {
                    MainViewModel.MinWin.Add(new List<Window>());
                }

                if (MainViewModel.MinWin.Last().Count < 5)
                {
                    MainViewModel.MinWin.Last().Add(Window.GetWindow(this));
                }
                else
                {
                    MainViewModel.MinWin.Add(new List<Window>());
                    MainViewModel.MinWin.Last().Add(Window.GetWindow(this));
                }


                Window.GetWindow(this).Left = Application.Current.MainWindow.ActualWidth - 30 - 210 * (MainViewModel.MinWin.Last().Count);
                Window.GetWindow(this).Top = Application.Current.MainWindow.ActualHeight - 30 - 30 * (MainViewModel.MinWin.Count);
            }
            else
            {
                Window.GetWindow(this).SizeToContent = SizeToContent.WidthAndHeight;
                Window.GetWindow(this).Top = 0.5 * (Application.Current.MainWindow.ActualHeight - Window.GetWindow(this).ActualHeight);
                Window.GetWindow(this).Left = 0.5 * (Application.Current.MainWindow.ActualWidth - Window.GetWindow(this).ActualWidth);

                fixTabs();
            }
        }

        private void fixTabs()
        {
            for (int i = 0; i < MainViewModel.MinWin.Count; i++)
            {
                MainViewModel.MinWin.ElementAt(i).Remove(Window.GetWindow(this));
                if (MainViewModel.MinWin.ElementAt(i).Count == 0)
                {
                    MainViewModel.MinWin.Remove(MainViewModel.MinWin.ElementAt(i));
                }
                if (i < MainViewModel.MinWin.Count - 1)
                {
                    if (MainViewModel.MinWin.ElementAt(i).Count < 5)
                    {
                        MainViewModel.MinWin.ElementAt(i).Add(MainViewModel.MinWin.ElementAt(i + 1).First());
                        MainViewModel.MinWin.ElementAt(i + 1).Remove(MainViewModel.MinWin.ElementAt(i + 1).First());
                    }
                }
            }

            for (int i = 0; i < MainViewModel.MinWin.Count; i++)
            {
                for (int j = 0; j < MainViewModel.MinWin.ElementAt(i).Count; j++)
                {
                    MainViewModel.MinWin.ElementAt(i).ElementAt(j).Left = Application.Current.MainWindow.ActualWidth - 30 - 210 * (j + 1);
                    MainViewModel.MinWin.ElementAt(i).ElementAt(j).Top = Application.Current.MainWindow.ActualHeight - 30 - 30 * (i + 1);
                }
            }
        }
    }
}