using System;
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

namespace MadeInHouse.Views.Layouts
{
    /// <summary>
    /// Lógica de interacción para TitleBarTemplate.xaml
    /// </summary>
    public partial class TitleBarTemplate : UserControl
    {
        public static readonly DependencyProperty titleProperty = DependencyProperty.Register("title", typeof(string), typeof(TitleBarTemplate), new PropertyMetadata(string.Empty));

        private const int ALTURA = 33;
        private const int ANCHO = 200;
        private static int tabsXfila = 0;

        public string title
        {
            get { return (string)GetValue(titleProperty); }
            set { SetValue(titleProperty, value); }
        }

        public TitleBarTemplate()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (Window.GetWindow(this).Width != ANCHO)
                    Window.GetWindow(this).DragMove();
            }
        }

        private void CloseWin_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
            if (Window.GetWindow(this).Width == ANCHO)
                fixTabs();
        }

        private void MinimizeWin_Click(object sender, RoutedEventArgs e)
        {
            if (tabsXfila == 0)
            {
                double anchoWin = Application.Current.MainWindow.Owner.ActualWidth;
                double calcTabs = ((anchoWin / 636) * 477) / (ANCHO);
                tabsXfila = (int)Math.Truncate(calcTabs);
            }
            if (Window.GetWindow(this).Width != ANCHO)
            {
                Window.GetWindow(this).SizeToContent = SizeToContent.Manual;
                Window.GetWindow(this).Width = ANCHO;
                Window.GetWindow(this).Height = ALTURA;
                if (MainViewModel.MinWin.Count == 0)
                {
                    MainViewModel.MinWin.Add(new List<Window>());
                }

                if (MainViewModel.MinWin.Last().Count < tabsXfila)
                {
                    MainViewModel.MinWin.Last().Add(Window.GetWindow(this));
                }
                else
                {
                    MainViewModel.MinWin.Add(new List<Window>());
                    MainViewModel.MinWin.Last().Add(Window.GetWindow(this));
                }


                Window.GetWindow(this).Left = Application.Current.MainWindow.Owner.ActualWidth - 5 - (ANCHO + 5) * (MainViewModel.MinWin.Last().Count);
                Window.GetWindow(this).Top = Application.Current.MainWindow.Owner.ActualHeight - 5 - (ALTURA + 5) * (MainViewModel.MinWin.Count);
            }
            else
            {
                Window.GetWindow(this).SizeToContent = SizeToContent.WidthAndHeight;
                Window.GetWindow(this).Top = 0.5 * (Application.Current.MainWindow.Owner.ActualHeight - Window.GetWindow(this).ActualHeight);
                Window.GetWindow(this).Left = 0.5 * (Application.Current.MainWindow.Owner.ActualWidth - Window.GetWindow(this).ActualWidth);

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
                    if (MainViewModel.MinWin.ElementAt(i).Count < tabsXfila)
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
                    MainViewModel.MinWin.ElementAt(i).ElementAt(j).Left = Application.Current.MainWindow.Owner.ActualWidth - 5 - (ANCHO + 5) * (j + 1);
                    MainViewModel.MinWin.ElementAt(i).ElementAt(j).Top = Application.Current.MainWindow.Owner.ActualHeight - 5 - (ALTURA + 5) * (i + 1);
                }
            }
        }
    }
}
