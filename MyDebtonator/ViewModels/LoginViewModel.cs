using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MyDebtonator.Helpers;

namespace MyDebtonator.ViewModels
{
    public class LoginViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private string _name;
        private string _title;

        private ICommand _exitApplicationCommand;

        #endregion

        #region Properties

        public string Name 
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

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

        #endregion

        #region Constructors

        public LoginViewModel()
        {
            Name = "LoginView";
            Title = "Welcome to My Debtonator";

            ExitApplicationCommand = new RelayCommand(ApplicationMethods.ExitApplication);
        }

        #endregion
    }
}
