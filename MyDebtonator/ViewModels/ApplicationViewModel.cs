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
        #region Fields

        private ICommand _openCalculatorCommand;
        private ICommand _aboutMessageCommand;
        private ICommand _exitApplicationCommand;

        #endregion

        #region Properties

        public ICommand OpenCalculatorCommand
        {
            get
            {
                return _openCalculatorCommand;
            }
            set
            {
                _openCalculatorCommand = value;
            }
        }

        public ICommand AboutMessageCommand
        {
            get
            {
                return _aboutMessageCommand;
            }
            set
            {
                _aboutMessageCommand = value;
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

        #region Constructor

        public ApplicationViewModel()
        {
            OpenCalculatorCommand = new RelayCommand(OpenCalculator);
            AboutMessageCommand = new RelayCommand(AboutMessage);
            ExitApplicationCommand = new RelayCommand(ExitApplication);
        }

        #endregion

        #region Methods

        public void OpenCalculator(object obj)
        {
            System.Diagnostics.Process.Start("Calc");
        }

        public void AboutMessage(object obj)
        {
            string message = "My Debtonator \n\nProgrammed by Joseph King\n\nTitle by Carla Olivares\n\n© 2015";

            MessageBox.Show(message, "About My Debtonator", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ExitApplication(object obj)
        {
            Application.Current.Shutdown();
        }

        #endregion
    }
}
