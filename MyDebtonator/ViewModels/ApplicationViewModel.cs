using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MyDebtonator.Helpers;

namespace MyDebtonator.ViewModels
{
    public class ApplicationViewModel : ObservableObject
    {
        private ICommand _exitApplicationCommand;

        public ICommand ExitApplicationCommand
        {
            get
            {
                return _exitApplicationCommand;
            }
            set
            {
                _exitApplicationCommand = value;
            }
        }

        public ApplicationViewModel()
        {
            ExitApplicationCommand = new RelayCommand(ExitApplication);
        }

        public void ExitApplication(object obj)
        {
            Application.Current.Shutdown();
        }
    }
}
