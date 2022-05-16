using System;
using System.Windows.Input;

namespace TourPlanner.ViewModels.Commands
{
    public class SelectedItemCommand : ICommand
    {
        public ViewModel ViewModel { get; set; }
        public SelectedItemCommand(ViewModel view)
        {
            this.ViewModel = view;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.ViewModel.CurTourName = parameter.ToString();
        }




    }
}
