using System.ComponentModel;
using System.Windows;
using TourPlanner.BussinesLayer;

namespace TourPlanner.ViewModels.Commands
{
    public class TourCommand : CommandBase
    {
        public TourChangesView ChangesView { get; set; }
        public TourCommand(TourChangesView changesView)
        {
            ChangesView = changesView;
            ChangesView.PropertyChanged += OnViewModelProbertyChanged;
        }
        public override bool CanExecute(object parameter)
        {
            if (string.IsNullOrEmpty(ChangesView.Tourname) || string.IsNullOrEmpty(ChangesView.From) || string.IsNullOrEmpty(ChangesView.To))
                return false;
            return true;
        }
        public override void Execute(object parameter)
        {
            this.ChangesView.CreateTourButton(parameter);
        }
        public virtual void OnViewModelProbertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ChangesView.Tourname) || e.PropertyName == nameof(ChangesView.From) || e.PropertyName == nameof(ChangesView.To))
                OnCanExecutedChanged();
        }
    }
    public class EditTourCommand : TourCommand
    {
        public EditTourCommand(TourChangesView changesView) : base(changesView) { }
        public override void Execute(object parameter) => this.ChangesView.EditTourButton(parameter);
    }
    public class DeleteCommand : CommandBaseOnChange
    {
        private ViewModel vm;
        public DeleteCommand(ViewModel viewModel) : base(viewModel)
        {
            vm = viewModel;
        }
        public override void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete Route", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    BussinessLogic.LogicInstance.DeleteRoute(parameter.ToString());
                    MessageBox.Show("Route successfully deleted", "Delete Route", MessageBoxButton.OK);
                    break;
            }
            vm.DataGridDescription?.Reset();
            vm.updateTourList();
        }
    }
}
