using System.ComponentModel;
using System.Windows;
using TourPlanner.BussinesLayer;
using TourPlanner.DTO;

namespace TourPlanner.ViewModels.Commands
{
    public class CreateLogCommand : CommandBase
    {
        public LogChangesView LogChangesView { get; set; }
        public CreateLogCommand(LogChangesView logChangesView)
        {
            this.LogChangesView = logChangesView;
        }
        public override void Execute(object parameter)
        {
            this.LogChangesView.CreateLogButton(parameter);
        }
    }

    public class EditLogCommand : CommandBase
    {
        public LogChangesView LogChangesView;
        public EditLogCommand(LogChangesView logChangesView)
        {
            this.LogChangesView = logChangesView;
        }
        public override void Execute(object parameter)
        {
            this.LogChangesView.EditLogButton();
        }
    }

    public class DeleteLogCommand : CommandBase
    {
        public ViewModel viewModel;
        public DeleteLogCommand(ViewModel vm)
        {
            viewModel = vm;
            viewModel.PropertyChanged += OnViewModelProbertyChanged;
        }
        public override bool CanExecute(object parameter)
        {
            if (string.IsNullOrEmpty(viewModel.NewTourLogDTO?.LogId))
                return false;
            return true;
        }
        public override void Execute(object parameter)
        {

            TourLogDTO tourLogDTO = new TourLogDTO();
            tourLogDTO = (TourLogDTO)parameter;
            if (tourLogDTO.LogId != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete Route", MessageBoxButton.YesNo);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        BussinessLogic.LogicInstance.Deletelog(tourLogDTO.RouteId.ToString(), tourLogDTO.LogId.ToString());
                        MessageBox.Show("Log successfully deleted!", "Delete Route", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Please select log to delete!", "Delete Route", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void OnViewModelProbertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.NewTourLogDTO))
                OnCanExecutedChanged();
        }
    }
}
