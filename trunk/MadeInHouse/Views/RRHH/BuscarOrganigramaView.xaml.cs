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
using MadeInHouse.ViewModels.RRHH;

namespace MadeInHouse.Views.RRHH
{
    /// <summary>
    /// Lógica de interacción para BuscarOrganigramaView.xaml
    /// </summary>
    public partial class BuscarOrganigramaView : UserControl
    {
        public BuscarOrganigramaView()
        {
            InitializeComponent();
            DataContext = new BuscarOrganigramaViewModel();
        }


    }
}
