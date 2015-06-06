using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MyDebtonator.Helpers
{
    public static class ApplicationMethods
    {
        public static void AboutMessage(object obj)
        {
            string message = "My Debtonator \n\nProgrammed by Joseph King\n\nTitle by Carla Olivares\n\n© 2015";

            MessageBox.Show(message, "About My Debtonator", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void ExitApplication(object obj)
        {
            Application.Current.Shutdown();
        }

        public static void OpenCalculator(object obj)
        {
            System.Diagnostics.Process.Start("Calc");
        }
    }
}
