using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.BussinesLayer;

namespace TourPlanner.ViewModels.Commands
{
    public class FavoriteYesCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Favorite Route", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    BussinessLogic.LogicInstance.MakeRouteFavorite(parameter.ToString());
                    MessageBox.Show("Route successfully added to favorites!", "Favorite Route", MessageBoxButton.OK);
                    break;
            }
        }
    }
    public class FavoriteNoCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Favorite Route", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    BussinessLogic.LogicInstance.DeleteRouteFromFavorites(parameter.ToString());
                    MessageBox.Show("Route successfully deleted from favorites!", "Favorite Route", MessageBoxButton.OK);
                    break;
            }
        }
    }
}
