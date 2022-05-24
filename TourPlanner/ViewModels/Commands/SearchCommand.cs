using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
