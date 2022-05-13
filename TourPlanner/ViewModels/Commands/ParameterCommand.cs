using System;
using System.Windows.Input;

namespace TourPlanner.ViewModels.Commands
{
    public class ParameterCommand : ICommand
    {
        public TourChangesView ChangesView { get; set; }
        public ParameterCommand(TourChangesView changesView)
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
            this.ChangesView.CreateTourButton(parameter);
        }




    }
}
