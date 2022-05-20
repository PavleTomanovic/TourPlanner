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
    public class TourReportCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (String.IsNullOrEmpty(parameter?.ToString()))
                return false;
            return true;
        }

        public void Execute(object? parameter)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Report Creation", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    bool createReport = BussinessLogic.LogicInstance.CreateRouteReport(parameter?.ToString());
                    if (createReport)
                        MessageBox.Show("TourReport successfully created!", "Report Creation", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("TourReport was not created!", "Report Creation", MessageBoxButton.OK, MessageBoxImage.Stop);
                    break;
            }
        }
    }

    public class SummarizeReportCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (String.IsNullOrEmpty(parameter?.ToString()))
                return false;
            return true;
        }

        public void Execute(object? parameter)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Report Creation", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    bool createReport = BussinessLogic.LogicInstance.CreateSummarizeReport(parameter?.ToString());
                    if (createReport)
                        MessageBox.Show("SummarizeReport successfully created!", "Report Creation", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("SummarizeReport was not created!", "Report Creation", MessageBoxButton.OK, MessageBoxImage.Stop);
                    break;
            }
        }
    }
}
