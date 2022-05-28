using System;
using System.ComponentModel;
using TourPlanner.DTO;

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
            EditTourWindow createPopup = new EditTourWindow(parameter.ToString(), vm.CurTourName, vm.CurFrom, vm.CurTo, vm.CurTransport, vm.CurComment);
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
        private ViewModel viewModel;
        public OpenEditLogWindowCommand(ViewModel viewModel)
        {
            viewModel = viewModel;
            viewModel.PropertyChanged += OnViewModelProbertyChanged;
        }
        public override bool CanExecute(object parameter)
        {
            if (String.IsNullOrEmpty(parameter?.ToString()))
                return false;
            return true;
        }
        public void OnViewModelProbertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.NewTourLogDTO))
                OnCanExecutedChanged();
        }
        public override void Execute(object parameter)
        {
            TourLogDTO tourLogDTO = new TourLogDTO();
            tourLogDTO = (TourLogDTO)parameter;
            EditLogWindow createPopup = new EditLogWindow(tourLogDTO);
            createPopup.Show();
        }
    }
}
