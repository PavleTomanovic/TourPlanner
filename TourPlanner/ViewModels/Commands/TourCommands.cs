using System;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BussinesLayer;

namespace TourPlanner.ViewModels.Commands
{
    public class TourCommand : CommandBase
    {
        public TourChangesView ChangesView { get; set; }
        public TourCommand(TourChangesView changesView)
        {
            this.ChangesView = changesView;
        }

        public override void Execute(object parameter)
        {
            this.ChangesView.CreateTourButton(parameter);
        }
    }
    public class EditTourCommand : CommandBase
    {
        public TourChangesView ChangesView { get; set; }
        public EditTourCommand(TourChangesView changesView)
        {
            this.ChangesView = changesView;

        }

        public override void Execute(object parameter)
        {
            this.ChangesView.EditTourButton(parameter);
        }
    }
    public class DeleteCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            if (String.IsNullOrEmpty(parameter.ToString()))
                return false;
            return true;
        }
        public override void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete Route", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    BussinessLogic.LogicInstance.DeleteRoute(parameter.ToString());
                    MessageBox.Show("Route successfully deleted", "Delete Route", MessageBoxButton.OK);
                    break;
            }
        }
    }
}
