using System;
using System.Windows.Input;

namespace TourPlanner.ViewModels.Commands
{
    public class EditTourCommand : ICommand
    {
        public TourChangesView ChangesView { get; set; }
        public EditTourCommand(TourChangesView changesView)
        {
            this.ChangesView = changesView;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.ChangesView.EditTourButton(parameter);
        }
    }
}
