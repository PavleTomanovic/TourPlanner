using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) { }
        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
