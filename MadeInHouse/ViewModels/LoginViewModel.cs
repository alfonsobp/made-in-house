using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels
{
    public class LoginViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public void enter()
        {
            WindowManager win = new WindowManager();
           
            MainViewModel main = new MainViewModel();
            win.ShowWindow(main);
            this.TryClose();
        }
    }
}
