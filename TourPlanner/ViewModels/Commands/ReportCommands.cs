using System.Windows;
using TourPlanner.BussinesLayer;

namespace TourPlanner.ViewModels.Commands
{
    public class TourReportCommand : CommandBaseOnChange
    {
        public TourReportCommand(ViewModel viewModel) : base(viewModel) { }
        public override void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Report Creation", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    UIServices.SetBusyState();
                    bool createReport = BussinessLogic.LogicInstance.CreateRouteReport(parameter.ToString());
                    if (createReport)
                        MessageBox.Show("TourReport successfully created!", "Report Creation", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("TourReport was not created!", "Report Creation", MessageBoxButton.OK, MessageBoxImage.Stop);
                    break;
            }
        }
    }

    public class SummarizeReportCommand : CommandBaseOnChange
    {
        public SummarizeReportCommand(ViewModel viewModel) : base(viewModel) { }
        public override void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Report Creation", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    UIServices.SetBusyState();
                    bool createReport = BussinessLogic.LogicInstance.CreateSummarizeReport(parameter.ToString());
                    if (createReport)
                        MessageBox.Show("SummarizeReport successfully created!", "Report Creation", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("SummarizeReport was not created!", "Report Creation", MessageBoxButton.OK, MessageBoxImage.Stop);
                    break;
            }
        }
    }
}
