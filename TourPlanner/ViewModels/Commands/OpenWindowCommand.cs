using System;
using System.Windows.Input;

namespace TourPlanner.ViewModels.Commands
{
    public class OpenWindowCommand : ICommand
    {
        public static NewTourWindow createPopup = new NewTourWindow();

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            createPopup.Show();
        }
    }
    public class OpenEditWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            if (String.IsNullOrEmpty(parameter.ToString()))
                return false;
            return true;
        }
        public void Execute(object parameter)
        {
            EditTourWindow createPopup = new EditTourWindow(parameter.ToString());
            createPopup.Show();
        }
    }

    public class OpenInsertLogWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            CreateLogWindow createPopup = new CreateLogWindow(parameter.ToString());
            createPopup.Show();
        }
    }

    public class OpenEditLogWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            EditLogWindow createPopup = new EditLogWindow(parameter.ToString());
            createPopup.Show();
        }
    }
}
