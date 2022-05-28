using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace TourPlanner.ViewModels.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public virtual bool CanExecute(object parameter) => true;
        public abstract void Execute(object parameter);
        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }

    public abstract class CommandBaseOnChange : CommandBase
    {
        private readonly ViewModel _viewModel;
        public CommandBaseOnChange(ViewModel viewModel)
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
        public virtual void OnViewModelProbertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.CurTourName))
                OnCanExecutedChanged();
        }
    }
}
