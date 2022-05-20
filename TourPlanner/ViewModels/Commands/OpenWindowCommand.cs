using System;
using System.ComponentModel;
using System.Windows.Input;

namespace TourPlanner.ViewModels.Commands
{
    public class OpenWindowCommand : CommandBase
    {
        public NewTourWindow createPopup;
        public override void Execute(object parameter)
        {
            createPopup = new NewTourWindow();
            createPopup.Show();
        }
    }
    public class OpenEditWindowCommand : CommandBase
    {
        private readonly ViewModel _viewModel;
        public OpenEditWindowCommand(ViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelProbertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            if (String.IsNullOrEmpty(parameter.ToString()))
                return false;
            return true;
        }
        public override void Execute(object parameter)
        {
            EditTourWindow createPopup = new EditTourWindow(parameter.ToString());
            createPopup.Show();
        }
        private void OnViewModelProbertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.CurTourName))
                OnCanExecutedChanged();
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
