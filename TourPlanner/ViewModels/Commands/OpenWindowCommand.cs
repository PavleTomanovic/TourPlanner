using System;
using System.Windows.Input;

namespace TourPlanner.ViewModels.Commands
{
    public class OpenWindowCommand : ICommand
    {
        public static NewTourWindow createPopup = new NewTourWindow();
        public OpenWindowCommand()
        {
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            createPopup.Show();
        }
    }
    public class OpenEditWindowCommand : ICommand
    {
        public OpenEditWindowCommand()
        {
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (parameter != null)
                return true;
            else
                return false;
        }

        public void Execute(object parameter)
        {
            EditTourWindow createPopup = new EditTourWindow(parameter.ToString());
            createPopup.Show();
        }
    }
}
