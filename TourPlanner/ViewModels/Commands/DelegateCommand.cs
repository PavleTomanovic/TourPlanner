using System;
using System.Windows.Input;

namespace TourPlanner.ViewModels.Commands
{
    public class DelegateCommand : ICommand
    {
        readonly Action<object> execute;
        readonly Predicate<object> canExecute;
        public DelegateCommand(Predicate<object> canExecute, Action<object> execute) =>
            (this.canExecute, this.execute) = (canExecute, execute);
        public DelegateCommand(Action<object> execute) : this(null, execute) { }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => this.canExecute(parameter);
        public void Execute(object parameter) => this.execute?.Invoke(parameter);
        protected void OnCanExecutedChanged() => this.CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}
