using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MyDebtonator.Helpers;
using MyDebtonator.Models;

namespace MyDebtonator.ViewModels
{
    public class ApplicationViewModel : ObservableObject
    {
        #region Fields

        private ICommand _openCalculatorCommand;
        private ICommand _aboutMessageCommand;
        private ICommand _exitApplicationCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModel;

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

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModel == null)
                {
                    _pageViewModel = new List<IPageViewModel>();
                }

                return _pageViewModel;
            }
        }

        #endregion

        #region Constructor

        public ApplicationViewModel()
        {
            OpenCalculatorCommand = new RelayCommand(ApplicationMethods.OpenCalculator);
            AboutMessageCommand = new RelayCommand(ApplicationMethods.AboutMessage);
            ExitApplicationCommand = new RelayCommand(ApplicationMethods.ExitApplication);

            PageViewModels.Add(new LoginViewModel());

            CurrentPageViewModel = PageViewModels[0];
        }

        #endregion
    }
}
