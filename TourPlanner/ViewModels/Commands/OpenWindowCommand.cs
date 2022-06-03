using System;
using System.ComponentModel;
using TourPlanner.DTO;

namespace TourPlanner.ViewModels.Commands
{
    public class OpenWindowCommand : CommandBase
    {
        private ViewModel vm;
        public OpenWindowCommand(ViewModel vm)
        {
            this.vm = vm;
        }

        public override void Execute(object parameter)
        {
            NewTourWindow createPopup = new NewTourWindow(vm);
            createPopup.Show();
        }
    }
    public class OpenEditWindowCommand : CommandBaseOnChange
    {
        ViewModel vm;
        public OpenEditWindowCommand(ViewModel viewModel) : base(viewModel)
        {
            this.vm = viewModel;
        }
        public override void Execute(object parameter)
        {
            EditTourWindow createPopup = new EditTourWindow(vm, parameter.ToString(), vm.CurTourName, vm.CurFrom, vm.CurTo, vm.CurTransport, vm.CurComment);
            createPopup.Show();
        }
    }
    public class OpenInsertLogWindowCommand : CommandBase
    {
        private ViewModel viewModel;
        public OpenInsertLogWindowCommand(ViewModel vm)
        {
            this.viewModel = vm;
        }
        public override void Execute(object parameter)
        {
            CreateLogWindow createPopup = new CreateLogWindow(parameter.ToString(), viewModel);
            createPopup.Show();
        }
    }
    public class OpenEditLogWindowCommand : CommandBase
    {
        private ViewModel viewModel;
        public OpenEditLogWindowCommand(ViewModel vm)
        {
            this.viewModel = vm;
            this.viewModel.PropertyChanged += OnViewModelProbertyChanged;
        }
        public override bool CanExecute(object parameter)
        {
            if (string.IsNullOrEmpty(viewModel.NewTourLogDTO?.LogId))
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
            EditLogWindow createPopup = new EditLogWindow(tourLogDTO, viewModel);
            createPopup.Show();
        }
    }
}
