using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BussinesLayer;

namespace TourPlanner.ViewModels.Commands
{
    public class CreateLogCommand : ICommand
    {
        public TourChangesView ChangesView { get; set; }
        public CreateLogCommand(TourChangesView changesView)
        {
            this.ChangesView = changesView;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            this.ChangesView.CreateLogButton(parameter);
        }
    }

    public class EditLogCommand : ICommand
    {
        public TourChangesView ChangesView { get; set; }
        public EditLogCommand(TourChangesView changesView)
        {
            this.ChangesView = changesView;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            this.ChangesView.EditLogButton(parameter);
        }
    }

    public class DeleteLogCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete Route", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    BussinessLogic.LogicInstance.Deletelog(parameter?.ToString());
                    MessageBox.Show("Log successfully deleted", "Delete Route", MessageBoxButton.OK);
                    break;
            }
        }
    }
}
