using System.Collections.Generic;
using TourPlanner.BussinesLayer;
using TourPlanner.Models;

namespace TourPlanner.ViewModels.Commands
{
    public class SearchCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            List<TourPreview> searchResult = new List<TourPreview>();
            searchResult = BussinessLogic.LogicInstance.PrepareListRouteForSearch(parameter?.ToString());
        }
    }
}
