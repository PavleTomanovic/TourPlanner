using System;
using System.Windows.Input;

namespace TourPlanner.ViewModels.Commands
{
    public class OpenWindowCommand : ICommand
    {
        private string Id;
        public static NewTourWindow createPopup = new NewTourWindow();
        public OpenWindowCommand(string id)
        {
            this.Id = id;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (Id != null)
                return true;
            else
                return false;
        }

        public void Execute(object parameter)
        {
            createPopup.Show();
        }
    }
    public class OpenEditWindowCommand : ICommand
    {
        private string Id;
        public static EditTourWindow createPopup = new EditTourWindow();
        public OpenEditWindowCommand(string id)
        {
            this.Id = id;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (Id != null)
                return true;
            else
                return false;
        }

        public void Execute(object parameter)
        {
            createPopup.Show();
        }
    }
}
