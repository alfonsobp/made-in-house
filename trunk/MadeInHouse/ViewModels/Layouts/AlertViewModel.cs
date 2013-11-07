using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.ComponentModel.Composition;

namespace MadeInHouse.ViewModels.Layouts
{
    [Export(typeof(AlertViewModel))]
    class AlertViewModel : Screen
    {
        private readonly IWindowManager _windowManager;

        #region constructor

        [ImportingConstructor]
        public AlertViewModel(IWindowManager windowmanager, string message)
        {
            _windowManager = windowmanager;
            Message = message;
        }

        #endregion

        #region atributos

        private string _isWarningImage;

        public string IsWarningImage
        {
            get { return _isWarningImage; }
            set
            {
                if (_isWarningImage == value) return;
                _isWarningImage = value;
                NotifyOfPropertyChange(() => IsWarningImage);
            }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                if (_message == value) return;
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        #endregion

        #region metodos

        public void Aceptar()
        {
            TryClose();
        }

        #endregion
    }
}
