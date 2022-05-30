using System.Collections.Generic;
using TourPlanner.BussinesLayer;
using TourPlanner.Models;

namespace TourPlanner.ViewModels.Commands
{
    public class SearchCommand : CommandBase
    {
        private ViewModel viewModel { get; set; }
        public SearchCommand(ViewModel vm)
        {
            this.viewModel = vm;
        }
        public override void Execute(object parameter)
        {
            List<TourPreview> searchResult = new List<TourPreview>();
            searchResult = BussinessLogic.LogicInstance.PrepareListRouteForSearch(parameter?.ToString());
            if (viewModel.TourObjectCollection != null)
                viewModel.TourObjectCollection.Clear();
            foreach (var item in searchResult)
                viewModel.TourObjectCollection?.Add(item);
        }
    }
}
