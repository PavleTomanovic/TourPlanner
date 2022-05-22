using System;
using System.ComponentModel;
using System.Windows.Input;

namespace TourPlanner.ViewModels.Commands
{
    public class OpenWindowCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            NewTourWindow createPopup = new NewTourWindow();
            createPopup.Show();
        }
    }
    public class OpenEditWindowCommand : CommandBaseOnChange
    {
        ViewModel vm;
        public OpenEditWindowCommand(ViewModel viewModel) : base(viewModel)
        {
            vm = viewModel;
        }
        public override void Execute(object parameter)
        {

            EditTourWindow createPopup = new EditTourWindow(parameter.ToString(), vm.CurTourName);
            createPopup.Show();
        }
    }
    public class OpenInsertLogWindowCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            CreateLogWindow createPopup = new CreateLogWindow(parameter.ToString());
            createPopup.Show();
        }
    }
    public class OpenEditLogWindowCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            EditLogWindow createPopup = new EditLogWindow(parameter.ToString());
            createPopup.Show();
        }
    }
}
