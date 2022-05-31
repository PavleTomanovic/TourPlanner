using System.Collections.Generic;
using TourPlanner.BussinesLayer;
using TourPlanner.DTO;

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
            List<TourPreviewDTO> searchResult = new List<TourPreviewDTO>();
            searchResult = BussinessLogic.LogicInstance.PrepareListRouteForSearch(parameter?.ToString());
            if (viewModel.TourObjectCollection != null)
                viewModel.TourObjectCollection.Clear();
            foreach (var item in searchResult)
                viewModel.TourObjectCollection?.Add(item);
        }
    }
}
