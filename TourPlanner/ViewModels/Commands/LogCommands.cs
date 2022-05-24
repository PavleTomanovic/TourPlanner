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
        public LogChangesView LogChangesView { get; set; }
        public EditLogCommand(LogChangesView logChangesView)
        {
            this.LogChangesView = logChangesView;
        }
        public override void Execute(object parameter)
        {
            this.LogChangesView.EditLogButton(parameter);
        }
    }

    public class DeleteLogCommand : CommandBase
    {
        private TourLogDTO tourLogDTO { get; set; }

        public override void Execute(object parameter)
        {
            this.tourLogDTO = (TourLogDTO)parameter;
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete Route", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    BussinessLogic.LogicInstance.Deletelog(tourLogDTO.RouteId.ToString(), tourLogDTO.LogId.ToString());
                    MessageBox.Show("Log successfully deleted", "Delete Route", MessageBoxButton.OK);
                    break;
            }
        }
    }
}
