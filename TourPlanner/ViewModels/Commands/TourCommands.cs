using System.Windows;
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
    public class DeleteCommand : CommandBaseOnChange
    {
        private ViewModel vm;
        public DeleteCommand(ViewModel viewModel) : base(viewModel)
        {
            vm = viewModel;
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
            vm.updateTourList();
            vm.DataGridDescription?.Reset();
        }
    }

    /* public class ReloadCommand : CommandBase
     {
         public ViewModel ViewModel { get; set; }
         public ReloadCommand(ViewModel viewModel)
         {
             this.ViewModel = viewModel;
         }

         public override void Execute(object parameter)
         {
             this.ViewModel.DataGridDescription?.Reset();
             this.ViewModel.updateTourList();
         }
     }*/
}
