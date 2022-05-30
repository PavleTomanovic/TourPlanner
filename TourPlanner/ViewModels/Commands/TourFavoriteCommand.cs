using System.Windows;
using TourPlanner.BussinesLayer;

namespace TourPlanner.ViewModels.Commands
{
    public class FavoriteYesCommand : CommandBaseOnChange
    {
        private ViewModel viewModel;
        public FavoriteYesCommand(ViewModel vm) : base(vm)
        {
            viewModel = vm;
        }
        public override void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Favorite Route", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    BussinessLogic.LogicInstance.MakeRouteFavorite(parameter.ToString());
                    MessageBox.Show("Route successfully added to favorites!", "Favorite Route", MessageBoxButton.OK);
                    viewModel.updateView();
                    break;
            }
        }
    }
    public class FavoriteNoCommand : CommandBaseOnChange
    {
        private ViewModel viewModel;
        public FavoriteNoCommand(ViewModel vm) : base(vm)
        {
            viewModel = vm;
        }
        public override void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Favorite Route", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    BussinessLogic.LogicInstance.DeleteRouteFromFavorites(parameter.ToString());
                    MessageBox.Show("Route successfully deleted from favorites!", "Favorite Route", MessageBoxButton.OK);
                    viewModel.updateView();
                    break;
            }
        }
    }
}
